using System.ComponentModel.DataAnnotations;

namespace DDF.Common.Enum
{
    // Note: The metadata ID for METADATA-SYSTEM and METADATA-RESOURCE must be 0 or *.
    // Note: METADATA-LOOKUP and METADATA-LOOKUP_TYPE requests only support STANDARD-XML format
    public enum MetadataTypes : byte
    {
        [Display(Name = "METADATA-SYSTEM")]
        METADATA_SYSTEM = 1,
        [Display(Name = "METADATA-RESOURCE")]
        METADATA_RESOURCE,
        [Display(Name = "METADATA-CLASS")]
        METADATA_CLASS,
        [Display(Name = "METADATA-LOOKUP")]
        METADATA_LOOKUP,
        [Display(Name = "METADATA-LOOKUP-TYPE")]
        METADATA_LOOKUP_TYPE,
        [Display(Name = "METADATA-TABLE")]
        METADATA_TABLE
    }
}