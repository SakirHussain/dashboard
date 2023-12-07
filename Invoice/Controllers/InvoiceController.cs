using Invoice.WebModels;
using Invoice.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Invoice.Web_Models;

namespace Invoice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly DatabaseOperationsInterface _interDbOp;
        private readonly HeaderVerificationInterface _interHeaderVer;

        public InvoiceController(DatabaseOperationsInterface interDbOp, HeaderVerificationInterface interHeaderVer)
        {
            _interDbOp = interDbOp;
            _interHeaderVer = interHeaderVer;
        }

        [HttpPost]
       public ApiResponseModel GetTopStates(RequestModel request)
        {   
            
          //  System.Net.Http.Headers.HttpRequestHeaders header = Request.Headers;

            ApiResponseModel response = new ApiResponseModel();

            AuthRequestHeaders requestHeaders = new AuthRequestHeaders();                
            

            HttpContext.Request.Headers.TryGetValue("client-id", out var client_id);
            HttpContext.Request.Headers.TryGetValue("client-secret", out var client_secret);
            HttpContext.Request.Headers.TryGetValue("login-id", out var login_id);
            HttpContext.Request.Headers.TryGetValue("token", out var token);

            requestHeaders.ClientId = client_id;
            requestHeaders.ClientSecret = client_secret;
            requestHeaders.LoginId = login_id;
            requestHeaders.AuthToken = token;
 
            bool test =  _interHeaderVer.clientVerification(requestHeaders);
            
            if (test == true) 
            {
                response.status = 1;                
            }
            else
            {
                response.status = 0;
                response.data = null;
                response.error.errorCode = StatusCodes.Status401Unauthorized;
                response.error.errorMessage = "Invalid Client Values";

                return response;
            }
         

            // verify token



            response.data = _interDbOp.topNStates(request.Data);


            return response;

            

          

            

        }
    }
}
