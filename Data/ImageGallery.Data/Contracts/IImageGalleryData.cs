namespace ImageGallery.Data.Contracts
{
    using Models;

    public interface IImageGalleryData
    {
        IRepository<User> Users { get; }

        IRepository<Image> Images { get; }

        IRepository<Album> Albums { get; }

        IRepository<Tag> Tags { get; }

        IRepository<Comment> Comments { get; }

        void SaveChanges();
    }
}
