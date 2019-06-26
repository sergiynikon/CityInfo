using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(IMailService mailService, ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _mailService = mailService;
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointsOfInterestEntities = _cityInfoRepository.GetPointsOfInterestForCity(cityId);

            var pointsOfInterestForCityResults = _mapper.Map<PointsOfInterestsDTO>(pointsOfInterestEntities);

            //var pointsOfInterestForCityResults = new List<PointsOfInterestsDTO>();
            
            //foreach (var pointsOfInterestEntity in pointsOfInterestEntities)
            //{
            //    pointsOfInterestForCityResults.Add(new PointsOfInterestsDTO()
            //    {
            //        Id = pointsOfInterestEntity.Id,
            //        Name = pointsOfInterestEntity.Name,
            //        Description = pointsOfInterestEntity.Description
            //    });
            //}

            return Ok(pointsOfInterestForCityResults);

            //var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //return Ok(city.PointsOfInterest);
        }

        [HttpGet("{cityId}/pointsofinterest/{pointOfInterestId}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfinterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);

            if (pointOfinterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestForCityResult = new PointsOfInterestsDTO()
            {
                Id = pointOfinterestEntity.Id,
                Name = pointOfinterestEntity.Name,
                Description = pointOfinterestEntity.Description
            };

            return Ok(pointOfInterestForCityResult);
            //var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            //if (pointOfInterest == null)
            //{
            //    return NotFound();
            //}

            //return Ok(pointOfInterest);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCteationDTO pointOfInterestForCteation)
        {
            if (pointOfInterestForCteation == null)
            {
                return BadRequest();
            }

            if (pointOfInterestForCteation.Name == pointOfInterestForCteation.Description)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            if (! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterestForCteation);

            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request!");
            }

            var createdPointOfInterestToReturn = _mapper.Map<Models.PointsOfInterestsDTO>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
                {
                    cityId = cityId, pointOfInterestId = createdPointOfInterestToReturn.Id
                },
                createdPointOfInterestToReturn);
        }

        [HttpPut("{cityId}/pointsofinterest/{pointOfInterestId}")]
        public IActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, 
            [FromBody] PointOfInterestForUpdateDTO pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            if (! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (! _cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, pointOfInterestEntity); //overwrites ponitofinterestentity with values of pointofinterest

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request!");
            }
            //var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => 
            //    p.Id == pointOfInterestId);

            //if (pointOfInterestFromStore == null)
            //{
            //    return NotFound();
            //}

            //pointOfInterestFromStore.Name = pointOfInterest.Name;
            //pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }
        [HttpPatch("{cityId}/pointsofinterest/{pointOfInterestId}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDTO>(pointOfInterestEntity);
            
            //var pointOfInterestToPatch =
            //    new PointOfInterestForUpdateDTO()
            //    {
            //        Name = pointOfInterestFromStore.Name,
            //        Description = pointOfInterestFromStore.Description
            //    };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Name == pointOfInterestToPatch.Description)
            {
                ModelState.AddModelError("Description", "The provided name should be different from description");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            if (! _cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request");
            }

            //pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            //pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsOfInterest/{pointOfInterestId}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            //var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            //if (city == null)
            //{
            //    return NotFound();
            //}

            if (! _cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

            if (! _cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request");
            }



            //send the mail when point of interest was deleted
            _mailService.Send("Point of interest deleted.", $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted");

            return NoContent();
        }
    }
}
