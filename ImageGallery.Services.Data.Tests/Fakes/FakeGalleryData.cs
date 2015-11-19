namespace ImageGallery.Services.Data.Tests.Fakes
{
    using System;
    using ImageGallery.Data.Contracts;
    using Models;

    public class FakeGalleryData : IImageGalleryData
    {
        private FakeRepository<Tag> tags;
        private FakeRepository<Album> albums;
        private FakeRepository<User> users;
        private FakeRepository<Image> images;
        private FakeRepository<Comment> comments;

        public IRepository<Album> Albums
        {
            get
            {
                if (this.albums == null)
                {
                    this.albums = new FakeRepository<Album>();
                }

                return this.albums;
            }
        }

        public IRepository<Image> Images
        {
            get
            {
                if (this.images == null)
                {
                    this.images = new FakeRepository<Image>();
                }

                return this.images;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (this.comments == null)
                {
                    this.comments = new FakeRepository<Comment>();
                }

                return this.comments;
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                if (this.tags == null)
                {
                    this.tags = new FakeRepository<Tag>();
                }

                return this.tags;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (this.users == null)
                {
                    this.users = new FakeRepository<User>();
                }

                return this.users;
            }
        }

        public int SaveChangesCallCount { get; set; }

        public void SaveChanges()
        {
            this.SaveChangesCallCount++;
        }
    }
}
