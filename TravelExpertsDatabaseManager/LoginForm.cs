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

/*
 * Purpose: Form for logging into application
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

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
            this.StartPosition = FormStartPosition.CenterScreen;
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

        private void LoginForm_Load(object sender, EventArgs e)
        {

            FormManager.registerForm(this);

            FormManager.setAllBackcolors(Color.Azure);

            //grab all buttons on the form
            var buttons = HelperMethods.GetAll(this, typeof(Button));

            //grab all labels on the form
            var labels = HelperMethods.GetAll(this, typeof(Label));

            //set the BackColor of each button 
            foreach (var b in buttons)
            {
                b.BackColor = Color.GhostWhite;
            }

            //Style the font of each label on the form
            foreach (var l in labels)
            {
                l.Font = new Font("Arial", (float)11, FontStyle.Bold);
                l.ForeColor = Color.RoyalBlue;
            }
        }
    }
}
