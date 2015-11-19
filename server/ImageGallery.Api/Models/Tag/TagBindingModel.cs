namespace ImageGallery.Api.Models.Tag
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;
    using ImageGallery.Models;
    using Infrastructure.Mappings;

    public class TagsBindingModel : IMapFrom<Tag>
    {
        [Required]
        public int AlbumId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxTagDescriptionLength)]
        public string Description { get; set; }
    }
}