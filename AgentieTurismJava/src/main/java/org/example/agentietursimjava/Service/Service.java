package org.example.agentietursimjava.Service;

import org.example.agentietursimjava.Domain.Angajat;
import org.example.agentietursimjava.Domain.Excursie;
import org.example.agentietursimjava.Domain.Rezervare;

import java.util.Optional;
import java.util.UUID;

public class Service {
    private final AngajatService angajatService;
    private final ExcursieService excursieService;
    private final RezervareService rezervareService;

    public Service(AngajatService angajatService, ExcursieService excursieService, RezervareService rezervareService) {
        this.angajatService = angajatService;
        this.excursieService = excursieService;
        this.rezervareService = rezervareService;
    }

    // Angajat
    public Optional<Angajat> findAngajat(UUID uuid) {
        return angajatService.findAngajat(uuid);
    }

    public Iterable<Angajat> findAllAngajati() {
        return angajatService.findAllAngajati();
    }

    public Optional<Angajat> addAngajat(Angajat angajat) {
        return angajatService.addAngajat(angajat);
    }

    public Optional<Angajat> deleteAngajat(Angajat angajat) {
        return angajatService.deleteAngajat(angajat);
    }

    public Optional<Angajat> updateAngajat(Angajat angajat) {
        return angajatService.updateAngajat(angajat);
    }

    public Optional<Angajat> findAngajatByUsername(String username) {
        return angajatService.findAngajatByUsername(username);
    }

    // Excursie
    public Optional<Excursie> findExcursie(UUID uuid) {
        return excursieService.findExcursie(uuid);
    }

    public Iterable<Excursie> findAllExcursii() {
        return excursieService.findAllExcursii();
    }

    public Optional<Excursie> addExcursie(Excursie excursie) {
        return excursieService.addExcursie(excursie);
    }

    public Optional<Excursie> deleteExcursie(Excursie excursie) {
        return excursieService.deleteExcursie(excursie);
    }

    public Optional<Excursie> updateExcursie(Excursie excursie) {
        return excursieService.updateExcursie(excursie);
    }

    public Iterable<Excursie> cautaExcursii(String numeObiectiv, Integer deLaOra, Integer panaLaOra) {
        return excursieService.cautaExcursii(numeObiectiv, deLaOra, panaLaOra);
    }

    // Rezervare
    public Optional<Rezervare> findRezervare(UUID uuid) {
        return rezervareService.findRezervare(uuid);
    }

    public Iterable<Rezervare> findAllRezervari() {
        return rezervareService.findAllRezervari();
    }

    public Optional<Rezervare> addRezervare(Rezervare rezervare) {
        return rezervareService.addRezervare(rezervare);
    }

    public Optional<Rezervare> deleteRezervare(Rezervare rezervare) {
        return rezervareService.deleteRezervare(rezervare);
    }

    public Optional<Rezervare> updateRezervare(Rezervare rezervare) {
        return rezervareService.updateRezervare(rezervare);
    }
}
