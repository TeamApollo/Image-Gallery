namespace ImageGallery.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface IImagesService
    {
        IQueryable<Image> GetAll(int albumId, string username);

        IQueryable<Image> GetById(int albumId, int mediaFileId, string username);

        int Add(Image mediaFile, int albumId, string username);

        int Delete(int albumId, int mediaFileId, string username);
    }
}
