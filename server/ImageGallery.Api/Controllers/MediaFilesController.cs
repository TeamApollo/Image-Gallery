namespace ImageGallery.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper.QueryableExtensions;
    using Models.MediaFile;
    using Services.Data.Contracts;

    public class MediaFilesController : ApiController
    {
        private readonly IMediaFilesService mediaFilesService;

        public MediaFilesController(IMediaFilesService mediaFilesService)
        {
            this.mediaFilesService = mediaFilesService;
        }

        // GET /api/MediaFiles/{albumId}
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get(int albumId)
        {
            string currentUserName = this.User.Identity.Name;

            var result = this.mediaFilesService
                .GetAll(albumId, currentUserName)
                .ProjectTo<MediaFileViewModel>()
                .ToList();

            return this.Ok(result);
        }
    }
}