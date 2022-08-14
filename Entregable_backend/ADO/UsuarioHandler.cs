using System.Data.SqlClient;

namespace Entregable_backend.ADO
{
    public class UsuarioHandler
    {
        //public const string QRY = "SELECT * FROM Producto WHERE Id = @idProducto";
        public const string QRY_USUARIOS = "SELECT * FROM Usuario";
        //public const string QRY3 = "insert into Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) values('hhhh', 'ttttt', 'rtrtrtr', '343434', 'uuuuuu@gmail.com')";

        public const string ConnectionString = "Server=G4X97D3;Database=SistemaGestion;Trusted_Connection=True";

        public List<Usuario> GetUsuarios()
        {
            List<Usuario> users = new List<Usuario>();     // Lista para almacenar lo que trae desde la DB.

            // El using nos asegura liberar la memoria luego de usarlo.
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(QRY_USUARIOS, sqlConnection))
                {
                    sqlConnection.Open();
                    //int idProducto = 3;
                    // Con sql parameters le explicitamos que le vamos a pasar en la query para evitar sql injection.
                    //SqlParameter parametro = new SqlParameter();
                    //parametro.ParameterName = "@idProducto";
                    //parametro.SqlDbType = SqlDbType.Int;
                    //parametro.Value = idProducto;
                    //cmd.Parameters.Add(parametro);

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
    }
}
