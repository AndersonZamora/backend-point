using Capa_Entidad;

namespace Capa_Validacion
{
    public interface IValidateSettings
    {
        Response AppSettingValue(Setting setting);
        Response ClaimsIdentValue(ClaimsIdent ident);
    }
}
