using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Model;
using Services;

namespace Networking.ObjectProtocol
{
    public class AgentieClientObjectWorker: IObserver
    {
        private readonly IService _server;
        private readonly TcpClient _connection;

        private readonly NetworkStream _stream;
        private readonly IFormatter _formatter;
        private volatile bool _connected;
        public AgentieClientObjectWorker(IService server, TcpClient connection)
        {
            _server = server;
            _connection = connection;
            try
            {
                _stream = connection.GetStream();
                _formatter = new BinaryFormatter();
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
                    var request = _formatter.Deserialize(_stream);
                    object response = HandleRequest((IRequest) request);
                    if (response != null)
                    {
                        SendResponse((IResponse) response);
                    }
                }
                catch (Exception e)
                {
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
        
        private IResponse HandleRequest(IRequest request)
		{
			IResponse response = null;
            switch (request)
            {
                case LoginRequest loginRequest:
                {
                    Console.WriteLine("Login request ...");
                    var angajat = loginRequest.Angajat;
                    try
                    {
                        lock (_server)
                        {
                            _server.Login(angajat, this);
                        }
                        response = new OkResponse();
                    }
                    catch (AgentieException e)
                    {
                        _connected = false;
                        response = new ErrorResponse(e.Message);
                    }

                    break;
                }
                case LogoutRequest logoutRequest:
                {
                    Console.WriteLine("Logout request ...");
                    var angajat = logoutRequest.Angajat;
                    try
                    {
                        lock (_server)
                        {
                            _server.Logout(angajat);
                        }
                        _connected = false;
                        response = new OkResponse();
                    }
                    catch (AgentieException e)
                    {
                        response = new ErrorResponse(e.Message);
                    }
                    break;
                }
                case FindAllExcursiiRequest _:
                {
                    Console.WriteLine("Find all excursii request ...");
                    try
                    {
                        IEnumerable<Excursie> excursii;
                        lock (_server)
                        {
                            excursii = _server.FindAllExcursii();
                        }
                        response = new FindAllExcursiiResponse(excursii);
                    }
                    catch (AgentieException e)
                    {
                        response = new ErrorResponse(e.Message);
                    }

                    break;
                }
                case FindAllExcursiiCautateRequest findExcursiiCautateRequest:
                {
                    Console.WriteLine("Find excursii cautate request ...");
                    try
                    {
                        IEnumerable<Excursie> excursii;
                        lock (_server)
                        {
                            excursii = _server.CautaExcursii(findExcursiiCautateRequest.ObiectivTuristic,
                                findExcursiiCautateRequest.DeLaOra, findExcursiiCautateRequest.PanaLaOra);
                        }

                        response = new FindExcursiiCautateResponse(excursii);
                    }
                    catch (AgentieException e)
                    {
                        response = new ErrorResponse(e.Message);
                    }
                    break;
                }
                case AdaugaRezervareRequest adaugaRezervareRequest:
                {
                    Console.WriteLine("Adauga rezervare request ...");
                    try
                    {
                        Rezervare rezervare;
                        lock (_server)
                        {
                            rezervare = _server.AddRezervare(adaugaRezervareRequest.Rezervare);
                        }

                        response = new AdaugaRezervareResponse(rezervare);
                    }
                    catch (AgentieException e)
                    {
                        response = new ErrorResponse(e.Message);
                    }
                    break;
                }
                case UpdateNumarLocuriExcursieRequest updateNumarLocuriExcursieRequest:
                {
                    Console.WriteLine("Update numar locuri excursie request ...");
                    try
                    {
                        Excursie excursie;
                        lock (_server)
                        {
                            excursie = _server.UpdateExcursie(updateNumarLocuriExcursieRequest.Excursie);
                        }
                        response = new UpdateNumarLocuriExcursieResponse(excursie);
                    }
                    catch (AgentieException e)
                    {
                        response = new ErrorResponse(e.Message);
                    }
                    break;
                }
            }
            return response;
		}

	private void SendResponse(IResponse response)
		{
			Console.WriteLine("sending response " + response);
			lock (_stream)
			{
				_formatter.Serialize(_stream, response);
				_stream.Flush();
			}

		}
    
        public void UpdateNumarLocuriExcursie(Excursie excursie)
        {
            try 
            {
                SendResponse(new UpdateResponse(excursie));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}