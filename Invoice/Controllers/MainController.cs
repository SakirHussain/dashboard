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
        private readonly HttpClient _httpClient;
        private readonly IControllerFactory _controllerFactory;

        public MainController(IControllerFactory controllerFactory, HeaderVerificationInterface interHeaderVer, HttpClient httpClient)
        {
            _interHeaderVer = interHeaderVer;
            _httpClient = httpClient;
            _controllerFactory = controllerFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Entry(string s) // action & data ;; data must be serialized json
        {
            ApiResponseModel response = new ApiResponseModel();

            var headers = HttpContext.Request.Headers;

            AuthRequestHeaders requestHeaders = new AuthRequestHeaders();
            requestHeaders.ClientId = headers["client-id"];
            requestHeaders.ClientSecret = headers["client-secret"];
            requestHeaders.LoginId = headers["login-id"];
            requestHeaders.AuthToken = headers["token"];

            bool check = _interHeaderVer.clientVerification(requestHeaders);
            TokenRequestModel obj = new TokenRequestModel();
            if (true)
            {
                if (true)
                {
                    /*var client = new HttpClient();*/
                    _httpClient.BaseAddress = new Uri("https://localhost:7272");
                    var targetUrl = Url.Action("GetToken", "Token");
                    var req = new HttpRequestMessage(HttpMethod.Get, targetUrl)
                    {
                        Content = new StringContent($"{{\"s\": \"{s}\"}}", Encoding.UTF8, "application/json")                        
                    };
                    var res = await _httpClient.GetAsync("https://localhost:7272/api/Token?s=s");


                }
                /*else if (request.Action == "Top States")
                {
                    return RedirectToAction("Invoice", "GetTopStates");
                }
            }
            else
            {
                *//*response.status = 0;
                response.error.errorCode = StatusCodes.Status401Unauthorized;
                response.error.errorMessage = "Unauthorized Access, Check header values";
                response.data = null;*//*

                return Ok(response);
            }*/

                return Ok(response);

            }



        }
    }
}
