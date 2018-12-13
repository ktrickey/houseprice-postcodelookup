using HousePrice.Postcodes.Services;
using Microsoft.AspNetCore.Mvc;

namespace HousePrice.Postcodes.Controllers
{
    /// <summary>
    /// Health endpoint - can K8S/Swarm can be configured to call this to check status
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HealthzController : ControllerBase
    {
        [HttpGet]
        public ActionResult< PostcodeLookup.PostcodeData> Get()
        {
            var data = PostcodeLookup.GetByPostcode("Test");

            return data;
        }

    }
}
