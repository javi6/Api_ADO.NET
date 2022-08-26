using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Repository
{
    public static class UsuarioHandler
    {
        public const string QRY_USUARIOS = "SELECT * FROM Usuario";
        public const string QRY_USUARIO_BY_USERNAME = "SELECT * FROM Usuario WHERE NombreUsuario = @user";
        public const string QRY_DELETE_USUARIO_BY_ID = "DELETE FROM Usuario WHERE Id = @id";
        public const string QRY_CREA_USUARIO = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES (@param_nombre, @param_apellido, @param_nombreusuario, @param_clave, @param_mail)";
        public const string QRY_MODIFICA_USUARIO = "UPDATE Usuario SET Nombre = @param_nombre, Apellido = @param_apellido, NombreUsuario = @param_nombreusuario, Contraseña = @param_clave, Mail = @param_mail WHERE Id = @param_id";
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
                                user.Nombre = Convert.ToString(reader["Nombre"]);
                                user.Apellido = Convert.ToString(reader["Apellido"]);
                                user.NombreUsuario = Convert.ToString(reader["NombreUsuario"]);
                                user.Mail = Convert.ToString(reader["Mail"]);

                                users.Add(user);
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
                                    usuario_byusername.Nombre = Convert.ToString(reader["Nombre"]);
                                    usuario_byusername.Apellido = Convert.ToString(reader["Apellido"]);
                                    usuario_byusername.NombreUsuario = Convert.ToString(reader["NombreUsuario"]);
                                    usuario_byusername.Mail = Convert.ToString(reader["Mail"]);
                                }
                                // Silas claves no coinciden devuelvo objeto nulo para no exponer datos.
                                else
                                {
                                    usuario_byusername = null;
                                    usuario_byusername.Contraseña = null;
                                }
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return usuario_byusername;
        }

        public static bool DeleteUsuario(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = "@id";
                parametro.SqlDbType = SqlDbType.BigInt;
                parametro.Value = id;

                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand(QRY_DELETE_USUARIO_BY_ID, sqlConnection))
                {
                    cmd.Parameters.Add(parametro);
                    int affectedrows = cmd.ExecuteNonQuery();
                    if (affectedrows > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }

        public static bool CrearUsuario(Usuario usuario)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlParameter param_nombre = new SqlParameter("param_nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter param_apellido = new SqlParameter("param_apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter param_nombreusuario = new SqlParameter("param_nombreusuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter param_clave = new SqlParameter("param_clave", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter param_mail = new SqlParameter("param_mail", SqlDbType.VarChar) { Value = usuario.Mail };

                sqlConnection.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(QRY_CREA_USUARIO, sqlConnection))
                    {
                        cmd.Parameters.Add(param_nombre);
                        cmd.Parameters.Add(param_apellido);
                        cmd.Parameters.Add(param_nombreusuario);
                        cmd.Parameters.Add(param_clave);
                        cmd.Parameters.Add(param_mail);

                        int affectedrows = cmd.ExecuteNonQuery();
                        if (affectedrows > 0)
                        {
                            resultado = true;
                        }
                    }
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                    resultado = false;
                }
            }
            return resultado;
        }

        public static bool ModificaUsuario(Usuario usuario)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlParameter param_nombre = new SqlParameter("param_nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter param_apellido = new SqlParameter("param_apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter param_nombreusuario = new SqlParameter("param_nombreusuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter param_clave = new SqlParameter("param_clave", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter param_mail = new SqlParameter("param_mail", SqlDbType.VarChar) { Value = usuario.Mail };
                SqlParameter param_id = new SqlParameter("param_id", SqlDbType.BigInt) { Value = usuario.Id };

                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand(QRY_MODIFICA_USUARIO, sqlConnection))
                {
                    cmd.Parameters.Add(param_nombre);
                    cmd.Parameters.Add(param_id);
                    cmd.Parameters.Add(param_apellido);
                    cmd.Parameters.Add(param_nombreusuario);
                    cmd.Parameters.Add(param_clave);
                    cmd.Parameters.Add(param_mail);
                    try
                    {
                        int affectedrows = cmd.ExecuteNonQuery();
                        if (affectedrows > 0)
                        {
                            Console.WriteLine("Modificación de usuario exitosa.");
                            resultado = true;
                        }
                        else
                        {
                            Console.WriteLine("Usuario inválido.");
                            resultado = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR: " + ex.Message);
                        resultado = false;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }

    }
}

