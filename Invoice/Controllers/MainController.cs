using Invoice.Interfaces;
using Invoice.Web_Models;
using Invoice.WebModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.Controllers
{
    [Route("api/main")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly HeaderVerificationInterface _interHeaderVer;

        public MainController(HeaderVerificationInterface interHeaderVer)
        {
            _interHeaderVer = interHeaderVer;
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Entry(RequestModel request) // action & data ;; data must be serialized json
        {
            ApiResponseModel response = new ApiResponseModel();

            var headers =  HttpContext.Request.Headers;          
     
            AuthRequestHeaders requestHeaders = new AuthRequestHeaders();
            requestHeaders.ClientId = headers["client-id"];
            requestHeaders.ClientSecret = headers["client-secret"];
            requestHeaders.LoginId = headers["login-id"];
            requestHeaders.AuthToken = headers["token"];

            bool check = _interHeaderVer.clientVerification(requestHeaders);

            if (check)
            {
                if(request.Action == "Get Token")
                {
                    RedirectToAction("Token","GetToken", request.Data);
                }
            }
            else
            {
                response.status = 0;
                response.error.errorCode = StatusCodes.Status401Unauthorized;
                response.error.errorMessage = "Unauthorized Access, Check header values";
                response.data = null;
            }

            return Ok(response);

        }
    }
}
