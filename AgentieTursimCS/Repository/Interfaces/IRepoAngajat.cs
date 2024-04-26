#nullable enable
using System;
using AgentieTurismCS.Domain;

namespace AgentieTurismCS.Repository.Interfaces
{
    public interface IRepoAngajat : IRepo<Guid, Angajat>
    {
        public Angajat? FindAngajatByUsername(string username);
    }
}