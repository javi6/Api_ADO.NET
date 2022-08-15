using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Repository
{
    public static class UsuarioHandler
    {        
        public const string QRY_USUARIOS = "SELECT * FROM Usuario";
        public const string QRY_USUARIO_BY_USERNAME = "SELECT * FROM Usuario WHERE NombreUsuario = @user";
        public const string ConnectionString = "Server=G4X97D3;Database=SistemaGestion;Trusted_Connection=True";

        public static List<Usuario> GetUsuarios()
        {
            List<Usuario> users = new List<Usuario>();     // Lista para almacenar lo que trae desde la DB.

            // El using nos asegura liberar la memoria luego de usarlo.
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(QRY_USUARIOS, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Usuario user = new Usuario();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user._nombre = Convert.ToString(reader["Nombre"]);
                                user._apellido = Convert.ToString(reader["Apellido"]);
                                user._nombreUsuario = Convert.ToString(reader["NombreUsuario"]);
                                user._mail = Convert.ToString(reader["Mail"]);                          

                                users.Add(user);
                                user.mostrardatos();
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return users;
        }

        // Este método toma dos strings que son el username y la clave en el login del frontend.
        public static Usuario GetUsuariobyClave(string username_from_login, string userpass_from_login)
        {
            Usuario? usuario_byusername = new Usuario();
            // El using nos asegura liberar la memoria luego de usarlo.
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(QRY_USUARIO_BY_USERNAME, sqlConnection))
                {
                    sqlConnection.Open();
                    // Con sql parameters le explicitamos que le vamos a pasar en la query para evitar sql injection.
                    SqlParameter parametro = new SqlParameter();
                    parametro.ParameterName = "@user";
                    parametro.SqlDbType = SqlDbType.VarChar;

                    // Le paso el parámetro username leído desde el frontend login para que consulte en la base de datos.
                    parametro.Value = username_from_login;

                    cmd.Parameters.Add(parametro);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) // Si el username fue erróneo en el login, la tabla va a estar vacía y no ingresa al if.
                        {
                            while (reader.Read())
                            {
                                // Si el username es correcto guardo en el objeto usuario
                                // la clave verdadera traída desde la base de datos para compararla.
                                usuario_byusername.set_clave(Convert.ToString(reader["Contraseña"]));

                                // Si las claves coinciden (userpass_from_login == Contraseña)
                                // cargo todos los parámetros del objeto para luego retornar el Usuario.
                                if (usuario_byusername.checkPassword(userpass_from_login))
                                {
                                    usuario_byusername.Id = Convert.ToInt32(reader["Id"]);
                                    usuario_byusername._nombre = Convert.ToString(reader["Nombre"]);
                                    usuario_byusername._apellido = Convert.ToString(reader["Apellido"]);
                                    usuario_byusername._nombreUsuario = Convert.ToString(reader["NombreUsuario"]);
                                    usuario_byusername._mail = Convert.ToString(reader["Mail"]);
                                }
                                // Silas claves no coinciden devuelvo objeto nulo para no exponer datos.
                                else
                                {
                                    usuario_byusername = null;
                                }                                
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return usuario_byusername;
        }
    }
}
