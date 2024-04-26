package org.example.agentietursimjava.Service;

import org.example.agentietursimjava.Domain.Excursie;
import org.example.agentietursimjava.Repository.DBRepos.ExcursieDBRepo;
import org.example.agentietursimjava.Repository.Interfaces.RepoExcursie;

import java.util.Optional;
import java.util.UUID;

public class ExcursieService {

    private final RepoExcursie excursieDBRepo;

    public ExcursieService(ExcursieDBRepo excursieDBRepo) {
        this.excursieDBRepo = excursieDBRepo;
    }

    public Optional<Excursie> findExcursie(UUID uuid) {
        try {
            return excursieDBRepo.findOne(uuid);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Iterable<Excursie> findAllExcursii() {
        try {
            return excursieDBRepo.findAll();
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Optional<Excursie> addExcursie(Excursie excursie) {
        try {
            return excursieDBRepo.save(excursie);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Optional<Excursie> deleteExcursie(Excursie excursie) {
        try {
            return excursieDBRepo.delete(excursie.getId());
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Optional<Excursie> updateExcursie(Excursie excursie) {
        try {
            return excursieDBRepo.update(excursie);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Iterable<Excursie> cautaExcursii(String numeObiectiv, Integer deLaOra, Integer panaLaOra) {
        try {
            return excursieDBRepo.cautaExcursii(numeObiectiv, deLaOra, panaLaOra);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

}
