using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.DTOs;
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

        [HttpPost]
        public bool CargarVenta([FromBody] List<PostVentaProducto> listaProductos, int id)
        {            
            List<ProductoVendido> ListaVenta = new List<ProductoVendido>();
            foreach (PostVentaProducto prod_from_web in listaProductos)
            {
                ListaVenta.Add(new ProductoVendido
                {                             
                    IdProducto = prod_from_web.IdProducto,
                    IdVenta = prod_from_web.IdVenta,
                    Stock = prod_from_web.Stock                    
                });
            }
            return VentaHandler.CargarVenta(ListaVenta, id);
        }
    }
}
