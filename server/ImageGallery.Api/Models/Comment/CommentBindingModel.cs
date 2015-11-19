namespace ImageGallery.Api.Models.Comment
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;
    using ImageGallery.Models;
    using Infrastructure.Mappings;

    public class CommentBindingModel : IMapFrom<Comment>
    {
        [Required]
        [MaxLength(ValidationConstants.MaxCommentBodyLength)]
        public string Body { get; set; }

        public int? AlbumId { get; set; }

        public int? ImageId { get; set; }
    }
}