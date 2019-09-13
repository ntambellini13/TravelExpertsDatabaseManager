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
        public FindableBindingList<Package> packages; // Holds all packages from database

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load the packages data source and set up the search by package name combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadPackageDataBinding();
            LoadPackageNameSearchComboBox();
        }


        /// <summary>
        /// Sets up the load by package name combo box
        /// </summary>
        private void LoadPackageNameSearchComboBox()
        {
            // Add all package names to combo box and selects the first
            searchByPackageNameComboBox.Items.Clear();
            foreach(Package package in packages)
            {
                searchByPackageNameComboBox.Items.Add(package.PackageName);
            }
            searchByPackageNameComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets up the package data binding
        /// </summary>
        private void LoadPackageDataBinding()
        {
            // Retrieves package list from DB and creates a new findable binding list  and sets it as new binding source
            packages = new FindableBindingList<Package>(PackagesDB.GetPackages());
            packageBindingSource.DataSource = packages;
        }

        /// <summary>
        /// Moves to previous package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void prevButton_Click(object sender, EventArgs e)
        {
            // Moves the binding source and package name combo box to previous item
            packageBindingSource.MovePrevious();
            if (searchByPackageNameComboBox.SelectedIndex > 0)
            {
                searchByPackageNameComboBox.SelectedIndex -= 1;
            }
        }

        /// <summary>
        /// Moves to next package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextButton_Click(object sender, EventArgs e)
        {
            // Moves the binding source and package name combo box to next item
            packageBindingSource.MoveNext();
            if (searchByPackageNameComboBox.SelectedIndex < searchByPackageNameComboBox.Items.Count-1)
            {
                searchByPackageNameComboBox.SelectedIndex += 1;
            }
        }

        /// <summary>
        /// Moves binding source to package selected in search by name combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchByPackageNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Find the package with the name chosen in the combo box
            int bsIndex = packageBindingSource.Find("PackageName", searchByPackageNameComboBox.SelectedItem.ToString());
            // Moves the binding source to the selected package
            if (bsIndex > -1)
            {
                packageBindingSource.Position = bsIndex;
            }
        }

        /// <summary>
        /// Opens a form to add package and adds to db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            AddEditPackageForm addForm = new AddEditPackageForm(); // Opens form in add mode
            // If we get an OK message, create a package, add to db, and requery db packages
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                Package package = new Package( // Create new package
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
                // Adds package to db. If not successful, show message.
                if (!PackagesDB.AddPackage(package))
                {
                    MessageBox.Show("Error. Could not add package to DB. Please try again");
                    
                }
                // Reload all packages from db regardless of success
                LoadPackageDataBinding();
                LoadPackageNameSearchComboBox();
                // Choose last package
                searchByPackageNameComboBox.SelectedIndex = searchByPackageNameComboBox.Items.Count - 1;
            }
        }

        /// <summary>
        /// Edits the selected package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, EventArgs e)
        {
            // Gets selected package and opens form in edit mode for that package
            Package oldPackage = (Package) packageBindingSource.Current;
            AddEditPackageForm editForm = new AddEditPackageForm(oldPackage);
            // If result is OK, edit package in DB
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
                // Updates package. Shows error if unsuccessful. Reloads all packages and moves to updated package
                if (PackagesDB.UpdatePackage(oldPackage, newPackage))
                {
                    LoadPackageDataBinding();
                    LoadPackageNameSearchComboBox();
                    searchByPackageNameComboBox.SelectedItem = newPackage.PackageName; // Move to updated package
                }
                else
                {
                    MessageBox.Show("Error. Could not update package. Please try again.");
                    LoadPackageDataBinding();
                    LoadPackageNameSearchComboBox();
                    searchByPackageNameComboBox.SelectedItem = oldPackage.PackageName; // Move to package that tried to update. (May see that it was updated by someone else).
                }
            }
        }

        /// <summary>
        /// Exits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Deletes the current package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            // Confirms user wants to delete package
            DialogResult result = MessageBox.Show("Are you sure you wold like to delete this package?", "Confirm delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Gets reference to current package. Deletes it. If unsuccessful, show error message.
                Package currentPackage = (Package) packageBindingSource.Current;
                if(PackagesDB.DeletePackage(currentPackage))
                {
                    packages.Remove(currentPackage);
                }
                else
                {
                    MessageBox.Show("Error. Could not delete package. Please try again.");
                }
                LoadPackageDataBinding();
                LoadPackageNameSearchComboBox();
            }
        }
    }
}
