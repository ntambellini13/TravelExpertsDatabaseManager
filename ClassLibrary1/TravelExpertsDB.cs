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
        /// <summary>
        /// Gets connection to Travel Experts DB
        /// </summary>
        /// <returns>SQLConnection object</returns>
        public static SqlConnection GetConnection()
        {
            String connectionString = "Data Source = localhost\\sqlexpress; Initial Catalog = TravelExperts; Integrated Security = True";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
