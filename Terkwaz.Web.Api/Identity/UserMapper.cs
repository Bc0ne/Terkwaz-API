namespace Terkwaz.Web.Api.Identity
{
    using AutoMapper;
    using Terkwaz.Api.Models.Identity;
    using Domain.User;
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserRegisterationOutputModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Token, opt => opt.Ignore());
        }
    }
}
