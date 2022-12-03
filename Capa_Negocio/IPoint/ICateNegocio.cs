using Capa_Entidad;

namespace Capa_Negocio
{
    public interface ICateNegocio
    {
        Task<int> AddCategoriaN(ECategoriaR categoria, string cnn);
        Task<bool> DeleteCategoriaN(int Id_Cat, string cnn);
        Task<bool> EditCategoriaN(ECategoria categoria, string cnn);
        Task<List<ECategoria>> ListAllCategoriaN(string cnn);
    }
}
