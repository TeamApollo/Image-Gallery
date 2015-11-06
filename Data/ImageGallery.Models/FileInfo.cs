namespace ImageGallery.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public abstract class FileInfo
    {
        public int Id { get; set; }

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
