using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository;
using WebApplication1.Controllers.DTOs;

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
