using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Repository
{
    public static class ProductoHandler
    {
        public const string QRY_PRODUCTOS = "SELECT * FROM Producto";
        public const string QRY_USUARIOS_ID = "SELECT Id FROM Usuario";
        public const string QRY_PRODUCTOS_ID = "SELECT Id FROM Producto";
        public const string QRY_DELETE_PRODUCTOVENDIDO_Y_PRODUCTO_BY_ID = "DELETE FROM ProductoVendido WHERE IdProducto = @idProducto DELETE FROM Producto WHERE Id = @idProducto";
        public const string QRY_CREA_PRODUCTO = "INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES (@param_desc, @param_costo, @param_precio_venta, @param_stock, @param_idusuario)";
        public const string QRY_MODIFICA_PRODUCTOO = "UPDATE Producto SET Descripciones = @param_desc, Costo = @param_costo, PrecioVenta = @param_precio_venta, Stock =@param_stock, IdUsuario = @param_idusuario WHERE Id = @param_id";        
        public const string ConnectionString = "Server=G4X97D3;Database=SistemaGestion;Trusted_Connection=True";

        public static List<Producto> GetProductos()
        {
            List<Producto> products = new List<Producto>();     // Lista para almacenar lo que trae desde la DB.

            // El using nos asegura liberar la memoria luego de usarlo.
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(QRY_PRODUCTOS, sqlConnection))
                {
                    sqlConnection.Open();

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
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return products;
        }

        public static bool DeleteProducto(int idProducto)
        {
            bool resultado = false;
            int affectedrows = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = "@idProducto";
                parametro.SqlDbType = SqlDbType.BigInt;
                parametro.Value = idProducto;

                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand(QRY_DELETE_PRODUCTOVENDIDO_Y_PRODUCTO_BY_ID, sqlConnection))
                {
                    cmd.Parameters.Add(parametro);
                    affectedrows = cmd.ExecuteNonQuery();
                    if (affectedrows > 0)
                    {
                        Console.WriteLine("Producto con ID: {0} modificado.", idProducto);
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }

        public static bool CreaProducto(Producto producto)
        {
            bool resultado = false;          

            if (validaProducto(producto))                // Valida los parámetros pasados a producto antes de hacer la conexión.
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    SqlParameter param_desc = new SqlParameter("param_desc", SqlDbType.VarChar) { Value = producto.Descripcion };
                    SqlParameter param_costo = new SqlParameter("param_costo", SqlDbType.Money) { Value = producto.Costo };
                    SqlParameter param_precio_venta = new SqlParameter("param_precio_venta", SqlDbType.Money) { Value = producto.PrecioVenta };
                    SqlParameter param_stock = new SqlParameter("param_stock", SqlDbType.Int) { Value = producto.Stock };
                    SqlParameter param_idusuario = new SqlParameter("param_idusuario", SqlDbType.BigInt) { Value = producto.IdUsuario };
                    sqlConnection.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(QRY_CREA_PRODUCTO, sqlConnection))
                        {
                            cmd.Parameters.Add(param_desc);
                            cmd.Parameters.Add(param_costo);
                            cmd.Parameters.Add(param_precio_venta);
                            cmd.Parameters.Add(param_stock);
                            cmd.Parameters.Add(param_idusuario);

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
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }

        public static bool ModificaProducto(Producto producto)
        {
            bool resultado = false;
            List<int> productos_id_list = obtenerListaId(QRY_PRODUCTOS_ID);     // Obtiene una lista de los Id de productos existentes en la base de datos.
            List<int> usuarios_id_list = obtenerListaId(QRY_USUARIOS_ID);       // Obtiene una lista de los Id de usuarios existentes en la base de datos.

            if(productos_id_list.Contains(producto.Id))                         // Si el id de producto existe.
            {
                if (validaProducto(producto))                // Valida los parámetros pasados a producto antes de hacer la conexión.
                {
                    if (usuarios_id_list.Contains(producto.IdUsuario))   // Valida que el id de usuario ingresado exista.
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                        {
                            SqlParameter param_desc = new SqlParameter("param_desc", SqlDbType.VarChar) { Value = producto.Descripcion };
                            SqlParameter param_costo = new SqlParameter("param_costo", SqlDbType.Money) { Value = producto.Costo };
                            SqlParameter param_precio_venta = new SqlParameter("param_precio_venta", SqlDbType.Money) { Value = producto.PrecioVenta };
                            SqlParameter param_stock = new SqlParameter("param_stock", SqlDbType.Int) { Value = producto.Stock };
                            SqlParameter param_idusuario = new SqlParameter("param_idusuario", SqlDbType.BigInt) { Value = producto.IdUsuario };
                            SqlParameter param_id = new SqlParameter("param_id", SqlDbType.BigInt) { Value = producto.Id };

                            sqlConnection.Open();

                            using (SqlCommand cmd = new SqlCommand(QRY_MODIFICA_PRODUCTOO, sqlConnection))
                            {
                                cmd.Parameters.Add(param_id);
                                cmd.Parameters.Add(param_desc);
                                cmd.Parameters.Add(param_costo);
                                cmd.Parameters.Add(param_precio_venta);
                                cmd.Parameters.Add(param_stock);
                                cmd.Parameters.Add(param_idusuario);
                                try
                                {
                                    int affectedrows = cmd.ExecuteNonQuery();
                                    if (affectedrows > 0)
                                    {
                                        resultado = true;
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
                    }
                    else
                    {
                        Console.WriteLine("Usuario inexistente.");
                        resultado = false;
                    }
                }
                else
                {                    
                    resultado = false;
                }
            }
            else
            {
                Console.WriteLine("Id de producto inválido.");
                resultado = false;
            }

            return resultado;
        }

        public static List<int> obtenerListaId(string qry)
        {
            List<int> id_list = new List<int>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(qry, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                id_list.Add(Convert.ToInt32(reader["Id"]));
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return id_list;
        }

        public static bool validaProducto(Producto producto)
        {
            bool resultado = false;

            if (producto.Stock <= 0)
            {
                resultado = false;
                Console.WriteLine("El stock de un producto a ingresar no debe ser negativo ni nulo.");
            }
            else
            {
                if (producto.PrecioVenta <= producto.Costo)
                {
                    resultado = false;
                    Console.WriteLine("El precio de venta no debe ser menor al valor de costo.");
                }
                else
                {
                    resultado = true;
                }
            }

            return resultado;
        }

    }
}
