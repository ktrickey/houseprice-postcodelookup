using HousePrice.Postcodes.Services;
using Microsoft.AspNetCore.Mvc;

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
