package agentie.model;

import java.time.LocalDateTime;
import java.util.Objects;
import java.util.UUID;

public class Excursie extends Entity<UUID> {

    private String obiectivTuristic;
    private String numeFirmaTransport;
    private LocalDateTime oraPlecare;
    private int nrLocuriDisponibile;
    private double pret;
    private Angajat angajatCareAModificatExcursia;

    public Excursie(String obiectivTuristic, String numeFirmaTransport, LocalDateTime oraPlecare, int nrLocuriDisponibile, double pret) {
        this.setId(UUID.randomUUID());
        this.obiectivTuristic = obiectivTuristic;
        this.numeFirmaTransport = numeFirmaTransport;
        this.oraPlecare = oraPlecare;
        this.nrLocuriDisponibile = nrLocuriDisponibile;
        this.pret = pret;
    }

    public Excursie(String obiectivTuristic, String numeFirmaTransport, LocalDateTime oraPlecare, int nrLocuriDisponibile, double pret, Angajat angajatCareAModificatExcursia) {
        this.setId(UUID.randomUUID());
        this.obiectivTuristic = obiectivTuristic;
        this.numeFirmaTransport = numeFirmaTransport;
        this.oraPlecare = oraPlecare;
        this.nrLocuriDisponibile = nrLocuriDisponibile;
        this.pret = pret;
        this.angajatCareAModificatExcursia = angajatCareAModificatExcursia;
    }

    public  Excursie() {

    }

    public String getObiectivTuristic() {
        return obiectivTuristic;
    }

    public void setObiectivTuristic(String obiectivTuristic) {
        this.obiectivTuristic = obiectivTuristic;
    }

    public String getNumeFirmaTransport() {
        return numeFirmaTransport;
    }

    public void setNumeFirmaTransport(String numeFirmaTransport) {
        this.numeFirmaTransport = numeFirmaTransport;
    }

    public LocalDateTime getOraPlecare() {
        return oraPlecare;
    }

    public void setOraPlecare(LocalDateTime oraPlecare) {
        this.oraPlecare = oraPlecare;
    }

    public int getNrLocuriDisponibile() {
        return nrLocuriDisponibile;
    }

    public void setNrLocuriDisponibile(int nrLocuriDisponibile) {
        this.nrLocuriDisponibile = nrLocuriDisponibile;
    }

    public double getPret() {
        return pret;
    }

    public void setPret(double pret) {
        this.pret = pret;
    }

    public Angajat getAngajatCareAModificatExcursia() {
        return angajatCareAModificatExcursia;
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

