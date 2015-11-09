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
        public IQueryable<Album> GetAll()
        {
            return this.data.Albums
                .All()
                .Where(p => !p.Private)
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
            var album = this.data.Albums
                .All()
                .Where(p => !p.Private
                    || (p.Private && p.Owner.UserName == currentUserName))
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
            newAlbum.CreatedOn = DateTime.Now;
            var currentUser = this.data.Users
                .All()
                .FirstOrDefault(u => u.UserName == creatorName);

            newAlbum.Owner = currentUser;

            this.data.Albums.Add(newAlbum);
            this.data.SaveChanges();

            return newAlbum.Id;
        }
    }
}