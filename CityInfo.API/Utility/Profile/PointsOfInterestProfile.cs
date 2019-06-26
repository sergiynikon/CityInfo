using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Utility.Profile
{
    public class PointsOfInterestProfile : AutoMapper.Profile
    {
        public PointsOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointsOfInterestsDTO>();
            CreateMap<Models.PointOfInterestForCteationDTO, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestForUpdateDTO, Entities.PointOfInterest>();
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDTO>();
        }
    }
}
