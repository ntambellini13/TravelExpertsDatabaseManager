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

        public FindableBindingList<Product> products;
        public FindableBindingList<Supplier> suppliers;

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

            InitializeProductDataBinding();
            InitializeProductNameSearchComboBox();
            InitializeSupplierDataBinding();
            InitializeSupplierNameSearchComboBox();

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


        private void InitializeProductNameSearchComboBox()
        {
            foreach(Product product in products)
            {
                productComboBox.Items.Add(product.ProductName);
            }
            productComboBox.SelectedIndex = 0;
        }

        private void InitializeProductDataBinding()
        {
            products = new FindableBindingList<Product>(ProductsDB.GetProducts());
            productBindingSource.DataSource = products;
        }
        
        private void InitializeSupplierNameSearchComboBox()
        {
            foreach (Supplier supplier in suppliers)
            {
                supplierComboBox.Items.Add(supplier.SupplierName);
            }
            supplierComboBox.SelectedIndex = 0;
        }
        
        private void InitializeSupplierDataBinding()
        {
            suppliers = new FindableBindingList<Supplier>(SuppliersDB.GetSuppliers());
            supplierBindingSource.DataSource = suppliers;
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


        private void productPrevButton_Click(object sender, EventArgs e)
        {
            productBindingSource.MovePrevious();
            if (productComboBox.SelectedIndex > 0)
            {
                productComboBox.SelectedIndex -= 1;
            }
        }

        private void productNextButton_Click(object sender, EventArgs e)
        {
            productBindingSource.MoveNext();
            if (productComboBox.SelectedIndex < productComboBox.Items.Count - 1)
            {
                productComboBox.SelectedIndex += 1;
            }
        }

        private void supplierPrevButton_Click(object sender, EventArgs e)
        {
            supplierBindingSource.MovePrevious();
            if (supplierComboBox.SelectedIndex > 0)
            {
                supplierComboBox.SelectedIndex -= 1;
            }
        }

        private void supplierNextButton_Click(object sender, EventArgs e)
        {
            supplierBindingSource.MoveNext();
            if (supplierComboBox.SelectedIndex < supplierComboBox.Items.Count - 1)
            {
                supplierComboBox.SelectedIndex += 1;
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

        private void productComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bsIndex = productBindingSource.Find("ProductName", productComboBox.SelectedItem.ToString());
            if (bsIndex > -1)
            {
                productBindingSource.Position = bsIndex;
            }
        }

        private void supplierComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bsIndex = supplierBindingSource.Find("SupplierName", supplierComboBox.SelectedItem.ToString());
            if (bsIndex > -1)
            {
                supplierBindingSource.Position = bsIndex;
            }
        }

        private void ProductAddButton_Click(object sender, EventArgs e)
        {
            AddEditForm addProduct = new AddEditForm("Product",true,false);//create instance of addeditform for product add

            DialogResult result = addProduct.ShowDialog(this);//variable the stores result returned from modal dialog form; shows addeditform for product add

            if (result == DialogResult.OK)
            {
                ProductsDB.AddProducts(addProduct.addedProductName);//ProductsDB class method call to add product
                InitializeProductDataBinding();
                InitializeProductNameSearchComboBox();
            }
        }

        private void ProductEditButton_Click(object sender, EventArgs e)
        {
            AddEditForm editProduct = new AddEditForm("Product",false,true, productNameTextBox.Text);//create instance of addeditform for product add

            DialogResult result = editProduct.ShowDialog(this);//variable the stores result returned from modal dialog form; shows addeditform for product add

            if (result == DialogResult.OK)
            {
                ProductsDB.EditProduct(editProduct.editedProductName, int.Parse(productIdTextBox.Text));
                InitializeProductDataBinding();
                InitializeProductNameSearchComboBox();
            }
        }

        private void SupplierAddButton_Click(object sender, EventArgs e)
        {
            AddEditForm addSupplier = new AddEditForm("Supplier", true, false);//create instance of addeditform for product add

            DialogResult result = addSupplier.ShowDialog(this);//variable the stores result returned from modal dialog form; shows addeditform for product add

            if (result == DialogResult.OK)
            {
                SuppliersDB.AddSuppliers(addSupplier.addedSupplierName);//ProductsDB class method call to add product
                InitializeSupplierDataBinding();
                InitializeSupplierNameSearchComboBox();
            }
        }

        private void SupplierEditButton_Click(object sender, EventArgs e)
        {
            AddEditForm editSupplier = new AddEditForm("Supplier", true, false);//create instance of addeditform for product add

            DialogResult result = editSupplier.ShowDialog(this);//variable the stores result returned from modal dialog form; shows addeditform for product add

            if (result == DialogResult.OK)
            {
                SuppliersDB.EditSuppliers(editSupplier.editedSupplierName, int.Parse(supplierIdTextBox.Text));//ProductsDB class method call to add product
                InitializeSupplierDataBinding();
                InitializeSupplierNameSearchComboBox();
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
