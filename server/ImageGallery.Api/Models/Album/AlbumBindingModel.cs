namespace ImageGallery.Api.Models.Album
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;
    using ImageGallery.Models;
    using Infrastructure.Mappings;

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