package org.example.agentietursimjava.Repository.Interfaces;

import org.example.agentietursimjava.Domain.Angajat;

import java.util.Optional;
import java.util.UUID;

public interface RepoAngajat extends Repo<UUID, Angajat>{

    Optional<Angajat> findOneByUsername(String username);
}
