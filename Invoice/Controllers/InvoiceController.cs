using Invoice.WebModels;
using Invoice.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Invoice.Web_Models;
using System.Text.Json;

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

        [HttpGet, HttpPost]
        public ApiResponseModel GetTopStates(string request) // new model
        {
            InvoiceRequestModel requestModel = JsonSerializer.Deserialize<InvoiceRequestModel>(request);

            /*InvoiceRequestModel requestModel = new InvoiceRequestModel
            {
                Id = 1,
                stateCode = 199,
                supType = "B2B",
                perdYear = 2023,
                perdMon = 8,
                outIn = "OUT"
            };*/


            ApiResponseModel response = new ApiResponseModel();


            response.data = JsonSerializer.Serialize(_interDbOp.GetReport(requestModel));
            response.status = 1;
            response.error = null;


            return response;
        }
    }
}
