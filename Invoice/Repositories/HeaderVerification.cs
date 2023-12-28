using Invoice.Data;
using Invoice.Interfaces;
using Invoice.RecordModels;
using Invoice.Web_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Invoice.Repositories
{
    
    public class HeaderVerification : HeaderVerificationInterface
    {
        private readonly DatabaseOperationsInterface _interDbOp;

        public HeaderVerification(DatabaseOperationsInterface interDbOp)
        {
            _interDbOp = interDbOp;
        }

        public bool clientVerification(AuthRequestHeaders request)
        {
            Dictionary<string, string> clientDetails = _interDbOp.ClientIdentityFetch(request);

            try
            {
                if (clientDetails["token"].IsNullOrEmpty())
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            

            if (request.ClientId != null && request.ClientSecret != null && request.LoginId != null && request.AuthToken != null)
            {
                if (request.ClientId == clientDetails["client_id"] && request.ClientSecret == clientDetails["client_secret"])
                {
                    if (DateTime.Parse(clientDetails["expiry"]) > DateTime.Now)
                    {
                        return (String.Equals(request.AuthToken, clientDetails["token"], StringComparison.OrdinalIgnoreCase));
                    }
                    else { return false; }
                }
                else { return false; }
            }
            else { return false; }
        }
    }
}
