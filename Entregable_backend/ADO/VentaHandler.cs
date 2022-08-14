
using System.Data.SqlClient;


namespace Entregable_backend.ADO
{
    public class VentaHandler
    {
        public const string QRY_VENTA = "SELECT * FROM Venta";

        public const string ConnectionString = "Server=G4X97D3;Database=SistemaGestion;Trusted_Connection=True";

        public List<Venta> GetVentas()
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
                                venta.mostrardatos();
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return ventas;
        }
    }
}
