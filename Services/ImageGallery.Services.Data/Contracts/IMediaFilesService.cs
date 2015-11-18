namespace ImageGallery.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface IMediaFilesService
    {
        IQueryable<MediaFile> GetAll(int albumId, string username);

        IQueryable<MediaFile> GetById(int albumId, int mediaFileId, string username);

        int Add(MediaFile mediaFile, int albumId, string username);

        int Delete(int albumId, int mediaFileId, string username);
    }
}
