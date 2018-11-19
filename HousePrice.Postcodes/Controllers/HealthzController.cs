using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HousePrice.Postcodes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace HousePrice.Postcodes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthzController : ControllerBase
    {
        // GET: api/Healthz
        [HttpGet]
        public ActionResult< PostcodeLookup.PostcodeData> Get()
        {
	        var data = PostcodeLookup.GetByPostcode("Test");

	        return data;
        }
		
    }
}
