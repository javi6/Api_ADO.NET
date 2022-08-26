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
        public bool CargarVenta([FromBody] List<PostVentaProducto> listaProductos, int idVendedor)
        {            
            List<ProductoVendido> ListaVenta = new List<ProductoVendido>();
            foreach (PostVentaProducto prod_from_web in listaProductos)
            {
                ListaVenta.Add(new ProductoVendido
                {                             
                    IdProducto = prod_from_web.IdProducto,                    
                    Stock = prod_from_web.Stock                    
                });
            }
            return VentaHandler.CargarVenta(ListaVenta, idVendedor);
        }

        [HttpDelete(Name = "Borra Venta")]
        public bool DeleteVenta([FromBody] int idVenta)
        {
            ProductosVendidosHandler.eliminaProductosVendidos(idVenta);
            return VentaHandler.DeleteVenta(idVenta);

        }
    }
}
