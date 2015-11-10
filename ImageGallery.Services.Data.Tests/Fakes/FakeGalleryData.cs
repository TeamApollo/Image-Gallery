﻿namespace ImageGallery.Services.Data.Tests.Fakes
{
    using System;
    using ImageGallery.Data.Contracts;
    using Models;

    public class FakeGalleryData : IImageGalleryData
    {
        private FakeRepository<Tag> tags;
        private FakeRepository<Album> albums;
        private FakeRepository<User> users;

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

        public IRepository<MediaFile> MediaFiles
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Image> Images
        {
            get
            {
                throw new NotImplementedException();
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

        public IRepository<Video> Videos
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int SaveChangesCallCount { get; set; }

        public void SaveChanges()
        {
            this.SaveChangesCallCount++;
        }
    }
}
