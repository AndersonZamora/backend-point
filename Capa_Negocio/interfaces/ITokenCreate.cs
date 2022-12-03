using Capa_Entidad;
using System.Security.Claims;

namespace Capa_Negocio
{
    public interface ITokenCreate
    {
        ClaimsIdent GetUser(IEnumerable<Claim> identity);
        string TokenCreate(ClaimsIdent ident, string codeToken);
        ClaimsIdent ValidarToken(string token, IEnumerable<Claim> identity, string codeToken);
    }
}
