using AutoMapper;
using TwitterBackUp.DomainModels;
using TwitterBackUp.DTO;
using TwitterBackUp.DTO.TwitterTimelineDtos;

namespace TwitterBackUp.Properties
{
    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            //this.CreateMap<PostViewModel, PostDto>(MemberList.Source);
            //this.CreateMap<PostDto, Post>(MemberList.Source);
            //this.CreateMap<CommentDto, Comment>(MemberList.Source);
            this.CreateMap<TwitterSearchDto, Twitter>(MemberList.Source);
        }
    }
}