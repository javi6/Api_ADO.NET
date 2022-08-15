using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet(Name = "GetVentas")]
        public List<Venta> GetVentas()
        {
            return VentaHandler.GetVentas(); 
        }
    }
}
