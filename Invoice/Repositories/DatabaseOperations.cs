using Invoice.WebModels;
using Invoice.Data;
using Invoice.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace Invoice.Repositories
{


    public class DatabaseOperations : DatabaseOperationsInterface
    {
        private readonly ApplicationDbContext _db;

        public DatabaseOperations(ApplicationDbContext db)
        {
            _db = db;
        }    

        public string topNStates(int n)
        { 
            var nStates = _db.GrossNetProduceStates
                                .OrderByDescending(s => s.grossIncome)
                                .Take(n).Select(s => s.state)
                                .ToList();

            return JsonSerializer.Serialize(nStates);
            
        }

        public (string,string) ClientIdentityFetch(string client_id)
        {
           var record =  _db.ClientIdentity.FirstOrDefault(u => u.client_id == client_id);

            if (record != null)
            {
                return (record.client_id, record.client_secret);
            }
            else
            {
                return (null, null);
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

        public Guid TokenCheck(string loginId)
        {
            var record = _db.LoginDetails.FirstOrDefault(u => u.LoginId == loginId);

            if (record.Token == null)
            {
                record.Token = Guid.NewGuid();
                //record.TokenExpiry = DateTime.Now.AddHours(1);
                record.TokenExpiry = DateTime.Now.AddMinutes(1);              
            }
            else
            {
                if(record.TokenExpiry < DateTime.Now)
                {
                    record.Token = Guid.NewGuid();
                    record.TokenExpiry = DateTime.Now.AddMinutes(1);
                }
            }

            _db.SaveChanges();

            return (Guid)record.Token;
        }

        
    }
}
