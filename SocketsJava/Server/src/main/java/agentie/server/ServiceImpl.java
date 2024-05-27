package agentie.server;

import agentie.model.Angajat;
import agentie.model.Excursie;
import agentie.model.Rezervare;
import agentie.persistence.Interfaces.IRepoAngajat;
import agentie.persistence.Interfaces.IRepoExcursie;
import agentie.persistence.Interfaces.IRepoRezervare;
import agentie.services.AgentieException;
import agentie.services.IObserver;
import agentie.services.IService;

import java.util.Map;
import java.util.Optional;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import static agentie.server.Password.verifyPassword;

public class ServiceImpl implements IService {
    private final IRepoAngajat repoAngajat;
    private final IRepoExcursie repoExcursie;
    private final IRepoRezervare repoRezervare;
    private final Map<String, IObserver> loggedAngajati;

    public ServiceImpl(IRepoAngajat repoAngajat, IRepoExcursie repoExcursie, IRepoRezervare rezervare) {
        this.repoAngajat = repoAngajat;
        this.repoExcursie = repoExcursie;
        this.repoRezervare = rezervare;
        loggedAngajati = new ConcurrentHashMap<>();
    }

    // ANGAJAT
    @Override
    public synchronized void login(Angajat angajat, IObserver client) throws AgentieException {
        // check if the user is in the database
        Optional<Angajat> angajatIntreg = repoAngajat.findOneByUsername(angajat.getUsername());
        if (angajatIntreg.isEmpty()) {
            throw new AgentieException("Authentication failed!");
        } else {
            if (!verifyPassword(angajat.getPassword(), angajatIntreg.get().getPassword())) {
                throw new AgentieException("Invalid username or password!");
            }
            if (loggedAngajati.get(angajatIntreg.get().getUsername()) != null) {
                throw new AgentieException("User already logged in!");
            }
            loggedAngajati.put(angajatIntreg.get().getUsername(), client);
        }
    }

    @Override
    public synchronized void logout(Angajat angajat) throws AgentieException {
        IObserver localClient = loggedAngajati.remove(angajat.getUsername());
        if (localClient == null)
            throw new AgentieException("User " + angajat.getUsername() + " is not logged in.");
    }


    @Override
    public synchronized Iterable<Excursie> findAllExcursii() {
        try {
            return repoExcursie.findAll();
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public synchronized Optional<Excursie> updateExcursie(Excursie excursie) {
        try {
            Optional<Excursie> ex =  repoExcursie.update(excursie);
            if (ex.isEmpty()) {
                int defaultThreadsNo = 5;
                ExecutorService executor = Executors.newFixedThreadPool(defaultThreadsNo);
                for (IObserver obs: loggedAngajati.values()) {
                    executor.execute(() -> {
                        try {
                            obs.updateNumarLocuriExcursie(excursie);
                        } catch (AgentieException e) {
                            throw new RuntimeException(e);
                        }
                    });
                }
                executor.shutdown();
            }
            return ex;
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public synchronized Iterable<Excursie> cautaExcursii(String numeObiectiv, Integer deLaOra, Integer panaLaOra) {
        try {
            return repoExcursie.cautaExcursii(numeObiectiv, deLaOra, panaLaOra);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    // REZERVARE
    @Override
    public synchronized Optional<Rezervare> addRezervare(Rezervare rezervare) {
        try {
            return repoRezervare.save(rezervare);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }
}
