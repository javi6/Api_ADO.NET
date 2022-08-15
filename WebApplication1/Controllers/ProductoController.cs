using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController: ControllerBase
    {
        [HttpGet(Name = "GetProductos")]
        public List<Producto> GetProductos()
        {
            return ProductoHandler.GetProductos(); 
        }
    }
}
