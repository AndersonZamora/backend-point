using Capa_Entidad;

namespace CoreCinCout
{
    public interface ICategoria
    {
        Task<Response> CategoriaAdd(ECategoriaR rquest, HttpContext httpContext);
    }
}
