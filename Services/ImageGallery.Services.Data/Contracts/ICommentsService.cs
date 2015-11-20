namespace ImageGallery.Services.Data.Contracts
{
    using System.Linq;
    using Models;  

    public interface ICommentsService
    {
        IQueryable<Comment> GetAll(int albumId);

        int Add(Comment comment);

        IQueryable<Comment> GetById(int id, string username);

        int DeleteCommentById(int id, string authorizedUser);
    }
}
