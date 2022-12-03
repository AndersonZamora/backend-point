using Capa_Entidad;
using Capa_Logica;
using Capa_Negocio;
using Capa_Validacion;
using CoreCinCout.helpers;

namespace CoreCinCout
{
    public class SCategoria : ICategoria
    {
        private readonly ICOSetting mSetting;
        private readonly IValidateSettings mValiSettings;
        private readonly ILoginCinCout mLoginCin;
        private readonly IConnection mConnection;
        private readonly ILogicaCate mLogicaCate;
        private readonly ICheckConnection mCheck;

        public SCategoria(
            ICOSetting mSetting,
            IValidateSettings mValiSettings,
            ILoginCinCout mLoginCin,
            IConnection mConnection,
            ILogicaCate mLogicaCate,
            ICheckConnection mCheck)
        {
            this.mSetting = mSetting;
            this.mValiSettings = mValiSettings;
            this.mLoginCin = mLoginCin;
            this.mConnection = mConnection;
            this.mLogicaCate = mLogicaCate;
            this.mCheck = mCheck;
        }

        public async Task<Response> CategoriaAdd(ECategoriaR rquest, HttpContext httpContext)
        {
            var user = mSetting.GetIdent(httpContext);
            var settings = mSetting.AppSettings();
            var respVs = mValiSettings.AppSettingValue(settings);
            if (!respVs.Ok) return respVs;

            var resCon = mCheck.Connection(settings.Conn);
            if (!resCon.Ok) return resCon;

            var admin = await mLoginCin.GetUsuarioAdmin(user.Email, settings.Conn);

            var cnn = mConnection.GetConnection(admin, settings.Hng);
            if (!cnn.Ok) return cnn;

            var resConUser = mCheck.Connection(cnn.Token);
            if (!resConUser.Ok) return resConUser;

            var resp = await mLogicaCate.CategoriaAdd(rquest, cnn.Token);
            if (!resp.Ok) return resp;

            return resp;
        }
    }
}
