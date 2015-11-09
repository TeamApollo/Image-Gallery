namespace ImageGallery.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Constants;
    using ImageGallery.Models;
    using Models.Album;
    using Services.Data.Contracts;

    public class AlbumsController : ApiController
    {
        private readonly IAlbumsService albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            this.albumsService = albumsService;
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            var result = this.albumsService
                .All()
                .ProjectTo<AlbumViewModel>()
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            string currentUserName = this.User.Identity.Name;

            var result = this.albumsService
                .GetById(id, currentUserName)
                .ProjectTo<AlbumViewModel>()
                .FirstOrDefault();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [Route("api/albums/all")]
        public IHttpActionResult Get(int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var result = this.albumsService
                .All()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<AlbumViewModel>()
                .ToList();

            return this.Ok(result);
        }

        [Authorize]
        public IHttpActionResult Post(AlbumsBindingModel albumRequestModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUser = this.User.Identity.Name;
            var newProject = Mapper.Map<Album>(albumRequestModel);

            var createdProjectId = this.albumsService
                .Add(newProject, currentUser);

            return this.Ok(createdProjectId);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}