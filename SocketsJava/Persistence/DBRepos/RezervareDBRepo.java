package agentie.persistence.DBRepos;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import agentie.model.Excursie;
import agentie.model.Rezervare;
import agentie.persistence.Interfaces.IRepoRezervare;
import agentie.persistence.JdbcUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.*;

public class RezervareDBRepo implements IRepoRezervare {

    private final JdbcUtils dbUtils;
    private static final Logger logger= LogManager.getLogger();

    public RezervareDBRepo(Properties props) {
        logger.info("Initializing RezervareDBRepo with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public Optional<Rezervare> findOne(UUID uuid) {
        logger.traceEntry("finding rezervare with id {}", uuid);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("""
                SELECT r."numeClient",
                       r."numarTelefonClient",
                       r."nrBilete",
                       r.excursie_id,
                       e."obiectivTuristic",
                       e."numeFirmaTransport",
                       e."oraPlecare",
                       e."nrLocuriDisponibile",
                       e.pret
                FROM rezervare r
                JOIN excursie e ON r.excursie_id = e.excursie_id
                WHERE r.rezervare_id = ?;""")) {
            preStmt.setObject(1, uuid);
            try (var result = preStmt.executeQuery()) {
                if (result.next()) {
                    String numeClient = result.getString("numeClient");
                    String numarTelefonClient = result.getString("numarTelefonClient");
                    int nrBilete = result.getInt("nrBilete");
                    UUID excursieId = (UUID) result.getObject("excursie_id");
                    Excursie excursie = new Excursie(
                            result.getString("obiectivTuristic"),
                            result.getString("numeFirmaTransport"),
                            result.getTimestamp("oraPlecare").toLocalDateTime(),
                            result.getInt("nrLocuriDisponibile"),
                            result.getDouble("pret")
                    );
                    excursie.setId(excursieId);
                    Rezervare rezervare = new Rezervare(numeClient, numarTelefonClient, nrBilete, excursie);
                    rezervare.setId(uuid);
                    logger.traceExit(rezervare);
                    return Optional.of(rezervare);
                }
            }
            return Optional.empty();
        } catch (SQLException ex) {
            logger.error(ex);
            logger.traceExit();
            throw new RuntimeException(ex);
        }
    }

    @Override
    public Iterable<Rezervare> findAll() {
        logger.traceEntry();
        List<Rezervare> rezervari = new ArrayList<>();
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("""
                SELECT *
                FROM rezervare r
                JOIN excursie e ON r.excursie_id = e.excursie_id;""")) {
            try (ResultSet result = preStmt.executeQuery()) {
                while (result.next()) {
                    UUID rezervareId = (UUID) result.getObject("rezervare_id");
                    String numeClient = result.getString("numeClient");
                    String numarTelefonClient = result.getString("numarTelefonClient");
                    int nrBilete = result.getInt("nrBilete");
                    UUID excursieId = (UUID) result.getObject("excursie_id");
                    Excursie excursie = new Excursie(
                            result.getString("obiectivTuristic"),
                            result.getString("numeFirmaTransport"),
                            result.getTimestamp("oraPlecare").toLocalDateTime(),
                            result.getInt("nrLocuriDisponibile"),
                            result.getDouble("pret")
                    );
                    excursie.setId(excursieId);
                    Rezervare rezervare = new Rezervare(numeClient, numarTelefonClient, nrBilete, excursie);
                    rezervare.setId(rezervareId);
                    rezervari.add(rezervare);
                }
            }
            logger.traceExit(rezervari);
            return rezervari;
        } catch (SQLException ex) {
            logger.error(ex);
            logger.traceExit();
            throw new RuntimeException(ex);
        }
    }

    @Override
    public Optional<Rezervare> save(Rezervare entity) {
        logger.traceEntry("saving rezervare {} ", entity);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("insert into rezervare(rezervare_id, \"numeClient\"," +
                " \"numarTelefonClient\", \"nrBilete\", excursie_id) values (?, ?, ?, ?, ?)")) {
            preStmt.setObject(1, entity.getId());
            preStmt.setString(2, entity.getNumeClient());
            preStmt.setString(3, entity.getNumarTelefonClient());
            preStmt.setInt(4, entity.getNrBilete());
            preStmt.setObject(5, entity.getExcursie().getId());
            int result = preStmt.executeUpdate();
            if (result == 0) {
                logger.traceExit();
                return Optional.of(entity);
            } else {
                logger.trace("Saved {} instances", result);
                logger.traceExit();
                return Optional.empty();
            }
        } catch (SQLException ex){
            logger.error(ex);
            logger.traceExit();
            throw new RuntimeException(ex);
        }
    }

    @Override
    public Optional<Rezervare> delete(UUID uuid) {
        Optional<Rezervare> rezervare = findOne(uuid);
        logger.traceEntry("deleting rezervare with id {}", uuid);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("delete from rezervare where rezervare_id = ?")) {
            preStmt.setObject(1, uuid);
            int result = preStmt.executeUpdate();
            if (result == 0) {
                logger.traceExit();
                return Optional.empty();
            } else {
                logger.trace("Deleted {} instances", result);
                logger.traceExit(rezervare);
                return rezervare;
            }
        } catch (SQLException ex) {
            logger.error(ex);
            logger.traceExit();
            throw new RuntimeException(ex);
        }
    }

    @Override
    public Optional<Rezervare> update(Rezervare entity) {
        logger.traceEntry("updating rezervare {} ", entity);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("update rezervare set \"numeClient\" = ?," +
                " \"numarTelefonClient\" = ?, \"nrBilete\" = ?, excursie_id = ?  where rezervare_id = ?")){
            preStmt.setString(1, entity.getNumeClient());
            preStmt.setString(2, entity.getNumarTelefonClient());
            preStmt.setInt(3, entity.getNrBilete());
            preStmt.setObject(4, entity.getExcursie().getId());
            preStmt.setObject(5, entity.getId());
            int result = preStmt.executeUpdate();
            if (result == 0) {
                logger.traceExit();
                return Optional.of(entity);
            } else {
                logger.trace("Updated {} instances", result);
                logger.traceExit();
                return Optional.empty();
            }
        } catch (SQLException ex){
            logger.error(ex);
            logger.traceExit();
            throw new RuntimeException(ex);
        }
    }
}

