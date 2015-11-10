namespace ImageGallery.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Common; 
    using ImageGallery.Data.Contracts;
    using ImageGallery.Services.Data.Contracts;
    using Models;

    public class MediaFilesService : IMediaFilesService
    {
        private readonly IImageGalleryData data;

        public MediaFilesService(IImageGalleryData data)
        {
            this.data = data;
        }

        public int Add(MediaFile mediaFile, int albumId, string username)
        {
            Validator.ValidateObjectIsNotNull(mediaFile);
            Validator.ValidateObjectIsNotNull(username);

            var album = this.data.Albums.GetById(albumId);

            Validator.ValidateObjectIsNotNull(album);

            if (album.Owner.UserName != username)
            {
                throw new ImageGalleryException("The User cannot add media file to foreign album!");
            }

            album.MediaFiles.Add(mediaFile);

            return mediaFile.Id;
        }  
       
        public void Delete(MediaFile mediaFile, int albumId, string username)
        {
            Validator.ValidateObjectIsNotNull(mediaFile);
            Validator.ValidateObjectIsNotNull(username);

            var album = this.data.Albums.GetById(albumId);

            Validator.ValidateObjectIsNotNull(album);

            if (album.Owner.UserName != username)
            {
                throw new ImageGalleryException("The User cannot delete media file from foreign album!");
            }

            album.MediaFiles.Remove(mediaFile);
        }

        public ICollection<MediaFile> GetAll(int albumId, string username)
        {
            Validator.ValidateObjectIsNotNull(username);

            var album = this.data.Albums.GetById(albumId);

            Validator.ValidateObjectIsNotNull(album);

            if (album.Private && album.Owner.UserName != username)
            {
                throw new ImageGalleryException("Access Denied!");
            }

            return album.MediaFiles;
        }

        public MediaFile GetById(int id, string username)
        {
            Validator.ValidateObjectIsNotNull(username);
            var currentUser = this.data.Users.All()
                .Where(u => u.UserName == username)
                .FirstOrDefault();

            Validator.ValidateObjectIsNotNull(currentUser);

            return currentUser.Albums.Select(al => al.MediaFiles.FirstOrDefault(mf => mf.Id == id)).FirstOrDefault();
        }
    }
}
