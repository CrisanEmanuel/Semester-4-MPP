#nullable enable
using System;
using Model;

namespace Persistence.Interfaces
{
    public interface IRepoAngajat : IRepo<Guid, Angajat>
    {
        public Angajat? FindAngajatByUsername(string username);
    }
}