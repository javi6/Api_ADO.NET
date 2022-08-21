using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository;
using WebApplication1.Controllers.DTOs;

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

        [HttpDelete(Name = "Borra PRODUCTO")]
        public bool DeleteProducto([FromBody] int id)
        {
            try
            {
                return ProductoHandler.DeleteProducto(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
                return false;
            }
        }
        [HttpPut]
        public bool ModificaProducto([FromBody] PutProducto producto)
        {
            return ProductoHandler.ModificaProducto(new Producto
            {
                Id = producto.Id,
                Descripcion = producto.Descripcion,
                Costo = producto.Costo,
                PrecioVenta = producto.PrecioVenta,
                Stock = producto.Stock,
                IdUsuario = producto.IdUsuario
            });
        }

        [HttpPost]
        public bool CreaProducto([FromBody] PostProducto producto)
        {
            try
            {
                return ProductoHandler.CreaProducto(new Producto
                {
                    Descripcion = producto.Descripcion,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return false;
            }
        }
    }    
}
