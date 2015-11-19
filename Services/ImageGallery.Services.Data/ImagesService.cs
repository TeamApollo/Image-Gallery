namespace ImageGallery.Services.Data
{
    using System.Collections.Generic;
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
        /// <param name="image">The new media file to be added.</param>
        /// <param name="albumId">The id of the album the media file to be added to.</param>
        /// <param name="username">The username of the user adding the media file.</param>
        /// <returns>The id of the created media file.</returns>
        public int Add(Image image, string username)
        {
            Validator.ValidateObjectIsNotNull(image);
            Validator.ValidateObjectIsNotNull(username);

            var album = this.data.Albums.GetById(image.AlbumId);

            Validator.ValidateObjectIsNotNull(album);

            if (album.Owner.UserName != username)
            {
                throw new ImageGalleryException("The User cannot add media file to foreign album!");
            }

            album.Images.Add(image);
            this.data.SaveChanges();

            return image.Id;
        }

        /// <summary>
        /// Deletes the media file with the provided id.
        /// </summary>
        /// <param name="mediaFile">The media file to be deleted.</param>
        /// <param name="albumId">The id of the media file to be deleted from.</param>
        /// <param name="username">The username of the user deleting the media file.</param>
        /// <returns>The id of the deleted media file or -1 if no item with such id is found.</returns>
        public int Delete(int albumId, int imageId, string username)
        {
            Validator.ValidateObjectIsNotNull(username);

            var album = this.data.Albums.GetById(albumId);

            var image = this.GetById(imageId, username);

            Validator.ValidateObjectIsNotNull(image);
            Validator.ValidateObjectIsNotNull(album);

            if (album.Owner.UserName != username)
            {
                throw new ImageGalleryException("The User cannot delete media file from foreign album!");
            }

            album.Images.Remove(image.First());

            return image.First().Id;
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

            var mediaFiles = album.Images.AsQueryable();

            return mediaFiles;
        }

        /// <summary>
        /// Gets the media file with the provided id.
        /// </summary>
        /// <param name="id">The id of the media file to get.</param>
        /// <param name="username">The username of the user getting the media file.</param>
        /// <returns>Found media file or null if not found.</returns>
        public IQueryable<Image> GetById(int imageId, string username)
        {
            var image = this.data.Images.GetById(imageId);
            Validator.ValidateObjectIsNotNull(image);

            var album = image.Album;
            if (album.Private && username != album.Owner.UserName)
            {
                throw new ImageGalleryException("The user does not have access to this image");
            }

            return new EnumerableQuery<Image>(new List<Image> {image});
        }
    }
}
