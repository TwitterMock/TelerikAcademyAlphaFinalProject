using System;
using AutoMapper;
using TwitterBackUp.DomainModels;
using TwitterBackUp.DTO;
using TwitterBackUp.Models;

namespace TwitterBackUp.Properties
{
    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            this.CreateMap<TweetDto, Tweet>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TweetId))
                .ForMember(d => d.TwitterId, opt => opt.MapFrom(s => s.Twitter.TwitterId))
                .ForMember(d => d.TwitterScreenName, opt => opt.MapFrom(s => s.Twitter.ScreenName))
                .ForMember(d => d.Url, opt => opt.MapFrom(s => $"https://twitter.com/{s.Twitter.ScreenName}/status/{s.TweetId}"))
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(s => DateTime.ParseExact(s.CreatedOn,
                    "ddd MMM dd HH:mm:ss +ffff yyyy",
                    System.Globalization.CultureInfo.InvariantCulture)));

            this.CreateMap<TwitterDto, Twitter>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TwitterId))
                .ForMember(d => d.Url, opt => opt.MapFrom(s => $"https://twitter.com/{s.ScreenName}"));

            this.CreateMap<TwitterDto, TwitterViewModel>()
                .ForMember(d => d.Url, opt => opt.MapFrom(s => $"https://twitter.com/{s.ScreenName}"));
            
            this.CreateMap<Twitter, TwitterViewModel>()
                .ForMember(d => d.TwitterId, opt => opt.MapFrom(s => s.Id));

            this.CreateMap<TweetDto, TweetViewModel>()
                .ForMember(d => d.TwitterId, opt => opt.MapFrom(s => s.Twitter.TwitterId))
                .ForMember(d => d.TwitterScreenName, opt => opt.MapFrom(s => s.Twitter.ScreenName))
                .ForMember(d => d.Url, opt => opt.MapFrom(s => $"https://twitter.com/{s.Twitter.ScreenName}/status/{s.TweetId}"))
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(s => DateTime.ParseExact(s.CreatedOn,
                    "ddd MMM dd HH:mm:ss +ffff yyyy",
                    System.Globalization.CultureInfo.InvariantCulture)));

            this.CreateMap<Tweet, TweetViewModel>(MemberList.None)
                .ForMember(d => d.TweetId, opt => opt.MapFrom(s => s.Id));
        }
    }
}