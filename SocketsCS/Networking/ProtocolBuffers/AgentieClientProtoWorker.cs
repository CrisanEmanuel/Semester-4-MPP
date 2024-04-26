using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using AgentieProto;
using Google.Protobuf;
using Services;
using Excursie = Model.Excursie;

namespace Networking.ProtocolBuffers
{
    public class AgentieClientProtoWorker: IObserver
    {
        private readonly IService _server;
        private readonly TcpClient _connection;

        private readonly NetworkStream _stream;
        private volatile bool _connected;
        public AgentieClientProtoWorker(IService server, TcpClient connection)
        {
            _server = server;
            _connection = connection;
            try
            {
                _stream = connection.GetStream();
                _connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        
        public void Run()
        {
            while(_connected)
            {
                try
                {
                    var request = AgentieRequest.Parser.ParseDelimitedFrom(_stream);
                    var response = HandleRequest(request);
                    if (response != null)
                    {
                        SendResponse(response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("FMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
                    Console.WriteLine(e.StackTrace);
                }
                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                _stream.Close();
                _connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error "+e);
            }
        }
        
        private AgentieResponse HandleRequest(AgentieRequest request)
		{
			AgentieResponse response = null;
            var requestType = request.Type;
            switch (requestType)
            {
                case AgentieRequest.Types.Type.Login:
                {
                    Console.WriteLine("Login request ...");
                    var angajat = ProtoUtils.GetAngajat(request);
                    try
                    {
                        lock (_server)
                        {
                            _server.Login(angajat, this);
                        }
                        response = ProtoUtils.CreateOkResponse();
                    }
                    catch (AgentieException e)
                    {
                        _connected = false;
                        response = ProtoUtils.CreateErrorResponse(e.Message);
                    }
                    break;
                }
                case AgentieRequest.Types.Type.Logout:
                {
                    Console.WriteLine("Logout request ...");
                    var angajat = ProtoUtils.GetAngajat(request);
                    try
                    {
                        lock (_server)
                        {
                            _server.Logout(angajat);
                        }
                        _connected = false;
                        response = ProtoUtils.CreateOkResponse();
                    }
                    catch (AgentieException e)
                    {
                        response = ProtoUtils.CreateErrorResponse(e.Message);
                    }
                    break;
                }
                case AgentieRequest.Types.Type.CautaToateExcursiile:
                {
                    Console.WriteLine("Find all excursii request ...");
                    try
                    {
                        IEnumerable<Excursie> excursii;
                        lock (_server)
                        {
                            excursii = _server.FindAllExcursii();
                        }
                        response = ProtoUtils.CreateFindAllExcursiiResponse(excursii);
                    }
                    catch (AgentieException e)
                    {
                        response = ProtoUtils.CreateErrorResponse(e.Message);
                    }

                    break;
                }
                case AgentieRequest.Types.Type.CautaExcursii:
                {
                    Console.WriteLine("Find excursii cautate request ...");
                    var excursie = request.ExcursieRequest;
                    try
                    {
                        IEnumerable<Excursie> excursii;
                        lock (_server)
                        {
                            excursii = _server.CautaExcursii(excursie.ObiectivTuristic,
                                excursie.DeLaOra, excursie.PanaLaOra);
                        }
                        response = ProtoUtils.CreateCautaExcursiiResponse(excursii);
                    }
                    catch (AgentieException e)
                    {
                        response = ProtoUtils.CreateErrorResponse(e.Message);
                    }
                    break;
                }
                case AgentieRequest.Types.Type.AdaugaRezervare:
                {
                    Console.WriteLine("Adauga rezervare request ...");
                    try
                    {
                        lock (_server)
                        {
                            _server.AddRezervare(ProtoUtils.BuildRezervareFromRezervareProto(request.Rezervare));
                        }
                        response = new AgentieResponse { Type = AgentieResponse.Types.Type.RezervareAdaugata };
                    }
                    catch (AgentieException e)
                    {
                        response = ProtoUtils.CreateErrorResponse(e.Message);
                    }
                    break;
                }
                case AgentieRequest.Types.Type.UpdateNumarLocuriExcursie:
                {
                    Console.WriteLine("Update numar locuri excursie request ...");
                    try
                    {
                        lock (_server)
                        {
                            _server.UpdateExcursie(ProtoUtils.BuildExcursieFromExcursieProto(request.Excursie));
                        }
                        response = new AgentieResponse { Type = AgentieResponse.Types.Type.ExcursieModificata };
                    }
                    
                    catch (AgentieException e)
                    {
                        response = ProtoUtils.CreateErrorResponse(e.Message);
                    }
                    break;
                }
                case AgentieRequest.Types.Type.Unknown:
                    break;
                case AgentieRequest.Types.Type.CautaAngajatDupaUsername:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return response;
		}

	private void SendResponse(IMessage response)
		{
			Console.WriteLine("sending response " + response);
			lock (_stream)
			{
				response.WriteDelimitedTo(_stream);
				_stream.Flush();
			}
		}
    
        public void UpdateNumarLocuriExcursie(Excursie excursie)
        {
            try 
            {
                SendResponse(ProtoUtils.CreateUpdateResponse(excursie));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }   
    
}