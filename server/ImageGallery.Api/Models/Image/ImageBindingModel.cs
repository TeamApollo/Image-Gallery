﻿namespace ImageGallery.Api.Models.Image
{
    using System.ComponentModel.DataAnnotations; 
    using ImageGallery.Common.Constants;   
    using ImageGallery.Models;
    using Infrastructure.Mappings;

    public class ImageBindingModel : IMapFrom<Image>
    {
        [Required]
        public int AlbumId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxOriginalFileNameLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxUrlPathLength)]
        public string UrlPath { get; set; }
    }
}