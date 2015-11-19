namespace ImageGallery.Services.Data.Tests
{
    using Common.Constants;
    using ImageGallery.Models;
    using ImageGallery.Services.Data.Tests.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class TagsServiceTests
    {
        //[TestMethod]
        //public void GetAll_ShouldReturnCorrectAmountOfTags()
        //{
        //    var data = new FakeGalleryData();
        //    var tagsService = new TagsService(data);

        //    Tag tagToAdd = new Tag();

        //    tagsService.Add(tagToAdd);

        //    Assert.AreEqual(1, tagsService.GetAll().Count());
        //}

        //[TestMethod]
        //public void GetAll_ShouldReturnZeroTagsWhenNoTagsWereAdded()
        //{
        //    var data = new FakeGalleryData();
        //    var tagsService = new TagsService(data);

        //    Assert.AreEqual(0, tagsService.GetAll().Count());
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_ShouldThrowArgumentNullExceptionWhenNullTagProvided()
        {
            var data = new FakeGalleryData();
            var tagsService = new TagsService(data);

            tagsService.Add(null);
        }

        //[TestMethod]
        //public void Add_ShouldAddTagsProperly()
        //{
        //    var data = new FakeGalleryData();
        //    var tagsService = new TagsService(data);

        //    Tag firstTag = new Tag();
        //    firstTag.Description = "A very important tag!";
        //    Tag secondTag = new Tag();
        //    secondTag.Description = "A very important tag!";

        //    tagsService.Add(firstTag);
        //    tagsService.Add(secondTag);

        //    Assert.AreEqual(2, tagsService.GetAll().Count());
        //}

        [TestMethod]
        public void GetById_ShouldReturnZeroWhenSuchTagIdIsNotFound()
        {
            var data = new FakeGalleryData();
            var tagsService = new TagsService(data);

            var foundTags = tagsService.GetById(GlobalConstants.DefaultTagIdForTesting);

            Assert.AreEqual(0, foundTags.Count());
        }

        [TestMethod]
        public void GetById_ShouldReturnProperAmountOfTags()
        {
            var data = new FakeGalleryData();
            var tagsService = new TagsService(data);

            var newTag = new Tag();
            var tagId = newTag.Id;

            tagsService.Add(newTag);
            var foundTags = tagsService.GetById(tagId);

            Assert.AreEqual(1, foundTags.Count());
        }

        [TestMethod]
        public void Delete_ShouldReturnProperDeletedTagId()
        {
            var data = new FakeGalleryData();
            var tagsService = new TagsService(data);

            var newTag = new Tag();
            var tagId = newTag.Id;

            tagsService.Add(newTag);
            tagsService.DeleteTagById(tagId);
            var foundTags = tagsService.GetById(tagId);

            Assert.AreEqual(0, foundTags.Count());
        }

        [TestMethod]
        public void Delete_ShouldReturnProperValueWhenTagIdDoesNotExist()
        {
            var data = new FakeGalleryData();
            var tagsService = new TagsService(data);

            var deletedTagId = tagsService.DeleteTagById(GlobalConstants.DefaultTagIdForTesting);

            Assert.AreEqual(-1, deletedTagId);
        }
    }
}
