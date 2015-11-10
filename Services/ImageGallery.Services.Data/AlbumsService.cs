namespace ImageGallery.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using ImageGallery.Data.Contracts;
    using Models;

    public class AlbumsService : IAlbumsService
    {
        private readonly IImageGalleryData data;

        public AlbumsService(IImageGalleryData data)
        {
            this.data = data;
        }

        /// <summary>
        /// Gets all non-private albums.
        /// </summary>
        /// <returns>All found non-private albums</returns>
        public IQueryable<Album> GetAll(string username)
        {
            return this.data.Albums
                .All()
                .Where(p => !p.Private
                    || (p.Private && p.Owner.UserName == username))
                .OrderByDescending(p => p.CreatedOn);
        }

        /// <summary>
        /// Gets the album with the provided id if it is not private or if it is private and owned by the requesting user.
        /// </summary>
        /// <param name="id">The id of the album to get.</param>
        /// <param name="currentUserName">The requesting user username (email).</param>
        /// <returns>Found album or null if not found.</returns>
        public IQueryable<Album> GetById(int id, string currentUserName)
        {
            var album = this.GetAll(currentUserName)
                .Where(p => p.Id == id);

            return album;
        }

        /// <summary>
        /// Adds a new album to the database.
        /// </summary>
        /// <param name="creatorName">The username(email) of the requesting user.</param>
        /// <returns>The id of the created album.</returns>
        public int Add(Album newAlbum, string creatorName)
        {
            if (creatorName == null)
            {
                throw new ArgumentNullException("Creator name must be specified.");
            }

            if (newAlbum == null)
            {
                throw new ArgumentNullException("Album cannot be null.");
            }

            var currentUser = this.data.Users
                .All()
                .FirstOrDefault(u => u.UserName == creatorName);

            if (currentUser == null)
            {
                throw new ArgumentException("No user with this username found.");
            }

            newAlbum.CreatedOn = DateTime.Now;
            newAlbum.Owner = currentUser;

            this.data.Albums.Add(newAlbum);
            this.data.SaveChanges();

            return newAlbum.Id;
        }

        /// <summary>
        /// Gets all albums that are owned by the provided username and are not private. If the authorized user is also the owner gets the private albums as well.
        /// </summary>
        /// <param name="ownerUsername">The owner of the albums to get.</param>
        /// <param name="authorizedUsername">The requesting authorized user username.</param>
        /// <returns></returns>
        public IQueryable<Album> GetAlbumsByUser(string ownerUsername, string authorizedUsername)
        {
            var albums = this.GetAll(authorizedUsername)
                .Where(a => a.Owner.UserName == ownerUsername);

            return albums;
        }

        /// <summary>
        /// Deletes the album with the specified id. If no album with this id returns -1.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>Returns the id of the deleted album or -1 if no album with such id was found.</returns>
        public int DeleteAlbumById(int id, string username)
        {
            var album = this.data.Albums.All().FirstOrDefault(a => a.Id == id && a.Owner.UserName == username);

            if (album == null)
            {
                return -1;
            }

            this.data.Albums.Delete(album);
            this.data.SaveChanges();
            return album.Id;
        }
    }
}