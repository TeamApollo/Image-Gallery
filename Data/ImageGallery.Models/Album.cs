namespace ImageGallery.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class Album
    {
        private ICollection<Image> mediaFiles;
        private ICollection<Tag> tags;
        private ICollection<Comment> comments;

        public Album()
        {
            this.mediaFiles = new HashSet<Image>();
            this.tags = new HashSet<Tag>();
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.AlbumNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.AlbumDescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Private { get; set; }
        
        [Required]
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<Image> MediaFiles
        {
            get { return this.mediaFiles; }
            set { this.mediaFiles = value; }
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