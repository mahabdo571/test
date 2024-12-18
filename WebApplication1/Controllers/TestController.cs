using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/Test")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index() {

            List<string> x =new List<string> {
                "cddcsadcadsc",
                "cddcsadcadsc",
                "cddcsadcadsc",
                "cddcsadcadsc",
                "cddcsadcadsc"
            };
            return Ok(x);
        }
    }
}
