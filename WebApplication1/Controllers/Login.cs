using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpGet("{nombre}/{clave}")]
        public Usuario Login(string nombre, string clave)
        {
            return UsuarioHandler.GetUsuariobyClave(nombre, clave);
        }
    }
}
