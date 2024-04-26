package agentie.networking.protocolBuffers;

import agentie.model.Angajat;
import agentie.model.Excursie;
import agentie.model.Rezervare;
import agentie.networking.dto.dtoExcursie;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

public class ProtoUtils {

    private static final DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");

    public static AgentieProto.AgentieRequest createLoginRequest(Angajat angajat) {
        AgentieProto.Angajat angajatDTO = AgentieProto.Angajat.newBuilder().setUsername(angajat.getUsername())
                .setPassword(angajat.getPassword()).build();
        return AgentieProto.AgentieRequest.newBuilder().setType(AgentieProto.AgentieRequest.Type.Login)
                .setAngajat(angajatDTO).build();
    }

    public static AgentieProto.AgentieRequest createLogoutRequest(Angajat angajat) {
        AgentieProto.Angajat angajatDTO = AgentieProto.Angajat.newBuilder().setUsername(angajat.getUsername())
                .setPassword(angajat.getPassword()).build();
        return AgentieProto.AgentieRequest.newBuilder().setType(AgentieProto.AgentieRequest.Type.Logout)
                .setAngajat(angajatDTO).build();
    }

    public static AgentieProto.AgentieRequest createFindAllExcursiiRequest() {
        return AgentieProto.AgentieRequest.newBuilder().setType(AgentieProto.AgentieRequest.Type.CautaToateExcursiile)
                .build();
    }

    public static AgentieProto.AgentieRequest createUpdateExcursieRequest(Excursie excursie) {
        AgentieProto.Excursie ex = AgentieProto.Excursie.newBuilder()
                .setObiectivTuristic(excursie.getObiectivTuristic())
                .setNumeFirmaTransport(excursie.getNumeFirmaTransport())
                .setOraPlecare(excursie.getOraPlecare().toString().formatted(formatter))
                .setNrLocuriDisponibile(excursie.getNrLocuriDisponibile())
                .setPret(excursie.getPret())
                .setUuid(excursie.getId().toString()).build();
        return AgentieProto.AgentieRequest.newBuilder().setType(AgentieProto.AgentieRequest.Type.UpdateNumarLocuriExcursie)
                .setExcursie(ex).build();
    }

    public static AgentieProto.AgentieRequest createCautaExcursiiRequest(dtoExcursie dtoExcursie) {
        AgentieProto.ExcursieRequest excursieRequest = AgentieProto.ExcursieRequest.newBuilder()
                .setObiectivTuristic(dtoExcursie.getObiectivTuristic())
                .setDeLaOra(dtoExcursie.getDeLaOra())
                .setPanaLaOra(dtoExcursie.getPanaLaOra()).build();
        return AgentieProto.AgentieRequest.newBuilder().setType(AgentieProto.AgentieRequest.Type.CautaExcursii)
                .setExcursieRequest(excursieRequest).build();
    }

    public static AgentieProto.AgentieRequest createAddRezervareRequest(Rezervare rezervare) {
        AgentieProto.Rezervare rez = AgentieProto.Rezervare.newBuilder()
                .setNrBilete(rezervare.getNrBilete())
                .setNumarTelefonClient(rezervare.getNumarTelefonClient())
                .setNumeClient(rezervare.getNumeClient())
                .setExcursie(getExcursieProtoFromRezervare(rezervare))
                .setUuid(rezervare.getId().toString()).build();
        return AgentieProto.AgentieRequest.newBuilder().setType(AgentieProto.AgentieRequest.Type.AdaugaRezervare)
                .setRezervare(rez).build();
    }

    public static String getError(AgentieProto.AgentieResponse response) {
        return response.getError();
    }

    public static Iterable<Excursie> getAllExcursii(AgentieProto.AgentieResponse response) {
        List<Excursie> excursieList = new ArrayList<>();
        for (int i = 0; i < response.getToateExcursiileCount(); i++) {
            AgentieProto.Excursie ex = response.getToateExcursiile(i);
            excursieList.add(buildExcursieFromExcursieProto(ex));
        }
        return excursieList;
    }

    public static Excursie getExcursie(AgentieProto.AgentieResponse response) {
        AgentieProto.Excursie exProto = response.getUpdatedExcursie();
        return buildExcursieFromExcursieProto(exProto);
    }

    public static Iterable<Excursie> getExcursiiCautate(AgentieProto.AgentieResponse response) {
        List<Excursie> excursieList = new ArrayList<>();
        for (int i = 0; i < response.getExcursiiCautateCount(); i++) {
            AgentieProto.Excursie ex = response.getExcursiiCautate(i);
            excursieList.add(buildExcursieFromExcursieProto(ex));
        }
        return excursieList;
    }

    private static AgentieProto.Excursie getExcursieProtoFromRezervare(Rezervare rezervare) {
        Excursie ex = rezervare.getExcursie();
        return AgentieProto.Excursie.newBuilder().setObiectivTuristic(ex.getObiectivTuristic())
                .setNumeFirmaTransport(ex.getNumeFirmaTransport())
                .setNrLocuriDisponibile(ex.getNrLocuriDisponibile())
                .setPret(ex.getPret())
                .setOraPlecare(ex.getOraPlecare().toString().formatted(formatter))
                .setUuid(ex.getId().toString()).build();
    }

    private static Excursie buildExcursieFromExcursieProto(AgentieProto.Excursie excursie) {
        Excursie ex = new Excursie();
        ex.setId(UUID.fromString(excursie.getUuid()));
        ex.setObiectivTuristic(excursie.getObiectivTuristic());
        ex.setNrLocuriDisponibile(excursie.getNrLocuriDisponibile());
        ex.setNumeFirmaTransport(excursie.getNumeFirmaTransport());
        ex.setPret(excursie.getPret());
        ex.setOraPlecare(LocalDateTime.parse(excursie.getOraPlecare(), formatter));
        return ex;
    }

}
