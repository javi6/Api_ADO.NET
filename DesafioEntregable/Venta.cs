
namespace DesafioEntregable
{
    internal class Venta
    {
        public int Id { get; set; }
        public string? Comentarios { get; set; }

        public Venta(int id, string comentarios)
        {
            Id = id;
            Comentarios = comentarios;
        }

        public void mostrardatos()
        {
            Console.WriteLine("Venta ID: " + Id.ToString());
            Console.WriteLine("Comentarios: "+ Comentarios);
        }
    }
}
