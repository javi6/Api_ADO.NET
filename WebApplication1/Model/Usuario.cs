

namespace WebApplication1
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? _nombre { get; set; }
        public string? _apellido { get; set; }
        public string? _nombreUsuario { get; set; }
        public string? _mail { get; set; }
        private string _contraseña { get; set; } = "Admin"; // Clave por defecto.

        public void mostrardatos()
        {
            Console.WriteLine("ID de usuario: " + Id.ToString());
            Console.WriteLine("_nombre: " + _nombre);
            Console.WriteLine("_apellido: " + _apellido);
            Console.WriteLine("_nombreUsuario: " + _nombreUsuario);
            Console.WriteLine("mail: " + _mail);
        }

        // Compara la clave 
        public bool checkPassword(string clave)
        {
            if (clave.Equals(_contraseña))   // Si las claves coinciden devuelve true.
                return true;
            else
                return false;
        }

        // Uso método para escribir el atributo privado contraseña.
        // En futuras entregas se hará un chequeo más exhaustivo para corroborar
        // longitudes de clave, cantidad de intentos erróneos, etc.
        public void set_clave(string clave)
        {
            _contraseña = clave;
        }



    }
}
