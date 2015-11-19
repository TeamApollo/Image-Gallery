namespace ImageGallery.Services.Data
{
    using System.Linq;
    using Common;
    using ImageGallery.Data.Contracts;
    using Contracts;
    using Models;

    public class ImagesService : IImagesService
    {
        private readonly IImageGalleryData data;

        public ImagesService(IImageGalleryData data)
        {
            this.data = data;
        }

        /// <summary>
        /// Adds a new media file to the database.
        /// </summary>
        /// <param name="mediaFile">The new media file to be added.</param>
        /// <param name="albumId">The id of the album the media file to be added to.</param>
        /// <param name="username">The username of the user adding the media file.</param>
        /// <returns>The id of the created media file.</returns>
        public int Add(Image mediaFile, int albumId, string username)
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

        /// <summary>
        /// Deletes the media file with the provided id.
        /// </summary>
        /// <param name="mediaFile">The media file to be deleted.</param>
        /// <param name="albumId">The id of the media file to be deleted from.</param>
        /// <param name="username">The username of the user deleting the media file.</param>
        /// <returns>The id of the deleted media file or -1 if no item with such id is found.</returns>
        public int Delete(int albumId, int mediaFileId, string username)
        {
            Validator.ValidateObjectIsNotNull(username);

            var album = this.data.Albums.GetById(albumId);

            var mediaFile = this.GetById(albumId, mediaFileId, username).FirstOrDefault();

            Validator.ValidateObjectIsNotNull(mediaFile);
            Validator.ValidateObjectIsNotNull(album);

            if (album.Owner.UserName != username)
            {
                throw new ImageGalleryException("The User cannot delete media file from foreign album!");
            }

            album.MediaFiles.Remove(mediaFile);

            return mediaFile.Id;
        }

        /// <summary>
        /// Gets all media files.
        /// </summary>
        /// <param name="id">The id of the album's files to get.</param>
        /// <param name="username">The username of the user getting the album files.</param>
        /// <returns>All found media files.</returns>
        public IQueryable<Image> GetAll(int albumId, string username)
        {
            var album = this.data.Albums.GetById(albumId);

            Validator.ValidateObjectIsNotNull(album);

            if (album.Private && album.Owner.UserName != username)
            {
                throw new ImageGalleryException("Access Denied!");
            }

            var mediaFiles = album.MediaFiles.AsQueryable();

            return mediaFiles;
        }

        /// <summary>
        /// Gets the media file with the provided id.
        /// </summary>
        /// <param name="id">The id of the media file to get.</param>
        /// <param name="username">The username of the user getting the media file.</param>
        /// <returns>Found media file or null if not found.</returns>
        public IQueryable<Image> GetById(int albumId, int mediaFileId, string username)
        {
            Validator.ValidateObjectIsNotNull(username);
            var currentUser = this.data.Users
                .All()
                .FirstOrDefault(u => u.UserName == username);

            Validator.ValidateObjectIsNotNull(currentUser);

            return this.GetAll(albumId, username).Where(a => a.Id == mediaFileId);
        }
    }
}
