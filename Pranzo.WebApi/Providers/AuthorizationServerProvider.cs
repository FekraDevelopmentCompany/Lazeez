using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using Lazeez.Model.DBModel;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Lazeez.WebApi.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override  async  Task  GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                UserService Service = unitOfWork.Service<UserService>();
                User MatchedUser = Service.GetAll(ex => ex.Name.ToLower() == context.UserName.ToLower()
                && ex.Password == context.Password).FirstOrDefault();

                if (MatchedUser == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    // Add key AuthorizationResponse as a flag to check it in InvalidAuthenticationMiddleware class 
                    // to return 401 (unauthorize) error not 400 (bad request)
                    context.Response.Headers.Add("AuthorizationResponse", new[] { "Failed" });
                    return;
                }
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }
    }
}