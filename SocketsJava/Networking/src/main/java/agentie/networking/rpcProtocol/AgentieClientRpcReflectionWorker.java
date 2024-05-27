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
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.net.Socket;
import java.util.Optional;

public class AgentieClientRpcReflectionWorker implements Runnable, IObserver {
    private final IService server;
    private final Socket connection;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private volatile boolean connected;
    public AgentieClientRpcReflectionWorker(IService server, Socket connection) {
        this.server = server;
        this.connection = connection;
        try{
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input = new ObjectInputStream(connection.getInputStream());
            connected = true;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    @Override
    public void run() {
        while(connected){
            try {
                Object request = input.readObject();
                Response response = handleRequest((Request) request);
                if (response != null){
                    sendResponse(response);
                }
            } catch (IOException | ClassNotFoundException e) {
                e.printStackTrace();
            }
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            System.out.println("Error " + e);
        }
    }

    private Response handleRequest(Request request){
        Response response = null;
        String handlerName = "handle" + (request).type();
        System.out.println("HandlerName " + handlerName);
        try {
            Method method = this.getClass().getDeclaredMethod(handlerName, Request.class);
            response = (Response) method.invoke(this, request);
            System.out.println("Method " + handlerName + " invoked");
        } catch (NoSuchMethodException | InvocationTargetException | IllegalAccessException e) {
            e.printStackTrace();
        }
        return response;
    }

    private void sendResponse(Response response) throws IOException {
        System.out.println("sending response " + response);
        synchronized (output) {
            output.writeObject(response);
            output.flush();
        }
    }

    @Override
    public void updateNumarLocuriExcursie(Excursie excursie) throws AgentieException {
        System.out.println("Trimit raspuns catre proxy pentru observer");
        Response response = new Response.Builder().type(ResponseType.UPDATED_NUMAR_LOCURI_EXCURSIE).data(excursie).build();
        try {
            sendResponse(response);
        } catch (IOException e) {
            throw new AgentieException("Sending error: " + e);
        }
    }

    @SuppressWarnings("unused")
    public Response handleCAUTA_EXCURSII(Request request) {
        try {
            dtoExcursie dtoExcursie = (dtoExcursie) request.data();
            Iterable<Excursie> excursiiCautate = server.cautaExcursii(dtoExcursie.getObiectivTuristic(), dtoExcursie.getDeLaOra(), dtoExcursie.getPanaLaOra());
            return new Response.Builder().type(ResponseType.OK).data(excursiiCautate).build();
        } catch (AgentieException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    @SuppressWarnings("unused")
    public Response handleCAUTA_TOATE_EXCURSIILE(Request request) {
        try {
            Iterable<Excursie> toateExcursiile = server.findAllExcursii();
            return new Response.Builder().type(ResponseType.OK).data(toateExcursiile).build();
        } catch (AgentieException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    @SuppressWarnings("unused")
    public Response handleADAUGA_REZERVARE(Request request) {
        try {
            server.addRezervare((Rezervare) request.data());
            return new Response.Builder().type(ResponseType.OK).build();
        } catch (AgentieException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    @SuppressWarnings("unused")
    public Response handleUPDATE_NUMAR_LOCURI_EXCURSIE(Request request) {
        System.out.println("2) handleUPDATE_NUMAR_LOCURI_EXCURSIE din worker");
        try {
            server.updateExcursie((Excursie) request.data());
            return new Response.Builder().type(ResponseType.OK).build();
        } catch (AgentieException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

//    @SuppressWarnings("unused")
//    public Response handleCAUTA_ANGAJAT_DUPA_USERNAME(Request request) {
//        try {
//            Optional<Angajat> angajat = server.findAngajatByUsername((String) request.data());
//            if (angajat.isPresent()) {
//                return new Response.Builder().type(ResponseType.OK).data(angajat.get()).build();
//            }
//            return new Response.Builder().type(ResponseType.ERROR).build();
//        } catch (AgentieException e) {
//            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
//        }
//    }

    @SuppressWarnings("unused")
    public Response handleLOGIN(Request request) {
        System.out.println("Sunt in handleLOGIN WORKER");
        try {
            Angajat angajat = (Angajat) request.data();
            System.out.println("Apelez login din serviceimpl WORKER");
            server.login(angajat, this);
            return new Response.Builder().type(ResponseType.OK).build();
        } catch (AgentieException e) {
            connected = false;
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    @SuppressWarnings("unused")
    public Response handleLOGOUT(Request request) {
        try {
            Angajat angajat = (Angajat) request.data();
            server.logout(angajat);
            connected = false;
            return new Response.Builder().type(ResponseType.OK).build();
        } catch (AgentieException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }
}
