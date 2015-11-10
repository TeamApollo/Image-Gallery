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

        // GET api/albums
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            string currentUserName = this.User.Identity.Name;

            var result = this.albumsService
                .GetAll(currentUserName)
                .ProjectTo<AlbumViewModel>()
                .ToList();

            return this.Ok(result);
        }

        // GET api/albums/{id}
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

        // GET api/albums/all
        [Route("api/albums/all")]
        public IHttpActionResult Get(int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            string currentUserName = this.User.Identity.Name;

            var result = this.albumsService
                .GetAll(currentUserName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<AlbumViewModel>()
                .ToList();

            return this.Ok(result);
        }

        // GET api/albums/user?owner={owner username}
        [Route("api/albums/user")]
        public IHttpActionResult GetUserAlbums(string owner)
        {
            string authorizedUsername = this.User.Identity.Name;

            var albums = this.albumsService.GetAlbumsByUser(owner, authorizedUsername)
                .ProjectTo<AlbumViewModel>()
                .ToList();

            return this.Ok(albums);
        }

        // POST api/albums
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

        // DELETE api/albums/{id}
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var currentUser = this.User.Identity.Name;

            int deletedId = this.albumsService.DeleteAlbumById(id, currentUser);

            if (deletedId < 0)
            {
                return this.NotFound();
            }

            return this.Ok(deletedId);
        }
    }
}