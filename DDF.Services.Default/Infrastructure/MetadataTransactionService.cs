using DDF.Common.GeneralSettings;
using DDF.Services.Contract.Infrastructure;
using DDF.Services.Contract.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDF.Services.Default.Infrastructure
{
    public class MetadataTransactionService : IMetadataTransactionService
    {
        private readonly DDFSettings _setting;
        private readonly ILogger _logger;
        private readonly ILoginTransactionService _loginService;

        public MetadataTransactionService(IOptions<DDFSettings> setting, ILogger<MetadataTransactionService> logger, ILoginTransactionService loginService)
        {
            _setting = setting.Value;
            _logger = logger;
            _loginService = loginService;
        }

        public async Task<IList<Lookup>> GetMetadata()
        {
            try
            {
                var url = $"{_setting.BaseUrl}{_setting.MetaDataSettings.ServiceUrl}?Type={_setting.MetaDataSettings.Type}&Format={_setting.MetaDataSettings.Format}&ID={_setting.MetaDataSettings.Id}";

                var cookieContainer = await _loginService.Login().ConfigureAwait(false);

                using var handler = new HttpClientHandler();
                var credCache = new CredentialCache();
                credCache.Add(new Uri(_setting.BaseUrl), "Digest", new NetworkCredential(_setting.Username, _setting.Password, _setting.BaseUrl));
                handler.Credentials = credCache;
                handler.CookieContainer = cookieContainer;

                using var client = new HttpClient(handler);
                
                var response = await client.GetAsync(url).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"GetMetadata has'nt SuccessStatusCode at {DateTime.Now} StatusCode:{response.StatusCode} url {url}.");
                    return null;
                }

                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var serializer = new XmlSerializer(typeof(MetadataRETS));
                var rets = (MetadataRETS)serializer.Deserialize(stream);

                if (!((rets?.ReplyCode ?? 0) == 0))
                {
                    _logger.LogWarning($"GetMetadata has other ReplyCode at {DateTime.Now} url {url} response {await response.Content.ReadAsStringAsync().ConfigureAwait(false)}.");
                    return null;
                }

                return rets.Metadata.MetadataLookup.Lookup;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, $"GetMetadata has error at {DateTime.Now} exception {exp} .");
                return null;
            }
        }
    }
}