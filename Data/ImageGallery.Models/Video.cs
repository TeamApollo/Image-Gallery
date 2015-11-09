namespace ImageGallery.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Videos")]
    public class Video : MediaFile
    {
        public int LengthInSeconds { get; set; }
    }
}
