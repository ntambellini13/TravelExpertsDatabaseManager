using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class PackagesProductsSuppliersDB
    {
        public static SortedList<int, string> getProductsSuppliersIdAndString_ByPackageId(int packageId)
        {
            SortedList<int, string> productsSuppliers = new SortedList<int, string>();
            SqlConnection connection = TravelExpertsDB.GetConnection();


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
                while (reader.Read())
                {
                    productsSuppliers.Add((int) reader["ProductSupplierId"], reader["ProdName"].ToString() + " from " + reader["SupName"].ToString());
                }
                connection.Close();
            }

            return productsSuppliers;
        }

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
