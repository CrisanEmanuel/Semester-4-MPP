using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;
using Networking.ObjectProtocol;
using Networking.ProtocolBuffers;
using Networking.Utils;
using Persistence.DBRepos;
using Persistence.Interfaces;
using Services;

namespace Server;

public abstract class StartServer
{
    private const int DefaultPort = 55556;
    private const string DefaultIp = "127.0.0.1";

    public static void Main(string[] args)
    {
        // IUserRepository userRepo = new UserRepositoryMock();
        Console.WriteLine(@"Reading properties from app.config ...");
        var port = DefaultPort;
        var ip = DefaultIp;
        
        var portS= ConfigurationManager.AppSettings["port"];
        if (portS == null)
        {
            Console.WriteLine("Port property not set. Using default value " + DefaultPort);
        }
        else
        {
            var result = int.TryParse(portS, out port);
            if (!result)
            {
                Console.WriteLine("Port property not a number. Using default value " + DefaultPort);
                port = DefaultPort;
                Console.WriteLine("Portul " + port);
            }
        }
        
        var ipS= ConfigurationManager.AppSettings["ip"];
        if (ipS == null)
        {
            Console.WriteLine("Port property not set. Using default value " + DefaultIp);
        }
        
        Console.WriteLine("Configuration Settings for database {0}", GetConnectionStringByName("agentiedeturism"));
        IDictionary<string, string> props = new SortedList<string, string>();
        props.Add("ConnectionString", GetConnectionStringByName("agentiedeturism"));
        IRepoAngajat repoAngajat = new RepoAngajat(props);
        IRepoExcursie repoExcursie = new RepoExcursie(props);
        IRepoRezervare repoRezervare = new RepoRezervare(props);
        IService serviceImpl = new ServerImpl(repoAngajat, repoExcursie, repoRezervare);
        
        Console.WriteLine(@"Starting server on IP {0} and port {1}", ip, port);
        // var server = new SerialAgentieServer(ip, port, serviceImpl);
        var server = new ProtoAgentieServer(ip, port, serviceImpl);
        server.Start();
        Console.WriteLine(@"Server started ...");
        //Console.WriteLine("Press <enter> to exit...");
        Console.ReadLine();
    }

    private static string GetConnectionStringByName(string name)
    {
        // Assume failure.
        string returnValue = null;

        // Look for the name in the connectionStrings section.
        var settings = ConfigurationManager.ConnectionStrings[name];

        // If found, return the connection string.
        if (settings != null)
            returnValue = settings.ConnectionString;

        return returnValue;
    }
}
public class SerialAgentieServer: ConcurrentServer 
{
    private readonly IService _server;
    private AgentieClientObjectWorker _worker;
    public SerialAgentieServer(string host, int port, IService server) : base(host, port)
    {
        _server = server;
        Console.WriteLine(@"SerialChatServer...");
    }
    protected override Thread CreateWorker(TcpClient client)
    {
        _worker = new AgentieClientObjectWorker(_server, client);
        return new Thread(_worker.Run);
    }
}

public class ProtoAgentieServer : ConcurrentServer
{
    private readonly IService _server;
    private AgentieClientProtoWorker _worker;
    public ProtoAgentieServer(string host, int port, IService server) : base(host, port)
    {
        _server = server;
        Console.WriteLine("ProtoChatServer...");
    }
    protected override Thread CreateWorker(TcpClient client)
    {
        _worker = new AgentieClientProtoWorker(_server, client);
        return new Thread(_worker.Run);
    }
}