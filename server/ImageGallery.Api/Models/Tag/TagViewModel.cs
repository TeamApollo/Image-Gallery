namespace ImageGallery.Api.Models.Tag
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Common.Constants;
    using ImageGallery.Models;
    using Infrastructure.Mappings;

    public class TagViewModel : IMapFrom<Tag>
    {
        public int Id { get; set; }
    }
}