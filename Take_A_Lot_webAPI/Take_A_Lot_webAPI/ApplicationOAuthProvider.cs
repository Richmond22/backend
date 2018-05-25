using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using Take_A_Lot_webAPI.Models;
using System.Security.Claims;

namespace Take_A_Lot_webAPI
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
             var db = new DBmodel();
            var user = db.Tblcustomers
                .FirstOrDefault(c => c.email == context.UserName && c.password == context.Password);
            var admin = db.Admins
                .FirstOrDefault(a => a.email == context.UserName && a.password == context.Password);
            var driver = db.drivers
                .FirstOrDefault(a => a.email == context.UserName && a.password == context.Password);
            var supplier = db.suppliers
                .FirstOrDefault(a => a.email == context.UserName && a.password == context.Password);
            if (user != null )
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("customerID", user.customerID.ToString()));
                identity.AddClaim(new Claim("firstname", user.firstname));
                identity.AddClaim(new Claim("lastname", user.lastname));
                identity.AddClaim(new Claim("email", user.email));
                identity.AddClaim(new Claim("password", user.password));
                identity.AddClaim(new Claim("phone", user.phone));
                context.Validated(identity);

            }
            else if(admin != null){
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("ID", admin.ID.ToString()));
                identity.AddClaim(new Claim("firstname", admin.firstname));
                context.Validated(identity);
            }
            else if (driver != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("ID", driver.ID.ToString()));
                identity.AddClaim(new Claim("firstname", driver.firstname));
                context.Validated(identity);
            }
            else if (supplier != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("ID", supplier.ID.ToString()));
                identity.AddClaim(new Claim("firstname", supplier.firstname));
                context.Validated(identity);
            }
            else
                return;
        }
    }

    
}