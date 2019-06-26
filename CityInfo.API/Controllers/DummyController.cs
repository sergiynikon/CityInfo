using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/textdatabase")]
    public class DummyController : Controller
    {
        private CityInfoContext _cityInfoContext;

        public DummyController(CityInfoContext cityInfoContext)
        {
            _cityInfoContext = cityInfoContext;
        }

        [HttpGet]
        public IActionResult TextDatabase()
        {
            return Ok();
        }
    }
}
