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
            InitializePackageDataBinding();
            InitializePackageNameSearchComboBox();
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
    }
}
