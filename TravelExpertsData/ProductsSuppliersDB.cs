using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: Class for creating ProductSupplier Database Objects
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsData
{
    public static class ProductsSuppliersDB
    {
        

        public static SortedList<int, string> getProductsSuppliersIdAndString()
        {
            SortedList<int, string> productsSuppliers = new SortedList<int, string>();
            SqlConnection connection = TravelExpertsDB.GetConnection();

            
            String query = "SELECT ProductSupplierId, ProdName, SupName " +
                                "FROM " +
                                "Products_Suppliers ps " +
                                "JOIN Products pr " +
                                "ON ps.ProductId = pr.ProductId " +
                                "JOIN Suppliers s " +
                                "ON ps.SupplierId = s.SupplierId ; ";


            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
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

        /// <summary>
        /// Public static class method for adding product supplier
        /// </summary>
        /// <param name="productId">int productId</param>
        /// <param name="supplierId">int supplierId</param>
        /// <returns>bool success</returns>
        public static bool addProductSupplier(int productId, int supplierId)
        {
            bool success = false;//bool success value returned to tell if query successful

            //Sql connection block to connect to TravelExpertsDB; closes connection at end of block
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

                    // Adds all parameters to new SQL Command
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId);

                    success = cmd.ExecuteNonQuery() > 0; // Success if rows have been deleted

                } // cmd object recycled
            }// connection object recycled

            return success;
        }

        /// <summary>
        /// Public static class method for removing product supplier
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static bool removeProductSupplier(int productId, int supplierId)
        {
            bool success = false;//bool success value returned to tell if query successful

            //Sql connection block to connect to TravelExpertsDB; closes connection at end of block
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query =
                    "DELETE FROM Products_Suppliers " +
                    "WHERE " +
                    "ProductId = @ProductId AND " +
                    "SupplierId = @SupplierId ; ";
                
                //sql command block; disposes command at end of block
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    //Adds all parameters to new SQL Command
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId);

                    success = cmd.ExecuteNonQuery() > 0; // Success if rows have been deleted

                } // cmd object recycled
            }// connection object recycled

            return success;
        }
    }
}
