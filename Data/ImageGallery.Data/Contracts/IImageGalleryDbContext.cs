namespace ImageGallery.Data.Contracts
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Models;

    public interface IImageGalleryDbContext
    {
        IDbSet<Album> Albums { get; set; }

        IDbSet<Image> Images { get; set; }

        IDbSet<Video> Videos { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry Entry<T>(T entity) where T : class;

        int SaveChanges();

        void Dispose();
    }
}