
using System.Data.SqlClient;

namespace WebApplication1.Repository
{
    public static class ProductoHandler
    {        
        public const string QRY_PRODUCTOS = "SELECT * FROM Producto"; 
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
                                producto.mostrardatos();
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return products;
        }
    }
}
