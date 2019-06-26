using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;

namespace CityInfo.API
{
    public static class CityInfoContextExtentions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            if (context.Cities.Any())
            {
                return;
            }

            //init seed data
            var cities = new List<City>()
            {
                new City()
                {
                    Name = "Lviv",
                    Description = "My native city <3",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "Concerts",
                            Description = "A lot of concerts are in the city: FestRepublic is a club for playing most of the concerts"
                        },
                        new PointOfInterest()
                        {
                            Name = "High Castle",
                            Description = "High Castle is a range of attractions. "
                        }
                    }
                },
                new City()
                {
                    Name = "Kyiv",
                    Description = "The Capital of Ukraine",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "Saint Sophia's Cathedral",
                            Description = "Saint Sophia’s Cathedral is an outstanding complex; work started on it in 1037 and lasted for just three years."
                        },
                        new PointOfInterest()
                        {
                            Name = "Mystetskyi Arsenal art quarter",
                            Description = "Mystetskyi Arsenal is an art quarter in the heart of Kiev. "
                        },
                        new PointOfInterest()
                        {
                            Name = "Saint Andrew's Church",
                            Description = "St. Andrew’s Church in Kiev is one of the most striking Baroque buildings. Its architecture and location are genuinely unique. "
                        }
                    }
                }
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}
