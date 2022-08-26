namespace WebApplication1.Controllers.DTOs
{
    public class PostProducto
    {
        public string? Descripcion { get; set; }
        public int Costo { get; set; }
        public int PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }
    }

    public class PutProducto
    {
        public string? Descripcion { get; set; }
        public int Id { get; set; }
        public int Costo { get; set; }
        public int PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }
    }

}
