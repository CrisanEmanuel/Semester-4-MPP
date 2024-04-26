#nullable enable
using System;
using System.Collections.Generic;
using AgentieTurismCS.Domain;
using AgentieTurismCS.Repository.Interfaces;

namespace AgentieTurismCS.Service
{
    public class AngajatService
    {
        private readonly IRepoAngajat _repoAngajat;
        
        public AngajatService(IRepoAngajat repoAngajat)
        {
            _repoAngajat = repoAngajat;
        }

        public Angajat? FindAngajatByUsername(string username)
        {
            return _repoAngajat.FindAngajatByUsername(username);
        }
        
        public Angajat? FindAngajat(Guid guid)
        {
            return _repoAngajat.FindOne(guid);
        }
        
        public IEnumerable<Angajat> FindAllAngajati()
        {
            return _repoAngajat.FindAll();
        } 
        
        public Angajat? AddAngajat(Angajat angajat)
        {
            return _repoAngajat.Save(angajat);
        }
        
        public Angajat? UpdateAngajat(Angajat angajat)
        {
            return _repoAngajat.Update(angajat);
        }
        
        public Angajat? DeleteAngajat(Angajat angajat)
        {
            return _repoAngajat.Delete(angajat.Id);
        }
    }
}