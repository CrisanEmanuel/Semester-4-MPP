#nullable enable
using System;
using System.Collections.Generic;
using AgentieTurismCS.Domain;

namespace AgentieTurismCS.Service
{
    public class Service
    {
        private readonly AngajatService _angajatService;
        private readonly ExcursieService _excursieService;
        private readonly RezervareService _rezervareService;

        public Service(AngajatService angajatService, ExcursieService excursieService, RezervareService rezervareService)
        {
            _angajatService = angajatService;
            _excursieService = excursieService;
            _rezervareService = rezervareService;
        }
        
        // Angajat
        public IEnumerable<Angajat> FindAllAngajati()
        {
            return _angajatService.FindAllAngajati();
        }
        
        public Angajat? FindAngajatByUsername(string username)
        {
            return _angajatService.FindAngajatByUsername(username);
        }
        
        public Angajat? FindAngajat(Guid guid)
        {
            return _angajatService.FindAngajat(guid);
        }
        
        public Angajat? AddAngajat(Angajat angajat)
        {
            return _angajatService.AddAngajat(angajat);
        }
        
        public Angajat? UpdateAngajat(Angajat angajat)
        {
            return _angajatService.UpdateAngajat(angajat);
        }
        
        public Angajat? DeleteAngajat(Angajat angajat)
        {
            return _angajatService.DeleteAngajat(angajat);
        }
        
        // Excursie
        public IEnumerable<Excursie> FindAllExcursii()
        { 
            return _excursieService.FindAllExcursii();
        }
        
        public Excursie? FindExcursie(Guid guid)
        {
            return _excursieService.FindExcursie(guid);
        }
        
        public Excursie? AddExcursie(Excursie excursie)
        {
            return _excursieService.AddExcursie(excursie);
        }
        
        public Excursie? UpdateExcursie(Excursie excursie)
        {
            return _excursieService.UpdateExcursie(excursie);
        }
        
        public Excursie? DeleteExcursie(Excursie excursie)
        {
            return _excursieService.DeleteExcursie(excursie);
        }
        
        public IEnumerable<Excursie> CautaExcursii(string obiectivTuristic, int deLaOra, int panaLaOra)
        {
            return _excursieService.CautaExcursii(obiectivTuristic, deLaOra, panaLaOra);
        }
        
        // Rezervare
        public IEnumerable<Rezervare> FindAllRezervari()
        {
            return _rezervareService.FindAllRezervari();
        }
        
        public Rezervare? FindRezervare(Guid guid)
        {
            return _rezervareService.FindRezervare(guid);
        }
        
        public Rezervare? AddRezervare(Rezervare rezervare)
        {
            return _rezervareService.AddRezervare(rezervare);
        }
        
        public Rezervare? UpdateRezervare(Rezervare rezervare)
        {
            return _rezervareService.UpdateRezervare(rezervare);
        }
        
        public Rezervare? DeleteRezervare(Rezervare rezervare)
        {
            return _rezervareService.DeleteRezervare(rezervare);
        }
    }
}