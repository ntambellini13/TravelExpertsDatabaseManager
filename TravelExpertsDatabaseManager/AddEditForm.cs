﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public string editedSupplierName;

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
        public AddEditForm(string dbAddEditObjectType, bool dbAddMode = false, bool dbEditMode = false, string editValue = "")
        {
            InitializeComponent();

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
    }
}
