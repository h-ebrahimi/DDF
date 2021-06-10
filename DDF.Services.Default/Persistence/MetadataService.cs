using DDF.Common.GeneralSettings;
using DDF.Services.Contract.Models;
using DDF.Services.Contract.Persistence;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DDF.Services.Default.Persistence
{
    public class MetadataService : IMetadataService
    {
        private readonly IMongoCollection<Metadata> _metadataCollection;

        public MetadataService(IOptions<MongodbSettings> options)
        {
            var mongodbSetting = options.Value;
            var client = new MongoClient(mongodbSetting.ConnectionString);
            var db = client.GetDatabase(mongodbSetting.DatabaseName);
            _metadataCollection = db.GetCollection<Metadata>("Metadata");
        }

        public async Task<Metadata> Create(Metadata metadata)
        {
            await _metadataCollection.InsertOneAsync(metadata);
            return metadata;
        }

        public async Task<Metadata> Find(long id)
        {
            var metadata = await _metadataCollection.FindAsync(exp => exp.ID == id);
            return metadata?.SingleOrDefault() ?? null;
        }
    }
}