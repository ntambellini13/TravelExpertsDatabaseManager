using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class ProductSupplierDB
    {
        public static bool addProductSupplier(int productId, int supplierId)
        {
            bool success = false;
            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query =
                    "INSERT INTO Products_Suppliers " +
                    "(ProductId,SupplierId) " +
                    "VALUES (@ProductId, @SupplierId) ; ";
                // Adds all parameters to new SQL Command
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId);

                    success = cmd.ExecuteNonQuery() > 0; // Success if rows have been deleted

                } // cmd object recycled
            }// connection object recycled

            return success;
        }

        public static bool removeProductSupplier(int productId, int supplierId)
        {
            bool success = false;
            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query =
                    "DELETE FROM Products_Suppliers " +
                    "WHERE " +
                    "ProductId = @ProductId AND " +
                    "SupplierId = @SupplierId ; ";
                // Adds all parameters to new SQL Command
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId);

                    success = cmd.ExecuteNonQuery() > 0; // Success if rows have been deleted

                } // cmd object recycled
            }// connection object recycled

            return success;
        }
    }
}
