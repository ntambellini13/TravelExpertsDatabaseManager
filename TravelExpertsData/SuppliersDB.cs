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
    public static class SuppliersDB
    {
        public static List<int> GetSupplierIds()
        {
            List<int> supplierIds = new List<int>();
            int supplierId; // for reading

            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT SupplierId " +
                               "FROM Suppliers " +
                               "ORDER BY SupplierId";
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

        public static List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            Supplier supplier; // for reading

            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT * " +
                               "FROM Suppliers " +
                               "ORDER BY SupplierId";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader =
                        cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        // Create new supplier with current row information
                        supplier = new Supplier((int)reader["SupplierId"], reader["SupName"].ToString());

                        suppliers.Add(supplier); // add to the list
                    }
                } // cmd object recycled
            }// connection object recycled
            return suppliers;
        }

        public static void AddSuppliers(string newSupplier)
        {
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "INSERT INTO Suppliers " +
                               "(SupName) " +
                               "VALUES(@supplierName)";

                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                //add and declare sql parameters for the adapter insert command

                /*********
                 * Add code here to take user input of product name
                 * 
                 * ********/

                //associate the insert command
                adapter.InsertCommand = new SqlCommand(query, connection);

                adapter.InsertCommand.Parameters.Add(new SqlParameter("@supplierName", newSupplier));

                adapter.InsertCommand.ExecuteNonQuery();//execute the insert
            }// connection object recycled
        }

        public static void EditSuppliers(string editSupplier, int editSupplierId)
        {
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "UPDATE Suppliers " +
                               "SET SupName = @supplierName " +
                               "WHERE ProductId = @supplierId";

                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();

                /*********
                 * Add code here to take user input of product name
                 * 
                 * ********/

                //associate the insert command
                adapter.UpdateCommand = new SqlCommand(query, connection);

                //add and declare sql parameters for the adapter update command
                adapter.UpdateCommand.Parameters.Add(new SqlParameter("@supplierName", editSupplier));
                adapter.UpdateCommand.Parameters.Add(new SqlParameter("@supplierId", editSupplierId));

                adapter.UpdateCommand.ExecuteNonQuery();
            }// connection object recycled
        }
    }
}