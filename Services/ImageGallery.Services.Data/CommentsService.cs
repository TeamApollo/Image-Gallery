namespace ImageGallery.Services.Data
{       
    using System.Linq;
    using Common;      
    using ImageGallery.Data.Contracts;
    using ImageGallery.Models;   
    using ImageGallery.Services.Data.Contracts;
       
    public class CommentsService : ICommentsService
    {
        private readonly IImageGalleryData data;

        public CommentsService(IImageGalleryData data)
        {
            this.data = data;
        }

        /// <summary>
        /// Adds a new comment to the database.
        /// </summary>
        /// <param name="comment">The new comment to be added.</param>
        /// <param name="username">The new comment to be added.</param>
        /// <returns>The id of the created comment.</returns>
        public int Add(Comment comment)
        {
            Validator.ValidateObjectIsNotNull(comment);
            
            var currentUser = this.data.Users
                .All()
                .FirstOrDefault(u => u.UserName == comment.UserName);

            Validator.ValidateObjectIsNotNull(currentUser);

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
        public IQueryable<Comment> GetAll(int albumId)
        {
            var album = this.data.Albums.GetById(albumId);
            Validator.ValidateObjectIsNotNull(album);

            //if (album.Owner.UserName != username)
            //{
            //    throw new ImageGalleryException("The user does not have access to this album");
            //}

            return album.Comments.AsQueryable();
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
