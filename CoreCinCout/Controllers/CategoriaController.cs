using Capa_Entidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCinCout.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly ICategoria mCategoria;
        public CategoriaController(ICategoria mCategoria)
        {
            this.mCategoria = mCategoria;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("/category")]
        public async Task<ActionResult<ECategoriaR>> Create([FromBody] ECategoriaR Request)
        {
            try
            {
                var resp = await mCategoria.CategoriaAdd(Request, HttpContext);
                if (!resp.Ok) return StatusCode(500, new { Ok = false, msg = resp.Msg });

                return StatusCode(201, new { Ok = true, lastId = resp.Id });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ok = false, msg = "Por favor hable con el administrador" });
            }
        }
    }
}
