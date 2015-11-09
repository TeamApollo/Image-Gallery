namespace ImageGallery.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface ITagsService
    {
        IQueryable<Tag> GetAll();

        int Add(Tag tag);

        IQueryable<Tag> GetById(int id);
    }
}
