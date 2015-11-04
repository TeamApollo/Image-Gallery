using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using ImageGallery.Models;

namespace ImageGallery.Data
{
    public class ImageGalleryDbContext : IdentityDbContext<User>, IImageGalleryDbContext
    {
        public ImageGalleryDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ImageGalleryDbContext Create()
        {
            return new ImageGalleryDbContext();
        }

        IDbSet<T> IImageGalleryDbContext.Set<T>()
        {
            return base.Set<T>();
        }

        DbEntityEntry IImageGalleryDbContext.Entry<T>(T entity)
        {
            return base.Entry<T>(entity);
        }

        public virtual IDbSet<Album> SoftwareProjects { get; set; }


    }
}
