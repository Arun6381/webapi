using AutoMapper;
using world.DTO.Country;
using world.Model;

namespace world.Comman
{
	public class MappingProfile:Profile
	{
		public MappingProfile() {
		
			CreateMap<Country,CreateCountryDTO >().ReverseMap();
		}
	}
}
