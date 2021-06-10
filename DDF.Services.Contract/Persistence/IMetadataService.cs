using DDF.Services.Contract.Models;
using System.Threading.Tasks;

namespace DDF.Services.Contract.Persistence
{
    public interface IMetadataService
    {
        Task<Metadata> Create(Metadata metadata);
        Task<Metadata> Find(long id);
    }
}
