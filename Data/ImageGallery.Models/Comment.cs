namespace ImageGallery.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxCommentBodyLength)]
        public string Body { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int? AlbumId { get; set; }

        public virtual Album Album { get; set; }

        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }
    }
}
