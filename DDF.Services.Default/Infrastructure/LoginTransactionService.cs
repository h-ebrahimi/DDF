using DDF.Common.GeneralSettings;
using DDF.Services.Contract.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DDF.Services.Default.Infrastructure
{
    public class LoginTransactionService : ILoginTransactionService
    {
        private readonly DDFSettings _setting;

        public LoginTransactionService(IOptions<DDFSettings> setting)
        {
            _setting = setting.Value;
        }

        public async Task<string> Login()
        {
            var url = new Uri(new Uri(_setting.BaseUrl), _setting.LoginSettings.ServiceUrl);
            
            using var handler = new HttpClientHandler();
            var credCache = new CredentialCache();
            credCache.Add(new Uri(_setting.BaseUrl), "Digest", new NetworkCredential(_setting.LoginSettings.Username, _setting.LoginSettings.Password, _setting.BaseUrl));
            handler.Credentials = credCache;

            var cookie = new CookieContainer();
            handler.CookieContainer = cookie;

            using var client = new HttpClient(handler);
            var response = await client.GetAsync(url);
            var headers = response.Headers;
            var result = await response.Content.ReadAsStringAsync();
            
            return result;
        }
    }
}