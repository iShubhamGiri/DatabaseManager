using System.Configuration;
using System.Data.SqlClient;

namespace DatabaseManager.Helpers
{
    public static class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            var connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return new SqlConnection(connStr);
        }
    }
}
