using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace CollegeManagementSystem
{
    class DataBase
    {
        public static string getcon()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = ".";
            builder.InitialCatalog = "CollegeDataBase";
            builder.IntegratedSecurity = true;
            builder.ConnectTimeout = 30;

            return builder.ConnectionString;

        }

        public SqlConnection con = new SqlConnection(getcon());
    }
}
