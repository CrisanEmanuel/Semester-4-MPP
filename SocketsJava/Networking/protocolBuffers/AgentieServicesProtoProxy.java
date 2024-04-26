package agentie.networking.protocolBuffers;

import agentie.model.Angajat;
import agentie.model.Excursie;
import agentie.model.Rezervare;
import agentie.networking.dto.dtoExcursie;
import agentie.services.AgentieException;
import agentie.services.IObserver;
import agentie.services.IService;

import java.io.*;
import java.net.Socket;
import java.util.Objects;
import java.util.Optional;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class AgentieServicesProtoProxy implements IService {
    private final String host;
    private final int port;

    private IObserver client;

    private InputStream input;
    private OutputStream output;
    private Socket connection;

    private final BlockingQueue<AgentieProto.AgentieResponse> qresponses;
    private volatile boolean finished;
    public AgentieServicesProtoProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses = new LinkedBlockingQueue<>();
    }

    // ANGAJAT
    @Override
    public void login(Angajat angajat, IObserver client) throws AgentieException {
        initializeConnection();
        sendRequest(ProtoUtils.createLoginRequest(angajat));
        AgentieProto.AgentieResponse response = readResponse();
        if (response.getType() == AgentieProto.AgentieResponse.Type.Ok) {
            this.client = client;
            return;
        }
        if (response.getType() == AgentieProto.AgentieResponse.Type.Error) {
            String err = ProtoUtils.getError(response);
            closeConnection();
            throw new AgentieException(err);
        }
    }

    @Override
    public void logout(Angajat angajat) throws AgentieException {
        sendRequest(ProtoUtils.createLogoutRequest(angajat));
        AgentieProto.AgentieResponse response = readResponse();
        closeConnection();
        if (response.getType() == AgentieProto.AgentieResponse.Type.Error){
            String err = ProtoUtils.getError(response);
            throw new AgentieException(err);
        }
    }

    @Override
    public Iterable<Excursie> findAllExcursii() throws AgentieException {
        sendRequest(ProtoUtils.createFindAllExcursiiRequest());
        AgentieProto.AgentieResponse response = readResponse();
        if (response.getType() == AgentieProto.AgentieResponse.Type.Error) {
            String err = ProtoUtils.getError(response);
            throw new AgentieException(err);
        }
        return ProtoUtils.getAllExcursii(response);
    }

    @Override
    public Optional<Excursie> updateExcursie(Excursie excursie) throws AgentieException {
        sendRequest(ProtoUtils.createUpdateExcursieRequest(excursie));
        AgentieProto.AgentieResponse response = readResponse();
        if (response.getType() == AgentieProto.AgentieResponse.Type.Error) {
            String err = ProtoUtils.getError(response);
            throw new AgentieException(err);
        }
        return Optional.empty();
    }

    @Override
    public Iterable<Excursie> cautaExcursii(String numeObiectiv, Integer deLaOra, Integer panaLaOra) throws AgentieException {
        dtoExcursie dtoExcursie = new dtoExcursie(numeObiectiv, deLaOra, panaLaOra);
        sendRequest(ProtoUtils.createCautaExcursiiRequest(dtoExcursie));
        AgentieProto.AgentieResponse response = readResponse();
        if (response.getType() == AgentieProto.AgentieResponse.Type.Error) {
            String err = ProtoUtils.getError(response);
            throw new AgentieException(err);
        }
        return ProtoUtils.getExcursiiCautate(response);
    }

    // REZERVARE
    @Override
    public Optional<Rezervare> addRezervare(Rezervare rezervare) throws AgentieException {
        sendRequest(ProtoUtils.createAddRezervareRequest(rezervare));
        AgentieProto.AgentieResponse response = readResponse();
        if (response.getType() == AgentieProto.AgentieResponse.Type.Error) {
            String err = ProtoUtils.getError(response);
            throw new AgentieException(err);
        }
        return Optional.empty();
    }

    private void initializeConnection() {
        try {
            connection = new Socket(host, port);
            output = connection.getOutputStream();
            input = connection.getInputStream();
            finished = false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void closeConnection() {
        finished = true;
        try {
            input.close();
            output.close();
            connection.close();
            client = null;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void sendRequest(AgentieProto.AgentieRequest request) throws AgentieException {
        try {
            request.writeDelimitedTo(output);
            output.flush();
        } catch (IOException e) {
            throw new AgentieException("Error sending object " + e);
        }
    }

    private AgentieProto.AgentieResponse readResponse() {
        AgentieProto.AgentieResponse response = null;
        try{
            response = qresponses.take();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }

    private void startReader(){
        Thread tw = new Thread(new ReaderThread());
        tw.start();
    }

    private void handleUpdate(AgentieProto.AgentieResponse updateResponse){
        if (updateResponse.getType() == AgentieProto.AgentieResponse.Type.UpdatedNumarLocuriExcursie) {
            try {
                client.updateNumarLocuriExcursie(ProtoUtils.getExcursie(updateResponse));
            } catch (AgentieException e) {
                e.printStackTrace();
            }
        }
    }

    private boolean isUpdate(AgentieProto.AgentieResponse.Type type){
        return Objects.requireNonNull(type) == AgentieProto.AgentieResponse.Type.UpdatedNumarLocuriExcursie;
    }

    private class ReaderThread implements Runnable {
        public void run() {
            while(!finished){
                try {
                    AgentieProto.AgentieResponse response = AgentieProto.AgentieResponse.parseDelimitedFrom(input);
                    System.out.println("response received " + response);
                    if (isUpdate(response.getType())){
                        handleUpdate(response);
                    } else {
                        try {
                            qresponses.put(response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException e) {
                    System.out.println("Reading error " + e.getMessage());
                }
            }
        }
    }
}
