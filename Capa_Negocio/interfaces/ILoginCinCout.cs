using Capa_Entidad;
namespace Capa_Negocio
{
    public interface ILoginCinCout
    {
        Task<bool> SearchEmail(string email, string conn);
        Task<bool> SearchEmailUsuarioAdmin(string email, string conn);
        Task<CinCout> GetUser(string email, string conn);
        Task<UsuarioAdmin> GetUsuarioAdmin(string email, string conn);
        Task<int> PostCinCout(CinCoutRegister request, string conn);
        Task<int> PostUsuarioAdmin(UsuarioAdminRegister request, string conn);
        ClaimsIdent CompleteIdent(CinCoutRegister request, int id);
        ClaimsIdent CompleteIdent(UsuarioAdmin request);
        ClaimsIdent CompleteIdent(CinCout request);
    }
}
