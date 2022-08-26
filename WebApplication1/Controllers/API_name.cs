using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        [HttpGet(Name = "GetNombreApi")]
        public string GetName()
        {
            return APIHandler.GetName();
        }
    }
}
