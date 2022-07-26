
namespace DesafioEntregable
{
    internal class ProductoVendido
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public int IdVenta { get; set; }

        public ProductoVendido(int id, int idProducto, int stock, int idVenta)
        {
            Id = id;
            IdProducto = idProducto;
            Stock = stock;
            IdVenta = idVenta;
        }

        public void mostrardatos()
        {
            Console.WriteLine("Producto Vendido ID: " + Id.ToString());
            Console.WriteLine("ID de Producto Vendido ID: " + IdProducto.ToString());
            Console.WriteLine("Stock Producto Vendido: " + Stock.ToString());
            Console.WriteLine("ID de venta de Producto Vendido: " + IdVenta.ToString());
        }
    }
}
