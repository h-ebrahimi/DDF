using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DDF.Services.Contract.Models
{
    public class Metadata
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public long ID { get; set; }
        public DateTime LastUpdated { get; set; }
        public long ListingID { get; set; }
    }

    public class MetadataRequestArgument
    {
        public string Type { get; set; }
        public string Format { get; set; }
        public string ID { get; set; }
        public string Culture { get; set; }
    }
}