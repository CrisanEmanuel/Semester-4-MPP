#nullable enable
using System.Collections.Generic;
using Model;

namespace Persistence.Interfaces
{
    public interface IRepo<in TId, TE> where TE: Entity<TId>
    {
        TE? FindOne(TId id);
    
        IEnumerable<TE> FindAll();
    
        TE? Save(TE entity);
    
        TE? Delete(TId id);
    
        TE? Update(TE entity);
    }
}