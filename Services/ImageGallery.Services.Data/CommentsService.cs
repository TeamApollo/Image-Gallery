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

        /// <summary>
        /// Adds a new comment to the database.
        /// </summary>
        /// <param name="comment">The new comment to be added.</param>
        /// <returns>The id of the created comment.</returns>
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

            if (currentUser == null)
            {
                throw new ArgumentNullException("There is no user with such username.");
            }

            comment.Author = currentUser;

            this.data.Comments.Add(comment);
            this.data.SaveChanges();

            return comment.Id;
        }

        /// <summary>
        /// Deletes the comment with the provided id.
        /// </summary>
        /// <param name="id">The id of the comment to delete.</param>
        /// <param name="authorizedUser">The username of the user whos comment to delete.</param>
        /// <returns>The id of the deleted comment or -1 if no item with such id is found.</returns>
        public int DeleteCommentById(int id, string authorizedUser)
        {
            var commentToDelete = this.data.Comments
                .All()
                .FirstOrDefault(c => c.Id == id && c.Author.UserName == authorizedUser);

            if (commentToDelete == null)
            {
                return -1;
            }

            this.data.Comments.Delete(commentToDelete);
            this.data.SaveChanges();

            return commentToDelete.Id;
        }

        /// <summary>
        /// Gets all comments.
        /// </summary>
        /// <param name="username">The username of the user whos comments to get.</param>
        /// <param name="albumId">The id of the album whos comments to get.</param>
        /// <returns>All found comments.</returns>
        public IQueryable<Comment> GetAll(string username, int albumId)
        {
            var comments = this.data.Comments
                .All()
                .Where(c => c.Author.UserName == username)
                .OrderByDescending(c => c.Id);

            return comments;
        }

        /// <summary>
        /// Gets the comment with the provided id.
        /// </summary>
        /// <param name="id">The id of the comment to get.</param>
        /// <param name="username">The username of the user whos comment to get.</param>
        /// <returns>Found comment or null if not found.</returns>
        public IQueryable<Comment> GetById(int id, string username)
        {
            var comments = this.data.Comments
                .All()
                .Where(c => c.Id == id && c.Author.UserName == username);

            return comments;
        }
    }
}
