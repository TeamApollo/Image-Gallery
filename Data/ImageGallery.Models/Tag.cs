namespace ImageGallery.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class Tag
    {
        private ICollection<Image> images;
        private ICollection<Album> albums;

        public Tag()
        {
            this.images = new HashSet<Image>();
            this.albums = new HashSet<Album>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxTagDescriptionLength)]
        public string Description { get; set; }

        public virtual ICollection<Image> MediaFiles
        {
            get { return this.images; }
            set { this.images = value; }
        }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }
    }
}
