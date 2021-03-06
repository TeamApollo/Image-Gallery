﻿namespace ImageGallery.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors; 
    using AutoMapper;
    using AutoMapper.QueryableExtensions;   
    using Common;
    using ImageGallery.Models;
    using Models.Comment;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    public class CommentsController : ApiController
    {
        private ICommentsService comments;

        public CommentsController(ICommentsService comments)
        {
            this.comments = comments;
        }

        // GET api/comments?albumId={albumId}
        public IHttpActionResult Get(int albumId)
        {
            var currentUserName = this.User.Identity.Name;

            IQueryable<CommentViewModel> result;

            try
            {
                var comments = this.comments
                    .GetAll(albumId);
                    result = comments.ProjectTo<CommentViewModel>();
            }
            catch (ImageGalleryException)
            {
                return this.Unauthorized();
            }
            catch (ArgumentNullException)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        // GET api/comments
        // public IHttpActionResult Get()
        // {
        //    var currentUserName = this.User.Identity.Name;

        //    var result = this.comments
        //        .GetAll(currentUserName)
        //        .ProjectTo<CommentViewModel>()
        //        .ToList();

        //    return this.Ok(result);
        // }

        // GET api/comments/all
        // public IHttpActionResult Get(int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        // {
        //    var currentUserName = this.User.Identity.Name;

        //    var result = this.comments
        //        .GetAll(currentUserName)
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ProjectTo<CommentViewModel>()
        //        .ToList();

        //    return this.Ok(result);
        // }

        // POST api/comments
        //[Authorize]
        public IHttpActionResult Post(CommentBindingModel commentsRequestModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newComment = Mapper.Map<Comment>(commentsRequestModel);

            int commentId;

            try
            {
                commentId = this.comments.Add(newComment);
            }
            catch (ArgumentNullException)
            {
                return this.NotFound();
            }

            return this.Ok(commentId);
        }

        // DELETE api/comments/{id}?imageId={id}
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