package org.example.agentietursimjava.Repository.Interfaces;

import org.example.agentietursimjava.Domain.Excursie;

import java.util.UUID;

public interface RepoExcursie extends Repo<UUID, Excursie>{
    Iterable<Excursie> cautaExcursii(String numeObiectiv, Integer deLaOra, Integer panaLaOra);
}

