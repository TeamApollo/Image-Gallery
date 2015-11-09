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

        public IQueryable<Album> All()
        {
            return this.data.Albums
                .All()
                .OrderByDescending(p => p.CreatedOn);
        }

        public IQueryable<Album> GetById(int id, string currentUserName)
        {
            var album = this.All()
                .Where(
                    p => p.Id == id &&
                    (!p.Private ||
                        (p.Private &&
                        p.Owner.UserName == currentUserName)));

            return album;
        }
    }
}