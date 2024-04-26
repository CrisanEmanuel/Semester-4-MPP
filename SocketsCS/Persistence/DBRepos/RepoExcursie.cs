#nullable enable
using System;
using System.Collections.Generic;
using log4net;
using Model;
using Persistence.Interfaces;
using Persistence.Utils;

namespace Persistence.DBRepos;

public class RepoExcursie: IRepoExcursie
    {
        private static readonly ILog Log = LogManager.GetLogger("RepoExcursie");

        private readonly IDictionary<string, string> _props;
        
        public RepoExcursie(IDictionary<string, string> props)
        {
            Log.Info("Creating RepoExcursie ");
            _props = props;
        }
        
        public Excursie? FindOne(Guid id)
        {
            Log.InfoFormat("Entering FindOne with value {0}", id);
            var con = DbUtils.GetConnection(_props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select \"obiectivTuristic\", \"numeFirmaTransport\", \"oraPlecare\", \"nrLocuriDisponibile\", pret " +
                                   "from excursie where excursie_id = @excursie_id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@excursie_id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var obiectivTuristic = dataR.GetString(0);
                        var numeFirmaTransport = dataR.GetString(1);
                        var oraPlecare = dataR.GetDateTime(2);
                        var nrLocuriDisponibile = dataR.GetInt32(3);
                        var pret = dataR.GetDouble(4);
                        var excursie = new Excursie(obiectivTuristic, numeFirmaTransport, oraPlecare, nrLocuriDisponibile, pret);
                        excursie.SetId(id);
                        Log.InfoFormat("Exiting findOne with value {0}", excursie);
                        return excursie;
                    } 
                }
            }
            Log.InfoFormat("Exiting FindOne with value {0}", null);
            return null;
        }

        public IEnumerable<Excursie> FindAll()
        {
            Log.InfoFormat("Entering FindAll");
            var con = DbUtils.GetConnection(_props);
            IList<Excursie> excursii = new List<Excursie>();
            using var comm = con.CreateCommand();
            comm.CommandText = "select excursie_id, \"obiectivTuristic\", \"numeFirmaTransport\", \"oraPlecare\", " +
                               "\"nrLocuriDisponibile\", pret from excursie";

            using var dataR = comm.ExecuteReader();
            while (dataR.Read())
            {
                var excursieId = dataR.GetGuid(0);
                var obiectivTuristic = dataR.GetString(1);
                var numeFirmaTransport = dataR.GetString(2);
                var oraPlecare = dataR.GetDateTime(3);
                var nrLocuriDisponibile = dataR.GetInt32(4);
                var pret = dataR.GetDouble(5);
                var excursie = new Excursie(obiectivTuristic, numeFirmaTransport, oraPlecare, nrLocuriDisponibile, pret);
                excursie.SetId(excursieId);
                excursii.Add(excursie);
            }
            Log.InfoFormat("Exiting FindAll with value {0}", excursii);
            return excursii;
        }

        public Excursie? Save(Excursie entity)
        {
            Log.InfoFormat("Entering Save with value {0}", entity);
            var con = DbUtils.GetConnection(_props);

            using var comm = con.CreateCommand();
            comm.CommandText = "insert into excursie(excursie_id, \"obiectivTuristic\", \"numeFirmaTransport\", " +
                               "\"oraPlecare\", \"nrLocuriDisponibile\", pret) values (@excursie_id, @obiectivTuristic, " +
                               "@numeFirmaTransport, @oraPlecare, @nrLocuriDisponibile, @pret)";
            var paramId = comm.CreateParameter();
            paramId.ParameterName = "@excursie_id";
            paramId.Value = entity.Id;
            comm.Parameters.Add(paramId);

            var paramObiectivTuristic = comm.CreateParameter();
            paramObiectivTuristic.ParameterName = "@obiectivTuristic";
            paramObiectivTuristic.Value = entity.ObiectivTuristic;
            comm.Parameters.Add(paramObiectivTuristic);

            var paramNumeFirmaTransport = comm.CreateParameter();
            paramNumeFirmaTransport.ParameterName = "@numeFirmaTransport";
            paramNumeFirmaTransport.Value = entity.NumeFirmaTransport;
            comm.Parameters.Add(paramNumeFirmaTransport);

            var paramOraPlecare = comm.CreateParameter();
            paramOraPlecare.ParameterName = "@oraPlecare";
            paramOraPlecare.Value = entity.OraPlecare;
            comm.Parameters.Add(paramOraPlecare);
                
            var paramNrLocuriDisponibile = comm.CreateParameter();
            paramNrLocuriDisponibile.ParameterName = "@nrLocuriDisponibile";
            paramNrLocuriDisponibile.Value = entity.NrLocuriDisponibile;
            comm.Parameters.Add(paramNrLocuriDisponibile);
                
            var paramPret = comm.CreateParameter();
            paramPret.ParameterName = "@pret";
            paramPret.Value = entity.Pret;
            comm.Parameters.Add(paramPret);

            var result = comm.ExecuteNonQuery();
            Log.InfoFormat("Exiting Save with value {0}", result == 0 ? entity : null);
            return result == 0 ? entity : null;
        }

        public Excursie? Delete(Guid id)
        {
            Log.InfoFormat("Entering Delete with value {0}", id);
            var excursie = FindOne(id);
            var con = DbUtils.GetConnection(_props);
            using var comm = con.CreateCommand();
            comm.CommandText = "delete from excursie where excursie_id = @excursie_id";
            var paramId = comm.CreateParameter();
            paramId.ParameterName = "@excursie_id";
            paramId.Value = id;
            comm.Parameters.Add(paramId);
            var result = comm.ExecuteNonQuery();
            Log.InfoFormat("Exiting Delete with value {0}", result == 0 ? null : excursie);
            return result == 0 ? null : excursie;
        }

        public Excursie? Update(Excursie entity)
        {
            Log.InfoFormat("Entering Update with value {0}", entity);
            var con = DbUtils.GetConnection(_props);

            using var comm = con.CreateCommand();
            comm.CommandText = "update excursie set \"obiectivTuristic\" = @obiectivTuristic, \"numeFirmaTransport\" = @numeFirmaTransport," +
                               " /*\"oraPlecare\" = @oraPlecare,*/ \"nrLocuriDisponibile\" =  @nrLocuriDisponibile, pret = @pret " +
                               "where excursie_id =  @excursie_id";
            var paramId = comm.CreateParameter();
            paramId.ParameterName = "@excursie_id";
            paramId.Value = entity.Id;
            comm.Parameters.Add(paramId);

            var paramObiectivTuristic = comm.CreateParameter();
            paramObiectivTuristic.ParameterName = "@obiectivTuristic";
            paramObiectivTuristic.Value = entity.ObiectivTuristic;
            comm.Parameters.Add(paramObiectivTuristic);

            var paramNumeFirmaTransport = comm.CreateParameter();
            paramNumeFirmaTransport.ParameterName = "@numeFirmaTransport";
            paramNumeFirmaTransport.Value = entity.NumeFirmaTransport;
            comm.Parameters.Add(paramNumeFirmaTransport);

            // var paramOraPlecare = comm.CreateParameter();
            // paramOraPlecare.ParameterName = "@oraPlecare";
            // paramOraPlecare.Value = entity.OraPlecare;
            // comm.Parameters.Add(paramOraPlecare);
                
            var paramNrLocuriDisponibile = comm.CreateParameter();
            paramNrLocuriDisponibile.ParameterName = "@nrLocuriDisponibile";
            paramNrLocuriDisponibile.Value = entity.NrLocuriDisponibile;
            comm.Parameters.Add(paramNrLocuriDisponibile);
                
            var paramPret = comm.CreateParameter();
            paramPret.ParameterName = "@pret";
            paramPret.Value = entity.Pret;
            comm.Parameters.Add(paramPret);

            var result = comm.ExecuteNonQuery();
            Log.InfoFormat("Exiting Update with value {0}", result == 0 ? entity : null);
            return result == 0 ? entity : null;
        }

        public IEnumerable<Excursie> CautaExcursii(string obiectivTuristic, int deLaOra, int panaLaOra)
        {
            Log.InfoFormat("Entering CautaExcursii");
            var con = DbUtils.GetConnection(_props);
            IList<Excursie> excursii = new List<Excursie>();
            using var comm = con.CreateCommand();
            comm.CommandText = "select excursie_id, \"obiectivTuristic\", \"numeFirmaTransport\", \"oraPlecare\", " +
                               "\"nrLocuriDisponibile\", pret from excursie where \"obiectivTuristic\" ilike @obiectivTuristic " +
                               "and extract(hour from \"oraPlecare\") >= @deLaOra " +
                               "and extract(hour from \"oraPlecare\") <= @panaLaOra";
            
            var paramObiectiv = comm.CreateParameter();
            paramObiectiv.ParameterName = "@obiectivTuristic";
            paramObiectiv.Value = obiectivTuristic;
            comm.Parameters.Add(paramObiectiv);
            
            var paramDeLaOra = comm.CreateParameter();
            paramDeLaOra.ParameterName = "@deLaOra";
            paramDeLaOra.Value = deLaOra + 3;
            comm.Parameters.Add(paramDeLaOra);
            
            var paramPanaLaOra = comm.CreateParameter();
            paramPanaLaOra.ParameterName = "@panaLaOra";
            paramPanaLaOra.Value = panaLaOra + 3;
            comm.Parameters.Add(paramPanaLaOra);

            using var dataR = comm.ExecuteReader();
            while (dataR.Read())
            {
                var excursieId = dataR.GetGuid(0);
                var numeObiectivTuristic = dataR.GetString(1);
                var numeFirmaTransport = dataR.GetString(2);
                var oraPlecare = dataR.GetDateTime(3);
                var nrLocuriDisponibile = dataR.GetInt32(4);
                var pret = dataR.GetDouble(5);
                var excursie = new Excursie(numeObiectivTuristic, numeFirmaTransport, oraPlecare, nrLocuriDisponibile, pret);
                excursie.SetId(excursieId);
                excursii.Add(excursie);
            }
            Log.InfoFormat("Exiting CautaExcursii with value {0}", excursii);
            return excursii;
        }
    }