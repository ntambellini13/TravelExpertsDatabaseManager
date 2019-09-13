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

        private bool editMode = false;

        private bool isValidPackageName = false;
        private bool isValidImage = false;
        private bool isValidPartnerURL = false;
        private bool isValidPackageDates = false;
        private bool isValidDescription = false;
        private bool isValidPackageBasePrice = false;
        private bool isValidPackageAgencyCommission = false;

        public AddEditPackageForm()
        {
            InitializeComponent();
        }

        public AddEditPackageForm(Package oldPackage) : this()
        {
            editMode = true;
            this.addSaveButton.Text = "Save";

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

            isValidDescription = true;
            isValidImage = true;
            isValidPackageAgencyCommission = true;
            isValidPackageBasePrice = true;
            isValidPackageDates = true;
            isValidPackageName = true;
            isValidPartnerURL = true;
        }

        private bool isValidForm()
        {
            String message = "";

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
            if (!isValidPackageAgencyCommission)
            {
                message += "Agency commission must be less than base price.\n";
            }

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
                Image = File.ReadAllBytes(openFileDialog.FileName);
                imagePathTextBox.Text = openFileDialog.FileName;
                isValidImage = true;
                return true;
            }
            else
            {
                return (Image!=null);
            }
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            isValidPackageName = Validation.ColorTextBoxValidation(nameTextBox, Validation.IsNotEmptyOrNull);
        }

        private void basePriceTextBox_TextChanged(object sender, EventArgs e)
        {
            isValidPackageBasePrice = Validation.ColorTextBoxValidation(basePriceTextBox, Validation.isValidPositiveDecimal);
        }

        private void agencyCommissionTextBox_TextChanged(object sender, EventArgs e)
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

        private void dateTimePickers_ValueChanged(object sender, EventArgs e)
        {
            isValidPackageDates = Validation.IsFutureDateGreaterThanPastDate(startDateTimePicker, endDateTimePicker);                     
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            isValidDescription = Validation.IsNotEmptyOrNull(descriptionTextBox);
        }

        private void imagePathTextBox_TextChanged(object sender, EventArgs e)
        {
            isValidImage = Validation.IsNotEmptyOrNull(imagePathTextBox);
        }

        private void partnerURLTextBox_TextChanged(object sender, EventArgs e)
        {
            isValidPartnerURL = Validation.IsNotEmptyOrNull(partnerURLTextBox);
        }

        private void chooseImageButton_Click(object sender, EventArgs e)
        {
            isValidImage = OpenFilePathDialog();           
        }

        private void addSaveButton_Click(object sender, EventArgs e)
        {
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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
