

namespace WebApplication1
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Mail { get; set; }
        public string Contraseña { get; set; } = "Admin"; // Clave por defecto.

        // Compara la clave 
        public bool checkPassword(string clave)
        {
            if (clave.Equals(Contraseña))   // Si las claves coinciden devuelve true.
                return true;
            else
                return false;
        }

        // Uso método para escribir el atributo privado contraseña.
        // En futuras entregas se hará un chequeo más exhaustivo para corroborar
        // longitudes de clave, cantidad de intentos erróneos, etc.
        public void set_clave(string clave)
        {
            Contraseña = clave;
        }



    }
}
