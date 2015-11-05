namespace ImageGallery.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface IAlbumsService
    {
        IQueryable<Album> All();

        int Add(Album album, string creatorName);
    }
}