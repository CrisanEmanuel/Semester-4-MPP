package agentie.persistence.Interfaces;

import agentie.model.Excursie;

import java.util.UUID;

public interface IRepoExcursie extends IRepo<UUID, Excursie> {
    Iterable<Excursie> cautaExcursii(String numeObiectiv, Integer deLaOra, Integer panaLaOra);
}

