package org.example.agentietursimjava.Domain;


import java.util.Objects;
import java.util.UUID;

public class Angajat extends Entity<UUID>{
    private String nume;
    private String prenume;
    private String username;
    private String password;
    private String agentieTurism;

    public Angajat(String nume, String prenume, String username, String password, String agentieTurism) {
        this.setId(UUID.randomUUID());
        this.nume = nume;
        this.prenume = prenume;
        this.username = username;
        this.password = password;
        this.agentieTurism = agentieTurism;
    }

    public String getNume() {
        return nume;
    }

    public void setNume(String nume) {
        this.nume = nume;
    }

    public String getPrenume() {
        return prenume;
    }

    public void setPrenume(String prenume) {
        this.prenume = prenume;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getAgentieTurism() {
        return agentieTurism;
    }

    public void setAgentieTurism(String agentieTurism) {
        this.agentieTurism = agentieTurism;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Angajat angajat)) return false;
        return Objects.equals(nume, angajat.nume) && Objects.equals(prenume, angajat.prenume) && Objects.equals(username, angajat.username) && Objects.equals(password, angajat.password) && Objects.equals(agentieTurism, angajat.agentieTurism);
    }

    @Override
    public int hashCode() {
        return Objects.hash(nume, prenume, username, password, agentieTurism);
    }

    @Override
    public String toString() {
        return "Angajat{" +
                "nume='" + nume + '\'' +
                ", prenume='" + prenume + '\'' +
                ", username='" + username + '\'' +
                ", password='" + password + '\'' +
                ", agentieTurism='" + agentieTurism + '\'' +
                '}';
    }
}

