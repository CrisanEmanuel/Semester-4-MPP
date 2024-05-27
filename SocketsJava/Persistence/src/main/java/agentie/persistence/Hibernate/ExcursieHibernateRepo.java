package agentie.persistence.Hibernate;

import agentie.model.Excursie;
import agentie.model.Hibernate.HibernateUtils;
import org.hibernate.Session;
import org.hibernate.Transaction;

import java.util.Optional;
import java.util.UUID;

public class ExcursieHibernateRepo implements agentie.persistence.Interfaces.IRepoExcursie {
    @Override
    public Iterable<Excursie> cautaExcursii(String numeObiectiv, Integer deLaOra, Integer panaLaOra) {
        try (Session session = HibernateUtils.getSessionFactory().openSession()) {
            return session.createQuery("from Excursie where obiectivTuristic = :numeObiectiv and hour(oraPlecare) >= :deLaOra and hour(oraPlecare) <= :panaLaOra", Excursie.class)
                    .setParameter("numeObiectiv", numeObiectiv)
                    .setParameter("deLaOra", deLaOra)
                    .setParameter("panaLaOra", panaLaOra)
                    .getResultList();
        }
    }

    @Override
    public Optional<Excursie> findOne(UUID uuid) {
        try (Session session = HibernateUtils.getSessionFactory().openSession()) {
            return Optional.ofNullable(session.createSelectionQuery("from Excursie where id=:idM ", Excursie.class)
                    .setParameter("idM", uuid)
                    .getSingleResultOrNull());
        }
    }

    @Override
    public Iterable<Excursie> findAll() {
        try(Session session = HibernateUtils.getSessionFactory().openSession()) {
            return session.createQuery("from Excursie ", Excursie.class).getResultList();
        }
    }

    @Override
    public Optional<Excursie> save(Excursie entity) {
        Transaction tx = null;
        try (Session session = HibernateUtils.getSessionFactory().openSession()) {
            tx = session.beginTransaction();
            session.persist(entity);
            tx.commit();
            if (entity.getId() != null) {
                return Optional.of(entity);
            }
        } catch (Exception e) {
            if (tx != null) tx.rollback();
        }
        return Optional.empty();
    }

    @Override
    public Optional<Excursie> delete(UUID uuid) {
        Transaction tx = null;
        try (Session session = HibernateUtils.getSessionFactory().openSession()) {
            tx = session.beginTransaction();
            Excursie excursie = session.createQuery("from Excursie where id=?1", Excursie.class)
                    .setParameter(1, uuid)
                    .uniqueResult();
            if (excursie != null) {
                session.remove(excursie);
                tx.commit();
                return Optional.of(excursie);
            }
        } catch (Exception e) {
            if (tx != null) tx.rollback();
        }
        return Optional.empty();
    }

    @Override
    public Optional<Excursie> update(Excursie entity) {
        Transaction tx = null;
        try (Session session = HibernateUtils.getSessionFactory().openSession()) {
            tx = session.beginTransaction();
            session.merge(entity);
            tx.commit();
        } catch (Exception e) {
            if (tx != null) tx.rollback();
            return Optional.of(entity);
        }
        return Optional.empty();
    }
}
