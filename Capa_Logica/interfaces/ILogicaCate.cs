using Capa_Entidad;

namespace Capa_Logica
{
    public interface ILogicaCate
    {
        Task<Response> CategoriaAdd(ECategoriaR categoria, string cnn);
    }
}
