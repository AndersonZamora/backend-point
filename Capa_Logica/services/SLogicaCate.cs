using Capa_Entidad;
using Capa_Negocio;
using Capa_Validacion;

namespace Capa_Logica
{
    public class SLogicaCate : ILogicaCate
    {
        private readonly IValidarCategoria mCategoria;
        private readonly ICateNegocio mCategoriaN;
        public SLogicaCate(IValidarCategoria mCategoria, ICateNegocio mCategoriaN)
        {
            this.mCategoria = mCategoria;
            this.mCategoriaN = mCategoriaN;
        }

        public async Task<Response> CategoriaAdd(ECategoriaR categoria, string cnn)
        {
            var resp = mCategoria.AddCategoriaV(categoria);
            if (!resp.Ok) return resp;

            var save = await mCategoriaN.AddCategoriaN(categoria, cnn);

            return new() { Ok = true, Id = save };
        }
    }
}
