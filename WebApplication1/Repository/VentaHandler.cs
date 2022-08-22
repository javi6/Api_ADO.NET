using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Repository
{
    public static class VentaHandler
    {
        public const string QRY_VENTA = "SELECT * FROM Venta";
        public const string QRY_CARGA_VENTA = "INSERT INTO Venta (Comentarios) VALUES (@param_comentarios)";
        public const string QRY_GET_ID_ULTIMA_VENTA = "SELECT * FROM Venta WHERE id=(SELECT max(id) FROM Venta)";
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

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                int idVenta = 0;
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
                            idVenta = GetIdVenta();     // Obtenemos Id de la venta.

                            if(idVenta != -1)
                            {   
                                // Carga los productos vendidos.
                                ProductosVendidosHandler.cargarProductosVendidos(productos, idVenta);
                                resultado = true;
                            }
                            else
                            {
                                resultado = false;
                            }
                            
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

        public static int GetIdVenta()
        {
            int idVenta = -1; 
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {                
                //Consulta Id de la última venta realizada.
                using (SqlCommand cmd = new SqlCommand(QRY_GET_ID_ULTIMA_VENTA, sqlConnection))
                {
                    sqlConnection.Open();
   
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            idVenta = Convert.ToInt32(reader["Id"]);   
                        }                                                
                    }
                    sqlConnection.Close();
                }
            }
            return idVenta;
        }
    }
}
