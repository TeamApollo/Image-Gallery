namespace ImageGallery.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Services.Data.Contracts;
    using Models.Comment;
    using AutoMapper.QueryableExtensions;
    using Common.Constants;
    using AutoMapper;
    using ImageGallery.Models;

    [EnableCors("*", "*", "*")]
    public class CommentController : ApiController
    {
        private ICommentsService comments;

        public CommentController(ICommentsService comments)
        {
            this.comments = comments;
        }

        // GET api/comments
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            var currentUserName = this.User.Identity.Name;

            var result = this.comments
                .GetAll(currentUserName)
                .ProjectTo<CommentViewModel>()
                .ToList();

            return this.Ok(result);
        }

        // GET api/comments/{id}
        public IHttpActionResult Get(int id)
        {
            var currentUserName = this.User.Identity.Name;

            var result = this.comments
                .GetAll(currentUserName)
                .ProjectTo<CommentViewModel>()
                .FirstOrDefault(c => c.Id == id);

            return this.Ok(result);
        }

        // GET api/comments/all
        public IHttpActionResult Get(int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var currentUserName = this.User.Identity.Name;

            var result = this.comments
                .GetAll(currentUserName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CommentViewModel>()
                .ToList();

            return this.Ok(result);
        }

        // POST api/comments
        public IHttpActionResult Post(CommentBindingModel commentsRequestModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUserName = this.User.Identity.Name;

            var newComment = Mapper.Map<Comment>(commentsRequestModel);

            var createdComment = this.comments.Add(newComment, currentUserName);

            return this.Ok(createdComment);
        }

        // DELETE api/comments/{id}
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var currentUserName = this.User.Identity.Name;

            var deletedCommentId = this.comments.DeleteCommentById(id, currentUserName);

            if (deletedCommentId == -1)
            {
                return this.NotFound();
            }

            return this.Ok(deletedCommentId);
        }
    }
}
