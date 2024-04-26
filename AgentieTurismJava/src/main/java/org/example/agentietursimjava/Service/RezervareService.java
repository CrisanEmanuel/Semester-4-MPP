package org.example.agentietursimjava.Service;

import org.example.agentietursimjava.Domain.Rezervare;
import org.example.agentietursimjava.Repository.DBRepos.RezervareDBRepo;
import org.example.agentietursimjava.Repository.Interfaces.RepoRezervare;

import java.util.Optional;
import java.util.UUID;

public class RezervareService {

    private final RepoRezervare rezervareDBRepo;

    public RezervareService(RezervareDBRepo rezervareDBRepo) {
        this.rezervareDBRepo = rezervareDBRepo;
    }

    public Optional<Rezervare> findRezervare(UUID uuid) {
        try {
            return rezervareDBRepo.findOne(uuid);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Iterable<Rezervare> findAllRezervari() {
        try {
            return rezervareDBRepo.findAll();
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Optional<Rezervare> addRezervare(Rezervare rezervare) {
        try {
            return rezervareDBRepo.save(rezervare);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Optional<Rezervare> deleteRezervare(Rezervare rezervare) {
        try {
            return rezervareDBRepo.delete(rezervare.getId());
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }

    public Optional<Rezervare> updateRezervare(Rezervare rezervare) {
        try {
            return rezervareDBRepo.update(rezervare);
        } catch (RuntimeException e) {
            throw new RuntimeException(e);
        }
    }
}
