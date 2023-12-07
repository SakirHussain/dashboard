using Invoice.Data;
using Invoice.Interfaces;
using Invoice.RecordModels;
using Invoice.Web_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
           (string,string, string) clientDetails =  _interDbOp.ClientIdentityFetch(request);
 

            if (request.ClientId != null && request.ClientSecret != null)
            {
                if (request.ClientId == clientDetails.Item1)
                {
                    if (request.ClientSecret == clientDetails.Item2)
                    {
                        return (String.Equals(request.AuthToken, clientDetails.Item3, StringComparison.OrdinalIgnoreCase));
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
