#nullable enable
using System;
using System.Collections.Generic;
using AgentieTurismCS.Domain;
using AgentieTurismCS.Repository.Interfaces;

namespace AgentieTurismCS.Service
{
    public class RezervareService
    {
        private readonly IRepoRezervare _repoRezervare;

        public RezervareService(IRepoRezervare repoRezervare)
        {
            _repoRezervare = repoRezervare;
        }

        public Rezervare? FindRezervare(Guid guid)
        {
            return _repoRezervare.FindOne(guid);
        }
        
        public IEnumerable<Rezervare> FindAllRezervari()
        {
            return _repoRezervare.FindAll();
        }
        
        public Rezervare? AddRezervare(Rezervare rezervare)
        {
            return _repoRezervare.Save(rezervare);
        }
        
        public Rezervare? UpdateRezervare(Rezervare rezervare)
        {
            return _repoRezervare.Update(rezervare);
        }
        
        public Rezervare? DeleteRezervare(Rezervare rezervare)
        {
            return _repoRezervare.Delete(rezervare.Id);
        }
    }
}