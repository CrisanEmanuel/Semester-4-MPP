#nullable enable
using System;
using System.Collections.Generic;
using log4net;
using Model;
using Persistence.Interfaces;
using Persistence.Utils;

namespace Persistence.DBRepos;

public class RepoRezervare: IRepoRezervare
    {
        private static readonly ILog Log = LogManager.GetLogger("RepoRezervare");

        private readonly IDictionary<string, string> _props;
        
        public RepoRezervare(IDictionary<string, string> props)
        {
            Log.Info("Creating RepoRezervare ");
            _props = props;
        }
        
        public Rezervare? FindOne(Guid id)
        {
            Log.InfoFormat("Entering FindOne with value {0}", id);
            var con = DbUtils.GetConnection(_props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select " +
                                   "r.\"numeClient\", " +
                                   "r.\"numarTelefonClient\", " +
                                   "r.\"nrBilete\", " +
                                   "r.excursie_id, " +
                                   "e.\"obiectivTuristic\", " +
                                   "e.\"numeFirmaTransport\", " +
                                   "e.\"oraPlecare\", " +
                                   "e.\"nrLocuriDisponibile\", " +
                                   "e.pret " +
                                   "from rezervare r join excursie e on r.excursie_id = e.excursie_id " +
                                   "where r.rezervare_id = @rezervare_id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@rezervare_id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var numeClient = dataR.GetString(0);
                        var numarTelefonClient = dataR.GetString(1);
                        var nrBilete = dataR.GetInt32(2);
                        var excursieId= dataR.GetGuid(3);
                        var obiectivTuristic = dataR.GetString(4);
                        var numeFirmaTransport = dataR.GetString(5);
                        var oraPlecare = dataR.GetDateTime(6);
                        var nrLocuriDisponibile = dataR.GetInt32(7);
                        var pret = dataR.GetDouble(8);
                        var excursie = new Excursie(obiectivTuristic, numeFirmaTransport, oraPlecare, nrLocuriDisponibile, pret);
                        excursie.SetId(excursieId);
                        var rezervare = new Rezervare(numeClient, numarTelefonClient, nrBilete, excursie);
                        rezervare.SetId(id);
                        Log.InfoFormat("Exiting FindOne with value {0}", rezervare);
                        return rezervare;
                    } 
                }
            }
            Log.InfoFormat("Exiting FindOne with value {0}", null);
            return null;
        }

        public IEnumerable<Rezervare> FindAll()
        {
            Log.InfoFormat("Entering FindAll");
            var con = DbUtils.GetConnection(_props);
            IList<Rezervare> rezervari = new List<Rezervare>();

            using var comm = con.CreateCommand();
            comm.CommandText = "select " +
                               "r.rezervare_id, " +
                               "r.\"numeClient\", " +
                               "r.\"numarTelefonClient\", " +
                               "r.\"nrBilete\", " +
                               "r.excursie_id, " +
                               "e.\"obiectivTuristic\", " +
                               "e.\"numeFirmaTransport\", " +
                               "e.\"oraPlecare\", " +
                               "e.\"nrLocuriDisponibile\", " +
                               "e.pret " +
                               "from rezervare r join excursie e on r.excursie_id = e.excursie_id";

            using var dataR = comm.ExecuteReader();
            while (dataR.Read())
            {
                var rezervareId = dataR.GetGuid(0);
                var numeClient = dataR.GetString(1);
                var numarTelefonClient = dataR.GetString(2);
                var nrBilete = dataR.GetInt32(3);
                var excursieId = dataR.GetGuid(4);
                var obiectivTuristic = dataR.GetString(5);
                var numeFirmaTransport = dataR.GetString(6);
                var oraPlecare = dataR.GetDateTime(7);
                var nrLocuriDisponibile = dataR.GetInt32(8);
                var pret = dataR.GetDouble(9);
                var excursie = new Excursie(obiectivTuristic, numeFirmaTransport, oraPlecare, nrLocuriDisponibile, pret);
                excursie.SetId(excursieId);
                var rezervare = new Rezervare(numeClient, numarTelefonClient, nrBilete, excursie);
                rezervare.SetId(rezervareId);
                rezervari.Add(rezervare);
            }
            Log.InfoFormat("Exiting FindAll with value {0}", rezervari);
            return rezervari;
        }

        public Rezervare? Save(Rezervare entity)
        {
            Log.InfoFormat("Entering Save with value {0}", entity);
            var con = DbUtils.GetConnection(_props);

            using var comm = con.CreateCommand();
            comm.CommandText = "insert into rezervare(rezervare_id, \"numeClient\", \"numarTelefonClient\", \"nrBilete\", excursie_id)" +
                               " values (@rezervare_id, @numeClient, @numarTelefonClient, @nrBilete, @excursie_id)";
            var paramId = comm.CreateParameter();
            paramId.ParameterName = "@rezervare_id";
            paramId.Value = entity.Id;
            comm.Parameters.Add(paramId);

            var paramNumeClient = comm.CreateParameter();
            paramNumeClient.ParameterName = "@numeClient";
            paramNumeClient.Value = entity.NumeClient;
            comm.Parameters.Add(paramNumeClient);

            var paramNumarTelefonClient = comm.CreateParameter();
            paramNumarTelefonClient.ParameterName = "@numarTelefonClient";
            paramNumarTelefonClient.Value = entity.NumarTelefonClient;
            comm.Parameters.Add(paramNumarTelefonClient);

            var paramNrBilete = comm.CreateParameter();
            paramNrBilete.ParameterName = "@nrBilete";
            paramNrBilete.Value = entity.NrBilete;
            comm.Parameters.Add(paramNrBilete);
                
            var paramExcursieId = comm.CreateParameter();
            paramExcursieId.ParameterName = "@excursie_id";
            paramExcursieId.Value = entity.Excursie.Id;
            comm.Parameters.Add(paramExcursieId);

            var result = comm.ExecuteNonQuery();
            Log.InfoFormat("Exiting Save with value {0}", result == 0 ? entity : null);
            return result == 0 ? entity : null;
        }

        public Rezervare? Delete(Guid id)
        {
            Log.InfoFormat("Entering Delete with value {0}", id);
            var rezervare = FindOne(id);
            var con = DbUtils.GetConnection(_props);
            using var comm = con.CreateCommand();
            comm.CommandText = "delete from rezervare where rezervare_id = @rezervare_id";
            var paramId = comm.CreateParameter();
            paramId.ParameterName = "@rezervare_id";
            paramId.Value = id;
            comm.Parameters.Add(paramId);
            var result = comm.ExecuteNonQuery();
            Log.InfoFormat("Exiting Delete with value {0}", result == 0 ? null : rezervare);
            return result == 0 ? null : rezervare;
        }

        public Rezervare? Update(Rezervare entity)
        {
            Log.InfoFormat("Entering Update with value {0}", entity);
            var con = DbUtils.GetConnection(_props);

            using var comm = con.CreateCommand();
            comm.CommandText = "update rezervare set \"numeClient\" = @numeClient, \"numarTelefonClient\" = @numarTelefonClient, " +
                               "\"nrBilete\" = @nrBilete, excursie_id = @excursie_id where rezervare_id = @rezervare_id";
            var paramId = comm.CreateParameter();
            paramId.ParameterName = "@rezervare_id";
            paramId.Value = entity.Id;
            comm.Parameters.Add(paramId);

            var paramNumeClient = comm.CreateParameter();
            paramNumeClient.ParameterName = "@numeClient";
            paramNumeClient.Value = entity.NumeClient;
            comm.Parameters.Add(paramNumeClient);

            var paramNumarTelefonClient = comm.CreateParameter();
            paramNumarTelefonClient.ParameterName = "@numarTelefonClient";
            paramNumarTelefonClient.Value = entity.NumarTelefonClient;
            comm.Parameters.Add(paramNumarTelefonClient);

            var paramNrBilete = comm.CreateParameter();
            paramNrBilete.ParameterName = "@nrBilete";
            paramNrBilete.Value = entity.NrBilete;
            comm.Parameters.Add(paramNrBilete);
                
            var paramExcursieId = comm.CreateParameter();
            paramExcursieId.ParameterName = "@excursie_id";
            paramExcursieId.Value = entity.Excursie.Id;
            comm.Parameters.Add(paramExcursieId);

            var result = comm.ExecuteNonQuery();
            Log.InfoFormat("Exiting Update with value {0}", result == 0 ? entity : null);
            return result == 0 ? entity : null;
        }
    }