#nullable enable
using System;
using System.Collections.Generic;
using AgentieTurismCS.Domain;
using AgentieTurismCS.Repository.Interfaces;
using AgentieTurismCS.Utils;
using log4net;

namespace AgentieTurismCS.Repository.DBRepo
{
    public class RepoAngajat: IRepoAngajat
    {
        private static readonly ILog Log = LogManager.GetLogger("RepoAngajat");

        private readonly IDictionary<string, string> _props;
        
        public RepoAngajat(IDictionary<string, string> props)
        {
            Log.Info("Creating RepoAngajat ");
            _props = props;
        }
        
        public Angajat? FindOne(Guid id)
        {
            Log.InfoFormat("Entering FindOne with value {0}", id);
            var con = DbUtils.GetConnection(_props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select nume, prenume, username, parola, \"agentieTurism\" from angajat where angajat_id = @angajat_id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@angajat_id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var nume = dataR.GetString(0);
                        var prenume = dataR.GetString(1);
                        var username = dataR.GetString(2);
                        var parola = dataR.GetString(3);
                        var agentieTurism = dataR.GetString(4);
                        var angajat = new Angajat(nume, prenume, username, parola, agentieTurism);
                        angajat.SetId(id);
                        Log.InfoFormat("Exiting findOne with value {0}", angajat);
                        return angajat;
                    } 
                }
            }
            Log.InfoFormat("Exiting FindOne with value {0}", null);
            return null;
        }

        public IEnumerable<Angajat> FindAll()
        {
            Log.InfoFormat("Entering FindAll");
            var con = DbUtils.GetConnection(_props);
            IList<Angajat> angajati = new List<Angajat>();
            using var comm = con.CreateCommand();
            comm.CommandText = "select angajat_id, nume, prenume, username, parola, \"agentieTurism\" from angajat";

            using var dataR = comm.ExecuteReader();
            while (dataR.Read())
            {
                var angajatId = dataR.GetGuid(0);
                var nume = dataR.GetString(1);
                var prenume = dataR.GetString(2);
                var username = dataR.GetString(3);
                var parola = dataR.GetString(4);
                var agentieTurism = dataR.GetString(5);
                var angajat = new Angajat(nume, prenume, username, parola, agentieTurism);
                angajat.SetId(angajatId);
                angajati.Add(angajat);
            }
            Log.InfoFormat("Exiting FindAll with value {0}", angajati);
            return angajati;
        }

        public Angajat? Save(Angajat entity)
        {
            Log.InfoFormat("Entering Save with value {0}", entity);
            var con = DbUtils.GetConnection(_props);

            using var comm = con.CreateCommand();
            comm.CommandText = "insert into angajat(angajat_id, nume, prenume, username, parola, \"agentieTurism\") values " +
                               "(@angajat_id, @nume, @prenume, @username, @parola, @agentieTurism)";
            var paramId = comm.CreateParameter();
            paramId.ParameterName = "@angajat_id";
            paramId.Value = entity.Id;
            comm.Parameters.Add(paramId);

            var paramNume = comm.CreateParameter();
            paramNume.ParameterName = "@nume";
            paramNume.Value = entity.Nume;
            comm.Parameters.Add(paramNume);

            var paramPrenume = comm.CreateParameter();
            paramPrenume.ParameterName = "@prenume";
            paramPrenume.Value = entity.Prenume;
            comm.Parameters.Add(paramPrenume);

            var paramUsername = comm.CreateParameter();
            paramUsername.ParameterName = "@username";
            paramUsername.Value = entity.Username;
            comm.Parameters.Add(paramUsername);
                
            var paramParola = comm.CreateParameter();
            paramParola.ParameterName = "@parola";
            paramParola.Value = entity.Password;
            comm.Parameters.Add(paramParola);
                
            var paramAgentie = comm.CreateParameter();
            paramAgentie.ParameterName = "@agentieTurism";
            paramAgentie.Value = entity.AgentieTurism;
            comm.Parameters.Add(paramAgentie);

            var result = comm.ExecuteNonQuery();
            Log.InfoFormat("Exiting Save with value {0}", result == 0 ? entity : null);
            return result == 0 ? entity : null;
        }

        public Angajat? Delete(Guid id)
        {
            Log.InfoFormat("Entering Delete with value {0}", id);
            var angajat = FindOne(id);
            var con = DbUtils.GetConnection(_props);
            using var comm = con.CreateCommand();
            comm.CommandText = "delete from angajat where angajat_id = @angajat_id";
            var paramId = comm.CreateParameter();
            paramId.ParameterName = "@angajat_id";
            paramId.Value = id;
            comm.Parameters.Add(paramId);
            var result = comm.ExecuteNonQuery();
            Log.InfoFormat("Exiting Delete with value {0}", result == 0 ? null : angajat);
            return result == 0 ? null : angajat;
        }

        public Angajat? Update(Angajat entity)
        {
            Log.InfoFormat("Entering Update with value {0}", entity);
            var con = DbUtils.GetConnection(_props);

            using var comm = con.CreateCommand();
            comm.CommandText = "update angajat set nume =  @nume, prenume = @prenume, username = @username," +
                               " parola = @parola, \"agentieTurism\" = @agentieTurism where angajat_id = @angajat_id";
            var paramId = comm.CreateParameter();
            paramId.ParameterName = "@angajat_id";
            paramId.Value = entity.Id;
            comm.Parameters.Add(paramId);

            var paramNume = comm.CreateParameter();
            paramNume.ParameterName = "@nume";
            paramNume.Value = entity.Nume;
            comm.Parameters.Add(paramNume);

            var paramPrenume = comm.CreateParameter();
            paramPrenume.ParameterName = "@prenume";
            paramPrenume.Value = entity.Prenume;
            comm.Parameters.Add(paramPrenume);

            var paramUsername = comm.CreateParameter();
            paramUsername.ParameterName = "@username";
            paramUsername.Value = entity.Username;
            comm.Parameters.Add(paramUsername);
                
            var paramParola = comm.CreateParameter();
            paramParola.ParameterName = "@parola";
            paramParola.Value = entity.Password;
            comm.Parameters.Add(paramParola);
                
            var paramAgentie = comm.CreateParameter();
            paramAgentie.ParameterName = "@agentieTurism";
            paramAgentie.Value = entity.AgentieTurism;
            comm.Parameters.Add(paramAgentie);

            var result = comm.ExecuteNonQuery();
            Log.InfoFormat("Exiting Save with value {0}", result == 0 ? entity : null);
            return result == 0 ? entity : null;
        }

        public Angajat? FindAngajatByUsername(string username)
        {
            Log.InfoFormat("Entering FindAngajatByUsername with value {0}", username);
            var con = DbUtils.GetConnection(_props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select angajat_id, nume, prenume, parola, \"agentieTurism\" from angajat where username = @username";
                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = username;
                comm.Parameters.Add(paramUsername);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var angajatId = dataR.GetGuid(0);
                        var nume = dataR.GetString(1);
                        var prenume = dataR.GetString(2);
                        var parola = dataR.GetString(3);
                        var agentieTurism = dataR.GetString(4);
                        var angajat = new Angajat(nume, prenume, username, parola, agentieTurism);
                        angajat.SetId(angajatId);
                        Log.InfoFormat("Exiting FindAngajatByUsername with value {0}", angajat);
                        return angajat;
                    } 
                }
            }
            Log.InfoFormat("Exiting FindOne with value {0}", null);
            return null;
        }
    }
}