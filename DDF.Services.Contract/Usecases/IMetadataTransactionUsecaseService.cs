using DDF.Services.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDF.Services.Contract.Usecases
{
    public interface IMetadataTransactionUsecaseService
    {
        Task<IList<Lookup>> GetAndSaveMetadata();
    }
}
