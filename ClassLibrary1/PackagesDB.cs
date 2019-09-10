using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class PackagesDB
    {
        public static List<int> GetPackageIds()
        {
            List<int> packageIds = new List<int>();
            int packageId; // for reading

            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT PackageId " +
                               "FROM Packages " +
                               "ORDER BY PackageId";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader =
                        cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        packageId = (int) reader["PackageId"];
                        packageIds.Add(packageId); // add to the list
                    }
                } // cmd object recycled
            }// connection object recycled
            return packageIds;
        }

        public static List<Package> GetPackages()
        {
            List<Package> packages = new List<Package>();
            Package package; // for reading

            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT * " +
                               "FROM Packages " +
                               "ORDER BY PackageId";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader =
                        cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        // Create new package with current row information
                        package = new Package
                            ((int) reader["PackageId"],
                            reader["PkgName"].ToString(),
                            reader["Image"].ToString(),
                            reader["Partner"].ToString(),
                            ((int) reader["AirfairInclusion"]==1),
                            (DateTime) reader["PkgStartDate"],
                            (DateTime) reader["PkgEndDate"],
                            reader["PkgDesc"].ToString(),
                            (decimal) reader["PkgBasePrice"],
                            (decimal) reader["PkgAgencyCommission"]);// empty invoice object
                        
                        packages.Add(package); // add to the list
                    }
                } // cmd object recycled
            }// connection object recycled
            return packages;
        }

    }
}
