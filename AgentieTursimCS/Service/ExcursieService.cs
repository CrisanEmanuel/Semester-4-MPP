#nullable enable
using System;
using System.Collections.Generic;
using AgentieTurismCS.Domain;
using AgentieTurismCS.Repository.Interfaces;

namespace AgentieTurismCS.Service
{
    public class ExcursieService
    {
        private readonly IRepoExcursie _repoExcursie;
        
        public ExcursieService(IRepoExcursie repoExcursie)
        {
            _repoExcursie = repoExcursie;
        }
        
        public Excursie? FindExcursie(Guid guid)
        {
            return _repoExcursie.FindOne(guid);
        }
        
        public IEnumerable<Excursie> FindAllExcursii()
        {
            return _repoExcursie.FindAll();
        }
        
        public Excursie? AddExcursie(Excursie excursie)
        {
            return _repoExcursie.Save(excursie);
        } 
        
        public Excursie? DeleteExcursie(Excursie excursie)
        {
            return _repoExcursie.Delete(excursie.Id);
        }
        
        public Excursie? UpdateExcursie(Excursie excursie)
        {
            return _repoExcursie.Update(excursie);
        }
        
        public IEnumerable<Excursie> CautaExcursii(string obiectivTuristic, int deLaOra, int panaLaOra)
        {
            return _repoExcursie.CautaExcursii(obiectivTuristic, deLaOra, panaLaOra);
        }
    }
}