using Capa_Entidad;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32.SafeHandles;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Capa_Negocio
{
    public class STokenCreate : ITokenCreate
    {
        public ClaimsIdent GetUser(IEnumerable<Claim> identity)
        {
            try
            {
                if (identity != null)
                {
                    
                    string email = identity.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;
                    string names = identity.FirstOrDefault(n => n.Type == ClaimTypes.Surname).Value;

                    ClaimsIdent user = new()
                    {
                       
                        Names = names,
                        Email = email
                    };

                    return user;
                }

                return new ClaimsIdent();
            }
            catch (Exception)
            {
                return new ClaimsIdent();
            }
        }

        public string TokenCreate(ClaimsIdent ident, string codeTken)
        {
            try
            {
                var keyBytes = Encoding.UTF8.GetBytes(codeTken);
                var clains = new ClaimsIdentity();

                clains.AddClaim(new Claim(ClaimTypes.Email, ident.Email));
                clains.AddClaim(new Claim(ClaimTypes.Surname, ident.Names));

                var tokenDescr = new SecurityTokenDescriptor
                {
                    Subject = clains,
                    Expires = DateTime.UtcNow.AddMinutes(480),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha512Signature)
                };

                var tokenHan = new JwtSecurityTokenHandler();
                
                var tokenC = tokenHan.CreateToken(tokenDescr);
                string token = tokenHan.WriteToken(tokenC);

                return token;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public ClaimsIdent ValidarToken(string token, IEnumerable<Claim> identity, string codeToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.UTF8.GetBytes(codeToken);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                ClaimsIdent user = GetUser(identity);

                string newToken = TokenCreate(user, user.Names);
                user.Token = newToken;

                return user;
            }
            catch (Exception)
            {
                return new ClaimsIdent();
            }
        }
    }
}
