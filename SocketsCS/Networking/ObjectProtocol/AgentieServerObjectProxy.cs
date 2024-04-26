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
    public class AgentieServerObjectProxy: IService
    {
        private readonly string _host;
        private readonly int _port;

        private IObserver _client;

        private NetworkStream _stream;
		
        private IFormatter _formatter;
        private TcpClient _connection;

        private readonly Queue<IResponse> _responses;
        private volatile bool _finished;
        private EventWaitHandle _waitHandle;
        public AgentieServerObjectProxy(string host, int port)
        {
            _host = host;
            _port = port;
            _responses = new Queue<IResponse>();
        }

        public void Login(Angajat angajat, IObserver client)
        {
            InitializeConnection();
            SendRequest(new LoginRequest(angajat));
            var response = ReadResponse();
            switch (response)
            {
	            case OkResponse _:
		            _client = client;
		            return;
	            case ErrorResponse err:
		            CloseConnection();
		            throw new AgentieException(err.Message);
            }
        }

        public void Logout(Angajat angajat)
        {
	        SendRequest(new LogoutRequest(angajat));
	        var response = ReadResponse(); 
	        CloseConnection();
	        if (response is ErrorResponse err)
	        {
		        throw new AgentieException(err.Message);
	        }
        }

        public IEnumerable<Excursie> FindAllExcursii()
        {	
			SendRequest(new FindAllExcursiiRequest());
			var response = ReadResponse();
			if (response is ErrorResponse err)
			{
	            throw new AgentieException(err.Message);
			}
			return ((FindAllExcursiiResponse) response).Excursii;
        }

        public Excursie UpdateExcursie(Excursie excursie)
        {
            SendRequest(new UpdateNumarLocuriExcursieRequest(excursie));
            var response = ReadResponse();
            if (response is ErrorResponse err)
			{
	            throw new AgentieException(err.Message);
			}
			return ((UpdateNumarLocuriExcursieResponse) response).Excursie;
        }

        public IEnumerable<Excursie> CautaExcursii(string numeObiectiv, int deLaOra, int panaLaOra)
        {
            SendRequest(new FindAllExcursiiCautateRequest(numeObiectiv, deLaOra, panaLaOra));
            var response = ReadResponse();
            if (response is ErrorResponse err)
			{
	            throw new AgentieException(err.Message);
			}
            return ((FindExcursiiCautateResponse) response).Excursii;
        }

        public Rezervare AddRezervare(Rezervare rezervare)
        {
            SendRequest(new AdaugaRezervareRequest(rezervare));
            var response = ReadResponse();
            if (response is ErrorResponse err)
            {
	            throw new AgentieException(err.Message);
            }
            return ((AdaugaRezervareResponse) response).Rezervare;
        }
        
		private void SendRequest(IRequest request)
		{
			try
			{
                _formatter.Serialize(_stream, request);
                _stream.Flush();
			}
			catch (Exception e)
			{
				throw new AgentieException("Error sending object " + e);
			}

		}

		private IResponse ReadResponse()
		{
			IResponse response = null;
			try
			{
                _waitHandle.WaitOne();
				lock (_responses)
				{
                    // Monitor.Wait(_responses); 
                    response = _responses.Dequeue();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
			return response;
		}
		
		private void InitializeConnection()
		{
			 try
			 {
				_connection = new TcpClient(_host, _port);
				_stream = _connection.GetStream();
                _formatter = new BinaryFormatter();
				_finished = false;
                _waitHandle = new AutoResetEvent(false);
				StartReader();
			}
			catch (Exception e)
			{
                Console.WriteLine(e.StackTrace);
			}
		}
		
		private void CloseConnection()
		{
			_finished = true;
			try
			{
				_stream.Close();
				_connection.Close();
				_waitHandle.Close();
				_client = null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}

		}
		
		private void StartReader()
		{
			var tw = new Thread(Run);
			tw.Start();
		}
		
		private void HandleUpdate(UpdateResponse update)
		{
			try
			{
				_client.UpdateNumarLocuriExcursie((Excursie) update.UpdatedObject);
			}
			catch (Exception e)
			{
				Console.WriteLine("Handle update PROXY ERROR: " + e.StackTrace);
			}
		}

		protected virtual void Run()
			{
				while(!_finished)
				{
					try
					{
                        var response = _formatter.Deserialize(_stream);
						Console.WriteLine("response received " + response);
						if (response is UpdateResponse updateResponse)
						{
							 HandleUpdate(updateResponse);
						}
						else
						{
							lock (_responses)
							{
                                _responses.Enqueue((IResponse) response);
							}
                            _waitHandle.Set();
						}
					}
					catch (Exception e)
					{
						//throw new AgentieException("Reading error " + e);
						Console.WriteLine("Reading error " + e);
					}
				}
			}
    }
}