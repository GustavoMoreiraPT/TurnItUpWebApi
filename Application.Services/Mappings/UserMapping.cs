using Application.Dto.Users;
using AutoMapper;
using Domain.Model.Users;

namespace Application.Services.Mappings
{
	public class UserMapping : Profile

	{
		public UserMapping()
		{
			CreateMap<RegisterCreateDto, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
		}
	}
}
