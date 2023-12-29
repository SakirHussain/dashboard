using Invoice.Interfaces;
using Invoice.Models;
using Invoice.Web_Models;
using Invoice.WebModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Invoice.Controllers
{
    /// <summary>
    /// Controller for managing token-related API endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly DatabaseOperationsInterface _interDbOp;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenController"/> class.
        /// </summary>
        /// <param name="interDbOp">Database operations interface.</param>
        public TokenController(DatabaseOperationsInterface interDbOp)
        {
            _interDbOp = interDbOp;
        }

        /// <summary>
        /// Retrieves a new token based on validity.
        /// </summary>
        /// <param name="request">Serialized JSON string representing the token request.</param>
        /// <returns>An <see cref="ApiResponseModel"/> containing the result of the operation.</returns>
        [HttpPost, HttpGet]
        public ApiResponseModel GetToken(string request)
        {
            ApiResponseModel response = new ApiResponseModel();

            try
            {
                // Deserialize the JSON request into a TokenRequestModel
                TokenRequestModel requestModel = JsonSerializer.Deserialize<TokenRequestModel>(request)!;

                // Verify login details using DatabaseOperationsInterface
                if (_interDbOp.LoginDetailsVerify(requestModel!))
                {
                    // Retrieve token details using DatabaseOperationsInterface
                    AuthResponseModel authRes = _interDbOp.TokenCheck(requestModel!.LoginId);

                    // Set response properties for successful operation
                    response.status = 1;
                    response.error = null;
                    response.data = JsonSerializer.Serialize(authRes);
                }
                else
                {
                    // Set response properties for invalid login details
                    response.status = 0;
                    response.data = null;
                    response.error = ErrorMessages.GetErrorMessage(response.error, new UnauthorizedAccessException());
                }
            }
            catch (Exception ex)
            {
                // Set response properties for exception scenarios
                response.status = 0;
                response.data = null;
                response.error = ErrorMessages.GetErrorMessage(response.error!, new FormatException());
            }

            return response;
        }
    }
}
