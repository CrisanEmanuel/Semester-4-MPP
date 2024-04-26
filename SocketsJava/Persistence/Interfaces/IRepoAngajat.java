package agentie.persistence.Interfaces;

import agentie.model.Angajat;
import java.util.Optional;
import java.util.UUID;

public interface IRepoAngajat extends IRepo<UUID, Angajat> {
    Optional<Angajat> findOneByUsername(String username);
}
