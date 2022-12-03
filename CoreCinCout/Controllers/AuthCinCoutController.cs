using Capa_Entidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCinCout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthCinCoutController : ControllerBase
    {
        private readonly IHandleLogin mLogin;
        public AuthCinCoutController(IHandleLogin mLogin)
        {
            this.mLogin = mLogin;
        }

        [HttpPost("/cin/auth")]
        public async Task<ActionResult<Login>> Login(Login request)
        {
            try
            {
                var response = await mLogin.ResponseStatusLogin(request);
                if (!response.Ok) return StatusCode(500, new { Ok = false, msg = response.Msg });
           
                return StatusCode(201, new { ok = true, response.Token, response.Name });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ok = false, msg = "Por favor hable con el administrador" });
            }
        }

        [HttpPost("/cin/auth/new")]
        public async Task<ActionResult<CinCoutRegister>> Register(CinCoutRegister request)
        {
            try
            {
                var response = await mLogin.ResponseStatusRegister(request);
                if (!response.Ok) return StatusCode(500, new { Ok = false, msg = response.Msg });

                return StatusCode(201, new { ok = true, response.Token, response.Name, response.Rol });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ok = false, msg = "Por favor hable con el administrador" });
            }
        }

        [Authorize]
        [HttpPost("/auth/new")]
        public async Task<ActionResult<CinCoutRegister>> RegisterUser(UsuarioAdminRegister request)
        {
            try
            {
                var response = await mLogin.ResponseStatusUsuarioRegister(request, HttpContext);
                if (!response.Ok) return StatusCode(500, new { Ok = false, msg = response.Msg });

                return StatusCode(201, new { ok = true });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ok = false, msg = "Por favor hable con el administrador" });
            }
        }
    }
}
