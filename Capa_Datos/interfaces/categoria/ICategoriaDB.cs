using Capa_Entidad;
using System.Data;

namespace Capa_Datos
{
    public interface ICategoriaDB
    {
        Task<int> AddCategoria(ECategoriaR categoria, string cnn);
        Task<bool> DeleteCategoria(int Id_Cat, string cnn);
        Task<bool> EditCategoria(ECategoria categoria, string cnn);
        Task<DataTable> ListAllCategoria(string cnn);
    }
}
