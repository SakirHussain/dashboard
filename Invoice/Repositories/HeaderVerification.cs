using Invoice.Data;
using Invoice.Interfaces;
using Invoice.RecordModels;
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

        public object clientVerification(string client_id , string client_secret)
        {
           (string,string) clientDetails =  _interDbOp.ClientIdentityFetch(client_id);
 

            if (client_id != null && client_secret != null)
            {
                if (client_id == clientDetails.Item1)
                {
                    return (client_secret == clientDetails.Item2);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
