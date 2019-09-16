using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class ProductsDB
    {
        public static List<int> GetProductIds()
        {
            List<int> productIds = new List<int>();
            int productId; // for reading

            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT ProductId " +
                               "FROM Products " +
                               "ORDER BY ProductId";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader =
                        cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        productId = (int)reader["ProductId"];
                        productIds.Add(productId); // add to the list
                    }
                } // cmd object recycled
            }// connection object recycled
            return productIds;
        }

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            Product product; // for reading

            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT * " +
                               "FROM Products " +
                               "ORDER BY ProductId";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader =
                        cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        // Create new product with current row information
                        product = new Product((int)reader["ProductId"],reader["ProdName"].ToString());

                        products.Add(product); // add to the list
                    }
                } // cmd object recycled
            }// connection object recycled
            return products;
        }

        public static void AddProducts(string newProduct)
        {
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "INSERT INTO Products " +
                               "(ProdName) " +
                               "VALUES(@productName)";

                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                //add and declare sql parameters for the adapter insert command

                /*********
                 * Add code here to take user input of product name
                 * 
                 * ********/

                //associate the insert command
                adapter.InsertCommand = new SqlCommand(query, connection);

                //add parameters
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@productName", newProduct));
                
                adapter.InsertCommand.ExecuteNonQuery();//execute the insert
            }// connection object recycled
        }

        public static void EditProduct(string editProduct, int editProductId)
        {
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "UPDATE Products " +
                               "SET ProdName = @productName " +
                               "WHERE ProductId = @productId";

                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();

                /*********
                 * Add code here to take user input of product name
                 * 
                 * ********/

                //associate the insert command
                adapter.UpdateCommand = new SqlCommand(query, connection);

                //add and declare sql parameters for the adapter update command
                adapter.UpdateCommand.Parameters.Add(new SqlParameter("@productName", editProduct));
                adapter.UpdateCommand.Parameters.Add(new SqlParameter("@productId", editProductId));

                adapter.UpdateCommand.ExecuteNonQuery();
            }// connection object recycled
        }
    }
}
