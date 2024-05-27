package agentie.model;
import java.util.Objects;

public class ExcursieDTO {
    private String obiectivTuristic;
    private String numeFirmaTransport;
    private String oraPlecare;
    private int nrLocuriDisponibile;
    private double pret;

    public ExcursieDTO(String obiectivTuristic, String numeFirmaTransport, String oraPlecare, int nrLocuriDisponibile, double pret) {
        this.obiectivTuristic = obiectivTuristic;
        this.numeFirmaTransport = numeFirmaTransport;
        this.oraPlecare = oraPlecare;
        this.nrLocuriDisponibile = nrLocuriDisponibile;
        this.pret = pret;
    }

    public ExcursieDTO() {
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

    public String getOraPlecare() {
        return oraPlecare;
    }

    public void setOraPlecare(String oraPlecare) {
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

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof ExcursieDTO that)) return false;
        return nrLocuriDisponibile == that.nrLocuriDisponibile && Double.compare(pret, that.pret) == 0 && Objects.equals(obiectivTuristic, that.obiectivTuristic) && Objects.equals(numeFirmaTransport, that.numeFirmaTransport) && Objects.equals(oraPlecare, that.oraPlecare);
    }

    @Override
    public int hashCode() {
        return Objects.hash(obiectivTuristic, numeFirmaTransport, oraPlecare, nrLocuriDisponibile, pret);
    }
}
