namespace ImageGallery.Api.Models.Image
{
    using ImageGallery.Api.Infrastructure.Mappings;
    using ImageGallery.Models;

    public class ImageViewModel : IMapFrom<Image>
    {
        public int Id { get; set; }

        public string UrlPath { get; set; }
    }
}