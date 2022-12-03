using Capa_Entidad;

namespace Capa_Validacion
{
    public interface IValidarLoginRegister
    {
        Response ValidarLogin(Login request);
        Response ValidarRegisterCinCout(CinCoutRegister request, string code);
        Response ValidarRegisterUsuarioRegister(UsuarioAdminRegister request, string code);
    }
}
