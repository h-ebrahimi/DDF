using DDF.Services.Contract.Infrastructure;
using DDF.Services.Contract.Models;
using DDF.Services.Contract.Persistence;
using DDF.Services.Contract.Usecases;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDF.Services.Default.Usecases
{
    public class MetadataTransactionUsecaseService : IMetadataTransactionUsecaseService
    {
        private readonly IMetadataTransactionService _metadataService;
        private readonly IMetadataService _metadataDbService;

        public MetadataTransactionUsecaseService(IMetadataTransactionService metadataService, IMetadataService metadataDbService)
        {
            _metadataService = metadataService;
            _metadataDbService = metadataDbService;
        }

        public async Task<IList<Lookup>> GetAndSaveMetadata()
        {
            var lstMetadata = await _metadataService.GetMetadata().ConfigureAwait(false);
            if (lstMetadata == null) return lstMetadata;

            //foreach (var item in lstMetadata)
            //{
            //    var isExist = _metadataDbService.Find(item.MetadataEntryID);
            //    await _metadataDbService.Insert(item).ConfigureAwait(false);
            //}

            await _metadataDbService.Insert(lstMetadata).ConfigureAwait(false);

            return lstMetadata;
        }
    }
}