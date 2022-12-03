using Capa_Entidad;
using Capa_Negocio;
using Capa_Validacion;
using CoreCinCout.helpers;

namespace CoreCinCout
{
    public class SHandleLogin : IHandleLogin
    {
        private readonly IValidarLoginRegister mValidateRL;
        private readonly ICreateHash mHash;
        private readonly ITokenCreate mToken;
        private readonly ICOSetting mSetting;
        private readonly ILoginCinCout mLoginCin;
        private readonly IValidateSettings mValiSettings;
        private readonly ICheckConnection mCheck;
        public SHandleLogin(
            IValidarLoginRegister mValidateRL,
            ICreateHash mHash,
            ITokenCreate mToken,
            ICOSetting mSetting,
            ILoginCinCout mLoginCin,
            IValidateSettings mValiSettings,
            ICheckConnection mCheck)
        {
            this.mValidateRL = mValidateRL;
            this.mHash = mHash;
            this.mToken = mToken;
            this.mSetting = mSetting;
            this.mLoginCin = mLoginCin;
            this.mValiSettings = mValiSettings;
            this.mCheck = mCheck;
        }

        public async Task<Response> ResponseStatusLogin(Login request)
        {
            var settings = mSetting.AppSettings();
            var respVs = mValiSettings.AppSettingValue(settings);
            if (!respVs.Ok) return respVs;

            var resCon = mCheck.Connection(settings.Conn);
            if (!resCon.Ok) return resCon;

            var loRes = mValidateRL.ValidarLogin(request);
            if (!loRes.Ok) return loRes;

            var resS = await mLoginCin.SearchEmail(request.Email, settings.Conn);
            if (!resS) return new Response() { Ok = false, Msg = "Credenciales incorrectas" };

            var user = await mLoginCin.GetUser(request.Email, settings.Conn);
            if (user == null) return new Response() { Ok = false, Msg = "Something went wrong error code 02, contact CinCout" };
            if (string.IsNullOrEmpty(user.Password)) return new Response() { Ok = false, Msg = "Something went wrong error code 542, contact CinCout" };

            var hasRes = mHash.PasswordDecrypt(user.Password, settings.Hng);
            if (string.IsNullOrEmpty(hasRes)) return new Response() { Ok = false, Msg = "Credenciales incorrectas" };
            if (!request.Password.Equals(hasRes)) return new Response() { Ok = false, Msg = "Credenciales incorrectas" };

            var ident = mLoginCin.CompleteIdent(user);
            var respVc = mValiSettings.ClaimsIdentValue(ident);
            if (!respVc.Ok) return respVc;

            var token = mToken.TokenCreate(ident, settings.Jwt);
            if (string.IsNullOrEmpty(token)) return new Response() { Ok = false, Msg = "Something went wrong error code 532, contact CinCout" };

            return new() { Ok = true, Token = token, Name = user.FullName };
        }

        public async Task<Response> ResponseStatusLoginUser(Login request)
        {
            var settings = mSetting.AppSettings();
            var respVs = mValiSettings.AppSettingValue(settings);
            if (!respVs.Ok) return respVs;

            var resCon = mCheck.Connection(settings.Conn);
            if (!resCon.Ok) return resCon;

            var loRes = mValidateRL.ValidarLogin(request);
            if (!loRes.Ok) return loRes;

            var resS = await mLoginCin.SearchEmailUsuarioAdmin(request.Email, settings.Conn);
            if (!resS) return new Response() { Ok = false, Msg = "Credenciales incorrectas" };

            var user = await mLoginCin.GetUsuarioAdmin(request.Email, settings.Conn);
            if (user == null) return new Response() { Ok = false, Msg = "Something went wrong error code 02, contact CinCout" };
            if (string.IsNullOrEmpty(user.Password)) return new Response() { Ok = false, Msg = "Something went wrong error code 542, contact CinCout" };

            var hasRes = mHash.PasswordDecrypt(user.Password, settings.Hng);
            if (string.IsNullOrEmpty(hasRes)) return new Response() { Ok = false, Msg = "Credenciales incorrectas" };
            if (!request.Password.Equals(hasRes)) return new Response() { Ok = false, Msg = "Credenciales incorrectas" };

            var ident = mLoginCin.CompleteIdent(user);
            var respVc = mValiSettings.ClaimsIdentValue(ident);
            if (!respVc.Ok) return respVc;

            var token = mToken.TokenCreate(ident, settings.Jwt);
            if (string.IsNullOrEmpty(token)) return new Response() { Ok = false, Msg = "Something went wrong error code 532, contact CinCout" };

            return new() { Ok = true, Token = token, Name = user.FullName };
        }

