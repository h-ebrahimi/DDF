using DDF.Common.GeneralSettings;
using DDF.Services.Contract.Infrastructure;
using DDF.Services.Contract.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace DDF.Services.Default.Infrastructure
{
    public class MetadataTransactionService : IMetadataTransactionService
    {
        private readonly HttpClient _client;
        private readonly DDFSettings _setting;
        public MetadataTransactionService(IHttpClientFactory httpClientFactory, IOptions<DDFSettings> setting)
        {
            _client = httpClientFactory.CreateClient("DDF");
            _setting = setting.Value;
        }

        public async Task<Metadata> GetMetadata()
        {
            return null;
        }
    }
}