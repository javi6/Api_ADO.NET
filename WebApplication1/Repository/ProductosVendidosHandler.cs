using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Repository
{
    public static class ProductosVendidosHandler
    {
        public const string QRY_PRODUCTOS_VENDIDOS = "SELECT * FROM ProductoVendido";
        public const string QRY_CARGA_PRODUCTO_VENDIDO = "INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) VALUES (@param_stock, @param_idproducto, @param_idventa)";
        public const string QRY_PRODUCTOS_ID = "SELECT Stock FROM Producto WHERE Id = @param_id";
        public const string QRY_MODIFICA_STOCK_PRODUCTO = "UPDATE Producto SET Stock = @param_stock WHERE Id = @param_id";
        public const string ConnectionString = "Server=G4X97D3;Database=SistemaGestion;Trusted_Connection=True";

        public static List<ProductoVendido> GetProductosVendidos()
        {
            List<ProductoVendido> prodVendidos = new List<ProductoVendido>();     // Lista para almacenar lo que trae desde la DB.

            // El using nos asegura liberar la memoria luego de usarlo.
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(QRY_PRODUCTOS_VENDIDOS, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductoVendido pvendido = new ProductoVendido();
                                pvendido.Id = Convert.ToInt32(reader["Id"]);
                                pvendido.Stock = Convert.ToInt32(reader["Stock"]);
                                pvendido.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                                pvendido.IdVenta = Convert.ToInt32(reader["IdVenta"]);
                                prodVendidos.Add(pvendido);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return prodVendidos;
        }

        // Toma la lista de productos y el Id de la venta.
        public static bool cargarProductosVendidos(List<ProductoVendido> productos, int idVenta)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                foreach (ProductoVendido productovendido in productos)
                {
                    if (validarId_Stock(productovendido))
                    {
                        SqlParameter param_stock = new SqlParameter("param_stock", SqlDbType.BigInt) { Value = productovendido.Stock };
                        SqlParameter param_idproducto = new SqlParameter("param_idproducto", SqlDbType.BigInt) { Value = productovendido.IdProducto };
                        SqlParameter param_idventa = new SqlParameter("param_idventa", SqlDbType.BigInt) { Value = idVenta };

                        sqlConnection.Open();
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(QRY_CARGA_PRODUCTO_VENDIDO, sqlConnection))
                            {
                                cmd.Parameters.Add(param_stock);
                                cmd.Parameters.Add(param_idproducto);
                                cmd.Parameters.Add(param_idventa);

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
                    else
                    {
                        break;
                    }
                }
                return resultado;
            }
        }

        public static bool validarId_Stock(ProductoVendido productovendido)
        {
            bool resultado = false;
            int stock = 0;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlParameter param_id = new SqlParameter("param_id", SqlDbType.BigInt) { Value = productovendido.IdProducto };

                //Consuñta stock por id de producto.
                using (SqlCommand cmd = new SqlCommand(QRY_PRODUCTOS_ID, sqlConnection))
                {
                    sqlConnection.Open();
                    cmd.Parameters.Add(param_id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            stock = Convert.ToInt32(reader["Stock"]);
                            // Si el stock actual del producto menos el stock del producto a vender es mayor o igual a cero se va a cargar la venta.
                            if ((stock - productovendido.Stock) >= 0)
                            {
                                descontarStockProducto((stock - productovendido.Stock), productovendido.IdProducto);
                                resultado = true;
                            }
                            else
                            {
                                Console.WriteLine("La cantidad de producto solicitada excede el stock actual.");
                                resultado = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("EL Id solicitado es inéxistente.");
                            resultado = false;
                        }
                        sqlConnection.Close();
                    }
                }
            }
            return resultado;
        }

        public static void descontarStockProducto(int stock, int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlParameter param_stock = new SqlParameter("param_stock", SqlDbType.BigInt) { Value = stock };
                SqlParameter param_id = new SqlParameter("param_id", SqlDbType.BigInt) { Value = id };

                //Actualiza el stock luego de una venta.
                using (SqlCommand cmd = new SqlCommand(QRY_MODIFICA_STOCK_PRODUCTO, sqlConnection))
                {
                    sqlConnection.Open();
                    cmd.Parameters.Add(param_stock);
                    cmd.Parameters.Add(param_id);

                    int affectedrows = cmd.ExecuteNonQuery();
                    if (affectedrows > 0)
                    {
                        Console.WriteLine("Stock de producto {0} actualizado: cantidad restante = {1}", id, stock);
                    }
                    else
                    {
                        Console.WriteLine("Stock sin cambios");
                    }
                    sqlConnection.Close();
                }


            }

        }
    }
}
