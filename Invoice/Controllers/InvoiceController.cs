using Invoice.Interfaces;
using Invoice.Web_Models;
using Invoice.WebModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Invoice.Controllers
{
    /// <summary>
    /// Controller for managing Einvoice API requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly DatabaseOperationsInterface _interDbOp;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceController"/> class.
        /// </summary>
        /// <param name="interDbOp">Database operations interface.</param>
        public InvoiceController(DatabaseOperationsInterface interDbOp)
        {
            _interDbOp = interDbOp;
        }

        /// <summary>
        /// Retrieves a report based on the provided Einvoice request.
        /// </summary>
        /// <param name="request">Serialized JSON string representing the invoice request.</param>
        /// <returns>An <see cref="ApiResponseModel"/> containing the result of the operation.</returns>
        [HttpPost, HttpGet]
        public ApiResponseModel GetReport(string request)
        {
            ApiResponseModel response = new ApiResponseModel();

            try
            {
                // Deserialize the JSON request into an InvoiceRequestModel
                InvoiceRequestModel requestModel = JsonSerializer.Deserialize<InvoiceRequestModel>(request)!;

                // Call the database operation to get the report
                var data = _interDbOp.GetReport(requestModel);

                if (data != null)
                {
                    // Serialize the result and set response properties
                    response.data = JsonSerializer.Serialize(data);
                    response.status = 1;
                    response.error = null;
                }
                else
                {
                    // Set response properties for invalid input values
                    response.data = null;
                    response.status = 0;
                    response.error.errorMessage = "Input Values Invalid";
                }
            }
            catch (Exception ex)
            {
                // Set response properties for exception scenarios
                response.data = null;
                response.status = 0;
                response.error.errorMessage = "An error occurred while processing the request.";
            }

            return response;
        }
    }
}
