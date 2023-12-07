using Invoice.WebModels;
using Invoice.Data;
using Invoice.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using Invoice.Web_Models;

namespace Invoice.Repositories
{


    public class DatabaseOperations : DatabaseOperationsInterface
    {
        private readonly ApplicationDbContext _db;

        public DatabaseOperations(ApplicationDbContext db)
        {
            _db = db;
        }    

        public string topNStates(string n)
        { 
            var nStates = _db.GrossNetProduceStates
                                .OrderByDescending(s => s.grossIncome)
                                .Take(int.Parse(n)).Select(s => s.state)
                                .ToList();

            return JsonSerializer.Serialize(nStates);
            
        }

        public Dictionary<string, string> ClientIdentityFetch(AuthRequestHeaders request)
        {
            var clientRecord =  _db.ClientIdentity.FirstOrDefault(u => u.client_id == request.ClientId);
            var loginRecord = _db.LoginDetails.FirstOrDefault(u => u.LoginId == request.LoginId);


            if (clientRecord != null && loginRecord != null)
            {
                Dictionary<string, string> returnValues = new Dictionary<string, string>();

                returnValues["client_id"] = clientRecord.client_id;
                returnValues["client_secret"] = clientRecord.client_secret;
                returnValues["token"] = loginRecord.Token.ToString();
                returnValues["expiry"] = loginRecord.TokenExpiry.ToString();

                return returnValues;
            }
            else
            {
                return null;
            }

        }

        public (string, string) LoginDetailsFetch(string loginId)
        {
            var record = _db.LoginDetails.FirstOrDefault(u => u.LoginId == loginId);
            if (record != null)
            {
                return (record.Password, record.PhoneNumber);
            }
            else { return (null, null); }
        }

        public AuthResponseModel TokenCheck(string loginId)
        {
            AuthResponseModel authResponse = new AuthResponseModel();

            var record = _db.LoginDetails.FirstOrDefault(u => u.LoginId == loginId);

            if (record == null) { return null; }

            if (record.Token == null)
            {
                record.Token = Guid.NewGuid();
                //record.TokenExpiry = DateTime.Now.AddHours(1);
                record.TokenExpiry = DateTime.Now.AddMinutes(3);              
            }
            else
            {
                if(record.TokenExpiry <= DateTime.Now)
                {
                    record.Token = Guid.NewGuid();
                    record.TokenExpiry = DateTime.Now.AddMinutes(3);
                }
            }

            _db.SaveChanges();

            authResponse.AuthToken = (Guid)record.Token;
            authResponse.TokenTime = record.TokenExpiry.ToString();

            return authResponse;
        }


        
    }
}
