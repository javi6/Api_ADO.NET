

namespace Entregable_backend
{
    public class Usuario
    {
        public int Id { get; set; }
        public string _nombre { get; set; }
        public string _apellido { get; set; }
        public string _nombreUsuario { get; set; }
        public string _mail { get; set; }
        private string _contraseña { get; set; }

        public void mostrardatos()
        {
            Console.WriteLine("ID de usuario: " + Id.ToString());
            Console.WriteLine("_nombre: " + _nombre);
            Console.WriteLine("_apellido: " + _apellido);
            Console.WriteLine("_nombreUsuario: " + _nombreUsuario);
            Console.WriteLine("mail: " + _mail);
        }
    }
}
