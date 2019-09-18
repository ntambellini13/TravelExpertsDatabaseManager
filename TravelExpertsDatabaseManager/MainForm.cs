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
            //Initialize and load data for main form components
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

        /// <summary>
        /// Private form class method initializes component
        /// </summary>
        private void InitializeProductNameSearchComboBox()
        {
            //iterate through products list from database and populate product combo box with name property of each Product class object
            foreach (Product product in products)
            {
                productComboBox.Items.Add(product.ProductName);
            }
        }

        /// <summary>
        /// Private form class method initializes component
        /// </summary>
        private void InitializeProductDataBinding()
        {
            products = new FindableBindingList<Product>(ProductsDB.GetProducts());//populates list with data retrieved from database
            productBindingSource.DataSource = products;//adds list data to binding source's datasource 
        }

        /// <summary>
        /// Private form class method initializes component
        /// </summary>
        private void InitializeSupplierNameSearchComboBox()
        {
            //iterate through suppliers list from database and populate supplier combo box with name property of each Supplier class object
            foreach (Supplier supplier in suppliers)
            {
                supplierComboBox.Items.Add(supplier.SupplierName);
            }
        }

        /// <summary>
        /// Private form class method initializes component
        /// </summary>
        private void InitializeSupplierDataBinding()
        {
            suppliers = new FindableBindingList<Supplier>(SuppliersDB.GetSuppliers());//populates list with data retrieved from database
            supplierBindingSource.DataSource = suppliers;//adds list data to binding source's datasource
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
                populateSupplierListBoxes(productComboBox.SelectedIndex);
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
                populateSupplierListBoxes(productComboBox.SelectedIndex);
            }
        }

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
        /// Populates suppliers associated and notassociated with a product listBoxes 
        /// </summary>
        /// <param name="index"></param>
        private void populateSupplierListBoxes(int index)
        {
            associatedSuppliersListBox.Items.Clear();
            nonAssociatedSuppliersListBox.Items.Clear();
            
            BindingList<Product> currentProducts = (BindingList<Product>)productBindingSource.DataSource;//grab current products list
            List<Supplier> associatedSupplier = currentProducts[index].Suppliers;//select associated suppliers for the current product

            List<Supplier> allSuppliers = suppliers.ToList();//converts bindable list to list and assigns the suppliers to another list

            //iterate through each supplier object in our list of all suppliers
            foreach (Supplier supplier in allSuppliers)
            {
                bool isAssociatedSupplier = false;//bool variable used to check if a supplier is associated with a product

                //iterate through each supplier object in the known associated suppliers 
                foreach(Supplier associated in associatedSupplier)
                {
                    //call class method to verify if suppler is associated, set association bool to true if they are
                    if (supplier.Equals(associated))
                    {
                        isAssociatedSupplier = true;
                        break;
                    }
                }
                if(isAssociatedSupplier)
                {
                    associatedSuppliersListBox.Items.Add(supplier);//add the confirmed associated suppliers to the associated list
                }
                else
                {
                    nonAssociatedSuppliersListBox.Items.Add(supplier);//add all other suppliers to the non associated list
                }
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
                populateProductListBoxes(supplierComboBox.SelectedIndex);
            }
        }

        /// <summary>
        /// Populates products associated and notassociated with a supplier listBoxes 
        /// </summary>
        /// <param name="index"></param>
        private void populateProductListBoxes(int selectedIndex)
        {
            associatedProductsListBox.Items.Clear();
            nonAssociatedProductsListBox.Items.Clear();

            BindingList<Supplier> currentSuppliers = (BindingList<Supplier>)supplierBindingSource.DataSource;//grab current products list
            List<Product> associatedProduct = currentSuppliers[selectedIndex].Products;//select associated suppliers for the current product

            //BindingList<Supplier> currentSuppliers = (BindingList<Supplier>)supplierBindingSource.List;

            List<Product> allProducts = products.ToList();//converts bindable list to list and assigns the products to another list

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
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
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
                AddEditForm editProduct = new AddEditForm("Product", false, true, productNameTextBox.Text);//create instance of addeditform for product add

                DialogResult result = editProduct.ShowDialog(this);//variable the stores result returned from modal dialog form; shows addeditform for product add

                if (result == DialogResult.OK)
                {
                    ProductsDB.EditProduct(editProduct.editedProductName, int.Parse(productIdTextBox.Text));
                    InitializeProductDataBinding();
                    InitializeProductNameSearchComboBox();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
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
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
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
                AddEditForm editSupplier = new AddEditForm("Supplier", true, false);//create instance of addeditform for product add

                DialogResult result = editSupplier.ShowDialog(this);//variable the stores result returned from modal dialog form; shows addeditform for product add

                if (result == DialogResult.OK)
                {
                    SuppliersDB.EditSuppliers(editSupplier.editedSupplierName, int.Parse(supplierIdTextBox.Text));//ProductsDB class method call to add product
                    InitializeSupplierDataBinding();
                    InitializeSupplierNameSearchComboBox();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                    ": " + ex.Message, ex.GetType().ToString());
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

        /// <summary>
        /// Adds a supplier to a products associated supplier list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addSupplierButton_Click(object sender, EventArgs e)
        {
            try
            {
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
                    if (ProductSupplierDB.addProductSupplier(productId, ((Supplier)nonAssociatedSuppliersListBox.Items[index]).SupplierId))
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
                    if (ProductSupplierDB.removeProductSupplier(productId, ((Supplier)associatedSuppliersListBox.Items[index]).SupplierId))
                    {
                        //add selected item from non associated items list box to the associated items listbox
                        nonAssociatedSuppliersListBox.Items.Add(associatedSuppliersListBox.Items[index]);

                        //remove the selected item from the non associated items list box
                        associatedSuppliersListBox.Items.RemoveAt(index);

                        //select the last item in list
                        nonAssociatedSuppliersListBox.SelectedIndex = nonAssociatedSuppliersListBox.Items.Count - 1;
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
                    if (ProductSupplierDB.addProductSupplier(((Product)nonAssociatedProductsListBox.Items[index]).ProductId, supplierId))
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
                    if (ProductSupplierDB.removeProductSupplier(((Product)associatedProductsListBox.Items[index]).ProductId, supplierId))
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
        }

        /// <summary>
        /// Main tab control selected index changed
        /// initializes data on each tabs controls by setting index when a tab is first selected by setting combo box selected index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedIndex == 1)
            {
                productComboBox.SelectedIndex = 0;
            }

            if (mainTabControl.SelectedIndex == 2)
            {
                supplierComboBox.SelectedIndex = 0;
            }
        }
    }
}
