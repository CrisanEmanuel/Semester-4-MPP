package agentie.server;

import agentie.networking.Utils.AbstractServer;
import agentie.networking.Utils.AgentieRpcConcurrentServer;
import agentie.networking.Utils.ServerException;
import agentie.persistence.DBRepos.AngajatDBRepo;
import agentie.persistence.DBRepos.ExcursieDBRepo;
import agentie.persistence.DBRepos.RezervareDBRepo;
import agentie.persistence.Interfaces.IRepoAngajat;
import agentie.persistence.Interfaces.IRepoExcursie;
import agentie.persistence.Interfaces.IRepoRezervare;
import agentie.services.IService;

import java.io.IOException;
import java.util.Properties;

public class StartRpcServer {
    private static final int defaultPort = 55555;
    public static void main(String[] args) {
        Properties serverProps = new Properties();
        try {
            serverProps.load(StartRpcServer.class.getResourceAsStream("/agentieserver.properties"));
            System.out.println("Server properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find agentieserver.properties "+e);
            return;
        }
        IRepoAngajat angajatDBRepo = new AngajatDBRepo(serverProps);
        IRepoExcursie excursieDBRepo = new ExcursieDBRepo(serverProps);
        IRepoRezervare rezervareDBRepo = new RezervareDBRepo(serverProps);
        IService agentieServerImpl = new ServiceImpl(angajatDBRepo, excursieDBRepo, rezervareDBRepo);
        int chatServerPort = defaultPort;
        try {
            chatServerPort = Integer.parseInt(serverProps.getProperty("agentie.server.port"));
        } catch (NumberFormatException nef){
            System.err.println("Wrong  Port Number" + nef.getMessage());
            System.err.println("Using default port " + defaultPort);
        }
        System.out.println("Starting server on port: " + chatServerPort);
        AbstractServer server = new AgentieRpcConcurrentServer(chatServerPort, agentieServerImpl);
        try {
            server.start();
        } catch (ServerException e) {
            System.err.println("Error starting the server" + e.getMessage());
        } finally {
            try {
                server.stop();
            } catch(ServerException e){
                System.err.println("Error stopping server " + e.getMessage());
            }
        }
    }
}