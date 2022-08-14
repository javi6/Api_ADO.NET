using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregable_backend
{
    public class ProductoVendido
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }


        public void mostrardatos()
        {
            Console.WriteLine("Producto vendido ID: " + Id.ToString());           
            Console.WriteLine("Id de Producto: " + IdProducto.ToString());            
            Console.WriteLine("Stock de Producto vendido: " + Stock.ToString());
            Console.WriteLine("ID de venta: " + IdVenta.ToString());
        }
    }
}
