using Invoice.Repositories;
using Invoice.Interfaces;
using Invoice.Web_Models;
using Invoice.WebModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Invoice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly HeaderVerificationInterface _interHeaderVer;
        private readonly DatabaseOperationsInterface _interDbOp;

        public TokenController(HeaderVerificationInterface interHeaderVer, DatabaseOperationsInterface interDbOp)
        {
            _interHeaderVer = interHeaderVer;
            _interDbOp = interDbOp;

        }

        [HttpGet, HttpPost]
        public ApiResponseModel GetToken(string s)
        {
            ApiResponseModel response = new ApiResponseModel();
            
            AuthRequestHeaders requestHeaders = new AuthRequestHeaders();       
           
/*
            (string, string) loginAuth = _interDbOp.LoginDetailsFetch(request.LoginId);
            
            if(loginAuth.Item1 == request.Password && loginAuth.Item2 == request.PhoneNumber)
            {
                AuthResponseModel authRes = _interDbOp.TokenCheck(request.LoginId);
                
                response.error = null;        
                response.data =  JsonSerializer.Serialize(authRes);
            }
            else
            {
                response.status = 0; // error
                response.data = null;
                response.error.errorCode = 104;
                response.error.errorMessage = "Invalid Login Details";
                response.error.TimeStamp = DateTime.Now;
            }*/


            return response;

        }
    }
}
