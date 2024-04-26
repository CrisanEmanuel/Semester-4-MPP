using System;
using System.Collections.Generic;
using Model;

namespace Persistence.Interfaces
{
    public interface IRepoExcursie: IRepo<Guid, Excursie>
    {
        IEnumerable<Excursie> CautaExcursii(string obiectivTuristic, int deLaOra, int panaLaOra);
    }
}