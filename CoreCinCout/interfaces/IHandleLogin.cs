using Capa_Entidad;

namespace CoreCinCout
{
    public interface IHandleLogin
    {
        Task<Response> ResponseStatusLogin(Login request);
        Task<Response> ResponseStatusLoginUser(Login request);
        Task<Response> ResponseStatusRegister(CinCoutRegister request);
        Task<Response> ResponseStatusUsuarioRegister(UsuarioAdminRegister request, HttpContext httpContext);
    }
}
