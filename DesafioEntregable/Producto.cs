
namespace DesafioEntregable
{
    internal class Producto
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }

        public Producto(int id, string descripcion, double costo, double precioVenta, int stock, int idUsuario)
        {
            Id = id;
            Descripcion = descripcion;
            Costo = costo;
            PrecioVenta = precioVenta;
            Stock = stock;
            IdUsuario = idUsuario;
        }

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
