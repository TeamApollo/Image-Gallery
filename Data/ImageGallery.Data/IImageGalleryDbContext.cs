using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ImageGallery.Models;

namespace ImageGallery.Data
{
    public interface IImageGalleryDbContext
    {
        IDbSet<Album> SoftwareProjects { get; set; }

        int SaveChanges();

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry Entry<T>(T entity) where T : class;

        void Dispose();
    }
}