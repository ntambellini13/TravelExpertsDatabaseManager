using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class TravelExpertsDB
    {
        public static SqlConnection GetConnection()
        {
            String connectionString = "Data Source = localhost\\sqlexpress; Initial Catalog = TravelExperts; Integrated Security = True";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
