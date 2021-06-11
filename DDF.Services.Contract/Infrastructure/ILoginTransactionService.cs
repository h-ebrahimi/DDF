using System.Net;
using System.Threading.Tasks;

namespace DDF.Services.Contract.Infrastructure
{
    public interface ILoginTransactionService
    {
        Task<CookieContainer> Login();
    }
}
