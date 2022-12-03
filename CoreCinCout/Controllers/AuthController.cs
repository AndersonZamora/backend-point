using Capa_Entidad;
using Microsoft.AspNetCore.Mvc;

namespace CoreCinCout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHandleLogin mLogin;
        public AuthController(IHandleLogin mLogin)
        {
            this.mLogin = mLogin;
        }

        [HttpPost("/point/auth")]
        public async Task<ActionResult<Login>> LoginUsu(Login request)
        {
            try
            {
                var response = await mLogin.ResponseStatusLoginUser(request);
                if (!response.Ok) return StatusCode(500, new { Ok = false, msg = response.Msg });

                return StatusCode(201, new { ok = true, response.Token, response.Name });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ok = false, msg = "Por favor hable con el administrador" });
            }
        }
    }
}
