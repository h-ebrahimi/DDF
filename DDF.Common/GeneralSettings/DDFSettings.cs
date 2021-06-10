namespace DDF.Common.GeneralSettings
{
    public class DDFSettings
    {
        public string BaseUrl { get; set; }
        public DDFLoginServiceSettings LoginSettings { get; set; }
        public DDFServiceSettings MetaDataSettings { get; set; }
    }

    public class DDFServiceSettings
    {
        public string ServiceUrl { get; set; }
    }

    public class DDFLoginServiceSettings : DDFServiceSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}