using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Repository
{
    public static class APIHandler
    {
        public static string GetName()
        {
            return "Nombre de API: WEB API";
        }
    }
}
