namespace ImageGallery.Services.Data.Contracts
{
    using System.Linq;
    using Common.Constants;
    using Models;

    public interface IAlbumsService
    {
        IQueryable<Album> All();

        int Add(Album album, string creatorName);
    }
}