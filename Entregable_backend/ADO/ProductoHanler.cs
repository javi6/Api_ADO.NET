
using System.Data.SqlClient;

namespace Entregable_backend.ADO
{
    public class ProductoHandler
    {
        //public const string QRY = "SELECT * FROM Producto WHERE Id = @idProducto";
        public const string QRY_PRODUCTOS = "SELECT * FROM Producto";
        //public const string QRY3 = "insert into Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) values('hhhh', 'ttttt', 'rtrtrtr', '343434', 'uuuuuu@gmail.com')";

        public const string ConnectionString = "Server=G4X97D3;Database=SistemaGestion;Trusted_Connection=True";

        public List<Producto> GetProductos()
        {
            List<Producto> products = new List<Producto>();     // Lista para almacenar lo que trae desde la DB.

            // El using nos asegura liberar la memoria luego de usarlo.
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {                
                using (SqlCommand cmd = new SqlCommand(QRY_PRODUCTOS, sqlConnection))
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
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(reader["Id"]);
                                producto.Descripcion = Convert.ToString(reader["Descripciones"]);
                                producto.PrecioVenta = Convert.ToInt32(reader["PrecioVenta"]);
                                producto.Costo = Convert.ToInt32(reader["Costo"]);
                                producto.Stock = Convert.ToInt32(reader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                                
                                products.Add(producto);
                                producto.mostrardatos();
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return products;
        }

        //public void UsarDataAdapter()
        //{
        //    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
        //    {
        //        SqlDataAdapter sqlDataApapter = new SqlDataAdapter(QRY2, ConnectionString);
        //        sqlConnection.Open();
        //        DataSet resultado = new DataSet();
        //        sqlDataApapter.Fill(resultado);
        //        Console.WriteLine();
        //        sqlConnection.Close();

        //    }
        //}
    }
}
