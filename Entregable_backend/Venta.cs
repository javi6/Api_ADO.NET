using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregable_backend
{
    public class Venta
    {
        public string? Comentarios { get; set; }
        public int Id { get; set; }

        public void mostrardatos()
        {
            Console.WriteLine("Venta ID: " + Id.ToString());
            Console.WriteLine("Comentarios: " + Comentarios?.ToString());              
        }

    }
}
