using Invoice.Interfaces;
using Invoice.Migrations;
using Invoice.Web_Models;
using Invoice.WebModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Controllers;

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

        [HttpPost]
        public IActionResult Entry(RequestModel request) // action & data ;; data must be serialized json
        {            
            
            ApiResponseModel response = new ApiResponseModel();


            var headers = HttpContext.Request.Headers;

            AuthRequestHeaders requestHeaders = new AuthRequestHeaders();
            requestHeaders.ClientId = headers["client-id"]!;
            requestHeaders.ClientSecret = headers["client-secret"]!;
            requestHeaders.LoginId = headers["login-id"]!;
            requestHeaders.AuthToken = headers["token"]!;

            bool check = _interHeaderVer.clientVerification(requestHeaders);

            if (request.Action == "Get Token")
            {
                return RedirectToAction("GetToken", "Token", new { request = request.Data });
            }

            if (check)
            {
                    if (request.Action == "Get Report")
                    {
                        return RedirectToAction("GetReport", "Invoice", new { request = request.Data });
                    }
            }
            else
            {
                response.status = 0;
                response.error.errorCode = StatusCodes.Status401Unauthorized;
                response.error.errorMessage = "Unauthorized Access, Check header values";
                response.data = null;

                return Ok(response);
            }

                return Ok(response);
        }
    }
}


