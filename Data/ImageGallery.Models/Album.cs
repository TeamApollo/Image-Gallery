namespace ImageGallery.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class Album
    {
        public Album()
        {
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.AlbumNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.AlbumDescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Private { get; set; }

        public int UserId { get; set; }

        public virtual User Owner { get; set; }
    }
}