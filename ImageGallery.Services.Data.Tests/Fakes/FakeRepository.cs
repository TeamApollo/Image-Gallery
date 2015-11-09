namespace ImageGallery.Services.Data.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ImageGallery.Data.Contracts;

    public class FakeRepository<T> : IRepository<T>
        where T : class
    {
        private List<T> entities = new List<T>();

        public void Add(T entity)
        {
            this.entities.Add(entity);
        }

        public IQueryable<T> All()
        {
            return this.entities.AsQueryable();
        }

        public T Attach(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            var lastEntity = this.entities[this.entities.Count];
            this.entities.Remove(lastEntity);
        }

        public void Delete(T entity)
        {
            this.entities.Remove(entity);
        }

        public void Detach(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
