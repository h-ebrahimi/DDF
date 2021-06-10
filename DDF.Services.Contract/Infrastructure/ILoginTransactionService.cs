using System.Threading.Tasks;

namespace DDF.Services.Contract.Infrastructure
{
    public interface ILoginTransactionService
    {
        Task<string> Login();
    }
}
