namespace ImageGallery.Api.Models.Comment
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Common.Constants;
    using ImageGallery.Models;
    using Infrastructure.Mappings;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [MaxLength(ValidationConstants.MaxCommentBodyLength)]
        public string Body { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Comment, CommentViewModel>()
                .ForMember(pr => pr.Author, opts => opts.MapFrom(pm => pm.Author.UserName));
        }
    }
}