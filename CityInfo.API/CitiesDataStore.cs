using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDTO> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDTO>()
            {
                new CityDTO()
                {
                    Id = 1,
                    Name = "Lviv",
                    Description = "My native city <3",
                    PointsOfInterest = new List<PointsOfInterestsDTO>()
                    {
                        new PointsOfInterestsDTO()
                        {
                            Id = 1,
                            Name = "Concerts",
                            Description = "a lot of concerts are in the city: FestRepublic is a club for playing most of the concerts"
                        }, 
                        new PointsOfInterestsDTO()
                        {
                            Id = 2,
                            Name = "High Castle",
                            Description = "High Castle is a range of attractions. Here is park 'High Castle', which was founded in 1835, ancient settlement of the city, grotto and sculptures of lions, and the actual remains of the High Castle, which was built in the 13th century under the leadership of Galicia-Volyn Prince Lev on Castle Hill."
                        }
                    }
                }, 
                new CityDTO()
                {
                    Id = 2,
                    Name = "Kyiv",
                    Description = "The Capital of Ukraine",
                    PointsOfInterest = new List<PointsOfInterestsDTO>()
                    {
                        new PointsOfInterestsDTO()
                        {
                            Id = 3,
                            Name = "Saint Sophia's Cathedral",
                            Description = "Saint Sophia’s Cathedral is an outstanding complex; work started on it in 1037 and lasted for just three years. As the architectural monument has had only a few reconstructions, you can marvel at the Byzantine cathedral in close to its original form. In addition, as it is located at the intersection of the four leading roads in Kiev, climb to the cathedral’s bell tower and you’ll be rewarded with a magnificent view from the top."
                        },
                        new PointsOfInterestsDTO()
                        {
                            Id = 4,
                            Name = "Mystetskyi Arsenal art quarter",
                            Description = "Mystetskyi Arsenal is an art quarter in the heart of Kiev. Every season it holds various exhibitions, festivals and markets that promote culture in Ukraine. Among them are Ukrainian fashion week, annual book fair and contemporary art celebrations."
                        },
                        new PointsOfInterestsDTO()
                        {
                            Id = 5,
                            Name = "Saint Andrew's Church",
                            Description = "St. Andrew’s Church in Kiev is one of the most striking Baroque buildings. Its architecture and location are genuinely unique. The church is built on an artificial hill, overlooking one of Kiev’s oldest neighbourhoods, and the foundation for it is the terrace of a two-story building, with a cast-iron staircase. It has been twice hit by lightning, but still the church is is flourishing and is open to the public for services and sightseeing tours."
                        }
                    }
                }
            };
        }
    }
}
