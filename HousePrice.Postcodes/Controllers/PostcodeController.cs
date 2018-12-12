using HousePrice.Postcodes.Services;
using Microsoft.AspNetCore.Mvc;

namespace HousePrice.Postcodes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostcodeController : ControllerBase
    {

        // GET api/values
        [HttpGet("{postcode}")]
        public ActionResult<PostcodeLookup.PostcodeData> Get(string postcode)
        {
            var data = PostcodeLookup.GetByPostcode(postcode);

            if (data != null)
            {
                return data;
            }

            return NotFound(postcode);

        }
    }
}