        public async Task<Response> ResponseStatusRegister(CinCoutRegister request)
        {
            var settings = mSetting.AppSettings();
            var respVs = mValiSettings.AppSettingValue(settings);
            if (!respVs.Ok) return respVs;

            var resCon = mCheck.Connection(settings.Conn);
            if (!resCon.Ok) return resCon;

            var resCin = mValidateRL.ValidarRegisterCinCout(request, settings.State);
            if (!resCin.Ok) return resCin;

            var resSe = await mLoginCin.SearchEmail(request.Email, settings.Conn);
            if (resSe) return new Response() { Ok = false, Msg = "Something went wrong error code 1036, contact CinCout" };

            var passHs = mHash.CreatePasswordEncrypt(request.Password, settings.Hng);
            if(string.IsNullOrEmpty(passHs)) return new Response() { Ok = false, Msg = "Something went wrong error code 8670, contact CinCout" };

            request.Password = passHs;
            var regsCi = await mLoginCin.PostCinCout(request, settings.Conn);
            if (regsCi <= 0) return new Response() { Ok = false, Msg = "Something went wrong error code 2568, contact CinCout" };

            var ident = mLoginCin.CompleteIdent(request, regsCi);
            var crToken = mToken.TokenCreate(ident, settings.Jwt);
            if (string.IsNullOrEmpty(crToken)) return new Response() { Ok = false, Msg = "UsuarioAdmin registrado correctamente" };

            return new() { Ok = true, Name = request.FullName, Token = crToken, Rol = request.IdRol };
        }

        public async Task<Response> ResponseStatusUsuarioRegister(UsuarioAdminRegister request, HttpContext httpContext)
        {
            var user = mSetting.GetIdent(httpContext);

            var settings = mSetting.AppSettings();
            var respVs = mValiSettings.AppSettingValue(settings);
            if (!respVs.Ok) return respVs;

            var resCon = mCheck.Connection(settings.Conn);
            if (!resCon.Ok) return resCon;

            var getUser = await mLoginCin.GetUser(user.Email, settings.Conn);
            if (getUser == null) return new Response() { Ok = false, Msg = "Something went wrong error code 02, contact CinCout" };
            if (getUser.IdRol != 6) return new Response() { Ok = false, Msg = "Something went wrong error code 205, contact CinCout" };

            var respVr = mValidateRL.ValidarRegisterUsuarioRegister(request, settings.State);
            if (!respVr.Ok) return respVr;

            var rsSe = await mLoginCin.SearchEmailUsuarioAdmin(request.Email, settings.Conn);
            if (rsSe) return new Response() { Ok = false, Msg = "Something went wrong error code 1036, contact CinCout" };

            var passHs = mHash.CreatePasswordEncrypt(request.Password, settings.Hng);
            if (string.IsNullOrEmpty(passHs)) return new Response() { Ok = false, Msg = "Something went wrong error code 8670, contact CinCout" };
            request.Password = passHs;

            var dataHs = mHash.CreatePasswordEncrypt(request.Data, settings.Hng);
            if (string.IsNullOrEmpty(dataHs)) return new Response() { Ok = false, Msg = "Something went wrong error code 8670, contact CinCout" };
            request.Data = dataHs;

            var initialHs = mHash.CreatePasswordEncrypt(request.Initial, settings.Hng);
            if (string.IsNullOrEmpty(initialHs)) return new Response() { Ok = false, Msg = "Something went wrong error code 8670, contact CinCout" };
            request.Initial = initialHs;

            var userHs = mHash.CreatePasswordEncrypt(request.User, settings.Hng);
            if (string.IsNullOrEmpty(userHs)) return new Response() { Ok = false, Msg = "Something went wrong error code 8670, contact CinCout" };
            request.User = userHs;

            var contraseniaHs = mHash.CreatePasswordEncrypt(request.Contrasenia, settings.Hng);
            if (string.IsNullOrEmpty(contraseniaHs)) return new Response() { Ok = false, Msg = "Something went wrong error code 8670, contact CinCout" };
            request.Contrasenia = contraseniaHs;

            var UsuernameHs = mHash.CreatePasswordEncrypt(request.Usuername, settings.Hng);
            if (string.IsNullOrEmpty(UsuernameHs)) return new Response() { Ok = false, Msg = "Something went wrong error code 8670, contact CinCout" };
            request.Usuername = UsuernameHs;

            var resp = await mLoginCin.PostUsuarioAdmin(request, settings.Conn);
            if (resp <= 0) return new Response() { Ok = false, Msg = "Something went wrong error code 2568, contact CinCout" };

            return new() { Ok = true };
        }
    }
}
