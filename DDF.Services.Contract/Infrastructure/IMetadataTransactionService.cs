using DDF.Services.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDF.Services.Contract.Infrastructure
{
    public interface IMetadataTransactionService
    {
        Task<IList<Lookup>> GetMetadata();
    }
}
