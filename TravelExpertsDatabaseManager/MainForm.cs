using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelExpertsData;

/*
 * Purpose: Main Form Class loads and runs main program functions
 * Author: Tawico 
 * Date: September 18, 2019
 * 
 * */
namespace TravelExpertsDatabaseManager
{
    public partial class MainForm : Form
    {

        public FindableBindingList<Product> products;// Holds all products from database
        public FindableBindingList<Supplier> suppliers;// Holds all suppliers from database
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
            LoginForm loginForm = new LoginForm(); 
            // Ensure agent logs in before using the form. If did not login, close application
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Initialize packages, products, and suppliers tabs
                InitializeProductDataBinding();
                InitializeProductNameSearchComboBox();
                InitializeSupplierDataBinding();
                InitializeSupplierNameSearchComboBox();
                LoadPackageDataBinding();
                LoadPackageNameSearchComboBox();
            }
            else
            {
                Application.Exit();
            }

        }

        

        // Packages Tab

        /// <summary>
        /// Moves to previous package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void packagePrevButton_Click(object sender, EventArgs e)
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
        private void packageNextButton_Click(object sender, EventArgs e)
        {
            // Moves the binding source and package name combo box to next item
            packageBindingSource.MoveNext();
            if (searchByPackageNameComboBox.SelectedIndex < searchByPackageNameComboBox.Items.Count - 1)
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
                populateProductSupplierListBoxes(bsIndex);
            }
            populateSupplierListBoxes(productComboBox.SelectedIndex);
        }

        /// <summary>
        /// Opens a form to add package and adds to db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPackageButton_Click(object sender, EventArgs e)
        {
            try
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
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }

        /// <summary>
        /// Edits the selected package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editPackageButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Gets selected package and opens form in edit mode for that package
                Package oldPackage = (Package)packageBindingSource.Current;
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
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
            populateProductListBoxes(supplierComboBox.SelectedIndex);
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
        private void deletePackageButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirms user wants to delete package
                DialogResult result = MessageBox.Show("Are you sure you wold like to delete this package?", "Confirm delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Gets reference to current package. Deletes it. If unsuccessful, show error message.
                    Package currentPackage = (Package)packageBindingSource.Current;
                    if (PackagesDB.DeletePackage(currentPackage))
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
            catch (SqlException ex)
            {
                //create a regex match option that searches the exception message string for the table that caused the foreign key constraint
                Match match = Regex.Match(ex.Message, "\"dbo.+\"", RegexOptions.IgnoreCase);

                //split the returned string by the '.' character name, then grab the second string out of the two and assign it to a variable
                String tableName = match.Value.Split('.').Last();

                //remove the last '\' character from the string AKA get the table name
                tableName = tableName.Substring(0, tableName.Count() - 1);
                MessageBox.Show($"This product is being referenced by the {tableName} table. Please modify or delete those entries before retying to delete the product.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }

        /// <summary>
        /// Adds the selected product supplier to the package (and DB)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addProductSupplierButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the selected index and package Id
                int selectedProductSupplierIndex = nonAssociatedProductSuppliersListBox.SelectedIndex;
                int packageId = ((Package)packageBindingSource.Current).PackageId;

                // Ensure proper indices are chosen
                if (selectedProductSupplierIndex != -1 && packageId != -1)
                {
                    // Gets product supplier Id from list box string and attempts to add package product supplier pair to DB
                    if (PackagesProductsSuppliersDB.addPackageProductSupplier(packageId, Convert.ToInt32(nonAssociatedProductSuppliersListBox.Items[selectedProductSupplierIndex].ToString().Split('|').First())))
                    {
                        // On success
                        //add selected item from non associated items list box to the associated items listbox
                        //then remove the selected item from the non associated items list box
                        associatedProductSuppliersListBox.Items.Add(nonAssociatedProductSuppliersListBox.Items[selectedProductSupplierIndex]);
                        nonAssociatedProductSuppliersListBox.Items.RemoveAt(selectedProductSupplierIndex);
                        associatedProductSuppliersListBox.SelectedIndex = associatedProductSuppliersListBox.Items.Count - 1;

                        // Attach new product suppliers list to current package
                        Package currentPackage = (Package)packageBindingSource.Current;
                        currentPackage.ProductSuppliers = PackagesProductsSuppliersDB.getProductsSuppliersIdAndString_ByPackageId(currentPackage.PackageId);
                    }
                    else
                    {
                        MessageBox.Show("Error in updating database. Application data will be refreshed");
                        // reload data
                        Package currentPackage = (Package)packageBindingSource.Current;
                        currentPackage.ProductSuppliers = PackagesProductsSuppliersDB.getProductsSuppliersIdAndString_ByPackageId(currentPackage.PackageId);
                        populateProductSupplierListBoxes(packageBindingSource.Position);
                    }

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }

        private void removeProductSupplierButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the selected index and package Id
                int selectedProductSupplierIndex = associatedProductSuppliersListBox.SelectedIndex;
                int packageId = ((Package)packageBindingSource.Current).PackageId;

                // Ensure proper indices are chosen
                if (selectedProductSupplierIndex != -1 && packageId != -1)
                {
                    // Get product supplier Id from list box string and attempt to remove the package product supplier pair
                    if (PackagesProductsSuppliersDB.removePackageProductSupplier(packageId, Convert.ToInt32(associatedProductSuppliersListBox.Items[selectedProductSupplierIndex].ToString().Split('|').First())))
                    {
                        //add selected item from associated items list box to the non associated items listbox
                        //then remove the selected item from the associated items list box
                        nonAssociatedProductSuppliersListBox.Items.Add(associatedProductSuppliersListBox.Items[selectedProductSupplierIndex]);
                        associatedProductSuppliersListBox.Items.RemoveAt(selectedProductSupplierIndex);
                        nonAssociatedProductSuppliersListBox.SelectedIndex = nonAssociatedProductSuppliersListBox.Items.Count - 1;

                        // Attach new product suppliers list to current package
                        Package currentPackage = (Package)packageBindingSource.Current;
                        currentPackage.ProductSuppliers = PackagesProductsSuppliersDB.getProductsSuppliersIdAndString_ByPackageId(currentPackage.PackageId);
                    }
                    else
                    {
                        MessageBox.Show("Error in updating database. Application data will be refreshed");
                        // reload data
                        Package currentPackage = (Package)packageBindingSource.Current;
                        currentPackage.ProductSuppliers = PackagesProductsSuppliersDB.getProductsSuppliersIdAndString_ByPackageId(currentPackage.PackageId);
                        populateProductSupplierListBoxes(packageBindingSource.Position);
                    }

                }
            }
            catch (SqlException ex)
            {
                //create a regex match option that searches the exception message string for the table that caused the foreign key constraint
                Match match = Regex.Match(ex.Message, "\"dbo.+\"", RegexOptions.IgnoreCase);

                //split the returned string by the '.' character name, then grab the second string out of the two and assign it to a variable
                String tableName = match.Value.Split('.').Last();

                //remove the last '\' character from the string AKA get the table name
                tableName = tableName.Substring(0, tableName.Count() - 1);
                MessageBox.Show($"This product is being referenced by the {tableName} table. Please modify or delete those entries before retying to delete the product.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
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
            try
            {
                // Retrieves package list from DB and creates a new findable binding list  and sets it as new binding source
                packages = new FindableBindingList<Package>(PackagesDB.GetPackages());
                packageBindingSource.DataSource = packages;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }

        /// <summary>
        /// Populates the product suppliers in the list boxes for the selected package
        /// </summary>
        /// <param name="selectedIndex">Package index</param>
        private void populateProductSupplierListBoxes(int selectedIndex)
        {
            try
            {
                associatedProductSuppliersListBox.Items.Clear();
                nonAssociatedProductSuppliersListBox.Items.Clear();

                BindingList<Package> currentPackages = (BindingList<Package>)packageBindingSource.DataSource;//grab current packages list
                SortedList<int, string> associatedProductSuppliers = currentPackages[selectedIndex].ProductSuppliers;//select associated product suppliers for the current package

                SortedList<int, string> allProductSuppliers = ProductsSuppliersDB.getProductsSuppliersIdAndString(); // Grabs all product suppliers

                // Go through all product suppliers. Add to associated or non associated product suppliers depending on if the sorted list contains the key
                foreach (KeyValuePair<int, string> entry in allProductSuppliers)
                {
                    if (associatedProductSuppliers.ContainsKey(entry.Key))
                    {
                        associatedProductSuppliersListBox.Items.Add(entry.Key + " | " + entry.Value);
                    }
                    else
                    {
                        nonAssociatedProductSuppliersListBox.Items.Add(entry.Key + " | " + entry.Value);
                    }

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }














        // Products Tab

        /// <summary>
        /// Moves to previous product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productPrevButton_Click(object sender, EventArgs e)
        {
            // Moves the binding source and product name combo box to previous item
            productBindingSource.MovePrevious();
            if (productComboBox.SelectedIndex > 0)
            {
                productComboBox.SelectedIndex -= 1;
                
            }
            
        }

        /// <summary>
        /// Moves to next product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productNextButton_Click(object sender, EventArgs e)
        {
            // Moves the binding source and product name combo box to next item
            productBindingSource.MoveNext();
            if (productComboBox.SelectedIndex < productComboBox.Items.Count - 1)
            {
                productComboBox.SelectedIndex += 1;
                
            }
        }

        /// <summary>
        /// combobox selectedindexchanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grab the selected product's index and assign to a variable
            int bsIndex = productBindingSource.Find("ProductName", productComboBox.SelectedItem.ToString());

            //if we have selected a product set the binding source position to match that of the selected ones index
            //populate associated and non associated supplier list boxes
            if (bsIndex > -1)
            {
                productBindingSource.Position = bsIndex;
                populateSupplierListBoxes(bsIndex);

            }
        }

        /// <summary>
        /// Opens dialog box to all adding a product in Product tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                AddEditForm addProduct = new AddEditForm("Product", true, false);//create instance of addeditform for product add

                DialogResult result = addProduct.ShowDialog(this);//variable the stores result returned from modal dialog form; shows addeditform for product add

                if (result == DialogResult.OK)
                {
                    ProductsDB.AddProducts(addProduct.addedProductName);//ProductsDB class method call to add product
                    InitializeProductDataBinding();
                    InitializeProductNameSearchComboBox();
                    InitializeSupplierDataBinding();
                    InitializeSupplierNameSearchComboBox();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }

        /// <summary>
        /// Opens dialog box to all editing a product in Product tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Gets selected product and opens form in edit mode for that package
                Product oldProduct = (Product)productBindingSource.Current;
                AddEditForm editProduct = new AddEditForm("Product", false, true, oldProduct, null);//create instance of addeditform for product add
                // If result is OK, edit package in DB
                if (editProduct.ShowDialog() == DialogResult.OK)
                {
                    Product newProduct = new Product(
                        editProduct.editedProductId,
                        editProduct.editedProductName);
                    // Updates package. Shows error if unsuccessful. Reloads all packages and moves to updated package
                    if (ProductsDB.UpdateProduct(oldProduct, newProduct))
                    {
                        InitializeProductDataBinding();
                        InitializeProductNameSearchComboBox();
                        productComboBox.SelectedItem = newProduct.ProductName; // Move to updated package
                        InitializeSupplierDataBinding();
                        InitializeSupplierNameSearchComboBox();

                    }
                    else
                    {
                        MessageBox.Show("Error. Could not update package. Please try again.");
                        InitializeProductDataBinding();
                        InitializeProductNameSearchComboBox();
                        productComboBox.SelectedItem = oldProduct.ProductName; // Move to package that tried to update. (May see that it was updated by someone else).
                        InitializeSupplierDataBinding();
                        InitializeSupplierNameSearchComboBox();
                    }
                }
             }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }

        }

        /// <summary>
        /// Adds a supplier to a products associated supplier list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addSupplierButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (nonAssociatedSuppliersListBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a supplier to add!", "Supplier Add Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                int index = nonAssociatedSuppliersListBox.SelectedIndex;//assign selected index from nonassociated list box to variable

                if (index != -1)
                {
                    int productId = -1;

                    //iterate through each item in the list 
                    foreach (Product product in products)
                    {
                        //if the combobox selected value matches an item in our master list assign its id to a variable
                        if (product.ProductName == productComboBox.SelectedItem.ToString())
                        {
                            productId = product.ProductId;
                            break;
                        }
                    }
                    if (productId == -1)
                    {
                        return;
                    }

                    //if the item was succesfully added to the database
                    if (ProductsSuppliersDB.addProductSupplier(productId, ((Supplier)nonAssociatedSuppliersListBox.Items[index]).SupplierId))
                    {
                        //add selected item from non associated items list box to the associated items listbox
                        associatedSuppliersListBox.Items.Add(nonAssociatedSuppliersListBox.Items[index]);

                        //remove the selected item from the non associated items list box
                        nonAssociatedSuppliersListBox.Items.RemoveAt(index);

                        //select the item just added to the list
                        associatedSuppliersListBox.SelectedIndex = associatedSuppliersListBox.Items.Count - 1;
                    }
                    else
                    {
                        MessageBox.Show("Error in updating database. Application data will be refreshed");
                        // reload data
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }
                

        /// <summary>
        /// Removes a supplier from a products nonassociated supplier list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeSupplierButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (associatedSuppliersListBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a supplier to remove!", "Supplier Remove Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                int index = associatedSuppliersListBox.SelectedIndex;//assign selected index from associated list box to variable

                if (index != -1)
                {
                    int productId = -1;

                    //iterate through each item in the list
                    foreach (Product product in products)
                    {
                        //if the combobox selected value matches an item in our master list assign its id to a variable
                        if (product.ProductName == productComboBox.SelectedItem.ToString())
                        {
                            productId = product.ProductId;
                            break;
                        }
                    }
                    if (productId == -1)
                    {
                        return;
                    }

                    //if the item was succesfully removed from the database
                    if (ProductsSuppliersDB.removeProductSupplier(productId, ((Supplier)associatedSuppliersListBox.Items[index]).SupplierId))
                    {
                        //add selected item from non associated items list box to the associated items listbox
                        nonAssociatedSuppliersListBox.Items.Add(associatedSuppliersListBox.Items[index]);

                        //remove the selected item from the non associated items list box
                        associatedSuppliersListBox.Items.RemoveAt(index);

                        int currentProductIndex = productComboBox.SelectedIndex;//capture current products combo box index

                        //change product combo box index then set back to current product to refresh list
                        productComboBox.SelectedIndex = currentProductIndex - 1;
                        productComboBox.SelectedIndex = currentProductIndex;
                    }
                    else
                    {
                        MessageBox.Show("Error in updating database. Application data will be refreshed");
                        // reload data
                    }
                }
            }
            catch (SqlException ex)
            {
                //create a regex match option that searches the exception message string for the table that caused the foreign key constraint
                Match match = Regex.Match(ex.Message, "\"dbo.+\"", RegexOptions.IgnoreCase);

                //split the returned string by the '.' character name, then grab the second string out of the two and assign it to a variable
                String tableName = match.Value.Split('.').Last();

                //remove the last '\' character from the string AKA get the table name
                tableName = tableName.Substring(0, tableName.Count() - 1);
                MessageBox.Show($"This product is being referenced by the {tableName} table. Please modify or delete those entries before retying to delete the product.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }

        /// <summary>
        /// Deletes the current product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirms user wants to delete package
                DialogResult result = MessageBox.Show("Are you sure you wold like to delete this product?", "Confirm delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Gets reference to current package. Deletes it. If unsuccessful, show error message.
                    Product currentProduct = (Product)productBindingSource.Current;
                    if (ProductsDB.DeleteProduct(currentProduct))
                    {
                        products.Remove(currentProduct);
                    }
                    else
                    {
                        MessageBox.Show("Error. Could not delete product. Please try again.");
                    }
                    InitializeProductDataBinding();
                    InitializeProductNameSearchComboBox();
                    InitializeSupplierDataBinding();
                    InitializeSupplierNameSearchComboBox();
                }
            }
            catch (SqlException ex)
            {
                //create a regex match option that searches the exception message string for the table that caused the foreign key constraint
                Match match = Regex.Match(ex.Message, "\"dbo.+\"", RegexOptions.IgnoreCase);

                //split the returned string by the '.' character name, then grab the second string out of the two and assign it to a variable
                String tableName = match.Value.Split('.').Last();

                //remove the last '\' character from the string AKA get the table name
                tableName = tableName.Substring(0, tableName.Count() - 1);
                MessageBox.Show($"This product is being referenced by the {tableName} table. Please modify or delete those entries before retrying to delete the product.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }


        /// <summary>
        /// Private form class method initializes component
        /// </summary>
        private void InitializeProductNameSearchComboBox()
        {
            productComboBox.Items.Clear();
            //iterate through products list from database and populate product combo box with name property of each Product class object
            foreach (Product product in products)
            {
                productComboBox.Items.Add(product.ProductName);
            }
            productComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Private form class method initializes component
        /// </summary>
        private void InitializeProductDataBinding()
        {
            try
            {
                products = new FindableBindingList<Product>(ProductsDB.GetProducts());//populates list with data retrieved from database
                productBindingSource.DataSource = products;//adds list data to binding source's datasource 
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }

        /// <summary>
        /// Populates suppliers associated and notassociated with a product listBoxes 
        /// </summary>
        /// <param name="index"></param>
        private void populateSupplierListBoxes(int index)
        {
            associatedSuppliersListBox.Items.Clear();
            nonAssociatedSuppliersListBox.Items.Clear();

            BindingList<Product> currentProducts = (BindingList<Product>)productBindingSource.DataSource;//grab current products list
            List<Supplier> associatedSupplier = currentProducts[index].Suppliers;//select associated suppliers for the current product

            //List<Supplier> allSuppliers = suppliers.ToList();//converts bindable list to list and assigns the suppliers to another list
            List<Supplier> allSuppliers = SuppliersDB.GetSuppliers();

            //iterate through each supplier object in our list of all suppliers
            foreach (Supplier supplier in allSuppliers)
            {
                bool isAssociatedSupplier = false;//bool variable used to check if a supplier is associated with a product

                //iterate through each supplier object in the known associated suppliers 
                foreach (Supplier associated in associatedSupplier)
                {
                    //call class method to verify if suppler is associated, set association bool to true if they are
                    if (supplier.Equals(associated))
                    {
                        isAssociatedSupplier = true;
                        break;
                    }
                }
                if (isAssociatedSupplier)
                {
                    associatedSuppliersListBox.Items.Add(supplier);//add the confirmed associated suppliers to the associated list
                }
                else
                {
                    nonAssociatedSuppliersListBox.Items.Add(supplier);//add all other suppliers to the non associated list
                }
            }

        }


















        // Suppliers Tab

        /// <summary>
        /// Moves to previous supplier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void supplierPrevButton_Click(object sender, EventArgs e)
        {
            // Moves the binding source and supplier name combo box to previous item
            supplierBindingSource.MovePrevious();
            if (supplierComboBox.SelectedIndex > 0)
            {
                supplierComboBox.SelectedIndex -= 1;
                
            }
            
        }

        /// <summary>
        /// Moves to next supplier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void supplierNextButton_Click(object sender, EventArgs e)
        {
            // Moves the binding source and supplier name combo box to next item
            supplierBindingSource.MoveNext();
            if (supplierComboBox.SelectedIndex < supplierComboBox.Items.Count - 1)
            {
                supplierComboBox.SelectedIndex += 1;
                
            }
        }                             

        /// <summary>
        /// combobox selectedindexchanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void supplierComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grab the selected supplier's index and assign to a variable
            int bsIndex = supplierBindingSource.Find("SupplierName", supplierComboBox.SelectedItem.ToString());

            //if we have selected a supplier set the binding source position to match that of the selected ones index
            //populate associated and non associated product list boxes
            if (bsIndex > -1)
            {
                supplierBindingSource.Position = bsIndex;
                populateProductListBoxes(bsIndex);
            }
        }

        /// <summary>
        /// Opens dialog box to all adding a supplier in Supplier tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupplierAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                AddEditForm addSupplier = new AddEditForm("Supplier", true, false);//create instance of addeditform for product add

                DialogResult result = addSupplier.ShowDialog(this);//variable the stores result returned from modal dialog form; shows addeditform for product add

                if (result == DialogResult.OK)
                {
                    SuppliersDB.AddSuppliers(addSupplier.addedSupplierName);//ProductsDB class method call to add product
                    InitializeSupplierDataBinding();
                    InitializeSupplierNameSearchComboBox();
                    InitializeProductDataBinding();
                    InitializeProductNameSearchComboBox();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }

        /// <summary>
        /// Opens dialog box to all editing a supplier in Supplier tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupplierEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Gets selected product and opens form in edit mode for that package
                Supplier oldSupplier = (Supplier)supplierBindingSource.Current;
                AddEditForm editSupplier = new AddEditForm("Supplier", false, true, null, oldSupplier);//create instance of addeditform for product add
                // If result is OK, edit package in DB
                if (editSupplier.ShowDialog() == DialogResult.OK)
                {
                    Supplier newSupplier = new Supplier(
                        editSupplier.editedSupplierId,
                        editSupplier.editedSupplierName);
                    // Updates package. Shows error if unsuccessful. Reloads all packages and moves to updated package
                    if (SuppliersDB.UpdateSuppliers(oldSupplier, newSupplier))
                    {
                        InitializeSupplierDataBinding();
                        InitializeSupplierNameSearchComboBox();
                        supplierComboBox.SelectedItem = newSupplier.SupplierName; // Move to updated package
                        InitializeProductDataBinding();
                        InitializeProductNameSearchComboBox();
                    }
                    else
                    {
                        MessageBox.Show("Error. Could not update package. Please try again.");
                        InitializeSupplierDataBinding();
                        InitializeSupplierNameSearchComboBox();
                        supplierComboBox.SelectedItem = oldSupplier.SupplierName; // Move to package that tried to update. (May see that it was updated by someone else).
                        InitializeProductDataBinding();
                        InitializeProductNameSearchComboBox();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }

        }

        
        /// <summary>
        /// Adds a product to a suppliers associated product list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addProductButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (nonAssociatedProductsListBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a product to add!", "Product Add Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                int index = nonAssociatedProductsListBox.SelectedIndex;//assign selected index from non associated list box to variable

                if (index != -1)
                {
                    int supplierId = -1;

                    //iterate through each item in the list
                    foreach (Supplier supplier in suppliers)
                    {
                        //if the combobox selected value matches an item in our master list assign its id to a variable
                        if (supplier.SupplierName == supplierComboBox.SelectedItem.ToString())
                        {
                            supplierId = supplier.SupplierId;
                            break;
                        }
                    }
                    if (supplierId == -1)
                    {
                        return;
                    }

                    //if the item was succesfully added to the database
                    if (ProductsSuppliersDB.addProductSupplier(((Product)nonAssociatedProductsListBox.Items[index]).ProductId, supplierId))
                    {
                        //add selected item from non associated items list box to the associated items listbox
                        associatedProductsListBox.Items.Add(nonAssociatedProductsListBox.Items[index]);

                        //remove the selected item from the non associated items list box
                        nonAssociatedProductsListBox.Items.RemoveAt(index);

                        //select the item just added to the list
                        associatedProductsListBox.SelectedIndex = associatedProductsListBox.Items.Count - 1;
                    }
                    else
                    {
                        MessageBox.Show("Error in updating database. Application data will be refreshed");
                        // reload data
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }

        }

        /// <summary>
        /// Removes a product from a suppliers associated product list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeProductButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (associatedProductsListBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a product to remove!", "Product Remove Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                int index = associatedProductsListBox.SelectedIndex;//assign selected index from associated list box to variable

                if (index != -1)
                {
                    int supplierId = -1;

                    //iterate through each item in the list
                    foreach (Supplier supplier in suppliers)
                    {
                        //if the combobox selected value matches an item in our master list assign its id to a variable
                        if (supplier.SupplierName == supplierComboBox.SelectedItem.ToString())
                        {
                            supplierId = supplier.SupplierId;
                            break;
                        }
                    }
                    if (supplierId == -1)
                    {
                        return;
                    }

                    //if the item was succesfully removed from the database
                    if (ProductsSuppliersDB.removeProductSupplier(((Product)associatedProductsListBox.Items[index]).ProductId, supplierId))
                    {
                        //add selected item from non associated items list box to the associated items listbox
                        nonAssociatedProductsListBox.Items.Add(associatedProductsListBox.Items[index]);

                        //remove the selected item from the non associated items list box
                        associatedProductsListBox.Items.RemoveAt(index);

                        //select the last item in the list
                        nonAssociatedProductsListBox.SelectedIndex = nonAssociatedProductsListBox.Items.Count - 1;
                    }
                    else
                    {
                        MessageBox.Show("Error in updating database. Application data will be refreshed");
                        // reload data
                    }
                }
            }
            catch (SqlException ex)
            {
                //create a regex match option that searches the exception message string for the table that caused the foreign key constraint
                Match match = Regex.Match(ex.Message, "\"dbo.+\"", RegexOptions.IgnoreCase);

                //split the returned string by the '.' character name, then grab the second string out of the two and assign it to a variable
                String tableName = match.Value.Split('.').Last();

                //remove the last '\' character from the string AKA get the table name
                tableName = tableName.Substring(0, tableName.Count() - 1);
                MessageBox.Show($"This product is being referenced by the {tableName} table. Please modify or delete those entries before retying to delete the product.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }

        /// <summary>
        /// Deletes the current supplier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void supplierDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirms user wants to delete package
                DialogResult result = MessageBox.Show("Are you sure you wold like to delete this supplier?", "Confirm delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Gets reference to current package. Deletes it. If unsuccessful, show error message.
                    Supplier currentSupplier = (Supplier)supplierBindingSource.Current;
                    if (SuppliersDB.DeleteSupplier(currentSupplier))
                    {
                        suppliers.Remove(currentSupplier);
                    }
                    else
                    {
                        MessageBox.Show("Error. Could not delete supplier. Please try again.");
                    }
                    InitializeSupplierDataBinding();
                    InitializeSupplierNameSearchComboBox();
                    InitializeProductDataBinding();
                    InitializeProductNameSearchComboBox();

                }
            }
            catch (SqlException ex)
            {
                //create a regex match option that searches the exception message string for the table that caused the foreign key constraint
                Match match = Regex.Match(ex.Message, "\"dbo.+\"", RegexOptions.IgnoreCase);

                //split the returned string by the '.' character name, then grab the second string out of the two and assign it to a variable
                String tableName = match.Value.Split('.').Last();

                //remove the last '\' character from the string AKA get the table name
                tableName = tableName.Substring(0, tableName.Count() - 1);
                MessageBox.Show($"This supplier is being referenced by the {tableName} table. Please modify or delete those entries before retrying to delete the supplier.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }



        /// <summary>
        /// Private form class method initializes component
        /// </summary>
        private void InitializeSupplierNameSearchComboBox()
        {
            supplierComboBox.Items.Clear();
            //iterate through suppliers list from database and populate supplier combo box with name property of each Supplier class object
            foreach (Supplier supplier in suppliers)
            {
                supplierComboBox.Items.Add(supplier.SupplierName);
            }
            supplierComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Private form class method initializes component
        /// </summary>
        private void InitializeSupplierDataBinding()
        {
            try
            {
                suppliers = new FindableBindingList<Supplier>(SuppliersDB.GetSuppliers());//populates list with data retrieved from database
                supplierBindingSource.DataSource = suppliers;//adds list data to binding source's datasource
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message + ". Please contact Tawico.");
            }
        }     
                           

        /// <summary>
        /// Populates products associated and notassociated with a supplier listBoxes 
        /// </summary>
        /// <param name="index"></param>
        private void populateProductListBoxes(int index)
        {
            associatedProductsListBox.Items.Clear();
            nonAssociatedProductsListBox.Items.Clear();

            BindingList<Supplier> currentSuppliers = (BindingList<Supplier>)supplierBindingSource.DataSource;//grab current suppliers list
            List<Product> associatedProduct = currentSuppliers[index].Products;//select associated products for the current supplier

            //List<Product> allProducts = products.ToList();//converts bindable list to list and assigns the products to another list
            List<Product> allProducts = ProductsDB.GetProducts();

            //iterate through each product object in our list of all products
            foreach (Product product in allProducts)
            {
                bool isAssociatedProduct = false;//bool variable used to check if a product is associated with a supplier

                //iterate through each product object in the known associated products
                foreach (Product associated in associatedProduct)
                {
                    //call class method to verify if product is associated, set association bool to true if they are
                    if (product.Equals(associated))
                    {
                        isAssociatedProduct = true;
                        break;
                    }
                }
                if (isAssociatedProduct)
                {
                    associatedProductsListBox.Items.Add(product);//add the confirmed associated products to the associated list
                }
                else
                {
                    nonAssociatedProductsListBox.Items.Add(product);//add all other products to the non associated list
                }
            }
        }

        
    }
}
