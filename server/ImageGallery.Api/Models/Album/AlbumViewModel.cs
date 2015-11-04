namespace ImageGallery.Api.Models.Album
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Common.Constants;
    using Infrastructure.Mappings;
    using ImageGallery.Models;

    public class AlbumViewModel : IMapFrom<Album>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [MaxLength(ValidationConstants.AlbumNameMaxLength)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Album, AlbumViewModel>()
                .ForMember(pr => pr.Author, opts => opts.MapFrom(pm => pm.Owner.UserName));
        }
    }
}