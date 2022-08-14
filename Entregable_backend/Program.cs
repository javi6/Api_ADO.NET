using Entregable_backend.ADO;

namespace Entregable_backend
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductoHandler prod = new ProductoHandler();
            UsuarioHandler usuario = new UsuarioHandler();
            ProductosVendidosHandler vendidos = new ProductosVendidosHandler();
            VentaHandler ventas = new VentaHandler();

            prod.GetProductos();
            usuario.GetUsuarios();
            vendidos.GetProductosVendidos();
            ventas.GetVentas();
            
      
        }
    }
}