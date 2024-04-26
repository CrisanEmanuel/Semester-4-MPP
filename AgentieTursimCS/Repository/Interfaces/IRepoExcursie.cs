using System;
using System.Collections.Generic;
using AgentieTurismCS.Domain;

namespace AgentieTurismCS.Repository.Interfaces
{
    public interface IRepoExcursie: IRepo<Guid, Excursie>
    {
        IEnumerable<Excursie> CautaExcursii(string obiectivTuristic, int deLaOra, int panaLaOra);
    }
}