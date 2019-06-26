using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _cityInfoContext;

        public CityInfoRepository(CityInfoContext cityInfoContext)
        {
            _cityInfoContext = cityInfoContext;
        }

        public bool CityExists(int cityId)
        {
            return _cityInfoContext.Cities.Any(c => c.Id == cityId);
        }

        public IEnumerable<City> GetCities()
        {
            return _cityInfoContext.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return _cityInfoContext.Cities
                    .Include(c => c.PointsOfInterest)
                    .FirstOrDefault(c => c.Id == cityId);
            }

            return _cityInfoContext.Cities
                .FirstOrDefault(c => c.Id == cityId);
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
        {
            return _cityInfoContext.PointsOfInterest
                .Where(p => p.CityId == cityId).ToList();
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            return _cityInfoContext.PointsOfInterest
                .FirstOrDefault(p => p.CityId == cityId && p.Id == pointOfInterestId);
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityId, false);
            city.PointsOfInterest.Add(pointOfInterest);
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _cityInfoContext.PointsOfInterest.Remove(pointOfInterest);
        }

        public bool Save()
        {
            var numberOfSavedChanges = _cityInfoContext.SaveChanges();
            return numberOfSavedChanges >= 0;
        }
    }
}
