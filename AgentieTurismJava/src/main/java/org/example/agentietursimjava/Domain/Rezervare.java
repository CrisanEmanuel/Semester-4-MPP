package org.example.agentietursimjava.Domain;

import java.util.Objects;
import java.util.UUID;

public class Rezervare extends Entity<UUID> {

    private String numeClient;
    private String numarTelefonClient;
    private int nrBilete;
    private Excursie excursie;

    public Rezervare(String numeClient, String numarTelefonClient, int nrBilete, Excursie excursie) {
        this.setId(UUID.randomUUID());
        this.numeClient = numeClient;
        this.numarTelefonClient = numarTelefonClient;
        this.nrBilete = nrBilete;
        this.excursie = excursie;
    }

    public String getNumeClient() {
        return numeClient;
    }

    public void setNumeClient(String numeClient) {
        this.numeClient = numeClient;
    }

    public String getNumarTelefonClient() {
        return numarTelefonClient;
    }

    public void setNumarTelefonClient(String numarTelefonClient) {
        this.numarTelefonClient = numarTelefonClient;
    }

    public int getNrBilete() {
        return nrBilete;
    }

    public void setNrBilete(int nrBilete) {
        this.nrBilete = nrBilete;
    }

    public Excursie getExcursie() {
        return excursie;
    }

    public void setExcursie(Excursie excursie) {
        this.excursie = excursie;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Rezervare rezervare)) return false;
        return nrBilete == rezervare.nrBilete && Objects.equals(numeClient, rezervare.numeClient) && Objects.equals(numarTelefonClient, rezervare.numarTelefonClient) && Objects.equals(excursie, rezervare.excursie);
    }

    @Override
    public int hashCode() {
        return Objects.hash(numeClient, numarTelefonClient, nrBilete, excursie);
    }

    @Override
    public String toString() {
        return "Rezervare{" +
                "numeClient='" + numeClient + '\'' +
                ", numarTelefonClient='" + numarTelefonClient + '\'' +
                ", nrBilete=" + nrBilete +
                ", excursie=" + excursie +
                '}';
    }
}
