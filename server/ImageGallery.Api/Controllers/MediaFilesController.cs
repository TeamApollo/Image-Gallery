namespace ImageGallery.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper.QueryableExtensions;
    using Models.MediaFile;
    using Services.Data.Contracts;
    using ImageGallery.Models;
    using AutoMapper;

    [EnableCors("*", "*", "*")]
    public class MediaFilesController : ApiController
    {
        private readonly IMediaFilesService mediaFilesService;

        public MediaFilesController(IMediaFilesService mediaFilesService)
        {
            this.mediaFilesService = mediaFilesService;
        }

        // GET api/albums/{albumId}/mediafiles
        [Route("api/albums/{albumId}/mediafiles")]
        public IHttpActionResult Get(int albumId)
        {
            string currentUserName = this.User.Identity.Name;

            var result = this.mediaFilesService
                .GetAll(albumId, currentUserName)
                .ProjectTo<MediaFileViewModel>()
                .ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        // GET api/albums/{albumId}/mediafiles/{mediaFileId}
        [Route("api/albums/{albumId}/mediafiles/{mediaFileId}")]
        public IHttpActionResult Get(int albumId, int mediaFileId)
        {
            string currentUserName = this.User.Identity.Name;

            var result = this.mediaFilesService
                .GetById(albumId, mediaFileId, currentUserName)
                .ProjectTo<MediaFileViewModel>()
                .FirstOrDefault();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        // POST api/albums/{albumId}/mediafiles
        [Authorize]
        [Route("api/albums/{albumId}/mediafiles")]
        public IHttpActionResult Post(int albumId, MediaFileBindingModel mediaFileRequestModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUser = this.User.Identity.Name;
            var newMediaFile = Mapper.Map<Image>(mediaFileRequestModel);

            var createdProjectId = this.mediaFilesService
                .Add(newMediaFile, albumId, currentUser);

            return this.Ok(createdProjectId);
        }

        // DELETE api/albums/{albumId}/mediaFiles/{mediaFileId}
        [Authorize]
        [Route("api/albums/{albumId}/mediafiles/{mediaFileId}")]
        public IHttpActionResult Delete(int albumId, int mediaFileId)
        {
            var currentUser = this.User.Identity.Name;

            int deletedId = this.mediaFilesService.Delete(mediaFileId, albumId, currentUser);

            if (deletedId < 0)
            {
                return this.NotFound();
            }

            return this.Ok(deletedId);
        }
    }
}