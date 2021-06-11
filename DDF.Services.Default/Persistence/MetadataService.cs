using DDF.Common.GeneralSettings;
using DDF.Services.Contract.Models;
using DDF.Services.Contract.Persistence;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDF.Services.Default.Persistence
{
    public class MetadataService : IMetadataService
    {
        private readonly IMongoCollection<Lookup> _metadataCollection;

        public MetadataService(IOptions<MongodbSettings> options)
        {
            var mongodbSetting = options.Value;
            var client = new MongoClient(mongodbSetting.ConnectionString);
            var db = client.GetDatabase(mongodbSetting.DatabaseName);
            _metadataCollection = db.GetCollection<Lookup>("Metadata");
        }

        public async Task<Lookup> Insert(Lookup lookup)
        {
            var builder = new FilterDefinitionBuilder<Lookup>();
            var filter = builder.Where(exp => exp.MetadataEntryID == lookup.MetadataEntryID);
            await _metadataCollection.ReplaceOneAsync(filter, lookup, new ReplaceOptions { IsUpsert = true }).ConfigureAwait(false);
            return lookup;
        }

        public async Task<Lookup> Find(int id)
        {
            var metadata = await _metadataCollection.FindAsync(exp => exp.MetadataEntryID == id);
            return metadata?.SingleOrDefault() ?? null;
        }

        public async Task<IList<Lookup>> Insert(IList<Lookup> lookups)
        {
            var bulk = new List<WriteModel<Lookup>>();
            foreach (var item in lookups)
            {
                var builder = new FilterDefinitionBuilder<Lookup>();
                var filter = builder.Where(exp => exp.MetadataEntryID == item.MetadataEntryID);
                bulk.Add(new ReplaceOneModel<Lookup>(filter, item) { IsUpsert = true });
            }
            await _metadataCollection.BulkWriteAsync(bulk).ConfigureAwait(false);
            return lookups;
        }
    }
}