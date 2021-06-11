using System.Xml.Serialization;

namespace DDF.Services.Contract.Models
{
    [XmlRoot(ElementName = "RETS")]
    public class RETS
    {
        [XmlElement(ElementName = "RETS-RESPONSE")]
        public string RETSRESPONSE { get; set; }
        [XmlAttribute(AttributeName = "ReplyCode")]
        public string ReplyCode { get; set; }
        [XmlAttribute(AttributeName = "ReplyText")]
        public string ReplyText { get; set; }
    }
}