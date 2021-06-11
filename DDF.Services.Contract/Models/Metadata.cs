using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DDF.Services.Contract.Models
{
    [XmlRoot(ElementName = "Lookup")]
    public class Lookup
    {
        [BsonId]
        [XmlElement(ElementName = "MetadataEntryID")]
        public int MetadataEntryID { get; set; }

        [XmlElement(ElementName = "LookupName")]
        public string LookupName { get; set; }

        [XmlElement(ElementName = "VisibleName")]
        public string VisibleName { get; set; }

        [XmlElement(ElementName = "Version")]
        public string Version { get; set; }

        [XmlElement(ElementName = "Date")]
        public string Date { get; set; }
    }

    [XmlRoot(ElementName = "METADATA-LOOKUP")]
    public class MetadataLookup
    {

        [XmlElement(ElementName = "Lookup")]
        public List<Lookup> Lookup { get; set; }

        [XmlAttribute(AttributeName = "Resource")]
        public string Resource { get; set; }

        [XmlAttribute(AttributeName = "Date")]
        public string Date { get; set; }

        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "METADATA")]
    public class Metadata
    {

        [XmlElement(ElementName = "METADATA-LOOKUP")]
        public MetadataLookup MetadataLookup { get; set; }
    }

    [XmlRoot(ElementName = "RETS")]
    public class MetadataRETS
    {

        [XmlElement(ElementName = "METADATA")]
        public Metadata Metadata { get; set; }

        [XmlAttribute(AttributeName = "ReplyCode")]
        public int ReplyCode { get; set; }

        [XmlAttribute(AttributeName = "ReplyText")]
        public string ReplyText { get; set; }

        [XmlText]
        public string Text { get; set; }
    }


}