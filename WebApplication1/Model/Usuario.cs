

namespace WebApplication1
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? _nombre { get; set; }
        public string? _apellido { get; set; }
        public string? _nombreUsuario { get; set; }
        public string? _mail { get; set; }
        public string _contraseña { get; set; } = "Admin"; // Clave por defecto.

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
