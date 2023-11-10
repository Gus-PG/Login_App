using Microsoft.EntityFrameworkCore;
using Login_App.Models;
using Login_App.Servicios.Contrato;

namespace Login_App.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly BdloginContext _bdContext;

        public UsuarioService(BdloginContext bdContext)
        {
            _bdContext = bdContext;
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuario_encontrado = await _bdContext.Usuarios
                .Where(u => u.Correo == correo && u.Clave == clave)
                .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _bdContext.Usuarios.Add(modelo);
            await _bdContext.SaveChangesAsync();
            return modelo;
        }
    }
}
