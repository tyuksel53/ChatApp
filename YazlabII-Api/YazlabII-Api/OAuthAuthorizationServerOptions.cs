using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using YazlabII_Api.Models.Managers;

namespace YazlabII_Api
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        DatabaseContext db = new DatabaseContext();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var username = context.UserName;
            var password = context.Password;

            var user = db.Users.FirstOrDefault(x =>
                x.Username == username && x.Password == password);

            if (user != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("Username", user.Username));
                context.Validated(identity);
            }
            else
            {
                context.SetError("Oturum Hatasi", "Yanlış kullanıcı adi veya şifre");
            }
        }


    }
}