
using System.Data.SqlClient;

namespace Entregable_backend.ADO
{
    public class ProductosVendidosHandler
    {
        public const string QRY_PRODUCTOS_VENDIDOS = "SELECT * FROM ProductoVendido";

        public const string ConnectionString = "Server=G4X97D3;Database=SistemaGestion;Trusted_Connection=True";

        public List<ProductoVendido> GetProductosVendidos()
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
                                pvendido.mostrardatos();
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return prodVendidos;
        }
    }
}
