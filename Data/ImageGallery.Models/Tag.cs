namespace ImageGallery.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class Tag
    {
        private ICollection<MediaFile> mediaFiles;
        private ICollection<Album> albums;

        public Tag()
        {
            this.mediaFiles = new HashSet<MediaFile>();
            this.albums = new HashSet<Album>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxTagDescriptionLength)]
        public string Description { get; set; }

        public virtual ICollection<MediaFile> MediaFiles
        {
            get { return this.mediaFiles; }
            set { this.mediaFiles = value; }
        }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }
    }
}
