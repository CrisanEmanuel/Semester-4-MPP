package agentie.networking.dto;

import java.io.Serializable;

public class dtoExcursie implements Serializable {
    private String obiectivTuristic;
    private int deLaOra;
    private int panaLaOra;

    public dtoExcursie(String obiectivTuristic, int deLaOra, int panaLaOra) {
        this.obiectivTuristic = obiectivTuristic;
        this.deLaOra = deLaOra;
        this.panaLaOra = panaLaOra;
    }

    public int getDeLaOra() {
        return deLaOra;
    }

    public void setDeLaOra(int deLaOra) {
        this.deLaOra = deLaOra;
    }

    public int getPanaLaOra() {
        return panaLaOra;
    }

    public void setPanaLaOra(int panaLaOra) {
        this.panaLaOra = panaLaOra;
    }

    public String getObiectivTuristic() {
        return obiectivTuristic;
    }

    public void setObiectivTuristic(String obiectivTuristic) {
        this.obiectivTuristic = obiectivTuristic;
    }

    @Override
    public String toString() {
        return "dtoExcursie{" +
                "obiectivTuristic='" + obiectivTuristic + '\'' +
                '}';
    }
}
