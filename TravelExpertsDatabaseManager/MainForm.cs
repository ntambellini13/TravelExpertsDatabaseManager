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

namespace TravelExpertsDatabaseManager
{
    public partial class MainForm : Form
    {
        public List<Package> packages;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializePackageDataBinding();
        }

        private void InitializePackageDataBinding()
        {
            packages = PackagesDB.GetPackages();
            packageBindingSource.DataSource = packages;
        }

        private void packageIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Package selectedPackage = null;
            for(int i = 0 ; i< packages.Count() ; i++)
            {
                if(packages[i].PackageId == (int) packageIdComboBox.SelectedValue)
                {
                    selectedPackage = packages[i];
                    break;
                }
            }
            if (selectedPackage != null)
            {
                airfairInclusionCheckBox.Checked = selectedPackage.AirfairInclusion;
                imagePathTextBox.Text = selectedPackage.ImagePath;
                packageAgencyCommissionTextBox.Text = selectedPackage.PackageAgencyCommission.ToString("c2");
                packageBasePriceTextBox.Text = selectedPackage.PackageBasePrice.ToString("c2");
                packageDescriptionTextBox.Text = selectedPackage.PackageDescription;
                packageEndDateDateTimePicker.Value = selectedPackage.PackageEndDate;
                packageNameTextBox.Text = selectedPackage.PackageName;
                packageStartDateDateTimePicker.Value = selectedPackage.PackageStartDate;
                partnerURLTextBox.Text = selectedPackage.PartnerURL;
            }
        }
    }
}
