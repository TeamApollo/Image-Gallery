namespace ImageGallery.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;    
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ImageGallery.Models;
    using Models.Tag;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    public class TagsController : ApiController
    {
        private readonly ITagsService tagsService;

        public TagsController(ITagsService tagsService)
        {
            this.tagsService = tagsService;
        }

        // GET api/tags?albumId={id}
        [Route("api/tags")]
        public IHttpActionResult Get(int albumId)
        {
            var result = this.tagsService
                .GetAll(albumId)
                .ProjectTo<TagViewModel>()
                .ToList();

            return this.Ok(result);
        }

        // GET api/tags/{id}
        [Route("api/tags/{id}")]
        [HttpGet]
        public IHttpActionResult GetTagAlbums(int id)
        {
            var result = this.tagsService
                .GetById(id)
                .ProjectTo<TagViewModel>()
                .FirstOrDefault();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        //// GET api/tags/all
        //[Route("api/tags/all")]
        //public IHttpActionResult Get(int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        //{
        //    var result = this.tagsService
        //        .GetAll()
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ProjectTo<TagViewModel>()
        //        .ToList();

        //    return this.Ok(result);
        //}

        // POST api/tags?albumId={id}
        [Authorize]
        public IHttpActionResult Post(TagsBindingModel tagRequestModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newTag = Mapper.Map<Tag>(tagRequestModel);

            var createdTag = this.tagsService
                .Add(newTag);

            return this.Ok(createdTag);
        }

        // DELETE api/tags/
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var result = this.tagsService
                .DeleteTagById(id);

            if (result == -1)
            {
                this.NotFound();
            }

            return this.Ok(result);
        }
    }
}
