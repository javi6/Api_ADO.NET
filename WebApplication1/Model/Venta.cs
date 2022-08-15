
namespace WebApplication1
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
