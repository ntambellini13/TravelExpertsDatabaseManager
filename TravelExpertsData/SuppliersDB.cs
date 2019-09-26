using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: Class for creating Supplier Database Objects
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsData
{
    public static class SuppliersDB
    {
        /// <summary>
        /// Public static class method for retrieving all SupplierId's from database
        /// </summary>
        /// <returns>int list of SupplierId's</returns>
        public static List<int> GetSupplierIds()
        {
            //int list variable to store returned supplierIds
            List<int> supplierIds = new List<int>(); 

            int supplierId;//stores each retrieved id

            //Sql connection block to connect to TravelExpertsDB; closes connection at end of block
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT SupplierId " +
                               "FROM Suppliers " +
                               "ORDER BY SupplierId";

                //sql command block; disposes command at end of block
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader =
                        cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        supplierId = (int)reader["ProductId"];
                        supplierIds.Add(supplierId); // add to the list
                    }
                } // cmd object recycled
            }// connection object recycled
            return supplierIds;
        }

        /// <summary>
        /// Public static method gets Suppliers from database, builds Supplier objects from
        /// their returned info, then loads each object into a list
        /// </summary>
        /// <returns>list of Supplier objects</returns>
        public static List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            Supplier supplier; // for reading

            //Sql connection block to connect to TravelExpertsDB; closes connection at end of block
            //Executes query to retrieve SupplierId's
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT * " +
                               "FROM Suppliers " +
                               "ORDER BY SupplierId";

                //sql command block; disposes command at end of block
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader =
                        cmd.ExecuteReader(CommandBehavior.CloseConnection);//new sqldatareader for accessing db
                    while (reader.Read())
                    {
                        // Create new supplier with current row information
                        supplier = new Supplier((int)reader["SupplierId"], reader["SupName"].ToString());

                        suppliers.Add(supplier); // add to the list
                    }
                } // cmd object recycled
            }// connection object recycled

            SqlConnection connection2 = TravelExpertsDB.GetConnection();//create new sql connection

            //for loop that iterates through the created list of Supplier objects
            for (int i = 0; i < suppliers.Count(); i++)
            {
                //runs a query that retrieves Products associated with each Supplier
                String subQuery = "SELECT p.ProductId, p.ProdName " +
                            "FROM Products p JOIN Products_Suppliers ps " +
                            "ON p.ProductId = ps.ProductId " +
                            "WHERE ps.SupplierId = @SupplierId; ";

                //clear current Suppliers's list property for Products
                //to be loaded with the retrieved Products
                suppliers[i].Products.Clear();

                //query that populates each retrieved Product's information into the Supplier
                //object's class property for Products

                //sql command block; disposes command at end of block
                using (SqlCommand subCmd = new SqlCommand(subQuery, connection2))
                {
                    connection2.Open();
                    //define and add sql parameter for our sqlcommand
                    subCmd.Parameters.AddWithValue("@SupplierId", suppliers[i].SupplierId);
                    SqlDataReader subReader =
                        subCmd.ExecuteReader(CommandBehavior.CloseConnection);//new sqldatareader for accessing db
                    while (subReader.Read())
                    {
                        //add a new product to the current supplier
                        suppliers[i].Products.Add(new Product((int)subReader["ProductId"], subReader["ProdName"].ToString()));
                    }
                    connection2.Close();
                }
            }

            return suppliers;
        }

        /// <summary>
        /// Public static method for adding a supplier to the suppliers database table 
        /// </summary>
        /// <param name="newSupplier">string name of new supplier</param>
        public static void AddSuppliers(string newSupplier)
        {
            //Sql connection block to connect to TravelExpertsDB; closes connection at end of block
            //Executes query to add suppliers 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "INSERT INTO Suppliers " +
                               "(SupName) " +
                               "VALUES(@supplierName)";

                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();//new sql adapter for modifying db

                //associate the insert command
                adapter.InsertCommand = new SqlCommand(query, connection);

                //add parameters
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@supplierName", newSupplier));

                adapter.InsertCommand.ExecuteNonQuery();//execute the insert
            }// connection object recycled
        }

        /// <summary>
        /// Public static class method for editing suppliers
        /// </summary>
        /// <param name="oldSupplier">Supplier class object for getting old values</param>
        /// <param name="newSupplier">Supplier class object for getting new values</param>
        /// <returns></returns>
        public static bool UpdateSuppliers(Supplier oldSupplier, Supplier newSupplier)
        {
            bool success;
            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query =
                    "UPDATE Suppliers " +
                    "SET " +
                    "SupName = @NewSupplierName " +

                    "WHERE " +
                    "SupplierId = @OldSupplierId AND " +
                    "SupName = @OldSupName ; ";


                // Creates command and adds all proper parameters
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    //Add parameters for sql query
                    cmd.Parameters.AddWithValue("@NewSupplierName", newSupplier.SupplierName);
                    cmd.Parameters.AddWithValue("@OldSupplierId", oldSupplier.SupplierId);
                    cmd.Parameters.AddWithValue("@OldSupName", oldSupplier.SupplierName);

                    success = cmd.ExecuteNonQuery() > 0; // Success if rows changed

                } // cmd object recycled
            }// connection object recycled


            return success;
        }

        /// <summary>
        /// Public static class method for deleting suppliers
        /// </summary>
        /// <param name="oldSupplier">Supplier class object for supplier being deleted</param>
        /// <returns></returns>
        public static bool DeleteSupplier(Supplier oldSupplier)
        {
            bool success = false;
            // Opens connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query =
                    "DELETE FROM Suppliers " +
                    "WHERE " +
                    "SupplierId = @OldSupplierId AND " +
                    "SupName = @OldSupplierName ; ";
                // Adds all parameters to new SQL Command
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    //Add parameters for sql query
                    cmd.Parameters.AddWithValue("@OldSupplierId", oldSupplier.SupplierId);
                    cmd.Parameters.AddWithValue("@OldSupplierName", oldSupplier.SupplierName);


                    success = cmd.ExecuteNonQuery() > 0; // Success if rows have been deleted

                } // cmd object recycled
            }// connection object recycled


            return success;
        }
    }
}