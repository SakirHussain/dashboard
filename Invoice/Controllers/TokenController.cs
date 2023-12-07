using Invoice.Repositories;
using Invoice.Interfaces;
using Invoice.Web_Models;
using Invoice.WebModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Invoice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly HeaderVerificationInterface _interHeaderVer;
        private readonly DatabaseOperationsInterface _interDbOp;

        public TokenController(HeaderVerificationInterface interHeaderVer, DatabaseOperationsInterface interDbOp)
        {
            _interHeaderVer = interHeaderVer;
            _interDbOp = interDbOp;

        }

        [HttpGet]
        public ApiResponseModel GetToken(TokenRequestModel request)
        {
            ApiResponseModel response = new ApiResponseModel();
            
            AuthRequestHeaders requestHeaders = new AuthRequestHeaders();


            HttpContext.Request.Headers.TryGetValue("client-id", out var client_id);
            HttpContext.Request.Headers.TryGetValue("client-secret", out var client_secret);
            HttpContext.Request.Headers.TryGetValue("token", out var token);

            requestHeaders.ClientId = client_id;
            requestHeaders.ClientSecret = client_secret;
            requestHeaders.AuthToken = token;

            bool test = _interHeaderVer.clientVerification(requestHeaders);

            if (test == true)
            {
                response.status = 1; // success
            }
            else
            {
                response.status = 0;
                response.data = null;
                response.error.errorCode = StatusCodes.Status401Unauthorized;
                response.error.errorMessage = "Invalid Client Values";

                return response;
            }
           

            (string, string) loginAuth = _interDbOp.LoginDetailsFetch(request.LoginId);
            
            if(loginAuth.Item1 == request.Password && loginAuth.Item2 == request.PhoneNumber)
            {
                AuthResponseModel authRes = _interDbOp.TokenCheck(request.LoginId);
                
                response.error = null;        
                response.data =  JsonSerializer.Serialize(authRes);
            }
            else
            {
                response.status = 0; // error
                response.data = null;
                response.error.errorCode = 104;
                response.error.errorMessage = "Invalid Login Details";
                response.error.TimeStamp = DateTime.Now;
            }


            return response;

        }
    }
}
