namespace ImageGallery.Api.Models.Video
{
    using System.ComponentModel.DataAnnotations;    
    using Common.Constants;
    using ImageGallery.Api.Infrastructure.Mappings;
    using ImageGallery.Models;  

    public class VideoBindingModel : IMapFrom<Video>
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

        public int LengthInSeconds { get; set; }
    }
}