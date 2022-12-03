using Capa_Entidad;

namespace Capa_Negocio
{
    public interface ILoginUsuario
    {
        Task<bool> SearchEmail(string email, string dni, string conn);
        Task<Usuario> GetUser(string email, string conn);
        Task<int> PostUser(Usuario request, string conn);
    }
}
