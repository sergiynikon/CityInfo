using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Utility.Profile
{
    public class CityMappingProfile : AutoMapper.Profile   
    {
        public CityMappingProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDTO>();
            CreateMap<Entities.City, Models.CityDTO>();

        }
    }
}
