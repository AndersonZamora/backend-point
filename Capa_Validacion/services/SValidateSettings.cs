using Capa_Entidad;

namespace Capa_Validacion
{
    public class SValidateSettings : IValidateSettings
    {
        public Response AppSettingValue(Setting setting)
        {
            if (setting == null) return new Response() { Ok = false, Msg = "Something went wrong error code 227, contact CinCout" };
            if (string.IsNullOrEmpty(setting.Conn)) return new Response() { Ok = false, Msg = "Something went wrong error code 101, contact CinCout" };
            if (string.IsNullOrEmpty(setting.Hng)) return new Response() { Ok = false, Msg = "Something went wrong error code 111, contact CinCout" };
            if (string.IsNullOrEmpty(setting.Jwt)) return new Response() { Ok = false, Msg = "Something went wrong error code 413, contact CinCout" };

            return new Response() { Ok = true };
        }

        public Response ClaimsIdentValue(ClaimsIdent ident)
        {
            if (ident == null) return new Response() { Ok = false, Msg = "Something went wrong error code 277, contact CinCout" };
            if (ident.Id <= 0) return new Response() { Ok = false, Msg = "Something went wrong error code 277, contact CinCout" };
            if (string.IsNullOrEmpty(ident.Names)) return new Response() { Ok = false, Msg = "Something went wrong error code 277, contact CinCout" };

            return new Response() { Ok = true };
        }
    }
}
