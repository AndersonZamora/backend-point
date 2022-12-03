using Capa_Entidad;
using System.Security.Claims;

namespace CoreCinCout
{
    public class SCOSetting : ICOSetting
    {
        private readonly IConfiguration mConfiguration;
        public SCOSetting(IConfiguration mConfiguration )
        {
            this.mConfiguration = mConfiguration;
        }

        public Setting AppSettings()
        {
            return mConfiguration.GetSection("AppSettings").Get<Setting>();
        }

        public ClaimsIdent GetIdent(HttpContext httpContext)
        {
            //var test = _context.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier).Value;

            if (httpContext.User.Identity is ClaimsIdentity identity)
            {
                string email = identity.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;
                string names = identity.Claims.FirstOrDefault(n => n.Type == ClaimTypes.Surname).Value;

                ClaimsIdent user = new()
                {
                    Email = email,
                    Names = names
                };

                return user;
            }

            return new();
        }
    }
}
