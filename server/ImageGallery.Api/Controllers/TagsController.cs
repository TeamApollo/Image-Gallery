namespace ImageGallery.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;    
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Constants;
    using ImageGallery.Models;
    using Models.Tag;
    using Services.Data.Contracts;

    public class TagsController : ApiController
    {
        private readonly ITagsService tagsService;

        public TagsController(ITagsService tagsService)
        {
            this.tagsService = tagsService;
        }

        // GET api/tags
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            var result = this.tagsService
                .GetAll()
                .ProjectTo<TagViewModel>()
                .ToList();

            return this.Ok(result);
        }

        // GET api/tags/{id}
        public IHttpActionResult Get(int id)
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

        // GET api/tags/all
        [Route("api/tags/all")]
        public IHttpActionResult Get(int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var result = this.tagsService
                .GetAll()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TagViewModel>()
                .ToList();

            return this.Ok(result);
        }

        // POST api/tags
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
