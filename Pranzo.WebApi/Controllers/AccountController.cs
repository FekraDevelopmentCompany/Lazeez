using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Lazeez.Entities.Model;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Model.ViewModel;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Service;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Lazeez.WebApi.Controllers
{
    //[Authorize]
    public class AccountController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage SignInWithFacebook(string FacebookAccount)
        {
            try
            {
                if (string.IsNullOrEmpty(FacebookAccount))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Facebook Account can not be Empty.");

                IUnitOfWork unitOfWork = new UnitOfWork();
                UserService Service = unitOfWork.Service<UserService>();

                User Model = Service.GetAll(ex => ex.FacebookAccount == FacebookAccount).FirstOrDefault();
                if (Model == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = "This account is not exsit" });
                else
                {
                    string HashedUserID = StringCipher.Encrypt(Model.ID.ToString());
                    string Token = GenerateLocalAccessTokenResponse(HashedUserID);
                    APIResponse Response = new APIResponse();
                    Response.State = true;
                    Response.Data = new { HashedUserID = HashedUserID, Token = Token };
                    return Request.CreateResponse(HttpStatusCode.OK, Response);
                }
            }
            catch (Exception ex)
            {
                HttpError ErrorMsg = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMsg);
            }
        }

        [HttpGet]
        public HttpResponseMessage SignInWithInstagram(string Instagram)
        {
            try
            {
                if (string.IsNullOrEmpty(Instagram))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Instagram Account can not be Empty.");

                IUnitOfWork unitOfWork = new UnitOfWork();
                UserService Service = unitOfWork.Service<UserService>();

                User MatchedUser = Service.GetAll(ex => ex.InstgramAccount == Instagram).FirstOrDefault();
                if (MatchedUser == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = "This account is not exsit" });
                else
                {
                    string HashedUserID = StringCipher.Encrypt(MatchedUser.ID.ToString());
                    string Token = GenerateLocalAccessTokenResponse(HashedUserID);
                    APIResponse Response = new APIResponse();
                    Response.State = true;
                    Response.Data = new { HashedUserID = HashedUserID, Token = Token };
                    return Request.CreateResponse(HttpStatusCode.OK, Response);
                }
            }
            catch (Exception ex)
            {
                HttpError ErrorMsg = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMsg);
            }
        }

        [HttpGet]
        public HttpResponseMessage SignIn(string UserNameOrEmail, string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(UserNameOrEmail))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "UserName Or Email can not be Empty.");
                if (string.IsNullOrEmpty(Password))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Password can not be Empty.");

                IUnitOfWork unitOfWork = new UnitOfWork();
                UserService Service = unitOfWork.Service<UserService>();
                User MatchedUser = Service.GetAll(ex => ex.Name.ToLower() == UserNameOrEmail.ToLower() || ex.Email.ToLower() == UserNameOrEmail.ToLower()).FirstOrDefault();
                if (MatchedUser == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = "User name or Email or Password is incorrect" });
                else
                {
                    //Match Password
                    if (MatchedUser.Password == StringCipher.Hash(Password))
                    {
                        string HashedUserID = StringCipher.Encrypt(MatchedUser.ID.ToString());
                        string Token = GenerateLocalAccessTokenResponse(HashedUserID);
                        APIResponse Response = new APIResponse();
                        Response.State = true;
                        Response.Data = new { HashedUserID = HashedUserID, Token = Token };
                        return Request.CreateResponse(HttpStatusCode.OK, Response);
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = "User name or Email or Password is incorrect" });
                }
            }
            catch (Exception ex)
            {
                HttpError ErrorMsg = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMsg);
            }
        }

        [HttpPost]
        public HttpResponseMessage SignUp(User Model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                IUnitOfWork unitOfWork = new UnitOfWork();
                UserService Service = unitOfWork.Service<UserService>();
                tbl_User UserModel = null;
                string message = "";
                bool isSaved = Service.SignUp(Model, out UserModel, out message);
                if (!isSaved)
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = message });

                if (unitOfWork.Save() > 0)
                {
                    string EncryptedUserID = StringCipher.Encrypt(UserModel.ID.ToString());
                    string Token = GenerateLocalAccessTokenResponse(EncryptedUserID);
                    APIResponse Response = new APIResponse();
                    Response.State = true;
                    Response.Data = new { HashedUserID = EncryptedUserID, Token = Token };
                    return Request.CreateResponse(HttpStatusCode.OK, Response);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = "Can not SignUp for this user" });
            }
            catch (Exception ex)
            {
                HttpError ErrorMsg = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMsg);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage UpdateDeviceToken(DeviceTokenModel Model)
        {
            try
            {
                if (string.IsNullOrEmpty(Model.DeviceToken))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Device Token can not be Empty.");
                if (string.IsNullOrEmpty(Model.HashedUserID))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Hashed UserID can not be Empty.");

                IUnitOfWork unitOfWork = new UnitOfWork();
                DeviceTokenService Service = unitOfWork.Service<DeviceTokenService>();
                int userID = int.Parse(StringCipher.Decrypt(Model.HashedUserID));
                Service.Insert(new UserDeviceToken { DeviceToken = Model.DeviceToken, UserID = userID });
                int isInserted = unitOfWork.Save();
                if (isInserted > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = true });
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = "Can not update or add device token." });
            }
            catch (Exception ex)
            {
                HttpError ErrorMsg = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMsg);
            }
        }

        [HttpPost]
        public HttpResponseMessage SplashScreen(DeviceTokenModel Model)
        {
            try
            {
                if (string.IsNullOrEmpty(Model.HashedUserID))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Hashed User ID can not be Empty.");

                IUnitOfWork unitOfWork = new UnitOfWork();
                UserService Service = unitOfWork.Service<UserService>();
                int userID = int.Parse(StringCipher.Decrypt(Model.HashedUserID));
                bool isExsit = Service.Exists(ex => ex.ID == userID);
                if (isExsit)
                {
                    string Token = GenerateLocalAccessTokenResponse(Model.HashedUserID);
                    APIResponse Response = new APIResponse();
                    Response.State = true;
                    Response.Data = new { HashedUserID = Model.HashedUserID, Token = Token };
                    return Request.CreateResponse(HttpStatusCode.OK, Response);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = "Hashed UserID is not exsit" });

            }
            catch (Exception ex)
            {
                HttpError ErrorMsg = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMsg);
            }
        }

        public string GenerateLocalAccessTokenResponse(string HashedUserID)
        {
            var tokenExpiration = TimeSpan.FromDays(1);

            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, HashedUserID));

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };

            var ticket = new AuthenticationTicket(identity, props);

            string accessToken = Startup.OAuthServerOptions.AccessTokenFormat.Protect(ticket);

            //JObject tokenResponse = new JObject(
            //                            new JProperty("userName", userName),
            //                            new JProperty("access_token", accessToken),
            //                            new JProperty("token_type", "bearer"),
            //                            new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
            //                            new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
            //                            new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString())
            // );

            return accessToken;
        }

    }
}
