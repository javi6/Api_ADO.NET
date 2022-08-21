using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.DTOs;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController: ControllerBase
    {
        [HttpGet(Name = "Obtiene los usuarios")]
        public List<Usuario> GetUsuarios()
        {
            return UsuarioHandler.GetUsuarios(); // Implementa el usuario handler.
        }

        [HttpDelete(Name = "Borra usuario")]
        public bool DeleteUsuario([FromBody] int id)
        {
            try
            {
                return UsuarioHandler.DeleteUsuario(id);
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
                return false;
            }
            
        }

        [HttpPut]
        public bool ModificaUsuario([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.ModificaUsuario(new Usuario
            {
                Id = usuario.Id,
                _apellido = usuario.Apellido,
                _mail = usuario.Mail,
                _nombreUsuario = usuario.NombreUsuario,
                _contraseña = usuario.Contraseña,
                _nombre = usuario.Nombre
            });
        }

        [HttpPost]
        public bool CrearUsuario([FromBody] PostUsuario usuario)
        {
            try
            {
                return UsuarioHandler.CrearUsuario(new Usuario
                {
                    _nombre = usuario.Nombre,
                    _apellido = usuario.Apellido,
                    _mail = usuario.Mail,
                    _nombreUsuario = usuario.NombreUsuario,
                    _contraseña = usuario.Contraseña
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return false;
            }
        }
    }
}
