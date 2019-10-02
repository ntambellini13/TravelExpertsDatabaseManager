using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: Class for accessing Packages-Products-Suppliers Database Objects
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsData
{
    public static class PackagesProductsSuppliersDB
    {
        /// <summary>
        /// Gets the associated productsuppliers for the selected package
        /// </summary>
        /// <param name="packageId">Package id</param>
        /// <returns>A sorted list with productsupplierId as key and formatted string as value</returns>
        public static SortedList<int, string> getProductsSuppliersIdAndString_ByPackageId(int packageId)
        {
            SortedList<int, string> productsSuppliers = new SortedList<int, string>();
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // Gets all associated productsuppliers
            String query = "SELECT pps.ProductSupplierId, ProdName, SupName " +
                                    "FROM Packages p " +
                                    "JOIN Packages_Products_Suppliers pps " +
                                    "ON p.PackageId = pps.PackageId " +
                                    "JOIN Products_Suppliers ps " +
                                    "ON pps.PackageId = ps.ProductSupplierId " +
                                    "JOIN Products pr " +
                                    "ON ps.ProductId = pr.ProductId " +
                                    "JOIN Suppliers s " +
                                    "ON ps.SupplierId = s.SupplierId " +
                                    "WHERE p.PackageId=@PackageId ; ";


            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@PackageId", packageId);
                SqlDataReader reader =
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                // Adds each to sorted list
                while (reader.Read())
                {
                    productsSuppliers.Add((int) reader["ProductSupplierId"], reader["ProdName"].ToString() + " from " + reader["SupName"].ToString());
                }
                connection.Close();
            }

            return productsSuppliers;
        }

        /// <summary>
        /// Inserts package id and product supplier id into packages product suppliers table
        /// </summary>
        /// <param name="packageId">Package Id</param>
        /// <param name="productSupplierId">ProductSupplier Id</param>
        /// <returns>Successful?</returns>
        public static bool addPackageProductSupplier(int packageId, int productSupplierId)
        {
            bool success = false;
            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query =
                    "INSERT INTO Packages_Products_Suppliers " +
                    "(PackageId, ProductSupplierId) " +
                    "VALUES (@PackageId, @ProductSupplierId) ; ";
                // Adds all parameters to new SQL Command
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@PackageId", packageId);
                    cmd.Parameters.AddWithValue("@ProductSupplierId", productSupplierId);

                    success = cmd.ExecuteNonQuery() > 0; // Success if rows have been deleted

                } // cmd object recycled
            }// connection object recycled

            return success;
        }

        /// <summary>
        /// Deletes package product supplier entry from table
        /// </summary>
        /// <param name="packageId">Package id</param>
        /// <param name="productSupplierId">Product supplier id</param>
        /// <returns>Successful?</returns>
        public static bool removePackageProductSupplier(int packageId, int productSupplierId)
        {
            bool success = false;
            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query =
                    "DELETE FROM Packages_Products_Suppliers " +
                    "WHERE " +
                    "PackageId = @PackageId AND " +
                    "ProductSupplierId = @ProductSupplierId ; ";
                // Adds all parameters to new SQL Command
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@PackageId", packageId);
                    cmd.Parameters.AddWithValue("@ProductSupplierId", productSupplierId);

                    success = cmd.ExecuteNonQuery() > 0; // Success if rows have been deleted

                } // cmd object recycled
            }// connection object recycled

            return success;
        }
    }
}
