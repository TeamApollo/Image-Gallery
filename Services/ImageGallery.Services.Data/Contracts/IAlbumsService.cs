﻿namespace ImageGallery.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface IAlbumsService
    {
        IQueryable<Album> GetAll();

        int Add(Album album, string username);

        IQueryable<Album> GetById(int id, string username);
    }
}