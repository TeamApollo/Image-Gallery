namespace ImageGallery.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Images")]
    public class Image : MediaFile
    {
        public int OriginalWidthInPixels { get; set; }

        public int OriginalHeightInPixels { get; set; }
    }
}
