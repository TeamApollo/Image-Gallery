namespace ImageGallery.Api.Models.Album
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Common.Constants;
    using Infrastructure.Mappings;
    using ImageGallery.Models;

    public class AlbumsBindingModel : IMapFrom<Album>
    {
        public bool Private { get; set; }

        [Required]
        [MaxLength(ValidationConstants.AlbumNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.AlbumDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
