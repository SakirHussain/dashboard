using Invoice.Interfaces;
using Invoice.Web_Models;
using Invoice.WebModels;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.Controllers
{
    /// <summary>
    /// API Entry point,token and login detials verified here.
    /// </summary>
    [Route("api/main")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly HeaderVerificationInterface _interHeaderVer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainController"/> class.
        /// </summary>
        /// <param name="interHeaderVer">Header verification interface.</param>
        public MainController(HeaderVerificationInterface interHeaderVer)
        {
            _interHeaderVer = interHeaderVer;
        }

        /// <summary>
        /// Entry point for processing all requests with action and serialized JSON data.
        /// </summary>
        /// <param name="request">The request model containing action and data.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost]
        public IActionResult Entry(RequestModel request)
        {
            // Initialize ApiResponseModel for response
            ApiResponseModel response = new ApiResponseModel();

            // Extract headers from the request
            var headers = HttpContext.Request.Headers;
            AuthRequestHeaders requestHeaders = new AuthRequestHeaders
            {
                ClientId = headers["client-id"]!,
                ClientSecret = headers["client-secret"]!,
                LoginId = headers["login-id"]!,
                AuthToken = headers["token"]!
            };

            // Perform client verification using HeaderVerificationInterface
            bool check = _interHeaderVer.ClientVerification(requestHeaders);

            if (request.Action == "Get Token")
            {
                // Redirect to TokenController's GetToken action with request data
                return RedirectToAction("GetToken", "Token", new { request = request.Data });
            }

            if (check)
            {
                if (request.Action == "Get Report")
                {
                    // Redirect to InvoiceController's GetReport action with request data
                    return RedirectToAction("GetReport", "Invoice", new { request = request.Data });
                }
            }
            else
            {
                // Set response properties for unauthorized access
                response.status = 0;
                response.error.errorCode = StatusCodes.Status401Unauthorized;
                response.error.errorMessage = "Unauthorized Access, Check header values";
                response.data = null;

                // Return unauthorized response
                return Ok(response);
            }

            // Return the response for valid requests
            return Ok(response);
        }
    }
}
