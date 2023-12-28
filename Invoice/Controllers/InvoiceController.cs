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

        public InvoiceController(DatabaseOperationsInterface interDbOp, HeaderVerificationInterface interHeaderVer)
        {
            _interDbOp = interDbOp;
        }

        [HttpGet, HttpPost]
        public ApiResponseModel GetReport(string request) // new model
        {
            ApiResponseModel response = new ApiResponseModel();

            try
            {
                InvoiceRequestModel requestModel = JsonSerializer.Deserialize<InvoiceRequestModel>(request)!;
                var dat = _interDbOp.GetReport(requestModel);

                if (dat != null)
                {
                    response.data = JsonSerializer.Serialize(dat);
                    response.status = 1;
                    response.error = null;
                }
                else
                {
                    response.data = null;
                    response.status = 0;
                    response.error.errorMessage = "Input Values Invalid";
                }
            }
            catch (Exception ex)
            {
                response.data = null;
                response.status = 0;
                response.error.errorMessage = "Input Values Invalid";
            }         
            return response;
        }
    }
}
