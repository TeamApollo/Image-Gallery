namespace ImageGallery.Api.Models.MediaFile
{
    using System.ComponentModel.DataAnnotations;
    using ImageGallery.Common.Constants;
    using Infrastructure.Mappings;
    using ImageGallery.Models;

    public class MediaFileBindingModel : IMapFrom<MediaFile>
    {
        [Required]
        [MaxLength(ValidationConstants.MaxOriginalFileNameLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxFileExtensionLength)]
        public string FileExtension { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxUrlPathLength)]
        public string UrlPath { get; set; }
    }
}