using DDF.Common.GeneralSettings;
using DDF.Services.Contract.Infrastructure;
using DDF.Services.Contract.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDF.Services.Default.Infrastructure
{
    public class LoginTransactionService : ILoginTransactionService
    {
        private readonly DDFSettings _setting;
        private readonly ILogger _logger;

        public LoginTransactionService(IOptions<DDFSettings> setting, ILogger<LoginTransactionService> logger)
        {
            _setting = setting.Value;
            _logger = logger;
        }

        public async Task<CookieContainer> Login()
        {
            try
            {
                var url = $"{_setting.BaseUrl}{_setting.LoginSettings.ServiceUrl}";

                var cookies = new CookieContainer();

                using var handler = new HttpClientHandler();
                var credCache = new CredentialCache();
                credCache.Add(new Uri(_setting.BaseUrl), "Digest", new NetworkCredential(_setting.Username, _setting.Password, _setting.BaseUrl));
                handler.Credentials = credCache;
                handler.CookieContainer = cookies;

                using var client = new HttpClient(handler);
                var response = await client.GetAsync(url).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode) return null;

                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var serializer = new XmlSerializer(typeof(RETS));
                var rets = (RETS)serializer.Deserialize(stream);

                if (!rets?.ReplyCode?.Equals("0", StringComparison.OrdinalIgnoreCase) ?? true) return null;

                var cookie = response.Headers.SingleOrDefault(header => header.Key.Equals("Set-Cookie", StringComparison.Ordinal)).Value?.FirstOrDefault();

                cookies.Add(new Cookie("X-SESSIONID", GetSessionId(cookie), "/", "data.crea.ca"));

                return cookies;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, $"Login has error at {DateTime.Now} exception {exp} .");
                return null;
            }
        }

        public string GetSessionId(string cookie)
        {
            var arrCookie = cookie.Split(";");
            foreach(var item in arrCookie)
            {
                var splItem = item.Split("=");
                if (splItem[0].Equals("X-SESSIONID"))
                    return splItem[1];
            }
            return string.Empty;
        }
    }
}