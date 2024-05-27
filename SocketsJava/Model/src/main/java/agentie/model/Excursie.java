package agentie.model;

import jakarta.persistence.Column;
import jakarta.persistence.Table;
import jakarta.persistence.Entity;

import java.time.LocalDateTime;
import java.util.Objects;
import java.util.UUID;

@Entity
@Table(name = "excursie")
public class Excursie extends agentie.model.Entity<UUID> {

    private String obiectivTuristic;
    private String numeFirmaTransport;
    private LocalDateTime oraPlecare;
    private int nrLocuriDisponibile;
    private double pret;

    public Excursie(String obiectivTuristic, String numeFirmaTransport, LocalDateTime oraPlecare, int nrLocuriDisponibile, double pret) {
        this.setId(UUID.randomUUID());
        this.obiectivTuristic = obiectivTuristic;
        this.numeFirmaTransport = numeFirmaTransport;
        this.oraPlecare = oraPlecare;
        this.nrLocuriDisponibile = nrLocuriDisponibile;
        this.pret = pret;
    }

    public Excursie() {
    }

    @Column(name = "\"obiectivTuristic\"")
    public String getObiectivTuristic() {
        return obiectivTuristic;
    }

    public void setObiectivTuristic(String obiectivTuristic) {
        this.obiectivTuristic = obiectivTuristic;
    }

    @Column(name = "\"numeFirmaTransport\"")
    public String getNumeFirmaTransport() {
        return numeFirmaTransport;
    }

    public void setNumeFirmaTransport(String numeFirmaTransport) {
        this.numeFirmaTransport = numeFirmaTransport;
    }

    @Column(name = "\"oraPlecare\"")
    public LocalDateTime getOraPlecare() {
        return oraPlecare;
    }

    public void setOraPlecare(LocalDateTime oraPlecare) {
        this.oraPlecare = oraPlecare;
    }

    @Column(name = "\"nrLocuriDisponibile\"")
    public int getNrLocuriDisponibile() {
        return nrLocuriDisponibile;
    }

    public void setNrLocuriDisponibile(int nrLocuriDisponibile) {
        this.nrLocuriDisponibile = nrLocuriDisponibile;
    }

    @Column(name = "pret")
    public double getPret() {
        return pret;
    }

    public void setPret(double pret) {
        this.pret = pret;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Excursie excursie)) return false;
        return nrLocuriDisponibile == excursie.nrLocuriDisponibile && Double.compare(pret, excursie.pret) == 0 && Objects.equals(obiectivTuristic, excursie.obiectivTuristic) && Objects.equals(numeFirmaTransport, excursie.numeFirmaTransport) && Objects.equals(oraPlecare, excursie.oraPlecare);
    }

    @Override
    public int hashCode() {
        return Objects.hash(obiectivTuristic, numeFirmaTransport, oraPlecare, nrLocuriDisponibile, pret);
    }

    @Override
    public String toString() {
        return "Excursie{" +
                "obiectivTuristic='" + obiectivTuristic + '\'' +
                ", numeFirmaTransport='" + numeFirmaTransport + '\'' +
                ", oraPlecare=" + oraPlecare +
                ", nrLocuriDisponibile=" + nrLocuriDisponibile +
                ", pret=" + pret +
                '}';
    }
}

