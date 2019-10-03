using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: Class for accessing Product-Supplier Database Objects
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsData
{
    public static class ProductsSuppliersDB
    {
        
        /// <summary>
        /// Gets sorted list of all product suppliers in DB. Key is prodsupplier id. Value is formatted string describing the pair.
        /// </summary>
        /// <returns>sorted list</returns>
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
        /// Gets a list of products by supplier id
        /// </summary>
        /// <returns>Product list</returns>
        public static List<Product> getProductsBySupplierId(int id)
        {
            List<Product> products = new List<Product>();
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // Gets products related to supplier id
            String query = "SELECT ps.ProductId, ProdName " +
                                "FROM " +
                                "Products_Suppliers ps " +
                                "JOIN Products pr " +
                                "ON ps.ProductId = pr.ProductId " +
                                "WHERE ps.SupplierId = @id " +
                                "ORDER BY ps.ProductID ; ";


            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader =
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                // Add all found products to list
                while (reader.Read())
                {
                    products.Add(new Product((int) reader["ProductId"],reader["ProdName"].ToString()));
                }
                connection.Close();
            }

            return products;
        }

        /// <summary>
        /// Gets a list of suppliers by product id
        /// </summary>
        /// <returns>Supplier list</returns>
        public static List<Supplier> getSuppliersByProductId(int id)
        {
            List<Supplier> suppliers = new List<Supplier>();
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // Gets suppliers related to product id
            String query = "SELECT ps.SupplierId, SupName " +
                                "FROM " +
                                "Products_Suppliers ps " +
                                "JOIN Suppliers s " +
                                "ON ps.SupplierId = s.SupplierId " +
                                "WHERE ps.ProductId = @id " +
                                "ORDER BY ps.SupplierId ";


            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader =
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                // Add all found suppliers to list
                while (reader.Read())
                {
                    suppliers.Add(new Supplier((int)reader["SupplierId"], reader["SupName"].ToString()));
                }
                connection.Close();
            }

            return suppliers;
        }

        /// <summary>
        /// Adds product supplier pair to DB
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="supplierId">Supplier ID</param>
        /// <returns>Successful?</returns>
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
        /// Removes product supplier pair to DB
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="supplierId">Supplier ID</param>
        /// <returns>Successful?</returns>
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
