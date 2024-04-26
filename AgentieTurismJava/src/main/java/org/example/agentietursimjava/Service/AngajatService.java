package org.example.agentietursimjava.Service;

import org.example.agentietursimjava.Domain.Angajat;
import org.example.agentietursimjava.Repository.DBRepos.AngajatDBRepo;
import org.example.agentietursimjava.Repository.Interfaces.RepoAngajat;

import java.util.Optional;
import java.util.UUID;

public class AngajatService {
    private final RepoAngajat angajatDBRepo;

    public AngajatService(AngajatDBRepo angajatDBRepo) {
        this.angajatDBRepo = angajatDBRepo;
    }

    public Optional<Angajat> findAngajatByUsername(String username) {
        try {
            return angajatDBRepo.findOneByUsername(username);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }
    public Optional<Angajat> findAngajat(UUID uuid) {
        try {
            return angajatDBRepo.findOne(uuid);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Iterable<Angajat> findAllAngajati() {
        try {
            return angajatDBRepo.findAll();
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Optional<Angajat> addAngajat(Angajat angajat) {
        try {
            return angajatDBRepo.save(angajat);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Optional<Angajat> deleteAngajat(Angajat angajat) {
        try {
            return angajatDBRepo.delete(angajat.getId());
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Optional<Angajat> updateAngajat(Angajat angajat) {
        try {
            return angajatDBRepo.update(angajat);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

}
