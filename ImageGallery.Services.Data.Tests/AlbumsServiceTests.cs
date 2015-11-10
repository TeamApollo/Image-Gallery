namespace ImageGallery.Services.Data.Tests
{
    using System;
    using System.Linq; 
    using Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class AlbumsServiceTests
    {
        #region GetAll

        [TestMethod]
        public void GetAll_ShouldReturnOnlyNonPrivateAlbums_WhenUserIsNotAuthorized()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryAlbumsData(data, 30);
            var service = new AlbumsService(data);
            string username = null;

            // act
            var actual = service.GetAll(username).Count();

            // assert
            Assert.AreEqual(15, actual);
        }

        [TestMethod]
        public void GetAll_ShouldReturnZeroAlbumsCount_WhenNoAlbumsInData()
        {
            // arrange
            var data = new FakeGalleryData();
            var service = new AlbumsService(data);
            string username = null;

            // act
            var actual = service.GetAll(username).Count();

            // assert
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void GetAll_ShouldReturnNonPrivateAndPrivateAlbumsOfTheAuthorizedUser_WhenUserIsAuthorized()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryAlbumsData(data, 30);
            var service = new AlbumsService(data);
            string username = "user2";

            // act
            var actual = service.GetAll(username).Count();

            // assert
            Assert.AreEqual(16, actual);
        }

        #endregion

        #region GetById

        [TestMethod]
        public void GetById_ShouldReturnCorrectAlbumIfNotPrivate_WhenUserIsNotAuthorized()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryAlbumsData(data, 5);
            var service = new AlbumsService(data);
            string username = null;

            // act
            var actual = service.GetById(1, username).Count();

            // assert
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void GetById_ShouldNotReturnAlbumIfAlbumIsPrivate_WhenUserIsNotAuthorized()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryAlbumsData(data, 5);
            var service = new AlbumsService(data);
            string username = null;

            // act
            var actual = service.GetById(2, username).Count();

            // assert
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void GetById_ShouldNotReturnAlbumIfAlbumIsPrivate_AndNotOwnerIsAuthorized()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryAlbumsData(data, 5);
            var service = new AlbumsService(data);
            string username = "user3";

            // act
            var actual = service.GetById(2, username).Count();

            // assert
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void GetById_ShouldReturnAlbumIfAlbumIsPrivate_AndOwnerIsAuthorized()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryAlbumsData(data, 5);
            var service = new AlbumsService(data);
            string username = "user2";

            // act
            var actual = service.GetById(2, username).Count();

            // assert
            Assert.AreEqual(1, actual);
        }

        #endregion

        #region Add

        [TestMethod]
        public void Add_ShouldAddAlbumIfAlbumAndUsernameAreValid()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryUsersData(data, 5);
            var service = new AlbumsService(data);
            string username = "user0";
            var album = this.GetAlbum("myalbum", false);

            // act
            service.Add(album, username);
            var actual = service.GetAll(username).Count();

            // assert
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_ShouldThrowIfAlbumIsNull()
        {
            // arrange
            var data = new FakeGalleryData();
            var service = new AlbumsService(data);
            string username = "user0";

            // act
            service.Add(null, username);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_ShouldThrow_WhenNoCreatorSpecified()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryUsersData(data, 5);
            var service = new AlbumsService(data);
            var album = this.GetAlbum("myalbum", false);

            // act
            service.Add(album, null);
        }

        [TestMethod]
        public void Add_ShouldAddAlbumWithCorrectDate()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryUsersData(data, 5);
            var service = new AlbumsService(data);
            string username = "user0";
            var album = this.GetAlbum("myalbum", false);

            // act
            service.Add(album, username);
            var addedAlbum = service.GetAll(username).First();
            var addedAlbumDate = addedAlbum.CreatedOn.Date;
            var addedAlbumHour = addedAlbum.CreatedOn.Hour;

            // assert
            Assert.AreEqual(DateTime.Now.Date, addedAlbumDate);
            Assert.AreEqual(DateTime.Now.Hour, addedAlbumDate.Hour);
        }

        [TestMethod]
        public void Add_ShouldCallDataSaveChanges()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryUsersData(data, 5);
            var service = new AlbumsService(data);
            string username = "user0";
            var album = this.GetAlbum("myalbum", false);

            // act
            service.Add(album, username);
            var actual = data.SaveChangesCallCount;

            // assert
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Add_ShouldReturnTheAddedAlbumId()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryUsersData(data, 5);
            var service = new AlbumsService(data);
            string username = "user0";
            var album = this.GetAlbum("myalbum", false);

            // act
            var actual = service.Add(album, username);

            // assert
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_ShouldThrowIfUserWithSpecifiedUsernameIsNotFound()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryUsersData(data, 5);
            var service = new AlbumsService(data);
            string username = "user8";
            var album = this.GetAlbum("myalbum", false);

            // act
            service.Add(album, username);
        }

        [TestMethod]
        public void Add_ShouldAddAlbumWithCorrectUserWhenUsernameIsValid()
        {
            // arrange
            var data = new FakeGalleryData();
            this.PopulateFakeGalleryUsersData(data, 5);
            var service = new AlbumsService(data);
            string username = "user0";
            var album = this.GetAlbum("myalbum", false);

            // act
            service.Add(album, username);
            var actual = service.GetAll(username).First().Owner.UserName;

            // assert
            Assert.AreEqual(username, actual);
        }

        #endregion

        private void PopulateFakeGalleryAlbumsData(
            FakeGalleryData data,
            int albumsCount)
        {
            for (int i = 0; i < albumsCount; i++)
            {
                var album = new Album()
                {
                    Id = i,
                    Name = "Album" + i,
                    Private = i % 2 == 0,
                    CreatedOn = DateTime.Now.AddDays(i),
                    Owner = new User()
                    {
                        UserName = "user" + i
                    }
                };

                data.Albums.Add(album);
            }
        }

        private void PopulateFakeGalleryUsersData(
            FakeGalleryData data,
            int usersCount)
        {
            for (int i = 0; i < usersCount; i++)
            {
                var user = new User()
                {
                    UserName = "user" + i
                };

                data.Users.Add(user);
            }
        }

        private Album GetAlbum(string name, bool isPrivate)
        {
            return new Album()
            {
                Name = name,
                Private = isPrivate,
                Id = 1
            };
        }
    }
}
