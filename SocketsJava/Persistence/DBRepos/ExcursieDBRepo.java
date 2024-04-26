package agentie.persistence.DBRepos;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import agentie.model.Excursie;
import agentie.persistence.Interfaces.IRepoExcursie;
import agentie.persistence.JdbcUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.time.LocalDateTime;
import java.util.*;

public class ExcursieDBRepo implements IRepoExcursie {

    private final JdbcUtils dbUtils;
    private static final Logger logger= LogManager.getLogger();

    public ExcursieDBRepo(Properties props) {
        logger.info("Initializing ExcursieDBRepo with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public Optional<Excursie> findOne(UUID uuid) {
        logger.traceEntry("finding excursie with id {}", uuid);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("select * from excursie where excursie_id = ?")) {
            preStmt.setObject(1, uuid);
            try (var result = preStmt.executeQuery()) {
                if (result.next()) {
                    String obiectivTuristic = result.getString("obiectivTuristic");
                    String numeFirmaTransport = result.getString("numeFirmaTransport");
                    LocalDateTime oraPlecare = result.getTimestamp("oraPlecare").toLocalDateTime();
                    int nrLocuriDisponibile = result.getInt("nrLocuriDisponibile");
                    double pret = result.getDouble("pret");
                    Excursie excursie = new Excursie(obiectivTuristic, numeFirmaTransport, oraPlecare, nrLocuriDisponibile, pret);
                    excursie.setId(uuid);
                    logger.traceExit(excursie);
                    return Optional.of(excursie);
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
    public Iterable<Excursie> findAll() {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        List<Excursie> excursii = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("select * from excursie")) {
            try (ResultSet result = preStmt.executeQuery()) {
                while (result.next()) {
                    UUID uuid = (UUID) result.getObject("excursie_id");
                    String obiectivTuristic = result.getString("obiectivTuristic");
                    String numeFirmaTransport = result.getString("numeFirmaTransport");
                    LocalDateTime oraPlecare = result.getTimestamp("oraPlecare").toLocalDateTime();
                    int nrLocuriDisponibile = result.getInt("nrLocuriDisponibile");
                    double pret = result.getDouble("pret");
                    Excursie excursie = new Excursie(obiectivTuristic, numeFirmaTransport, oraPlecare, nrLocuriDisponibile, pret);
                    excursie.setId(uuid);
                    excursii.add(excursie);
                }
            }
            logger.traceExit(excursii);
            return excursii;
        }catch (SQLException ex){
            logger.error(ex);
            logger.traceExit();
            throw new RuntimeException(ex);
        }
    }

    @Override
    public Optional<Excursie> save(Excursie entity) {
        logger.traceEntry("saving excursie {} ", entity);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("insert into excursie(excursie_id, \"obiectivTuristic\"," +
                " \"numeFirmaTransport\", \"oraPlecare\", \"nrLocuriDisponibile\", pret) values (?, ?, ?, ?, ?, ?)")) {
            preStmt.setObject(1, entity.getId());
            preStmt.setString(2, entity.getObiectivTuristic());
            preStmt.setString(3, entity.getNumeFirmaTransport());
            preStmt.setObject(4, entity.getOraPlecare());
            preStmt.setInt(5, entity.getNrLocuriDisponibile());
            preStmt.setDouble(6, entity.getPret());
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
    public Optional<Excursie> delete(UUID uuid) {
        Optional<Excursie> excursie = findOne(uuid);
        logger.traceEntry("deleting excursie with id {}", uuid);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("delete from excursie where excursie_id = ?")) {
            preStmt.setObject(1, uuid);
            int result = preStmt.executeUpdate();
            if (result == 0) {
                logger.traceExit();
                return Optional.empty();
            } else {
                logger.trace("Deleted {} instances", result);
                logger.traceExit(excursie);
                return excursie;
            }
        } catch (SQLException ex) {
            logger.error(ex);
            logger.traceExit();
            throw new RuntimeException(ex);
        }
    }

    @Override
    public Optional<Excursie> update(Excursie entity) {
        logger.traceEntry("updating excursie {} ", entity);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("update excursie set \"obiectivTuristic\" = ?," +
                " \"numeFirmaTransport\" = ?, \"oraPlecare\" = ?, \"nrLocuriDisponibile\" = ?, pret = ? where excursie_id = ?")){
            preStmt.setString(1, entity.getObiectivTuristic());
            preStmt.setString(2, entity.getNumeFirmaTransport());
            preStmt.setObject(3, entity.getOraPlecare());
            preStmt.setInt(4, entity.getNrLocuriDisponibile());
            preStmt.setDouble(5, entity.getPret());
            preStmt.setObject(6, entity.getId());
            int result = preStmt.executeUpdate();
            if (result == 0) {
                logger.traceExit();
                return Optional.of(entity);
            } else {
                logger.trace("Updated {} instances", result);
                logger.traceExit();
                return Optional.empty();
            }
        }catch (SQLException ex){
            logger.error(ex);
            logger.traceExit();
            throw new RuntimeException(ex);
        }
    }

    public Iterable<Excursie> cautaExcursii(String numeObiectiv, Integer deLaOra, Integer panaLaOra) {
        logger.traceEntry("cautare excursii cu obiectivul {} intre orele {} si {}", numeObiectiv, deLaOra, panaLaOra);
        Connection con = dbUtils.getConnection();
        List<Excursie> excursii = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("select * from excursie where \"obiectivTuristic\" ilike ? and " +
                "extract(hour from \"oraPlecare\") >= ? and extract(hour from \"oraPlecare\") <= ?")){
            preStmt.setString(1, numeObiectiv);
            preStmt.setInt(2, deLaOra);
            preStmt.setInt(3, panaLaOra);
            try (ResultSet result = preStmt.executeQuery()) {
                while (result.next()) {
                    UUID uuid = (UUID) result.getObject("excursie_id");
                    String obiectivTuristic = result.getString("obiectivTuristic");
                    String numeFirmaTransport = result.getString("numeFirmaTransport");
                    LocalDateTime oraPlecare = result.getTimestamp("oraPlecare").toLocalDateTime();
                    int nrLocuriDisponibile = result.getInt("nrLocuriDisponibile");
                    double pret = result.getDouble("pret");
                    Excursie excursie = new Excursie(obiectivTuristic, numeFirmaTransport, oraPlecare, nrLocuriDisponibile, pret);
                    excursie.setId(uuid);
                    excursii.add(excursie);
                }
            }
            logger.traceExit(excursii);
            return excursii;
        }catch (SQLException ex){
            logger.error(ex);
            logger.traceExit();
            throw new RuntimeException(ex);
        }
    }
}

