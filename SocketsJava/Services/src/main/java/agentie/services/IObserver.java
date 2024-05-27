package agentie.services;

import agentie.model.Excursie;

public interface IObserver {
    void updateNumarLocuriExcursie(Excursie excursie) throws AgentieException;
}
