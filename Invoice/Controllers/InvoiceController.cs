using Invoice.WebModels;
using Invoice.Data;
using Invoice.Repositories;
using Invoice.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        [HttpPost("{num:int}")]
        /*[Route("TopStates")]*/
       public ApiResponseModel GetTopStates(int num)
        {          
            ApiResponseModel response = new ApiResponseModel();

            Request.Headers.TryGetValue("client-id", out var client_id);
            Request.Headers.TryGetValue("client-secret", out var client_secret);
 
            object test =  _interHeaderVer.clientVerification(client_id, client_secret);
            
            if (test != null) /**/
            {
                if ((bool)test == true) /**/
                {
                    response.status = StatusCodes.Status200OK;                
                }
                else
                {
                    response.error.errorCode = StatusCodes.Status401Unauthorized;
                    response.error.errorMessage = "Invalid Client Values";

                    return response;
                }

            }
            else
            {
                response.error.errorCode = StatusCodes.Status400BadRequest;
                response.error.errorMessage = "Incomplete Header in Request";

                return response;
            }


            response.data = _interDbOp.topNStates(num);


            return response;

            

          

            

        }
    }
}
