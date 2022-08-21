using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Repository
{
    public static class VentaHandler
    {
        public const string QRY_VENTA = "SELECT * FROM Venta";
        public const string QRY_CARGA_VENTA = "INSERT INTO Venta (Comentarios) VALUES (@param_comentarios)";        
        public const string ConnectionString = "Server=G4X97D3;Database=SistemaGestion;Trusted_Connection=True";

        public static List<Venta> GetVentas()
        {
            List<Venta> ventas = new List<Venta>();     // Lista para almacenar lo que trae desde la DB.

            // El using nos asegura liberar la memoria luego de usarlo.
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(QRY_VENTA, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta = new Venta();
                                venta.Id = Convert.ToInt32(reader["Id"]);
                                venta.Comentarios = Convert.ToString(reader["Comentarios"]);
                                ventas.Add(venta);                                
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return ventas;
        }

        public static bool CargarVenta(List<ProductoVendido> productos, int idUsuarioVendedor)
        {
            bool resultado = false;

            // Validamos si en la lista de productos vendidos al menos alguno de ellos es correcto como para 
            // registrar una venta exitosa.
            if(ProductosVendidosHandler.cargarProductosVendidos(productos, idUsuarioVendedor))
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    SqlParameter param_comentarios = new SqlParameter("param_comentarios", SqlDbType.VarChar) { Value = "Nueva venta de usuario: " + idUsuarioVendedor };

                    sqlConnection.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(QRY_CARGA_VENTA, sqlConnection))
                        {
                            cmd.Parameters.Add(param_comentarios);

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
    }
}
