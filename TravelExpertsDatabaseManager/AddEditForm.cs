using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public AddEditForm(string dbAddEditObjectType, bool dbAddMode = false, bool dbEditMode = false, string editValue = "")
        {
            InitializeComponent();

            localEditObjectType = dbAddEditObjectType;
            localAddMode = dbAddMode;
            localEditMode = dbEditMode;

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

        private void addEditCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                //Need to add validation here
                /*
                switch (localEditObjectType)
                {


                    case "Product":
                        if (localAddMode)
                        {



                        }

                        if (localEditMode)
                        {

                        }
                        break;
                    case "Supplier":
                        if (localAddMode)
                        {

                        }

                        if (localEditMode)
                        {

                        }
                        break;
                    default:
                        MessageBox.Show("No Mode!");
                        break;
                }
                */

                DialogResult = DialogResult.OK;//all fields are valid so set the dialog response to ok and close the form
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addEditTextBox_TextChanged(object sender, EventArgs e)
        {
            switch (localEditObjectType)
            {
                case "Product":
                    if (localAddMode)
                    {
                        addedProductName = addEditTextBox.Text.Trim();//Assign textbox value to class variable so it can be passed to another form
                    }

                    if (localEditMode)
                    {
                        editedProductName = addEditTextBox.Text.Trim();//Assign textbox value to class variable so it can be passed to another form
                    }
                    break;
                case "Supplier":
                    if (localAddMode)
                    {
                        addedSupplierName = addEditTextBox.Text.Trim();//Assign textbox value to class variable so it can be passed to another form
                    }

                    if (localEditMode)
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
