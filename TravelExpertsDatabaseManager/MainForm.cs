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
            populateProductSupplierListBoxes(searchByPackageNameComboBox.SelectedIndex);
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
                populateSupplierListBoxes(productComboBox.SelectedIndex);
            }
        }

        private void productNextButton_Click(object sender, EventArgs e)
        {
            productBindingSource.MoveNext();
            if (productComboBox.SelectedIndex < productComboBox.Items.Count - 1)
            {
                productComboBox.SelectedIndex += 1;
                populateSupplierListBoxes(productComboBox.SelectedIndex);
            }
        }

        private void supplierPrevButton_Click(object sender, EventArgs e)
        {
            supplierBindingSource.MovePrevious();
            if (supplierComboBox.SelectedIndex > 0)
            {
                supplierComboBox.SelectedIndex -= 1;
                populateSupplierListBoxes(supplierComboBox.SelectedIndex);
            }
        }

        private void supplierNextButton_Click(object sender, EventArgs e)
        {
            supplierBindingSource.MoveNext();
            if (supplierComboBox.SelectedIndex < supplierComboBox.Items.Count - 1)
            {
                supplierComboBox.SelectedIndex += 1;
                populateSupplierListBoxes(supplierComboBox.SelectedIndex);
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
        }

        private void productComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bsIndex = productBindingSource.Find("ProductName", productComboBox.SelectedItem.ToString());
            if (bsIndex > -1)
            {
                productBindingSource.Position = bsIndex;
                populateSupplierListBoxes(bsIndex);
                
            }
        }

      

        private void populateSupplierListBoxes(int index)
        {
            associatedSuppliersListBox.Items.Clear();
            nonAssociatedSuppliersListBox.Items.Clear();
            
            BindingList<Product> currentProducts = (BindingList<Product>)productBindingSource.DataSource;//grab current products list
            List<Supplier> associatedSupplier = currentProducts[index].Suppliers;//select associated suppliers for the current product

            //BindingList<Supplier> currentSuppliers = (BindingList<Supplier>)supplierBindingSource.List;

            List<Supplier> allSuppliers = suppliers.ToList();

            foreach (Supplier supplier in allSuppliers)
            {
                bool isAssociatedSupplier = false;
                foreach(Supplier associated in associatedSupplier)
                {
                    if (supplier.Equals(associated))
                    {
                        isAssociatedSupplier = true;
                        break;
                    }
                }
                if(isAssociatedSupplier)
                {
                    associatedSuppliersListBox.Items.Add(supplier);
                }
                else
                {
                    nonAssociatedSuppliersListBox.Items.Add(supplier);
                }
            }

        }


        private void supplierComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bsIndex = supplierBindingSource.Find("SupplierName", supplierComboBox.SelectedItem.ToString());
            if (bsIndex > -1)
            {
                supplierBindingSource.Position = bsIndex;
                populateProductListBoxes(supplierComboBox.SelectedIndex);
                
            }
           
        }

        private void populateProductListBoxes(int selectedIndex)
        {
            associatedProductsListBox.Items.Clear();
            nonAssociatedProductsListBox.Items.Clear();

            BindingList<Supplier> currentSuppliers = (BindingList<Supplier>)supplierBindingSource.DataSource;//grab current products list
            List<Product> associatedProduct = currentSuppliers[selectedIndex].Products;//select associated suppliers for the current product

            //BindingList<Supplier> currentSuppliers = (BindingList<Supplier>)supplierBindingSource.List;

            List<Product> allProducts = products.ToList();

            foreach (Product product in allProducts)
            {
                bool isAssociatedProduct = false;
                foreach (Product associated in associatedProduct)
                {
                    if (product.Equals(associated))
                    {
                        isAssociatedProduct = true;
                        break;
                    }
                }
                if (isAssociatedProduct)
                {
                    associatedProductsListBox.Items.Add(product);
                }
                else
                {
                    nonAssociatedProductsListBox.Items.Add(product);
                }
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

        private void addSupplierButton_Click(object sender, EventArgs e)
        {
            int index = nonAssociatedSuppliersListBox.SelectedIndex;

            if(index != -1)
            {
                int productId=-1;
                foreach (Product product in products)
                {
                    if(product.ProductName== productComboBox.SelectedItem.ToString())
                    {
                        productId = product.ProductId;
                        break;
                    }
                }
                if (productId == -1)
                {
                    return;
                }
                //add selected item from non associated items list box to the associated items listbox
                //then remove the selected item from the non associated items list box
                if (ProductsSuppliersDB.addProductSupplier(productId, ((Supplier)nonAssociatedSuppliersListBox.Items[index]).SupplierId))
                {
                    associatedSuppliersListBox.Items.Add(nonAssociatedSuppliersListBox.Items[index]);
                    nonAssociatedSuppliersListBox.Items.RemoveAt(index);
                    associatedSuppliersListBox.SelectedIndex = associatedSuppliersListBox.Items.Count - 1;
                }
                else
                {
                    MessageBox.Show("Error in updating database. Application data will be refreshed");
                    // reload data
                }

            }


            
        }

        private void removeSupplierButton_Click(object sender, EventArgs e)
        {
            int index = associatedSuppliersListBox.SelectedIndex;

            if (index != -1)
            {
                int productId = -1;
                foreach (Product product in products)
                {
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
                //add selected item from non associated items list box to the associated items listbox
                //then remove the selected item from the non associated items list box
                if (ProductsSuppliersDB.removeProductSupplier(productId, ((Supplier)associatedSuppliersListBox.Items[index]).SupplierId))
                {
                    nonAssociatedSuppliersListBox.Items.Add(associatedSuppliersListBox.Items[index]);
                    associatedSuppliersListBox.Items.RemoveAt(index);
                    nonAssociatedSuppliersListBox.SelectedIndex = nonAssociatedSuppliersListBox.Items.Count - 1;
                }
                else
                {
                    MessageBox.Show("Error in updating database. Application data will be refreshed");
                    // reload data
                }
                
            }


        }

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

        private void addProductButton_Click(object sender, EventArgs e)
        {
            int index = nonAssociatedProductsListBox.SelectedIndex;

            if (index != -1)
            {
                int supplierId = -1;
                foreach (Supplier supplier in suppliers)
                {
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
                //add selected item from non associated items list box to the associated items listbox
                //then remove the selected item from the non associated items list box
                if (ProductsSuppliersDB.addProductSupplier(((Product)nonAssociatedProductsListBox.Items[index]).ProductId, supplierId))
                {
                    associatedProductsListBox.Items.Add(nonAssociatedProductsListBox.Items[index]);
                    nonAssociatedProductsListBox.Items.RemoveAt(index);
                    associatedProductsListBox.SelectedIndex = associatedProductsListBox.Items.Count - 1;
                }
                else
                {
                    MessageBox.Show("Error in updating database. Application data will be refreshed");
                    // reload data
                }

            }
        }

        private void removeProductButton_Click(object sender, EventArgs e)
        {
            int index = associatedProductsListBox.SelectedIndex;

            if (index != -1)
            {
                int supplierId = -1;
                foreach (Supplier supplier in suppliers)
                {
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
                //add selected item from non associated items list box to the associated items listbox
                //then remove the selected item from the non associated items list box
                if (ProductsSuppliersDB.removeProductSupplier(((Product)associatedProductsListBox.Items[index]).ProductId, supplierId))
                {
                    nonAssociatedProductsListBox.Items.Add(associatedProductsListBox.Items[index]);
                    associatedProductsListBox.Items.RemoveAt(index);
                    nonAssociatedProductsListBox.SelectedIndex = nonAssociatedProductsListBox.Items.Count - 1;
                }
                else
                {
                    MessageBox.Show("Error in updating database. Application data will be refreshed");
                    // reload data
                }

            }
        }

        private void populateProductSupplierListBoxes(int selectedIndex)
        {
            associatedProductSuppliersListBox.Items.Clear();
            nonAssociatedProductSuppliersListBox.Items.Clear();

            BindingList<Package> currentPackages = (BindingList<Package>) packageBindingSource.DataSource;//grab current packages list
            SortedList<int, string> associatedProductSuppliers = currentPackages[selectedIndex].ProductSuppliers;//select associated product suppliers for the current package

            SortedList<int, string> allProductSuppliers = ProductsSuppliersDB.getProductsSuppliersIdAndString();

            foreach (KeyValuePair<int, string> entry in allProductSuppliers)
            {
                if (associatedProductSuppliers.ContainsKey(entry.Key))
                {
                    associatedProductSuppliersListBox.Items.Add(entry.Key +" | " + entry.Value);
                }
                else
                {
                    nonAssociatedProductSuppliersListBox.Items.Add(entry.Key + " | " + entry.Value);
                }
                
            }
        }

        private void addProductSupplierButton_Click(object sender, EventArgs e)
        {
            int selectedProductSupplier = nonAssociatedProductSuppliersListBox.SelectedIndex;
            int packageId = ((Package) packageBindingSource.Current).PackageId;

            if (selectedProductSupplier != -1 && packageId!=-1)
            {
                //add selected item from non associated items list box to the associated items listbox
                //then remove the selected item from the non associated items list box
                if (PackagesProductsSuppliersDB.addPackageProductSupplier(packageId, Convert.ToInt32(nonAssociatedProductSuppliersListBox.Items[selectedProductSupplier].ToString().Split('|').First())))
                {
                    associatedProductSuppliersListBox.Items.Add(nonAssociatedProductSuppliersListBox.Items[selectedProductSupplier]);
                    nonAssociatedProductSuppliersListBox.Items.RemoveAt(selectedProductSupplier);
                    associatedProductSuppliersListBox.SelectedIndex = associatedProductSuppliersListBox.Items.Count - 1;

                    Package currentPackage = (Package) packageBindingSource.Current;
                    currentPackage.ProductSuppliers = PackagesProductsSuppliersDB.getProductsSuppliersIdAndString_ByPackageId(currentPackage.PackageId);
                }
                else
                {
                    MessageBox.Show("Error in updating database. Application data will be refreshed");
                    // reload data
                }

            }
        }

        private void removeProductSupplierButton_Click(object sender, EventArgs e)
        {
            int selectedProductSupplier = associatedProductSuppliersListBox.SelectedIndex;
            int packageId = ((Package) packageBindingSource.Current).PackageId;

            if (selectedProductSupplier != -1 && packageId != -1)
            {
                //add selected item from non associated items list box to the associated items listbox
                //then remove the selected item from the non associated items list box
                if (PackagesProductsSuppliersDB.removePackageProductSupplier(packageId, Convert.ToInt32(associatedProductSuppliersListBox.Items[selectedProductSupplier].ToString().Split('|').First())))
                {
                    nonAssociatedProductSuppliersListBox.Items.Add(associatedProductSuppliersListBox.Items[selectedProductSupplier]);
                    associatedProductSuppliersListBox.Items.RemoveAt(selectedProductSupplier);
                    nonAssociatedProductSuppliersListBox.SelectedIndex = nonAssociatedProductSuppliersListBox.Items.Count - 1;

                    Package currentPackage = (Package) packageBindingSource.Current;
                    currentPackage.ProductSuppliers = PackagesProductsSuppliersDB.getProductsSuppliersIdAndString_ByPackageId(currentPackage.PackageId);
                }
                else
                {
                    MessageBox.Show("Error in updating database. Application data will be refreshed");
                    // reload data
                }

            }
        }
    }
}
