using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: Class for accessing Product Database Objects
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsData
{
    public static class ProductsDB
    {
        /// <summary>
        /// Public static class method for retrieving all ProductId's from database
        /// </summary>
        /// <returns>int list of ProductId's</returns>
        public static List<int> GetProductIds()
        {
            //int list variable to store returned productIds
            List<int> productIds = new List<int>();

            int productId; //stores each retrieved id

            //Sql connection block to connect to TravelExpertsDB; closes connection at end of block
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT ProductId " +
                               "FROM Products " +
                               "ORDER BY ProductId";
                //sql command block; disposes command at end of block
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader =
                        cmd.ExecuteReader(CommandBehavior.CloseConnection);//new sqldatareader for accessing db
                    while (reader.Read())
                    {
                        productId = (int)reader["ProductId"];
                        productIds.Add(productId); // add to the list
                    }
                } // cmd object recycled
            }// connection object recycled
            return productIds;
        }

        /// <summary>
        /// Public static method gets Products from database, builds Product objects from
        /// their returned info, then loads each object into a list
        /// </summary>
        /// <returns>list of Product objects</returns>
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            Product product; // for reading

            //Sql connection block to connect to TravelExpertsDB; closes connection at end of block
            //Executes query to retrieve ProductId's
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT * " +
                               "FROM Products " +
                               "ORDER BY ProductId";
                //sql command block; disposes command at end of block
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader =
                        cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        int productId = (int)reader["ProductId"]; // get product ID
                        // Run subquery on product id to get list of suppliers
                        
                        

                        // Create new product with current row information
                        product = new Product((int)reader["ProductId"],reader["ProdName"].ToString());

                        products.Add(product); // add to the list
                    }
                } // cmd object recycled
            }// connection object recycled

            SqlConnection connection2 = TravelExpertsDB.GetConnection();//create new sql connection

            //for loop that iterates through the created list of Product objects
            for (int i = 0; i < products.Count(); i++)
            {
                //runs a query that retrieves Suppliers associated with each Product
                String subQuery = "SELECT s.SupplierId, s.SupName " +
                            "FROM Suppliers s JOIN Products_Suppliers ps " +
                            "ON s.SupplierId = ps.SupplierId " +
                            "WHERE ps.ProductId = @ProductId; ";

                //clear current Product's list property for Suppliers
                //to be loaded with the retrieved Suppliers
                products[i].Suppliers.Clear();

                //query that populates each retrieved Supplier's information into the Product
                //object's class property for Suppliers

                //sql command block; disposes command at end of block
                using (SqlCommand subCmd = new SqlCommand(subQuery, connection2))
                {
                    connection2.Open();
                    //define and add sql parameter for our sqlcommand
                    subCmd.Parameters.AddWithValue("@ProductId", products[i].ProductId);
                    SqlDataReader subReader =
                        subCmd.ExecuteReader(CommandBehavior.CloseConnection);//new sqldatareader for accessing db
                    while (subReader.Read())
                    {
                        //add a new supplier to the current product
                        products[i].Suppliers.Add(new Supplier((int) subReader["SupplierId"],subReader["SupName"].ToString()));
                    }
                    connection2.Close();
                }
            }
            
            return products;
        }

        /// <summary>
        /// Public static method for adding a product to the suppliers database table 
        /// </summary>
        /// <param name="newSupplier">string name of new product</param>
        public static void AddProducts(string newProduct)
        {
            //Sql connection block to connect to TravelExpertsDB; closes connection at end of block
            //Executes query to add products
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "INSERT INTO Products " +
                               "(ProdName) " +
                               "VALUES(@productName)";

                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();//new sql adapter for modifying db

                //associate the insert command
                adapter.InsertCommand = new SqlCommand(query, connection);

                //add parameters
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@productName", newProduct));
                
                adapter.InsertCommand.ExecuteNonQuery();//execute the insert
            }// connection object recycled
        }

        /// <summary>
        /// Public static class method for editing products
        /// </summary>
        /// <param name="oldProduct">Product class object for getting old values</param>
        /// <param name="newProduct">Product class object for getting new values</param>
        /// <returns></returns>
        public static bool UpdateProduct(Product oldProduct, Product newProduct)
        {
            bool success;
            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query =
                    "UPDATE Products " +
                    "SET " +
                    "ProdName = @NewProductName " +
                    
                    "WHERE " +
                    "ProductId = @OldProductId AND " +
                    "ProdName = @OldProdName ; ";
                    

                // Creates command and adds all proper parameters
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    //Add parameters for sql query
                    cmd.Parameters.AddWithValue("@NewProductName", newProduct.ProductName);
                    cmd.Parameters.AddWithValue("@OldProductId", oldProduct.ProductId);
                    cmd.Parameters.AddWithValue("@OldProdName", oldProduct.ProductName);

                    success = cmd.ExecuteNonQuery() > 0; // Success if rows changed

                } // cmd object recycled
            }// connection object recycled


            return success;
        }

        /// <summary>
        /// Public static class method for deleting products
        /// </summary>
        /// <param name="oldProduct">Product class object for products being deleted</param>
        /// <returns></returns>
        public static bool DeleteProduct(Product oldProduct)
        {
            bool success = false;
            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query =
                    "DELETE FROM Products " +
                    "WHERE " +
                    "ProductId = @OldProductId AND " +
                    "ProdName = @OldProductName ; ";
                // Adds all parameters to new SQL Command
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    //Add parameters for sql query
                    cmd.Parameters.AddWithValue("@OldProductId", oldProduct.ProductId);
                    cmd.Parameters.AddWithValue("@OldProductName", oldProduct.ProductName);
                    

                    success = cmd.ExecuteNonQuery() > 0; // Success if rows have been deleted

                } // cmd object recycled
            }// connection object recycled


            return success;
        }
    }
}
