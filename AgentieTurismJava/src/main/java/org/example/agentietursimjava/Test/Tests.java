package org.example.agentietursimjava.Test;

import org.example.agentietursimjava.Domain.Angajat;
import org.example.agentietursimjava.Domain.Excursie;
import org.example.agentietursimjava.Domain.Rezervare;
import org.example.agentietursimjava.Repository.DBRepos.AngajatDBRepo;
import org.example.agentietursimjava.Repository.DBRepos.ExcursieDBRepo;
import org.example.agentietursimjava.Repository.DBRepos.RezervareDBRepo;

import java.io.FileReader;
import java.io.IOException;
import java.time.LocalDateTime;
import java.util.List;
import java.util.Properties;

public class Tests {

    public static void runTests() {
        Properties props=new Properties();
        try {
            props.load(new FileReader("application.properties"));
        } catch (IOException e) {
            System.out.println("Cannot find application.properties "+e);
        }
        AngajatDBRepo angajatDBRepo = new AngajatDBRepo(props);
        Angajat angajat = new Angajat("test", "test", "test", "test", "test");
        assert (angajatDBRepo.save(angajat).isEmpty());
        assert (angajatDBRepo.findOne(angajat.getId()).isPresent());
        List<Angajat> angajati = (List<Angajat>) angajatDBRepo.findAll();
        assert (angajati.size() == 1);
        angajat.setNume("test2");
        assert (angajatDBRepo.update(angajat).isEmpty());
        assert (angajatDBRepo.findOne(angajat.getId()).get().getNume().equals("test2"));
        assert (angajatDBRepo.delete(angajat.getId()).isPresent());
        assert(angajatDBRepo.findOne(angajat.getId()).isEmpty());

        ExcursieDBRepo excursieDBRepo = new ExcursieDBRepo(props);
        Excursie excursie = new Excursie("test", "test", LocalDateTime.now(), 1, 3.4);
        assert (excursieDBRepo.save(excursie).isEmpty());
        assert (excursieDBRepo.findOne(excursie.getId()).isPresent());


        RezervareDBRepo rezervareDBRepo = new RezervareDBRepo(props);
        Rezervare rezervare = new Rezervare("test", "test", 1, excursie);
        assert (rezervareDBRepo.save(rezervare).isEmpty());
        assert (rezervareDBRepo.findOne(rezervare.getId()).isPresent());

        assert (excursieDBRepo.delete(excursie.getId()).isPresent()); // cascade delete should delete the rezervare

    }
}
