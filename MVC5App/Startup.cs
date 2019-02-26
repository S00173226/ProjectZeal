using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using MVC5App.AWSCognitoSettings;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC5App.Startup))]
namespace MVC5App
{
    public partial class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);

            

        }

        //public void ConfigureOAuth(IAppBuilder app)
        //{
        //    var issuer = "https://cognito-idp.eu-west-1.amazonaws.com/eu-west-1_3dztKQlHW";
        //    //var audience = "099153c2625149bc8ecb3e85e03f0022";
        //    var secret = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");

        //    // Api controllers with an [Authorize] attribute will be validated with JWT
        //    app.UseJwtBearerAuthentication(
        //        new JwtBearerAuthenticationOptions
        //        {
        //            AuthenticationMode = AuthenticationMode.Active,
        //            Provider
        //            IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
        //            {
        //                new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
        //            }
        //        });

        //}


    }
}
