namespace ImageGallery.Services.Data.Contracts
{
    using Models;
    using System.Linq;

    public interface ICommentsService
    {
        IQueryable<Comment> GetAll(string username);

        int Add(Comment comment, string username);

        IQueryable<Comment> GetById(int id, string username);

        int DeleteCommentById(int id, string authorizedUser);
    }
}
