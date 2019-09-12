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

        public static bool UpdatePackage(Package oldPackage, Package newPackage)
        {
            bool success = false;
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

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@NewPkgName", newPackage.PackageName);
                    cmd.Parameters.AddWithValue("@NewImage", newPackage.ImagePath);
                    cmd.Parameters.AddWithValue("@NewPartner", newPackage.PartnerURL);
                    cmd.Parameters.AddWithValue("@NewAirfairInclusion", newPackage.AirfairInclusion ? 1 : 0);
                    cmd.Parameters.AddWithValue("@NewPkgStartDate", newPackage.PackageStartDate);
                    cmd.Parameters.AddWithValue("@NewPkgEndDate", newPackage.PackageEndDate);
                    cmd.Parameters.AddWithValue("@NewPkgDesc", newPackage.PackageDescription);
                    cmd.Parameters.AddWithValue("@NewPkgBasePrice", newPackage.PackageBasePrice);
                    cmd.Parameters.AddWithValue("@NewPkgAgencyCommission", newPackage.PackageAgencyCommission);

                    cmd.Parameters.AddWithValue("@OldPackageId", oldPackage.PackageId);
                    cmd.Parameters.AddWithValue("@OldPkgName", oldPackage.PackageName);
                    cmd.Parameters.AddWithValue("@OldImage", oldPackage.ImagePath);
                    cmd.Parameters.AddWithValue("@OldPartner", oldPackage.PartnerURL);
                    cmd.Parameters.AddWithValue("@OldAirfairInclusion", oldPackage.AirfairInclusion ? 1 : 0);
                    cmd.Parameters.AddWithValue("@OldPkgStartDate", oldPackage.PackageStartDate);
                    cmd.Parameters.AddWithValue("@OldPkgEndDate", oldPackage.PackageEndDate);
                    cmd.Parameters.AddWithValue("@OldPkgDesc", oldPackage.PackageDescription);
                    cmd.Parameters.AddWithValue("@OldPkgBasePrice", oldPackage.PackageBasePrice);
                    cmd.Parameters.AddWithValue("@OldPkgAgencyCommission", oldPackage.PackageAgencyCommission);

                    success = cmd.ExecuteNonQuery() > 0;

                } // cmd object recycled
            }// connection object recycled
            if (success && oldPackage.ImagePath!=newPackage.ImagePath)
            {
                     // Do something for images     
   
            }

            return success;
        }

        public static bool AddPackage(Package newPackage)
        {
            bool success = false;
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "INSERT INTO Packages VALUES " +
                               $"(@PkgName,@Image,@Partner,@AirfairInclusion,@PkgStartDate,@PkgEndDate,@PkgDesc,@PkgBasePrice,@PkgAgencyCommission); ";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@PkgName", newPackage.PackageName);
                    cmd.Parameters.AddWithValue("@Image", newPackage.ImagePath);
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
            if (success)
            {
                // Copy image over
               /*
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create) + "\\TravelExpertsDatabaseManager\\packageImages\\";
                string fileSavePath = Path.Combine(appDataPath, newPackage.ImagePath.Split('\\').Last());
                File.Copy(newPackage.ImagePath, fileSavePath,true);
                */

            }
            
            return success;
        }

    }
}
