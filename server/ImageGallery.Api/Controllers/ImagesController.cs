namespace ImageGallery.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper.QueryableExtensions;
    using Models.Image;
    using Services.Data.Contracts;
    using ImageGallery.Models;
    using AutoMapper;
    using Common;

    [EnableCors("*", "*", "*")]
    public class ImagesController : ApiController
    {
        private readonly IImagesService imagesService;

        public ImagesController(IImagesService imagesService)
        {
            this.imagesService = imagesService;
        }

        // GET api/images?albumId=XXX
        public IHttpActionResult Get(int albumId)
        {
            string currentUserName = this.User.Identity.Name;

            List<ImageViewModel> result;
            try
            {
                result = this.imagesService
                    .GetAll(albumId, currentUserName)
                    .ProjectTo<ImageViewModel>().ToList();
            }
            catch (ImageGalleryException)
            {
                return this.Unauthorized();
            }
            catch(ArgumentNullException)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        // GET api/images?album=XXX&imageId=XXX
        public IHttpActionResult Get(int albumId, int imageId)
        {
            string currentUserName = this.User.Identity.Name;

            var result = this.imagesService
                .GetById(albumId, imageId, currentUserName)
                .ProjectTo<ImageViewModel>()
                .FirstOrDefault();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        // POST api/images/{albumId}
        [Authorize]
        public IHttpActionResult Post(int albumId, ImageBindingModel mediaFileRequestModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUser = this.User.Identity.Name;
            var newMediaFile = Mapper.Map<Image>(mediaFileRequestModel);

            var createdProjectId = this.imagesService
                .Add(newMediaFile, albumId, currentUser);

            return this.Ok(createdProjectId);
        }

        // DELETE api/images/{albumId}/{imageId}
        [Authorize]
        public IHttpActionResult Delete(int albumId, int mediaFileId)
        {
            var currentUser = this.User.Identity.Name;

            int deletedId = this.imagesService.Delete(mediaFileId, albumId, currentUser);

            if (deletedId < 0)
            {
                return this.NotFound();
            }

            return this.Ok(deletedId);
        }
    }
}