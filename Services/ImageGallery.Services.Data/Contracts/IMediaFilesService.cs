namespace ImageGallery.Services.Data.Contracts
{      
    using System.Collections.Generic;
    using Models;  

    public interface IMediaFilesService
    {
        ICollection<MediaFile> GetAll(int albumId, string username);

        MediaFile GetById(int id, string username);

        int Add(MediaFile mediaFile, int albumId, string username);

        void Delete(MediaFile mediaFile, int albumId, string username);
    }
}
