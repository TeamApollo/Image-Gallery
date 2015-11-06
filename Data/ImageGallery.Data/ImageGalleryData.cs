namespace ImageGallery.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Models;

    public class ImageGalleryData : IImageGalleryData
    {
        private readonly IImageGalleryDbContext context;
        private readonly IDictionary<Type, object> repositories;

        private bool disposed;

        public ImageGalleryData(IImageGalleryDbContext context, IDictionary<Type, object> repositories)
        {
            this.context = context;
            this.repositories = repositories;
        }

        public IRepository<User> Users => this.GetRepository<User>();

        public IRepository<Album> Albums => this.GetRepository<Album>();

        public IRepository<Image> Images => this.GetRepository<Image>();

        public IRepository<Video> Videos => this.GetRepository<Video>();

        public IRepository<Tag> Tags => this.GetRepository<Tag>();

        public IRepository<Comment> Comments => this.GetRepository<Comment>();

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}