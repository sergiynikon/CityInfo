using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult GetCities()
        {
            //return Ok(CitiesDataStore.Current.Cities);
            var cityEntities = _cityInfoRepository.GetCities();

            //var results = new List<CityWithoutPointsOfInterestDTO>();

            //foreach (var cityEntity in cityEntities)
            //{
            //    results.Add(new CityWithoutPointsOfInterestDTO()
            //    {
            //       Id = cityEntity.Id,
            //       Name = cityEntity.Name,
            //       Description = cityEntity.Description
            //    });
            //}

            var results = _mapper.Map<IEnumerable<CityWithoutPointsOfInterestDTO>>(cityEntities);

            return Ok(results);
        }

        [HttpGet("{cityId}")]
        public IActionResult GetCity(int cityId, bool includePointsOfInterest = false)
        {
            var cityEntity = _cityInfoRepository.GetCity(cityId, includePointsOfInterest);

            if (cityEntity == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var cityResult = _mapper.Map<CityDTO>(cityEntity);
                //var cityResult = new CityDTO()
                //{
                //    Id = cityEntity.Id,
                //    Name = cityEntity.Name,
                //    Description = cityEntity.Description,
                //};
                //foreach (var pointOfInterest in cityEntity.PointsOfInterest)
                //{
                //    cityResult.PointsOfInterest.Add(new PointsOfInterestsDTO()
                //    {
                //        Id = pointOfInterest.Id,
                //        Name = pointOfInterest.Name,
                //        Description = pointOfInterest.Description
                //    });
                //}

                return Ok(cityResult);
            }

            var cityWithoutPointOfInterestResult = _mapper.Map<CityWithoutPointsOfInterestDTO>(cityEntity);

            return Ok(cityWithoutPointOfInterestResult);

            //var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            //if (cityToReturn == null)
            //{
            //    return NotFound();
            //}

            //return Ok(cityToReturn);
        }
    }
}
