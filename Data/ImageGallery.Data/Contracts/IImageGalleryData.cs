namespace ImageGallery.Data.Contracts
{
    using System;
    using Models;

    public interface IImageGalleryData : IDisposable
    {
        IRepository<User> Users { get; }

        IRepository<Album> Albums { get; }

        IRepository<Image> Images { get; }

        IRepository<Video> Videos { get; }

        IRepository<Tag> Tags { get; }

        IRepository<Comment> Comments { get; }

        void SaveChanges();
    }
}
