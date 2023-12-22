using Microsoft.Data.SqlClient;

namespace Invoice.Repositories
{
    public class ConnectionDAL
    {
        private SqlConnection con = new SqlConnection();
        private SqlDataReader reader;
        private SqlCommand com = new SqlCommand();
        public ConnectionDAL(string connectionString) 
        {
            /*con.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;*/
        }

    }
}