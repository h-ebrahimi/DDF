namespace DDF.Common.GeneralSettings
{
    public class DDFSettings
    {
        public string BaseUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DDFServiceSettings LoginSettings { get; set; }
        public MetaDataServiceSettings MetaDataSettings { get; set; }
    }

    public class DDFServiceSettings
    {
        public string ServiceUrl { get; set; }
    }

    public class MetaDataServiceSettings : DDFServiceSettings
    {
        public string Type { get; set; }
        public string Format { get; set;}
        public string Id { get; set; }
    }
}