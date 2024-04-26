package agentie.networking.Utils;

import agentie.networking.rpcProtocol.AgentieClientRpcReflectionWorker;
import agentie.services.IService;

import java.net.Socket;


public class AgentieRpcConcurrentServer extends AbsConcurrentServer {
    private final IService agentieServer;

    public AgentieRpcConcurrentServer(int port, IService agentieServer) {
        super(port);
        this.agentieServer = agentieServer;
        System.out.println("Agentie- AgentieRpcConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {
        AgentieClientRpcReflectionWorker worker = new AgentieClientRpcReflectionWorker(agentieServer, client);
        return new Thread(worker);
    }

    @Override
    public void stop(){
        System.out.println("Stopping services ...");
    }
}