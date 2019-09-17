using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class PackagesDB
    {
        /// <summary>
        /// Gets all the package ids in the db
        /// </summary>
        /// <returns>List of package ids</returns>
        public static List<int> GetPackageIds()
        {
            List<int> packageIds = new List<int>(); // Empty list
            int packageId; // for reading

            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT PackageId " +
                               "FROM Packages " +
                               "ORDER BY PackageId";
                // Creates command
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Runs command
                    connection.Open();
                    SqlDataReader reader =
                        cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    // Reads and adds all package ids to the list
                    while (reader.Read())
                    {
                        packageId = (int) reader["PackageId"];
                        packageIds.Add(packageId); // add to the list
                    }
                } // cmd object recycled
            }// connection object recycled
            return packageIds;
        }

        /// <summary>
        /// Gets a list of all packages in the DB
        /// </summary>
        /// <returns>List of packages</returns>
        public static List<Package> GetPackages()
        {
            List<Package> packages = new List<Package>(); // Empty list
            Package package; // for reading

            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT * " +
                               "FROM Packages " +
                               "ORDER BY PackageId";

                // Creates command
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader =
                        cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    // Reads all data into new package and adds to list
                    while (reader.Read())
                    {
                        // Create new package with current row information
                        package = new Package
                            ((int) reader["PackageId"],
                            reader["PkgName"].ToString(),
                            (byte[]) reader["Image"],
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

        /// <summary>
        /// Updates the specified package. Ensures no concurrency error by checking that it will only update if the package has not changed.
        /// </summary>
        /// <param name="oldPackage">Package info that program currently has</param>
        /// <param name="newPackage">New package info</param>
        /// <returns>Successful?</returns>
        public static bool UpdatePackage(Package oldPackage, Package newPackage)
        {
            bool success;
            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query =
                    "UPDATE Packages " +
                    "SET " +
                    "PkgName = @NewPkgName, " +
                    "Image = @NewImage, " +
                    "Partner = @NewPartner, " +
                    "AirfairInclusion = @NewAirfairInclusion, " +
                    "PkgStartDate = @NewPkgStartDate, " +
                    "PkgEndDate = @NewPkgEndDate, " +
                    "PkgDesc = @NewPkgDesc, " +
                    "PkgBasePrice = @NewPkgBasePrice, " +
                    "PkgAgencyCommission = @NewPkgAgencyCommission " +
                    "WHERE " +
                    "PackageId = @OldPackageId AND " +
                    "PkgName = @OldPkgName AND " +
                    "Image = @OldImage AND " +
                    "Partner = @OldPartner AND " +
                    "AirfairInclusion = @OldAirfairInclusion AND " +
                    "PkgStartDate = @OldPkgStartDate AND " +
                    "PkgEndDate = @OldPkgEndDate AND " +
                    "PkgDesc = @OldPkgDesc AND " +
                    "PkgBasePrice = @OldPkgBasePrice AND " +
                    "PkgAgencyCommission = @OldPkgAgencyCommission ; ";

                // Creates command and adds all proper parameters
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@NewPkgName", newPackage.PackageName);
                    cmd.Parameters.AddWithValue("@NewImage", newPackage.Image);
                    cmd.Parameters.AddWithValue("@NewPartner", newPackage.PartnerURL);
                    cmd.Parameters.AddWithValue("@NewAirfairInclusion", newPackage.AirfairInclusion ? 1 : 0);
                    cmd.Parameters.AddWithValue("@NewPkgStartDate", newPackage.PackageStartDate);
                    cmd.Parameters.AddWithValue("@NewPkgEndDate", newPackage.PackageEndDate);
                    cmd.Parameters.AddWithValue("@NewPkgDesc", newPackage.PackageDescription);
                    cmd.Parameters.AddWithValue("@NewPkgBasePrice", newPackage.PackageBasePrice);
                    cmd.Parameters.AddWithValue("@NewPkgAgencyCommission", newPackage.PackageAgencyCommission);

                    cmd.Parameters.AddWithValue("@OldPackageId", oldPackage.PackageId);
                    cmd.Parameters.AddWithValue("@OldPkgName", oldPackage.PackageName);                  
                    cmd.Parameters.AddWithValue("@OldImage", oldPackage.Image);                    
                    cmd.Parameters.AddWithValue("@OldPartner", oldPackage.PartnerURL);
                    cmd.Parameters.AddWithValue("@OldAirfairInclusion", oldPackage.AirfairInclusion ? 1 : 0);
                    cmd.Parameters.AddWithValue("@OldPkgStartDate", oldPackage.PackageStartDate);
                    cmd.Parameters.AddWithValue("@OldPkgEndDate", oldPackage.PackageEndDate);
                    cmd.Parameters.AddWithValue("@OldPkgDesc", oldPackage.PackageDescription);
                    cmd.Parameters.AddWithValue("@OldPkgBasePrice", oldPackage.PackageBasePrice);
                    cmd.Parameters.AddWithValue("@OldPkgAgencyCommission", oldPackage.PackageAgencyCommission);

                    success = cmd.ExecuteNonQuery() > 0; // Success if rows changed

                } // cmd object recycled
            }// connection object recycled
                        

            return success;
        }

        /// <summary>
        /// Deletes package specified only if all information is the same as in the program
        /// </summary>
        /// <param name="oldPackage">Package to delete</param>
        /// <returns>Successful?</returns>
        public static bool DeletePackage(Package oldPackage)
        {
            bool success = false;
            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query =
                    "DELETE FROM Packages " +
                    "WHERE " +
                    "PackageId = @OldPackageId AND " +
                    "PkgName = @OldPkgName AND " +
                    "Image = @OldImage AND " +
                    "Partner = @OldPartner AND " +
                    "AirfairInclusion = @OldAirfairInclusion AND " +
                    "PkgStartDate = @OldPkgStartDate AND " +
                    "PkgEndDate = @OldPkgEndDate AND " +
                    "PkgDesc = @OldPkgDesc AND " +
                    "PkgBasePrice = @OldPkgBasePrice AND " +
                    "PkgAgencyCommission = @OldPkgAgencyCommission ; ";
                // Adds all parameters to new SQL Command
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@OldPackageId", oldPackage.PackageId);
                    cmd.Parameters.AddWithValue("@OldPkgName", oldPackage.PackageName);
                    cmd.Parameters.AddWithValue("@OldImage", oldPackage.Image);
                    cmd.Parameters.AddWithValue("@OldPartner", oldPackage.PartnerURL);
                    cmd.Parameters.AddWithValue("@OldAirfairInclusion", oldPackage.AirfairInclusion ? 1 : 0);
                    cmd.Parameters.AddWithValue("@OldPkgStartDate", oldPackage.PackageStartDate);
                    cmd.Parameters.AddWithValue("@OldPkgEndDate", oldPackage.PackageEndDate);
                    cmd.Parameters.AddWithValue("@OldPkgDesc", oldPackage.PackageDescription);
                    cmd.Parameters.AddWithValue("@OldPkgBasePrice", oldPackage.PackageBasePrice);
                    cmd.Parameters.AddWithValue("@OldPkgAgencyCommission", oldPackage.PackageAgencyCommission);

                    success = cmd.ExecuteNonQuery() > 0; // Success if rows have been deleted

                } // cmd object recycled
            }// connection object recycled
         

            return success;
        }

        /// <summary>
        /// Adds a new package to the DB
        /// </summary>
        /// <param name="newPackage">Package to add</param>
        /// <returns>Successful?</returns>
        public static bool AddPackage(Package newPackage)
        {
            bool success = false;
            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "INSERT INTO Packages VALUES " +
                               $"(@PkgName,@Image,@Partner,@AirfairInclusion,@PkgStartDate,@PkgEndDate,@PkgDesc,@PkgBasePrice,@PkgAgencyCommission); ";
                // Builds command with parameters
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@PkgName", newPackage.PackageName);
                    cmd.Parameters.AddWithValue("@Image", newPackage.Image);
                    cmd.Parameters.AddWithValue("@Partner", newPackage.PartnerURL);
                    cmd.Parameters.AddWithValue("@AirfairInclusion", newPackage.AirfairInclusion?1:0);
                    cmd.Parameters.AddWithValue("@PkgStartDate", newPackage.PackageStartDate);
                    cmd.Parameters.AddWithValue("@PkgEndDate", newPackage.PackageEndDate);
                    cmd.Parameters.AddWithValue("@PkgDesc", newPackage.PackageDescription);
                    cmd.Parameters.AddWithValue("@PkgBasePrice", newPackage.PackageBasePrice);
                    cmd.Parameters.AddWithValue("@PkgAgencyCommission", newPackage.PackageAgencyCommission);

                    success = cmd.ExecuteNonQuery()>0;
                    
                } // cmd object recycled
            }// connection object recycled

            
            return success;
        }

    }
}
