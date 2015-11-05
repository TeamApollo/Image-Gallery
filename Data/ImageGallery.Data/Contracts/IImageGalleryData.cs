namespace ImageGallery.Data.Contracts
{
    using System;
    using Models;

    public interface IImageGalleryData : IDisposable
    {
        IRepository<User> Users { get; }

        IRepository<Album> Albums { get; }

        void SaveChanges();
    }
}
