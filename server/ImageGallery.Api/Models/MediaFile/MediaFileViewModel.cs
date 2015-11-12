namespace ImageGallery.Api.Models.MediaFile
{
    using ImageGallery.Api.Infrastructure.Mappings;
    using ImageGallery.Models;

    public class MediaFileViewModel : IMapFrom<MediaFile>
    {
        public int Id { get; set; }

        public string UrlPath { get; set; }
    }
}