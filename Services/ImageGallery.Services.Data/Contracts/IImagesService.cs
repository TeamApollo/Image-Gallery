﻿namespace ImageGallery.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface IImagesService
    {
        IQueryable<Image> GetAll(int albumId, string username);

        IQueryable<Image> GetById(int imageId, string username);

        int Add(Image image, string username);

        int Delete(int imageId, string username);
    }
}
