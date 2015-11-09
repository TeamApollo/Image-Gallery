namespace ImageGallery.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface IAlbumsService
    {
        IQueryable<Album> GetAll(string username);

        int Add(Album album, string username);

        IQueryable<Album> GetById(int id, string username);

        IQueryable<Album> GetAlbumsByUser(string username, string authorizedUsername);

        int DeleteAlbumById(int id, string authorizedUser);
    }
}