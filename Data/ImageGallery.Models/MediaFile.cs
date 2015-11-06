namespace ImageGallery.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MediaFiles")]
    public abstract class MediaFile : FileInfo
    {
        private ICollection<Album> albums;
        private ICollection<Tag> tags;
        private ICollection<Comment> comments;

        protected MediaFile()
        {
            this.albums = new HashSet<Album>();
            this.tags = new HashSet<Tag>();
            this.comments = new HashSet<Comment>();
        }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
