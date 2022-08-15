using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController: ControllerBase
    {
        [HttpGet(Name = "GetProductosVendidos")]
        public List<ProductoVendido> GetProductosVendidos()
        {
            return ProductosVendidosHandler.GetProductosVendidos(); 
        }
    }
}
