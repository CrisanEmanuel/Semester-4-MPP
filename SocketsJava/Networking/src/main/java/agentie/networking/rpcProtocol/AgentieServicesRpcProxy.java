package agentie.networking.rpcProtocol;

import agentie.model.Angajat;
import agentie.model.Excursie;
import agentie.model.Rezervare;
import agentie.networking.dto.dtoExcursie;
import agentie.services.AgentieException;
import agentie.services.IObserver;
import agentie.services.IService;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.Optional;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class AgentieServicesRpcProxy implements IService {
    private final String host;
    private final int port;

    private IObserver client;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private Socket connection;

    private final BlockingQueue<Response> qresponses;
    private volatile boolean finished;
    public AgentieServicesRpcProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses = new LinkedBlockingQueue<>();
    }

    // ANGAJAT
    @Override
    public void login(Angajat angajat, IObserver client) throws AgentieException {
        System.out.println("Initizlizez conexiunea pentru client PROXY");
        initializeConnection();
        System.out.println("Fac req de log in catre worker PROXY");
        Request req = new Request.Builder().type(RequestType.LOGIN).data(angajat).build();
        sendRequest(req);
        Response response = readResponse();
        System.out.println("Am primit raspuns " + response.type() + "PROXY");
        if (response.type() == ResponseType.OK) {
            this.client = client;
            return;
        }
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            System.out.println("Inchid conexiune: " + err + "PROXY");
            closeConnection();
            throw new AgentieException(err);
        }
    }

    @Override
    public void logout(Angajat angajat) throws AgentieException {
        Request req = new Request.Builder().type(RequestType.LOGOUT).data(angajat).build();
        sendRequest(req);
        Response response = readResponse();
        closeConnection();
        if (response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            throw new AgentieException(err);
        }
    }

    @Override
    public Iterable<Excursie> findAllExcursii() throws AgentieException {
        Request req = new Request.Builder().type(RequestType.CAUTA_TOATE_EXCURSIILE).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new AgentieException(err);
        }
        return (Iterable<Excursie>) response.data();
    }

    @Override
    public synchronized Optional<Excursie> updateExcursie(Excursie excursie) throws AgentieException {
        System.out.println("1) updateExcursie din PROXY");
        Request req = new Request.Builder().type(RequestType.UPDATE_NUMAR_LOCURI_EXCURSIE).data(excursie).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new AgentieException(err);
        }
        return (Optional<Excursie>) response.data();
    }

    @Override
    public Iterable<Excursie> cautaExcursii(String numeObiectiv, Integer deLaOra, Integer panaLaOra) throws AgentieException {
        dtoExcursie dtoExcursie = new dtoExcursie(numeObiectiv, deLaOra, panaLaOra);
        Request req = new Request.Builder().type(RequestType.CAUTA_EXCURSII).data(dtoExcursie).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new AgentieException(err);
        }
        return (Iterable<Excursie>) response.data();
    }

    // REZERVARE
    @Override
    public Optional<Rezervare> addRezervare(Rezervare rezervare) throws AgentieException {
        Request req = new Request.Builder().type(RequestType.ADAUGA_REZERVARE).data(rezervare).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new AgentieException(err);
        }
        return (Optional<Rezervare>) response.data();
    }

    private void initializeConnection() {
        try {
            connection = new Socket(host, port);
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input = new ObjectInputStream(connection.getInputStream());
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

    private void sendRequest(Request request) throws AgentieException {
        try {
            output.writeObject(request);
            output.flush();
        } catch (IOException e) {
            throw new AgentieException("Error sending object " + e);
        }
    }

    private Response readResponse() {
        Response response = null;
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

    private void handleUpdate(Response response){
        if (response.type().equals(ResponseType.UPDATED_NUMAR_LOCURI_EXCURSIE)) {
            try {
                System.out.println("Incerc sa modific observerele din PROXY");
                client.updateNumarLocuriExcursie((Excursie) response.data());
            } catch (AgentieException e) {
                e.printStackTrace();
            }
        }
    }

    private boolean isUpdate(Response response){
        return response.type() == ResponseType.UPDATED_NUMAR_LOCURI_EXCURSIE;

    }

    private class ReaderThread implements Runnable {
        public void run() {
            while(!finished){
                try {
                    Object response = input.readObject();
                    System.out.println("response received " + response);
                    if (isUpdate((Response) response)){
                        handleUpdate((Response) response);
                    } else {
                        try {
                            qresponses.put((Response) response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException | ClassNotFoundException e) {
                    System.out.println("Reading error " + e.getMessage());
                }
            }
        }
    }
}
