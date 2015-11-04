namespace ImageGallery.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using Models;
    using ImageGallery.Data;

    public class AlbumsService : IAlbumsService
    {
        private readonly IRepository<Album> albums;
        private readonly IRepository<User> users;

        public AlbumsService(IRepository<Album> albumsRepo, IRepository<User> usersRepo)
        {
            this.albums = albumsRepo;
            this.users = usersRepo;
        }

        public int Add(Album newAlbum, string creatorName)
        {
            newAlbum.CreatedOn = DateTime.Now;
            var currentUser = this.users
                .All()
                .FirstOrDefault(u => u.UserName == creatorName);

            newAlbum.Owner = currentUser;

            this.albums.Add(newAlbum);
            this.albums.SaveChanges();

            return newAlbum.Id;
        }

        public IQueryable<Album> All()
        {
            return this.albums
                .All()
                .OrderByDescending(p => p.CreatedOn);
        }
    }
}