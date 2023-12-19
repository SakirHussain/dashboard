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
        public ApiResponseModel GetTopStates(string request)
        {
            ApiResponseModel response = new ApiResponseModel();

            response.data = (_interDbOp.topNStates(request));
            response.status = 1;
            response.error = null;


            return response;
        }
    }
}
