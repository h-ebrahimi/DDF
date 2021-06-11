using DDF.Services.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDF.Services.Contract.Persistence
{
    public interface IMetadataService
    {
        Task<Lookup> Insert(Lookup metadata);
        Task<Lookup> Find(int id);
        Task<IList<Lookup>> Insert(IList<Lookup> lookups);
    }
}
