namespace ImageGallery.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public interface IMediasService
    {
        IQueryable<Album> GetAll(IEnumerable<Album> albums);       
    }
}
