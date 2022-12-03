using Capa_Entidad;

namespace CoreCinCout
{
    public interface ICOSetting
    {
        Setting AppSettings();
        ClaimsIdent GetIdent(HttpContext httpContext);
    }
}
