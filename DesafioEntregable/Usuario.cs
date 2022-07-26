
namespace DesafioEntregable
{
    internal class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        // Sólo se escribe mediante constructor y método.
        private string Clave { get; set; }

        // Constructor.
        public Usuario(int id, string nombre, string apellido, string mail)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;

            Console.WriteLine("Ingrese la contraseña, mínimo 4 caracteres:");
            Clave = obtieneClave();
        }

        public void mostrardatos()
        {
            Console.WriteLine("Usuario ID: " + Id.ToString());
            Console.WriteLine("Nombre de Usuario: " + Nombre);
            Console.WriteLine("Apellido de Usuario: " + Apellido);
            Console.WriteLine("Mail de Usuario: " + Mail);
        }

        // Toma la clave del teclado y corrobora mediante otro método
        // que lo que se ingresó sea apropiado.
        private string obtieneClave()
        {
            bool longClaveOk = false;
            ConsoleKeyInfo cki;
            string code = "";
            int intentos = 3;
            
            do
            {
                // Elimino la visualización por consola para ocultar la clave.
                cki = Console.ReadKey(true);
                code += cki.KeyChar.ToString(); // Agrego caracter a la cadena.
                Console.Write("*");             // Imprimo azterisco para no exponer la clave.

                if (cki.Key == ConsoleKey.Enter)    // Si presionó enter.
                {
                    longClaveOk = checkLongClave(code);   // ¿La clave pasa las condiciones?
                    code = "";                    
                    intentos--;
                    if(intentos >= 1)
                    {
                        Console.WriteLine("\n\rContraseña demasiado corta, intente nuevamente\n\r");
                        Console.WriteLine("Intentos restantes: {0}\n\r", intentos);
                    }
                    else
                    {
                        Console.WriteLine("Cantidad de intentos agotados, cuenta bloqueada ¡¡");
                    }
                    
                }

            } while (longClaveOk == false && intentos > 0);
          
        }

        // Corrobora longitud de la clave y la escribe.
        private bool checkLongClave(string code)
        {
            if (code.Length <= 3)   // Se asegura que tenga un mínimo de 4 caracteres.
            {
                return false;
            }
            else
            {
                return true;
            }                   
        }

    }
}
