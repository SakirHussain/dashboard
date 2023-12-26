using Invoice.WebModels;
using Invoice.Data;
using Invoice.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using Invoice.Web_Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.Repositories
{


    public class DatabaseOperations : DatabaseOperationsInterface
    {
        /*private readonly DbContextOptionsFactory _dbContextOptionsFactory;

        public DatabaseOperations(DbContextOptionsFactory dbContextOptionsFactory)
        {
            _dbContextOptionsFactory = dbContextOptionsFactory;
        }*/

        public void Carti(string connectionString)
        {
            /*var options = _dbContextOptionsFactory.CreateOptions<ApplicationDbContext>(connectionString);*/

            var dbContext = DbContextOptionsFactory.Create("DB3");
           
            var record = dbContext.LoginDetails.FirstOrDefault(u => u.LoginName == "Varun Gupta");

            record.LoginName = "nihha";
        }

        /*public string topNStates(InvoiceRequestModel requestModel)
        { 
            List<InvoiceResponseModel> eInvoiceResponse = GetReport(requestModel);

            *//*var nStates = _db.GrossNetProduceStates
                                .OrderByDescending(s => s.grossIncome)
                                .Take(int.Parse(n)).Select(s => s.state)
                                .ToList();
*//*
            return JsonSerializer.Serialize(nStates);
            
        }

        private List<InvoiceResponseModel> GetReport(InvoiceRequestModel requestModel)
        {
            string conststr = string.Empty;
            SqlCommand cmd;

            conststr = getConnectionString("POST");

            List<InvoiceResponseModel> eInvoiceReponse = new List<InvoiceResponseModel>();

            using (ConnectionDAL cd = new ConnectionDAL(conststr)) { 
                try
                { // executing procedure inside based on connection string
                    cmd = new SqlCommand();

                    cmd.CommandText = "usp_get_einv_app_stdata";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int));

                    cmd.Parameters["@id"].Value = requestModel.Id;
                }
            }
            throw new NotImplementedException();
        }

        private string getConnectionString(string v)
        {
            if(v == "POST")
            {
                return "EInvoice";
            }
            throw new NotImplementedException();
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

        public bool LoginDetailsVerify(TokenRequestModel req)
        {
            var record = _db.LoginDetails.FirstOrDefault(u => u.LoginId == req.LoginId);
            if (record.Password == req.Password && record.PhoneNumber == req.PhoneNumber)
            {
                return true;
            }
            else { return false; }
        }

        public AuthResponseModel TokenCheck(string loginId)
        {
            AuthResponseModel authResponse = new AuthResponseModel();

            var record = _db.LoginDetails.FirstOrDefault(u => u.LoginId == loginId);

            if (record == null) { return null; }

            if (record.Token == null)
            {
                record.Token = Guid.NewGuid();
                record.TokenExpiry = DateTime.Now.AddHours(1);
                //record.TokenExpiry = DateTime.Now.AddMinutes(3);              
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
        }*/



    }
}
