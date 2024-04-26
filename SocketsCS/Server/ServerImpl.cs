#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using Persistence.Interfaces;
using Services;
using static Server.Password;

namespace Server
{
    public class ServerImpl(IRepoAngajat repoAngajat, IRepoExcursie repoExcursie, IRepoRezervare repoRezervare) :IService
    {
        private readonly IDictionary <string, IObserver> _loggedAngajati = new Dictionary<string, IObserver>();

        // ANGAJATI
        public void Login(Angajat angajat, IObserver client)
        {
            // check if the user is in the database
            var angajatDb = repoAngajat.FindAngajatByUsername(angajat.Username);
            if (angajatDb == null)
            {
                throw new AgentieException("Authentication failed!");
            }

            if (!VerifyPassword(angajat.Password, angajatDb.Password)) {
                throw new AgentieException("Invalid username or password!");
            }
            
            if (_loggedAngajati.ContainsKey(angajat.Username)) {
                throw new AgentieException("User already logged in!");
            }
            _loggedAngajati[angajatDb.Username] = client;
        }

        public void Logout(Angajat angajat)
        {
            var localAngajat= _loggedAngajati[angajat.Username];
            if (localAngajat == null)
                throw new AgentieException("User " + angajat.Username + " is not logged in.");
            _loggedAngajati.Remove(angajat.Username);
        }

        // EXCURSII
        public IEnumerable<Excursie> FindAllExcursii()
        {
            return repoExcursie.FindAll();
        }

        public Excursie? UpdateExcursie(Excursie excursie)
        {
            var ex =  repoExcursie.Update(excursie);
            if (ex != null) return null;
            foreach (var observer in _loggedAngajati.Values)
            {
                Task.Run(() => observer.UpdateNumarLocuriExcursie(excursie));
            }
            return ex;
        }

        public IEnumerable<Excursie> CautaExcursii(string numeObiectiv, int deLaOra, int panaLaOra)
        {
            return repoExcursie.CautaExcursii(numeObiectiv, deLaOra, panaLaOra);
        }
        
        // REZERVARI
        public Rezervare? AddRezervare(Rezervare rezervare)
        {
            return repoRezervare.Save(rezervare);
        }
    }
}