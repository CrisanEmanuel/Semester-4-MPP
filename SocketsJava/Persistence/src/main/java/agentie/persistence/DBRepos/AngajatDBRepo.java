package agentie.persistence.DBRepos;


import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import agentie.model.Angajat;
import agentie.persistence.Interfaces.IRepoAngajat;
import agentie.persistence.JdbcUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.*;

public class AngajatDBRepo implements IRepoAngajat {

    private final JdbcUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();

    public AngajatDBRepo(Properties props) {
        logger.info("Initializing AngajatDBRepo with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public Optional<Angajat> findOne(UUID uuid) {
        logger.traceEntry("finding angajat with id {}", uuid);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("select * from angajat where angajat_id = ?")) {
            preStmt.setObject(1, uuid);
            try (var result = preStmt.executeQuery()) {
                if (result.next()) {
                    String nume = result.getString("nume");
                    String prenume = result.getString("prenume");
                    String username = result.getString("username");
                    String parola = result.getString("parola");
                    String agentieTurism = result.getString("agentieTurism");
                    Angajat angajat = new Angajat(nume, prenume, username, parola, agentieTurism);
                    angajat.setId(uuid);
                    logger.traceExit(angajat);
                    return Optional.of(angajat);
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
    public Iterable<Angajat> findAll() {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        List<Angajat> angajati = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("select * from angajat")) {
            try (ResultSet result = preStmt.executeQuery()) {
                while (result.next()) {
                    UUID id = (UUID) result.getObject("angajat_id");
                    String nume = result.getString("nume");
                    String prenume = result.getString("prenume");
                    String username = result.getString("username");
                    String parola = result.getString("parola");
                    String agentieTurism = result.getString("agentieTurism");
                    Angajat angajat = new Angajat(nume, prenume, username, parola, agentieTurism);
                    angajat.setId(id);
                    angajati.add(angajat);
                }
            }
            logger.traceExit(angajati);
            return angajati;
        }catch (SQLException ex){
            logger.error(ex);
            logger.traceExit();
            throw new RuntimeException(ex);
        }
    }

    @Override
    public Optional<Angajat> save(Angajat entity) {
        logger.traceEntry("saving angajat {} ", entity);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("insert into angajat(angajat_id, nume, prenume, username, " +
                "parola, \"agentieTurism\" ) values (?, ?, ?, ?, ?, ?)")) {
            preStmt.setObject(1, entity.getId());
            preStmt.setString(2, entity.getNume());
            preStmt.setString(3, entity.getPrenume());
            preStmt.setString(4, entity.getUsername());
            preStmt.setString(5, entity.getPassword());
            preStmt.setString(6, entity.getAgentieTurism());
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
    public Optional<Angajat> delete(UUID uuid) {
        Optional<Angajat> angajat = findOne(uuid);
        logger.traceEntry("deleting angajat with id {}", uuid);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("delete from angajat where angajat_id = ?")) {
            preStmt.setObject(1, uuid);
            int result = preStmt.executeUpdate();
            if (result == 0) {
                logger.traceExit();
                return Optional.empty();
            } else {
                logger.trace("Deleted {} instances", result);
                logger.traceExit(angajat);
                return angajat;
            }
        } catch (SQLException ex) {
            logger.error(ex);
            logger.traceExit();
            throw new RuntimeException(ex);
        }
    }

    @Override
    public Optional<Angajat> update(Angajat entity) {
        logger.traceEntry("updating angajat {} ", entity);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("update angajat set nume = ?, prenume = ?, username = ?, " +
                "parola = ?, \"agentieTurism\" = ? where angajat_id = ?")){
            preStmt.setString(1, entity.getNume());
            preStmt.setString(2, entity.getPrenume());
            preStmt.setString(3, entity.getUsername());
            preStmt.setString(4, entity.getPassword());
            preStmt.setString(5, entity.getAgentieTurism());
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

    @Override
    public Optional<Angajat> findOneByUsername(String username) {
        logger.traceEntry("finding angajat with username {}", username);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("select * from angajat where username = ?")) {
            preStmt.setString(1, username);
            try (var result = preStmt.executeQuery()) {
                if (result.next()) {
                    UUID id = (UUID) result.getObject("angajat_id");
                    String nume = result.getString("nume");
                    String prenume = result.getString("prenume");
                    String parola = result.getString("parola");
                    String agentieTurism = result.getString("agentieTurism");
                    Angajat angajat = new Angajat(nume, prenume, username, parola, agentieTurism);
                    angajat.setId(id);
                    logger.traceExit(angajat);
                    return Optional.of(angajat);
                }
            }
            return Optional.empty();
        } catch (SQLException ex) {
            logger.error(ex);
            logger.traceExit();
            throw new RuntimeException(ex);
        }
    }
}
