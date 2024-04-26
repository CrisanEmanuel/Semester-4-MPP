package agentie.services;

import agentie.model.Angajat;
import agentie.model.Excursie;
import agentie.model.Rezervare;

import java.util.Optional;

public interface IService {
    // Angajat
    void login(Angajat angajat, IObserver client) throws AgentieException;
    void logout(Angajat angajat) throws AgentieException;
    // Optional<Angajat> findAngajatByUsername(String username) throws AgentieException;

    // Excursie
    Iterable<Excursie> findAllExcursii() throws AgentieException;
    Optional<Excursie> updateExcursie(Excursie excursie) throws AgentieException;
    Iterable<Excursie> cautaExcursii(String numeObiectiv, Integer deLaOra, Integer panaLaOra) throws AgentieException;

    // Rezervare
      Optional<Rezervare> addRezervare(Rezervare rezervare) throws AgentieException;
}
