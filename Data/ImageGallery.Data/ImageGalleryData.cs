namespace ImageGallery.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Models;

    public class ImageGalleryData : IImageGalleryData
    {
        private readonly IImageGalleryDbContext context;
        private readonly IDictionary<Type, object> repositories = new Dictionary<Type, object>();

        public ImageGalleryData(IImageGalleryDbContext context)
        {
            this.context = context;
        }

        public IRepository<User> Users => this.GetRepository<User>();

        public IRepository<Image> Images => this.GetRepository<Image>();

        public IRepository<Album> Albums => this.GetRepository<Album>();

        public IRepository<Tag> Tags => this.GetRepository<Tag>();

        public IRepository<Comment> Comments => this.GetRepository<Comment>();

        public void SaveChanges()
        {
            this.context.SaveChanges();
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
    }
}