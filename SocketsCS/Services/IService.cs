#nullable enable
using System.Collections.Generic;
using Model;

namespace Services
{
    public interface IService
    {
        // Angajat
        void Login(Angajat angajat, IObserver client);
        void Logout(Angajat angajat);
        
        // Excursie
        IEnumerable<Excursie> FindAllExcursii();
        Excursie? UpdateExcursie(Excursie excursie);
        IEnumerable<Excursie> CautaExcursii(string numeObiectiv, int deLaOra, int panaLaOra);

        // Rezervare
        Rezervare? AddRezervare(Rezervare rezervare);
    }
}