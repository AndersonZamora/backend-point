using Capa_Entidad;

namespace Capa_Validacion
{
    public class SValidarCategoria : IValidarCategoria
    {
        private readonly IValidarCampos mCampos;
        public SValidarCategoria(IValidarCampos mCampos)
        {
            this.mCampos = mCampos;
        }

        public Response AddCategoriaV(ECategoriaR categoria)
        {
            if (string.IsNullOrEmpty(categoria.Categoria)) return new Response() { Ok = false, Msg = "Nombre de la categoría  requerido" };
            if (!mCampos.ValidarLetrasNumeros(categoria.Categoria)) return new Response { Ok = false, Msg = "La categoría solo puede tener Letras y Números" };

            return new() { Ok = true };
        }
    }
}
