namespace ImageGallery.Api.Models.Image
{
    using ImageGallery.Models;
    using Infrastructure.Mappings;

    public class ImageViewModel : IMapFrom<Image>
    {
        public string UrlPath { get; set; }

        public int OriginalWidthInPixels { get; set; }

        public int OriginalHeightInPixels { get; set; }
    }
}