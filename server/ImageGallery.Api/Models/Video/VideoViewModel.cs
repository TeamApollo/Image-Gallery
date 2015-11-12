namespace ImageGallery.Api.Models.Video
{
    using System;
    using AutoMapper;
    using ImageGallery.Api.Infrastructure.Mappings;
    using ImageGallery.Models;

    public class VideoViewModel : IMapFrom<Video>
    {
        public string UrlPath { get; set; }

        public int LengthInSeconds { get; set; }
    }
}