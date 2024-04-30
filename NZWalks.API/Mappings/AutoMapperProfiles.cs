using System;
using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Mappings
{
	//Below is an example which is commented
	/*public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			//Here RevereMap will automatically map revertse also
			//We can use ForMember when property names don't match 
			CreateMap<UserDTO, UserDomain>()
				.ForMember(x=>x.Name, opt=> opt.MapFrom(x => x.FullName))
				.ReverseMap();
		}
	}

	public class UserDTO
	{
		public string FullName { get; set; }
	}

	public class UserDomain
	{
		public string Name { get; set; }
	}*/
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles() {
			CreateMap<Region, RegionDTO>().ReverseMap();
			CreateMap<AddRegionRequestDto, Region>().ReverseMap();
			CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
		}
	}
}

