using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelExpertsData;

namespace TravelExpertsDatabaseManager
{
    public partial class LoginForm : Form
    {
        // Gets the logged in status for when the form is closed
        public bool LoggedIn
        {
            get
            {
                return loggedIn;
            }
        }

        private bool loggedIn = false; 

        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Processes login request.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                String username = usernameTextBox.Text;
                String password = passwordTextBox.Text;
                if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Must show password and username");
                }
                else
                {
                    bool loggedIn = AgentDB.loginRequest(username, password);
                    if (loggedIn)
                    {
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Your username or password are incorrect");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
        }

        /// <summary>
        /// Exits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitLoginButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
