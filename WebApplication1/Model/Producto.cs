
namespace WebApplication1
{
    public class Producto
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }

        public void mostrardatos()
        {
            Console.WriteLine("Producto ID: " + Id.ToString());
            Console.WriteLine("Descripción de Producto: " + Descripcion);
            Console.WriteLine("Costo de Producto: " + Costo.ToString());
            Console.WriteLine("Precio de venta de Producto: " + PrecioVenta.ToString());
            Console.WriteLine("Stock de Producto: " + Stock.ToString());
            Console.WriteLine("ID de Usuario de Producto: " + IdUsuario.ToString());
        }
    }

}


