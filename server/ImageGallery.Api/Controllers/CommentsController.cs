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

    public class CommentsController : ApiController
    {
        private ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        // GET api/comments
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            var currentUserName = this.User.Identity.Name;

            var result = this.commentsService
                .GetAll(currentUserName)
                .ProjectTo<CommentViewModel>()
                .ToList();

            return this.Ok(result);
        }

        // GET api/comments/{id}
        public IHttpActionResult Get(int id)
        {
            var currentUserName = this.User.Identity.Name;

            var result = this.commentsService
                .GetAll(currentUserName)
                .ProjectTo<CommentViewModel>()
                .FirstOrDefault(c => c.Id == id);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        // GET api/comments/all
        [Route("api/comments/all")]
        public IHttpActionResult Get(int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var currentUserName = this.User.Identity.Name;

            var result = this.commentsService
                .GetAll(currentUserName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CommentViewModel>()
                .ToList();

            return this.Ok(result);
        }

        // POST api/comments
        [Authorize]
        public IHttpActionResult Post(CommentBindingModel commentsRequestModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUserName = this.User.Identity.Name;
            var newComment = Mapper.Map<Comment>(commentsRequestModel);

            var createdComment = this.commentsService.Add(newComment, currentUserName);

            return this.Ok(createdComment);
        }

        // DELETE api/comments/{id}
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var currentUserName = this.User.Identity.Name;

            var deletedCommentId = this.commentsService.DeleteCommentById(id, currentUserName);

            if (deletedCommentId == -1)
            {
                return this.NotFound();
            }

            return this.Ok(deletedCommentId);
        }
    }
}
