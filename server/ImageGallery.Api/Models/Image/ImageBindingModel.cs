namespace ImageGallery.Api.Models.Image
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;
    using ImageGallery.Models;
    using Infrastructure.Mappings;

    public class ImageBindingModel : IMapFrom<Image>
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

        public int OriginalWidthInPixels { get; set; }

        public int OriginalHeightInPixels { get; set; }
    }
}