using Invoice.WebModels;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Invoice.Record_Models;
using Invoice.Data;
using Invoice.Interfaces;
using Invoice.Web_Models;

namespace Invoice.Repositories
{
    /// <summary>
    /// Repository class implementing DatabaseOperationsInterface for handling database operations.
    /// </summary>
    public class DatabaseOperations : DatabaseOperationsInterface
    {
        /// <summary>
        /// Generic method to fetch reports based on the provided request model.
        /// </summary>
        /// <typeparam name="T">The type of report to fetch.</typeparam>
        /// <param name="requestModel">The invoice request model containing information for the report.</param>
        /// <returns>A list of reports of the specified type.</returns>
        private List<T> ReportFetch<T>(InvoiceRequestModel requestModel) where T : class
        {
            using (var _db = DbContextOptionsFactory.Create("EInvoice"))
            {
                string cmd = "EXEC usp_get_einv_app_stdata @Id, @StateCode, @SupType, @PerdYear, @PerdMon,'','', @OutIn, @ForUpto";

                List<T> eInvoiceReponse = _db.Set<T>().FromSqlRaw(cmd,
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
        }

        /// <summary>
        /// Retrieves a report based on the provided invoice request model.
        /// </summary>
        /// <param name="requestModel">The invoice request model containing information for the report.</param>
        /// <returns>An object representing the generated report.</returns>
        public object GetReport(InvoiceRequestModel requestModel)
        {
            using (var _db = DbContextOptionsFactory.Create("EInvoice"))
            {
                string cmd = "EXEC usp_get_einv_app_stdata @Id, @StateCode, @SupType, @PerdYear, @PerdMon,'','', @OutIn, @ForUpto";

                if (requestModel.Id == 1)
                {
                    List<ForIdOne> eInvoiceResponse = ReportFetch<ForIdOne>(requestModel);
                    return eInvoiceResponse;
                }
                else if (requestModel.Id == 2)
                {
                    List<ForIdTwo> eInvoiceResponse = ReportFetch<ForIdTwo>(requestModel);
                    return eInvoiceResponse;
                }
                else if (requestModel.Id == 3)
                {
                    List<ForIdThree> eInvoiceResponse = ReportFetch<ForIdThree>(requestModel);
                    return eInvoiceResponse;
                }
                else if (requestModel.Id == 4)
                {
                    List<ForIdFour> eInvoiceResponse = ReportFetch<ForIdFour>(requestModel);
                    return eInvoiceResponse;
                }
                else if (requestModel.Id == 5)
                {
                    List<ForIdFive> eInvoiceResponse = ReportFetch<ForIdFive>(requestModel);
                    return eInvoiceResponse;
                }
                else if (requestModel.Id == 6)
                {
                    List<ForIdSix> eInvoiceResponse = ReportFetch<ForIdSix>(requestModel);
                    return eInvoiceResponse;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Fetches client identity information based on the provided request headers.
        /// </summary>
        /// <param name="request">The request headers containing client information.</param>
        /// <returns>A dictionary containing client identity information.</returns>
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

        /// <summary>
        /// Verifies login details based on the provided token request model.
        /// </summary>
        /// <param name="req">The token request model containing login details.</param>
        /// <returns>True if login details are valid; otherwise, false.</returns>
        public bool LoginDetailsVerify(TokenRequestModel req)
        {
            using (var _db = DbContextOptionsFactory.Create("DefaultConnect"))
            {
                try
                {
                    var record = _db.LoginDetails.FirstOrDefault(u => u.LoginId == req.LoginId);
                    if (record.Password == req.Password && record.PhoneNumber == req.PhoneNumber)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Checks the validity of a token based on the provided login ID.
        /// </summary>
        /// <param name="loginId">The login ID for which the token is checked.</param>
        /// <returns>An authentication response model containing token information.</returns>
        public AuthResponseModel TokenCheck(string loginId)
        {
            using (var _db = DbContextOptionsFactory.Create("DefaultConnect"))
            {
                AuthResponseModel authResponse = new AuthResponseModel();

                var record = _db.LoginDetails.FirstOrDefault(u => u.LoginId == loginId);

                if (record == null)
                {
                    return null;
                }

                if (record.Token == null)
                {
                    record.Token = Guid.NewGuid();
                    record.TokenExpiry = DateTime.Now.AddHours(1);
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
                authResponse.TokenTime = record.TokenExpiry.ToString()!;

                return authResponse;
            }
        }
    }
}
