namespace ImageGallery.Services.Data
{
    using System.Linq;
    using Models;
    using ImageGallery.Services.Data.Contracts;
    using ImageGallery.Data.Contracts;
    using System;

    public class TagsService : ITagsService
    {
        private readonly IImageGalleryData data;

        public TagsService(IImageGalleryData data)
        {
            this.data = data;
        }

        /// <summary>
        /// Adds a new tag to the database.
        /// </summary>
        /// <param name="newTag">The new tag to be added.</param>
        /// <returns>The id of the created tag.</returns>
        public int Add(Tag newTag)
        {
            if (newTag == null)
            {
                throw new ArgumentNullException("Tag cannot be null.");
            }

            this.data.Tags.Add(newTag);
            this.data.SaveChanges();

            return newTag.Id;
        }

        /// <summary>
        /// Gets all tags.
        /// </summary>
        /// <returns>All found tags.</returns>
        public IQueryable<Tag> GetAll()
        {
            var tags = this.data.Tags.All();

            return tags;
        }

        /// <summary>
        /// Gets the tag with the provided id.
        /// </summary>
        /// <param name="id">The id of the tag to get.</param>
        /// <returns>Found tag or null if not found.</returns>
        public IQueryable<Tag> GetById(int id)
        {
            var tag = this.data.Tags
                .All()
                .Where(t => t.Id == id);

            return tag;
        }

        /// <summary>
        /// Deletets the tag with the provided id.
        /// </summary>
        /// <param name="id">The id of the tag to delete.</param>
        /// <returns>The id of the deleted tag or -1 if no item with such id is found.</returns>
        public int DeleteTagById(int id)
        {
            var tagToDelete = this.data.Tags
                .All()
                .FirstOrDefault(t => t.Id == id);

            if (tagToDelete == null)
            {
                return -1;
            }

            this.data.Tags.Delete(tagToDelete);
            this.data.SaveChanges();

            return id;
        }
    }
}
