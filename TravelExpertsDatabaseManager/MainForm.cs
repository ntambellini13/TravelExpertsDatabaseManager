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
        public FindableBindingList<Product> products;
        public FindableBindingList<Supplier> suppliers;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializePackageDataBinding();
            InitializePackageNameSearchComboBox();
            InitializeProductDataBinding();
            InitializeProductNameSearchComboBox();
            InitializeSupplierDataBinding();
            InitializeSupplierNameSearchComboBox();
        }

        private void InitializePackageNameSearchComboBox()
        {
            foreach(Package package in packages)
            {
                searchByPackageNameComboBox.Items.Add(package.PackageName);
            }
            searchByPackageNameComboBox.SelectedIndex = 0;
        }

        private void InitializePackageDataBinding()
        {
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

        private void searchByPackageNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bsIndex = packageBindingSource.Find("PackageName", searchByPackageNameComboBox.SelectedItem.ToString());
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

        
    }
}
