using DDF.Services.Contract.Persistence;
using DDF.Services.Contract.Usecases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DDF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        private readonly IMetadataTransactionUsecaseService _metadataUsecaseService;
        private readonly IMetadataService _metadataDbService;

        public MetadataController(IMetadataTransactionUsecaseService metadataUsecaseService, IMetadataService metadataDbService)
        {
            _metadataUsecaseService = metadataUsecaseService;
            _metadataDbService = metadataDbService;
        }

        [HttpPost("Import")]
        public async Task<IActionResult> GetMetadata()
        {
            var response = await _metadataUsecaseService.GetAndSaveMetadata().ConfigureAwait(false);
            return Ok(response);
        }

        [HttpGet("{metadataId}")]
        public async Task<IActionResult> GetMetadata(int metadataId)
        {
            var metadata = await _metadataDbService.Find(metadataId).ConfigureAwait(false);
            return Ok(metadata);
        }
    }
}