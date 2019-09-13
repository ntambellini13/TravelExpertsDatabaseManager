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
        public FindableBindingList<Package> packages;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadPackageDataBinding();
            LoadPackageNameSearchComboBox();
        }

        private void LoadPackageNameSearchComboBox()
        {
            searchByPackageNameComboBox.Items.Clear();
            foreach(Package package in packages)
            {
                searchByPackageNameComboBox.Items.Add(package.PackageName);
            }
            searchByPackageNameComboBox.SelectedIndex = 0;
        }

        private void LoadPackageDataBinding()
        {
            packages = new FindableBindingList<Package>(PackagesDB.GetPackages());
            packageBindingSource.DataSource = packages;
        }

        private void packageIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            packageBindingSource.MovePrevious();
            if (searchByPackageNameComboBox.SelectedIndex > 0)
            {
                searchByPackageNameComboBox.SelectedIndex -= 1;
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            packageBindingSource.MoveNext();
            if (searchByPackageNameComboBox.SelectedIndex < searchByPackageNameComboBox.Items.Count-1)
            {
                searchByPackageNameComboBox.SelectedIndex += 1;
            }
        }

        private void searchByPackageNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bsIndex = packageBindingSource.Find("PackageName", searchByPackageNameComboBox.SelectedItem.ToString());
            if (bsIndex > -1)
            {
                packageBindingSource.Position = bsIndex;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddEditPackageForm addForm = new AddEditPackageForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                Package package = new Package(
                    -1,
                    addForm.PackageName,
                    addForm.Image,
                    addForm.PartnerURL,
                    addForm.AirfairInclusion,
                    addForm.PackageStartDate,
                    addForm.PackageEndDate,
                    addForm.PackageDescription,
                    addForm.PackageBasePrice,
                    addForm.PackageAgencyCommission);
                if (PackagesDB.AddPackage(package))
                {
                    LoadPackageDataBinding();
                    LoadPackageNameSearchComboBox();
                    searchByPackageNameComboBox.SelectedIndex = searchByPackageNameComboBox.Items.Count - 1;
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            Package oldPackage = (Package) packageBindingSource.Current;
            AddEditPackageForm editForm = new AddEditPackageForm(oldPackage);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                Package newPackage = new Package(
                    editForm.PackageId,
                    editForm.PackageName,
                    editForm.Image,
                    editForm.PartnerURL,
                    editForm.AirfairInclusion,
                    editForm.PackageStartDate,
                    editForm.PackageEndDate,
                    editForm.PackageDescription,
                    editForm.PackageBasePrice,
                    editForm.PackageAgencyCommission);
                if (PackagesDB.UpdatePackage(oldPackage, newPackage))
                {
                    LoadPackageDataBinding();
                    LoadPackageNameSearchComboBox();
                    searchByPackageNameComboBox.SelectedItem = newPackage.PackageName;
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you wold like to delete this package?", "Confirm delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Package currentPackage = (Package) packageBindingSource.Current;
                if(PackagesDB.DeletePackage(currentPackage))
                {
                    packages.Remove(currentPackage);
                    packageBindingSource.MoveNext();
                }
                else
                {
                    MessageBox.Show("Error. Could not delete package. Please try again.");
                }
                Object selectedItem = searchByPackageNameComboBox.SelectedItem;
                LoadPackageDataBinding();
                LoadPackageNameSearchComboBox();
                searchByPackageNameComboBox.SelectedItem = selectedItem;
            }
        }
    }
}
