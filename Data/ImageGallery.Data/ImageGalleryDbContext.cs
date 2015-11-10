namespace ImageGallery.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class ImageGalleryDbContext : IdentityDbContext<User>, IImageGalleryDbContext
    {
        public ImageGalleryDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Album> Albums { get; set; }

        public virtual IDbSet<MediaFile> MediaFiles { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<Video> Videos { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public static ImageGalleryDbContext Create()
        {
            return new ImageGalleryDbContext();
        }

        IDbSet<T> IImageGalleryDbContext.Set<T>()
        {
            return this.Set<T>();
        }

        DbEntityEntry IImageGalleryDbContext.Entry<T>(T entity)
        {
            return this.Entry<T>(entity);
        }
    }
}
