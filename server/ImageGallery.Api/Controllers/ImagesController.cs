namespace ImageGallery.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common;
    using ImageGallery.Models; 
    using Models.Image;
    using Services.Data.Contracts;   

    [EnableCors("*", "*", "*")]
    public class ImagesController : ApiController
    {
        private readonly IImagesService imagesService;

        public ImagesController(IImagesService imagesService)
        {
            this.imagesService = imagesService;
        }

        // GET api/images?albumId=XXX
        [HttpGet]
        public IHttpActionResult GetAll(int albumId)
        {
            string currentUserName = this.User.Identity.Name;

            List<ImageViewModel> result;
            try
            {
                result = this.imagesService
                    .GetAll(albumId, currentUserName)
                    .ProjectTo<ImageViewModel>()
                    .ToList();
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

        // GET api/images?imageId=XXX
        public IHttpActionResult Get(int imageId)
        {
            string currentUserName = this.User.Identity.Name;

            List<ImageViewModel> result;

            try
            {
                result = this.imagesService
                    .GetById(imageId, currentUserName)
                    .ProjectTo<ImageViewModel>()
                    .ToList();
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

        // POST api/images
        [Authorize]
        public IHttpActionResult Post(ImageBindingModel imageBindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUser = this.User.Identity.Name;
            var image = Mapper.Map<Image>(imageBindingModel);

            int imageId;
            try
            {
                imageId = this.imagesService
                    .Add(image, currentUser);
            }
            catch (ImageGalleryException)
            {
                return this.Unauthorized();
            }
            catch (ArgumentNullException)
            {
                return this.NotFound();
            }

            return this.Ok(imageId);
        }

        // DELETE api/images/{id}
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var currentUser = this.User.Identity.Name;

            int deletedId;
            try
            {
                deletedId = this.imagesService.Delete(id, currentUser);
            }
            catch (ImageGalleryException)
            {
                return this.Unauthorized();
            }
            catch (ArgumentNullException)
            {
                return this.NotFound();
            }

            return this.Ok(deletedId);
        }
    }
}