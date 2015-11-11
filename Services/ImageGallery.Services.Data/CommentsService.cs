namespace ImageGallery.Services.Data
{
    using ImageGallery.Services.Data.Contracts;
    using System;
    using System.Linq;
    using ImageGallery.Models;
    using ImageGallery.Data.Contracts;

    public class CommentsService : ICommentsService
    {
        private IImageGalleryData data;

        public CommentsService(IImageGalleryData data)
        {
            this.data = data;
        }

        public int Add(Comment comment, string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username cannot be null.");
            }

            if (comment == null)
            {
                throw new ArgumentNullException("Comment cannot be null");
            }

            var currentUser = this.data.Users
                .All()
                .FirstOrDefault(u => u.UserName == username);

            comment.Author = currentUser;

            this.data.Comments.Add(comment);
            this.data.SaveChanges();

            return comment.Id;
        }

        public int DeleteCommentById(int id, string authorizedUser)
        {
            var commentToDelete = this.data.Comments
                .All()
                .FirstOrDefault(c => c.Id == id && c.Author.UserName == authorizedUser);

            if (commentToDelete == null)
            {
                return -1;
            }

            return commentToDelete.Id;
        }

        public IQueryable<Comment> GetAll(string username)
        {
            var comments = this.data.Comments
                .All()
                .Where(c => c.Author.UserName == username);

            return comments;
        }

        public IQueryable<Comment> GetById(int id, string username)
        {
            var comments = this.data.Comments
                .All()
                .Where(c => c.Id == id && c.Author.UserName == username);

            return comments;
        }
    }
}
