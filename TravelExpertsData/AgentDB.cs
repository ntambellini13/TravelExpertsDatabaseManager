﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: Class for verifying Agent login info against encrypted 
 * credentials stored in database
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsData
{
    public static class AgentDB
    {
        /// <summary>
        /// Verifies hashed password in db by grabbing salt and using same algorithm to generate hash to verify it
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool loginRequest(string username, string password)
        {            
            // Open connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string query = "SELECT Password, Salt " +
                    "FROM Agents " +
                    "WHERE Username = @Username ; ";
                // Grab hashed password and salt
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Username", username);
                    // Execute query
                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                    if (reader.HasRows) // Ensure username exists in DB
                    {
                        reader.Read();
                        // converts stored salt to byte array (length 16)
                        // converts stored hashed salted password to byte array (length 36)
                        byte[] salt = Convert.FromBase64String(reader["Salt"].ToString());
                        byte[] dbHashedPassword = Convert.FromBase64String(reader["Password"].ToString());
                        // Takes user entered password and encrypts it in same way used to store password
                        var pbkdf2 = new Rfc2898DeriveBytes(password.ToString(), salt, 10000);
                        byte[] hashedPassword = pbkdf2.GetBytes(20); // Length 20


                        // First 16 bytes in hashed password are salt, 17th byte is 1st actual byte to start comparing
                        for (int i = 16; i < dbHashedPassword.Count(); i++)
                        {
                            if (dbHashedPassword[i] != hashedPassword[i - 16])
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        
    }
}
