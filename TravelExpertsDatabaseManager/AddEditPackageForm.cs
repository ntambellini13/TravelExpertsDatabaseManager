using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelExpertsData;


namespace TravelExpertsDatabaseManager
{
    public partial class AddEditPackageForm : Form
    {
        // Defines all parameters needed to create a package
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public byte[] Image { get; set; }
        public string PartnerURL { get; set; }
        public bool AirfairInclusion { get; set; }
        public DateTime PackageStartDate { get; set; }
        public DateTime PackageEndDate { get; set; }
        public string PackageDescription { get; set; }
        public decimal PackageBasePrice { get; set; }
        public decimal PackageAgencyCommission { get; set; }

        // Properties for validation
        private bool isValidPackageName = false;
        private bool isValidImage = false;
        private bool isValidPartnerURL = false;
        private bool isValidPackageDates = false;
        private bool isValidDescription = false;
        private bool isValidPackageBasePrice = false;
        private bool isValidPackageAgencyCommission = false;

        /// <summary>
        /// Creates empty form to add a new package
        /// </summary>
        public AddEditPackageForm()
        {
            InitializeComponent();

            //set screen start position of form
            this.StartPosition = FormStartPosition.CenterScreen;

            //set back color of popup dialog
            this.BackColor = Color.Azure;
        }

        /// <summary>
        /// Creates a form to edit package
        /// </summary>
        /// <param name="oldPackage">Package to edit</param>
        public AddEditPackageForm(Package oldPackage) : this()
        {            
            this.addSaveButton.Text = "Save"; // Makes button say Save

            // Populate all fields
            idTextBox.Text = oldPackage.PackageId.ToString();
            nameTextBox.Text = oldPackage.PackageName;
            imagePathTextBox.Text = "";
            partnerURLTextBox.Text = oldPackage.PartnerURL;
            airfairInclusionCheckBox.Checked = oldPackage.AirfairInclusion;
            startDateTimePicker.Value = oldPackage.PackageStartDate;
            endDateTimePicker.Value = oldPackage.PackageEndDate;
            descriptionTextBox.Text = oldPackage.PackageDescription;
            basePriceTextBox.Text = oldPackage.PackageBasePrice.ToString();
            agencyCommissionTextBox.Text = oldPackage.PackageAgencyCommission.ToString();
            Image = oldPackage.Image;

            // Sets all validation to true
            isValidDescription = true;
            isValidImage = true;
            isValidPackageAgencyCommission = true;
            isValidPackageBasePrice = true;
            isValidPackageDates = true;
            isValidPackageName = true;
            isValidPartnerURL = true;
        }

        
        /// <summary>
        /// Validates name text box on change. Colors it appropriately.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            isValidPackageName = Validation.ColorTextBoxValidation(nameTextBox, Validation.IsNotEmptyOrNull);
        }

        /// <summary>
        /// Validates base price on change. Colors it appropriately.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void basePriceTextBox_TextChanged(object sender, EventArgs e)
        {
            isValidPackageBasePrice = Validation.ColorTextBoxValidation(basePriceTextBox, Validation.isValidPositiveDecimal);
            ValidateAgencyCommission();
        }

        /// <summary>
        /// Validates commission on change. Colors it appropriately.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void agencyCommissionTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateAgencyCommission();
        }

        
        /// <summary>
        /// Validates date values on change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePickers_ValueChanged(object sender, EventArgs e)
        {
            isValidPackageDates = Validation.IsFutureDateGreaterThanPastDate(startDateTimePicker, endDateTimePicker);                     
        }

        /// <summary>
        /// Validates description text box on change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            isValidDescription = Validation.IsNotEmptyOrNull(descriptionTextBox);
        }

        /// <summary>
        /// Validates imafe path on change. Valid if path supplied.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imagePathTextBox_TextChanged(object sender, EventArgs e)
        {
            isValidImage = Validation.IsNotEmptyOrNull(imagePathTextBox);
        }

        /// <summary>
        /// Validates partner url on change. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void partnerURLTextBox_TextChanged(object sender, EventArgs e)
        {
            isValidPartnerURL = Validation.ColorTextBoxValidation(partnerURLTextBox,Validation.IsValidURL);
        }

        /// <summary>
        /// Opens dialog to choose the image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chooseImageButton_Click(object sender, EventArgs e)
        {
            OpenFilePathDialog();           
        }

        /// <summary>
        /// Processes add and save requests
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addSaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // If the form is valid, set all public properties to the values in the form
                if (isValidForm())
                {
                    PackageName = nameTextBox.Text.Trim();
                    PartnerURL = partnerURLTextBox.Text.Trim();
                    PackageStartDate = startDateTimePicker.Value;
                    PackageEndDate = endDateTimePicker.Value;
                    PackageBasePrice = decimal.Parse(basePriceTextBox.Text.Trim());
                    PackageAgencyCommission = decimal.Parse(agencyCommissionTextBox.Text.Trim());
                    AirfairInclusion = airfairInclusionCheckBox.Checked;
                    PackageDescription = descriptionTextBox.Text.Trim();
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cancels form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Checks if form is valid. If its invalid, show a messagebox with error.
        /// </summary>
        /// <returns>Form valid?</returns>
        private bool isValidForm()
        {
            String message = ""; // Empty string

            // Goes through validation and adds message to string
            if (!isValidPackageName)
            {
                message += "Must enter a package name.\n";
            }
            if (!isValidImage)
            {
                message += "Must select a path for the image.\n";
            }
            if (!isValidPartnerURL)
            {
                message += "Must enter a partner url.\n";
            }
            if (!isValidPackageDates)
            {
                message += "Start date must be before end date.\n";
            }
            if (!isValidDescription)
            {
                message += "Must enter a description.\n";
            }
            if (!isValidPackageBasePrice)
            {
                message += "Must enter a base price.\n";
            }
            if (!isValidPackageAgencyCommission && agencyCommissionTextBox.Text.Trim() == "")
            {
                message += "Must enter an agency commission.\n";
            }
            else if (!isValidPackageAgencyCommission)
            {
                message += "Agency commission must be less than base price.\n";
            }

            // If string is empty, all validation passed. Show error message otherwise.
            if (message != "")
            {
                MessageBox.Show(message, "Form Error");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Creates a file dialog to open file
        /// </summary>
        /// <returns>Was the file opened successfully?</returns>
        private bool OpenFilePathDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png",
                DefaultExt = ".jpg",
                Title = "Choose image .."
            };
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Reads image as byte[] and stores image path in text box
                Image = File.ReadAllBytes(openFileDialog.FileName);
                imagePathTextBox.Text = openFileDialog.FileName;
                isValidImage = true;
                return true;
            }
            else
            {
                // Sets valid image to false if no image was yet selected in app
                isValidImage = (Image != null);
                return false;
            }
        }

        /// <summary>
        /// Validates the agency commission text box (valid decimal and less than base price)
        /// </summary>
        private void ValidateAgencyCommission()
        {
            if (Validation.isValidPositiveDecimal(agencyCommissionTextBox) &&
                            Validation.isFirstDecimalLessThanSecondDecimal(agencyCommissionTextBox, basePriceTextBox))
            {
                agencyCommissionTextBox.ForeColor = Color.Black;
                isValidPackageAgencyCommission = true;
            }
            else
            {
                agencyCommissionTextBox.ForeColor = Color.Red;
                isValidPackageAgencyCommission = false;
            }
        }

        private void AddEditPackageForm_Load(object sender, EventArgs e)
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
