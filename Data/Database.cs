using System.Data.SqlClient;
using System.Configuration;

namespace Data
{
    public class Database
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString);
        }
    }
}
