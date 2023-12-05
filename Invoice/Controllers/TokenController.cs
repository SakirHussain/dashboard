using Invoice.Repositories;
using Invoice.Interfaces;
using Invoice.Web_Models;
using Invoice.WebModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.Controllers
{
    [Route("[controller]")]
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

            Request.Headers.TryGetValue("client-id", out var client_id);
            Request.Headers.TryGetValue("client-secret", out var client_secret);

            object test = _interHeaderVer.clientVerification(client_id, client_secret);

            if (test != null) /**/
            {
                if ((bool)test == true) /**/
                {
                    response.status = StatusCodes.Status200OK;
                }
                else
                {
                    response.error.errorCode = StatusCodes.Status401Unauthorized;
                    response.error.errorMessage = "Invalid Client Values";

                    return response;
                }
            }
            else
            {
                response.error.errorCode = StatusCodes.Status400BadRequest;
                response.error.errorMessage = "Incomplete Header in Request";

                return response;
            }

            (string, string) loginAuth = _interDbOp.LoginDetailsFetch(request.LoginId);
            
            if(loginAuth.Item1 == request.Password && loginAuth.Item2 == request.PhoneNumber)
            {
                response.status = StatusCodes.Status200OK;
                response.data =  _interDbOp.TokenCheck(request.LoginId).ToString();
            }
            else
            {
                response.error.errorCode = StatusCodes.Status401Unauthorized;
                response.error.errorMessage = "Invalid Login Details";
                response.error.TimeStamp = DateTime.Now;
            }


            return response;

        }
    }
}
