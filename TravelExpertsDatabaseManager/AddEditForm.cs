using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelExpertsData;

/*
 * Purpose: Form for adding or editing suppliers and products
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsDatabaseManager
{
    public partial class AddEditForm : Form
    {
        //Declare public class variables
        public string addedProductName;
        public string addedSupplierName;
        public string editedProductName;
        public int editedProductId;
        public string editedSupplierName;
        public int editedSupplierId;

        public string localEditObjectType;
        public bool localAddMode;
        public bool localEditMode;

        /// <summary>
        /// Public Constructor for AddEditForm - populated based on mode and object type
        /// </summary>
        /// <param name="dbAddEditObjectType">string product or supplier; used to set fields</param>
        /// <param name="dbAddMode">bool to set add mode</param>
        /// <param name="dbEditMode">bool to set edit mode</param>
        /// <param name="editValue">string value being edited</param>
        public AddEditForm(string dbAddEditObjectType, bool dbAddMode = false, bool dbEditMode = false, Product oldProduct = null, Supplier oldSupplier = null)
        {
            InitializeComponent();

            //set screen start position of form
            this.StartPosition = FormStartPosition.CenterScreen;

            //set back color of popup dialog
            this.BackColor = Color.Azure;

            //initialize variable for value to be edited
            string editValue = "";

            if(oldProduct != null)
            {
                editValue = oldProduct.ProductName;
                editedProductId = oldProduct.ProductId;
            }
            else if(oldSupplier != null)
            {
                editValue = oldSupplier.SupplierName;
                editedSupplierId = oldSupplier.SupplierId;
            }

            //assign input parameters to class variables
            localEditObjectType = dbAddEditObjectType;
            localAddMode = dbAddMode;
            localEditMode = dbEditMode;

            //Switch triggered on input parameters used to set fields or load values
            switch (dbAddEditObjectType)
            {
                case "Product":
                    if(dbAddMode)
                    {
                        addEditLabel.Text = "New Product Name:";
                        addEditButton.Text = "Add";
                        this.Text = "Add New Product";
                    }
                    
                    if(dbEditMode)
                    {
                        addEditLabel.Text = "Modify Product Name:";
                        addEditButton.Text = "Edit";
                        addEditTextBox.Text = editValue;
                        this.Text = "Edit Product";
                    }
                    break;
                case "Supplier":
                    if (dbAddMode)
                    {
                        addEditLabel.Text = "New Supplier Name:";
                        addEditButton.Text = "Add";
                        this.Text = "Add New Supplier";
                    }

                    if (dbEditMode)
                    {
                        addEditLabel.Text = "Modify Supplier Name:";
                        addEditButton.Text = "Edit";
                        addEditTextBox.Text = editValue;
                        this.Text = "Edit Supplier";
                    }
                    break;
                default:
                    MessageBox.Show("No Mode!");
                    break;
            }
        }

        /// <summary>
        /// Cancel button click event; cancels the operation
        /// </summary>
        /// <param name="sender">event parameter</param>
        /// <param name="e">event parameter</param>
        private void addEditCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// AddEdit button click event; adds or edits an value for a supplier or product
        /// </summary>
        /// <param name="sender">event parameter</param>
        /// <param name="e">event parameter</param>
        private void addEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;//all fields are valid so set the dialog response to ok and close the form
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Textbox text changed event
        /// </summary>
        /// <param name="sender">event parameter</param>
        /// <param name="e">event parameter</param>
        private void addEditTextBox_TextChanged(object sender, EventArgs e)
        {
            //switch driven by object type; product or supplier
            switch (localEditObjectType)
            {
                case "Product":
                    if (localAddMode)//product add mode
                    {
                        addedProductName = addEditTextBox.Text.Trim();//Assign textbox value to class variable so it can be passed to another form
                    }

                    if (localEditMode)//product edit mode
                    {
                        editedProductName = addEditTextBox.Text.Trim();//Assign textbox value to class variable so it can be passed to another form
                    }
                    break;
                case "Supplier":
                    if (localAddMode)//supplier add mode
                    {
                        addedSupplierName = addEditTextBox.Text.Trim();//Assign textbox value to class variable so it can be passed to another form
                    }

                    if (localEditMode)//supplier edit mode
                    {
                        editedSupplierName = addEditTextBox.Text.Trim();//Assign textbox value to class variable so it can be passed to another form
                    }
                    break;
                default:
                    MessageBox.Show("No Mode!");
                    break;
            }
        }

        private void AddEditForm_Load(object sender, EventArgs e)
        {
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
                l.Font = new Font("Arial", (float)8.25);
                l.ForeColor = Color.RoyalBlue;
            }
        }
    }
}
