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
using Microsoft.EntityFrameworkCore;
using Invoice.Record_Models;

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

            var dbContext = DbContextOptionsFactory.Create("DefaultConnect");
           
            var record = dbContext.LoginDetails.FirstOrDefault(u => u.LoginName == "Varun Gupta");

            record.LoginName = "nihha";
        }

        /*public string topNStates(InvoiceRequestModel requestModel)
        {
            List<InvoiceResponseModel> eInvoiceResponse = GetReport(requestModel);

            *//*var nStates = _db.GrossNetProduceStates
                                .OrderByDescending(s => s.grossIncome)
                                .Take(int.Parse(n)).Select(s => s.state)
                                .ToList();*/

            /*return JsonSerializer.Serialize(nStates);*//*
            return "";

        }*/

        public object GetReport(InvoiceRequestModel requestModel)
        {
            using (var _db = DbContextOptionsFactory.Create("EInvoice"))
            {
                string cmd = "EXEC usp_get_einv_app_stdata @Id, @StateCode, @SupType, @PerdYear, @PerdMon,'','', @OutIn, @ForUpto";

                if (requestModel.Id == 1)
                {                    
                    List<ForIdOne> eInvoiceReponse = _db.ForIdOne.FromSqlRaw(cmd,
                        new SqlParameter("@Id", requestModel.Id),
                        new SqlParameter("@StateCode", requestModel.stateCode),
                        new SqlParameter("@SupType", requestModel.supType),
                        new SqlParameter("@PerdYear", requestModel.perdYear),
                        new SqlParameter("@PerdMon", requestModel.perdMon),
                        new SqlParameter("@OutIn", requestModel.outIn),
                        new SqlParameter("@ForUpto", requestModel.for_upto)
                    ).AsNoTracking().ToList();

                    return eInvoiceReponse;

                }
                else if (requestModel.Id == 2)
                {
                    List<ForIdTwo> eInvoiceReponse = _db.ForIdTwo.FromSqlRaw(cmd,
                        new SqlParameter("@Id", requestModel.Id),
                        new SqlParameter("@StateCode", requestModel.stateCode),
                        new SqlParameter("@SupType", requestModel.supType),
                        new SqlParameter("@PerdYear", requestModel.perdYear),
                        new SqlParameter("@PerdMon", requestModel.perdMon),
                        new SqlParameter("@OutIn", requestModel.outIn),
                        new SqlParameter("@ForUpto", requestModel.for_upto)
                    ).AsNoTracking().ToList();

                    return eInvoiceReponse;

                }
                else if (requestModel.Id == 3)
                {
                    List<ForIdThree> eInvoiceReponse = _db.ForIdThree.FromSqlRaw(cmd,
                        new SqlParameter("@Id", requestModel.Id),
                        new SqlParameter("@StateCode", requestModel.stateCode),
                        new SqlParameter("@SupType", requestModel.supType),
                        new SqlParameter("@PerdYear", requestModel.perdYear),
                        new SqlParameter("@PerdMon", requestModel.perdMon),
                        new SqlParameter("@OutIn", requestModel.outIn),
                        new SqlParameter("@ForUpto", requestModel.for_upto)
                    ).AsNoTracking().ToList();

                    return eInvoiceReponse;

                }
                else if (requestModel.Id == 4)
                {
                    List<ForIdFour> eInvoiceReponse = _db.ForIdFour.FromSqlRaw(cmd,
                        new SqlParameter("@Id", requestModel.Id),
                        new SqlParameter("@StateCode", requestModel.stateCode),
                        new SqlParameter("@SupType", requestModel.supType),
                        new SqlParameter("@PerdYear", requestModel.perdYear),
                        new SqlParameter("@PerdMon", requestModel.perdMon),
                        new SqlParameter("@OutIn", requestModel.outIn),
                        new SqlParameter("@ForUpto", requestModel.for_upto)
                    ).AsNoTracking().ToList();

                    return eInvoiceReponse;

                }
                else if (requestModel.Id == 5)
                {
                    List<ForIdFive> eInvoiceReponse = _db.ForIdFive.FromSqlRaw(cmd,
                        new SqlParameter("@Id", requestModel.Id),
                        new SqlParameter("@StateCode", requestModel.stateCode),
                        new SqlParameter("@SupType", requestModel.supType),
                        new SqlParameter("@PerdYear", requestModel.perdYear),
                        new SqlParameter("@PerdMon", requestModel.perdMon),
                        new SqlParameter("@OutIn", requestModel.outIn),
                        new SqlParameter("@ForUpto", requestModel.for_upto)
                    ).AsNoTracking().ToList();

                    return eInvoiceReponse;

                }
                else if (requestModel.Id == 6)
                {
                    List<ForIdSix> eInvoiceReponse = _db.ForIdSix.FromSqlRaw(cmd,
                        new SqlParameter("@Id", requestModel.Id),
                        new SqlParameter("@StateCode", requestModel.stateCode),
                        new SqlParameter("@SupType", requestModel.supType),
                        new SqlParameter("@PerdYear", requestModel.perdYear),
                        new SqlParameter("@PerdMon", requestModel.perdMon),
                        new SqlParameter("@OutIn", requestModel.outIn),
                        new SqlParameter("@ForUpto", requestModel.for_upto)
                    ).AsNoTracking().ToList();

                    return eInvoiceReponse;

                }
                else
                {
                    return null;
                }



            }
            


            // NEED TO EXECUTE STORED PRODECURE HERE

            throw new NotImplementedException();
        }

        public Dictionary<string, string> ClientIdentityFetch(AuthRequestHeaders request)
        {            
            using (var _db = DbContextOptionsFactory.Create("DefaultConnect"))
            {
                var clientRecord = _db.ClientIdentity.FirstOrDefault(u => u.client_id == request.ClientId);
                var loginRecord = _db.LoginDetails.FirstOrDefault(u => u.LoginId == request.LoginId);


                if (clientRecord != null && loginRecord != null)
                {
                    Dictionary<string, string> returnValues = new Dictionary<string, string>();

                    returnValues["client_id"] = clientRecord.client_id;
                    returnValues["client_secret"] = clientRecord.client_secret;
                    returnValues["token"] = loginRecord.Token.ToString()!;
                    returnValues["expiry"] = loginRecord.TokenExpiry.ToString()!;

                    return returnValues;
                }
                else
                {
                    return null;
                }

            }           

        }

        public bool LoginDetailsVerify(TokenRequestModel req)
        {
            using(var _db = DbContextOptionsFactory.Create("DefaultConnect"))
            {
                try
                {
                    var record = _db.LoginDetails.FirstOrDefault(u => u.LoginId == req.LoginId);
                    if (record.Password == req.Password && record.PhoneNumber == req.PhoneNumber)
                    {
                        return true;
                    }
                    else { return false; }
                }
                catch (Exception ex)
                {
                    return false;
                }
                
            }
            
        }

        public AuthResponseModel TokenCheck(string loginId)
        {
            using(var _db = DbContextOptionsFactory.Create("DefaultConnect"))
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
                    if (record.TokenExpiry <= DateTime.Now)
                    {
                        record.Token = Guid.NewGuid();
                        record.TokenExpiry = DateTime.Now.AddHours(1);
                    }
                }

                _db.SaveChanges();


                authResponse.AuthToken = (Guid)record.Token;
                authResponse.TokenTime = record.TokenExpiry.ToString();

                return authResponse;
            }
            
        }
    }
}
