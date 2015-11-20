using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ImageGallery.Services.Data.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using Api;
    using Api.Models.Album;
    using MyTested.WebApi;

    [TestClass]
    public class AlbumsTests
    {
        [TestMethod]
        public void GetAlbumsShouldReturnCorrectResponse()
        {
            MyWebApi.Server().Working<Startup>()
                .WithHttpRequestMessage(r => r
                .WithMethod(HttpMethod.Get)
                .WithRequestUri("/api/albums"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK)
                .WithResponseModelOfType<ICollection<AlbumViewModel>>()
                .Passing(collection =>
                {
                    Assert.AreEqual(0, collection.Count);
                });
        }

        [TestMethod]
        public void GetAlbumsByIdShouldReturnCorrectResponse()
        {
            MyWebApi.Server().Working<Startup>()
                .WithHttpRequestMessage(r => r
                .WithMethod(HttpMethod.Get)
                .WithRequestUri("/api/albums/1"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithResponseModelOfType<AlbumViewModel>()
                .Passing(album =>
                {
                    Assert.AreEqual(null, album);
                });
        }

        [TestMethod]
        public void GetAlbumsAllShouldReturnCorrectResponse()
        {
            MyWebApi.Server().Working<Startup>()
                .WithHttpRequestMessage(r => r
                .WithMethod(HttpMethod.Get)
                .WithRequestUri("/api/albums/all"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK)
                .WithResponseModelOfType<ICollection<AlbumViewModel>>()
                .Passing(collection =>
                {
                    Assert.AreEqual(0, collection.Count);
                });
        }

        [TestMethod]
        public void GetAlbumsByUserShouldReturnCorrectResponse()
        {
            MyWebApi.Server().Working<Startup>()
                .WithHttpRequestMessage(r => r
                .WithMethod(HttpMethod.Get)
                .WithRequestUri("/api/albums/user"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void PostAlbumsShouldReturnCorrectResponseWhenNotAuthorized()
        {
            MyWebApi.Server().Working<Startup>()
                .WithHttpRequestMessage(r => r
                .WithMethod(HttpMethod.Post)
                .WithRequestUri("/api/albums"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        [TestMethod]
        public void DeleteAlbumShouldReturnCorrectResponseWhenNotAuthorized()
        {
            MyWebApi.Server().Working<Startup>()
                .WithHttpRequestMessage(r => r
                .WithMethod(HttpMethod.Delete)
                .WithRequestUri("/api/albums/1"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

    }
}
