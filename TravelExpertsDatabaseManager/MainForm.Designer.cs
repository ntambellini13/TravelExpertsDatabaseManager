namespace TravelExpertsDatabaseManager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label airfairInclusionLabel;
            System.Windows.Forms.Label imagePathLabel;
            System.Windows.Forms.Label packageAgencyCommissionLabel;
            System.Windows.Forms.Label packageBasePriceLabel;
            System.Windows.Forms.Label packageDescriptionLabel;
            System.Windows.Forms.Label packageEndDateLabel;
            System.Windows.Forms.Label packageIdLabel;
            System.Windows.Forms.Label packageStartDateLabel;
            System.Windows.Forms.Label partnerURLLabel;
            System.Windows.Forms.Label productIdLabel;
            System.Windows.Forms.Label productNameLabel;
            System.Windows.Forms.Label supplierIdLabel;
            System.Windows.Forms.Label supplierNameLabel;
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.packagesTabPage = new System.Windows.Forms.TabPage();
            this.exitButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.searchByPackageNameComboBox = new System.Windows.Forms.ComboBox();
            this.searchByPackageNameLabel = new System.Windows.Forms.Label();
            this.nextButton = new System.Windows.Forms.Button();
            this.prevButton = new System.Windows.Forms.Button();
            this.packageIdTextBox = new System.Windows.Forms.TextBox();
            this.airfairInclusionCheckBox = new System.Windows.Forms.CheckBox();
            this.packageAgencyCommissionTextBox = new System.Windows.Forms.TextBox();
            this.packageBasePriceTextBox = new System.Windows.Forms.TextBox();
            this.packageDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.packageEndDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.packageStartDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.partnerURLTextBox = new System.Windows.Forms.TextBox();

            this.productsTabPage = new System.Windows.Forms.TabPage();
            this.productComboBox = new System.Windows.Forms.ComboBox();
            this.searchProductLabel = new System.Windows.Forms.Label();
            this.productNextButton = new System.Windows.Forms.Button();
            this.productPrevButton = new System.Windows.Forms.Button();
            this.productEditButton = new System.Windows.Forms.Button();
            this.productAddButton = new System.Windows.Forms.Button();
            this.productNameTextBox = new System.Windows.Forms.TextBox();
            this.productIdTextBox = new System.Windows.Forms.TextBox();
            this.suppliersTabPage = new System.Windows.Forms.TabPage();
            this.supplierComboBox = new System.Windows.Forms.ComboBox();
            this.searchSupplierLabel = new System.Windows.Forms.Label();
            this.supplierNextButton = new System.Windows.Forms.Button();
            this.supplierPrevButton = new System.Windows.Forms.Button();
            this.supplierEditButton = new System.Windows.Forms.Button();
            this.supplierAddButton = new System.Windows.Forms.Button();
            this.supplierNameTextBox = new System.Windows.Forms.TextBox();
            this.supplierIdTextBox = new System.Windows.Forms.TextBox();
            this.packageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.productBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.supplierBindingSource = new System.Windows.Forms.BindingSource(this.components);

            this.imageForPictureBoxPictureBox = new System.Windows.Forms.PictureBox();
            this.deleteButton = new System.Windows.Forms.Button();

            airfairInclusionLabel = new System.Windows.Forms.Label();
            imagePathLabel = new System.Windows.Forms.Label();
            packageAgencyCommissionLabel = new System.Windows.Forms.Label();
            packageBasePriceLabel = new System.Windows.Forms.Label();
            packageDescriptionLabel = new System.Windows.Forms.Label();
            packageEndDateLabel = new System.Windows.Forms.Label();
            packageIdLabel = new System.Windows.Forms.Label();
            packageStartDateLabel = new System.Windows.Forms.Label();
            partnerURLLabel = new System.Windows.Forms.Label();
            productIdLabel = new System.Windows.Forms.Label();
            productNameLabel = new System.Windows.Forms.Label();
            supplierIdLabel = new System.Windows.Forms.Label();
            supplierNameLabel = new System.Windows.Forms.Label();
            this.mainTabControl.SuspendLayout();
            this.packagesTabPage.SuspendLayout();

            this.productsTabPage.SuspendLayout();
            this.suppliersTabPage.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.imageForPictureBoxPictureBox)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.packageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.supplierBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // airfairInclusionLabel
            // 
            airfairInclusionLabel.AutoSize = true;
            airfairInclusionLabel.Location = new System.Drawing.Point(28, 206);
            airfairInclusionLabel.Name = "airfairInclusionLabel";
            airfairInclusionLabel.Size = new System.Drawing.Size(81, 13);
            airfairInclusionLabel.TabIndex = 0;
            airfairInclusionLabel.Text = "Airfair Inclusion:";
            // 
            // imagePathLabel
            // 
            imagePathLabel.AutoSize = true;
            imagePathLabel.Location = new System.Drawing.Point(28, 364);
            imagePathLabel.Name = "imagePathLabel";
            imagePathLabel.Size = new System.Drawing.Size(39, 13);
            imagePathLabel.TabIndex = 2;
            imagePathLabel.Text = "Image:";
            // 
            // packageAgencyCommissionLabel
            // 
            packageAgencyCommissionLabel.AutoSize = true;
            packageAgencyCommissionLabel.Location = new System.Drawing.Point(27, 178);
            packageAgencyCommissionLabel.Name = "packageAgencyCommissionLabel";
            packageAgencyCommissionLabel.Size = new System.Drawing.Size(150, 13);
            packageAgencyCommissionLabel.TabIndex = 4;
            packageAgencyCommissionLabel.Text = "Package Agency Commission:";
            // 
            // packageBasePriceLabel
            // 
            packageBasePriceLabel.AutoSize = true;
            packageBasePriceLabel.Location = new System.Drawing.Point(27, 152);
            packageBasePriceLabel.Name = "packageBasePriceLabel";
            packageBasePriceLabel.Size = new System.Drawing.Size(107, 13);
            packageBasePriceLabel.TabIndex = 6;
            packageBasePriceLabel.Text = "Package Base Price:";
            // 
            // packageDescriptionLabel
            // 
            packageDescriptionLabel.AutoSize = true;
            packageDescriptionLabel.Location = new System.Drawing.Point(28, 234);
            packageDescriptionLabel.Name = "packageDescriptionLabel";
            packageDescriptionLabel.Size = new System.Drawing.Size(109, 13);
            packageDescriptionLabel.TabIndex = 8;
            packageDescriptionLabel.Text = "Package Description:";
            // 
            // packageEndDateLabel
            // 
            packageEndDateLabel.AutoSize = true;
            packageEndDateLabel.Location = new System.Drawing.Point(27, 112);
            packageEndDateLabel.Name = "packageEndDateLabel";
            packageEndDateLabel.Size = new System.Drawing.Size(101, 13);
            packageEndDateLabel.TabIndex = 10;
            packageEndDateLabel.Text = "Package End Date:";
            // 
            // packageIdLabel
            // 
            packageIdLabel.AutoSize = true;
            packageIdLabel.Location = new System.Drawing.Point(27, 62);
            packageIdLabel.Name = "packageIdLabel";
            packageIdLabel.Size = new System.Drawing.Size(65, 13);
            packageIdLabel.TabIndex = 12;
            packageIdLabel.Text = "Package Id:";
            // 
            // packageStartDateLabel
            // 
            packageStartDateLabel.AutoSize = true;
            packageStartDateLabel.Location = new System.Drawing.Point(27, 85);
            packageStartDateLabel.Name = "packageStartDateLabel";
            packageStartDateLabel.Size = new System.Drawing.Size(104, 13);
            packageStartDateLabel.TabIndex = 16;
            packageStartDateLabel.Text = "Package Start Date:";
            // 
            // partnerURLLabel
            // 
            partnerURLLabel.AutoSize = true;
            partnerURLLabel.Location = new System.Drawing.Point(27, 390);
            partnerURLLabel.Name = "partnerURLLabel";
            partnerURLLabel.Size = new System.Drawing.Size(69, 13);
            partnerURLLabel.TabIndex = 18;
            partnerURLLabel.Text = "Partner URL:";
            // 
            // productIdLabel
            // 
            productIdLabel.AutoSize = true;
            productIdLabel.Location = new System.Drawing.Point(44, 64);
            productIdLabel.Name = "productIdLabel";
            productIdLabel.Size = new System.Drawing.Size(59, 13);
            productIdLabel.TabIndex = 0;
            productIdLabel.Text = "Product Id:";
            // 
            // productNameLabel
            // 
            productNameLabel.AutoSize = true;
            productNameLabel.Location = new System.Drawing.Point(25, 90);
            productNameLabel.Name = "productNameLabel";
            productNameLabel.Size = new System.Drawing.Size(78, 13);
            productNameLabel.TabIndex = 2;
            productNameLabel.Text = "Product Name:";
            // 
            // supplierIdLabel
            // 
            supplierIdLabel.AutoSize = true;
            supplierIdLabel.Location = new System.Drawing.Point(43, 64);
            supplierIdLabel.Name = "supplierIdLabel";
            supplierIdLabel.Size = new System.Drawing.Size(60, 13);
            supplierIdLabel.TabIndex = 0;
            supplierIdLabel.Text = "Supplier Id:";
            // 
            // supplierNameLabel
            // 
            supplierNameLabel.AutoSize = true;
            supplierNameLabel.Location = new System.Drawing.Point(24, 90);
            supplierNameLabel.Name = "supplierNameLabel";
            supplierNameLabel.Size = new System.Drawing.Size(79, 13);
            supplierNameLabel.TabIndex = 2;
            supplierNameLabel.Text = "Supplier Name:";
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(608, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.packagesTabPage);
            this.mainTabControl.Controls.Add(this.productsTabPage);
            this.mainTabControl.Controls.Add(this.suppliersTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(12, 39);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(571, 451);
            this.mainTabControl.TabIndex = 1;
            // 
            // packagesTabPage
            // 
            this.packagesTabPage.AutoScroll = true;
            this.packagesTabPage.Controls.Add(this.imageForPictureBoxPictureBox);
            this.packagesTabPage.Controls.Add(this.exitButton);
            this.packagesTabPage.Controls.Add(this.deleteButton);
            this.packagesTabPage.Controls.Add(this.editButton);
            this.packagesTabPage.Controls.Add(this.addButton);
            this.packagesTabPage.Controls.Add(this.searchByPackageNameComboBox);
            this.packagesTabPage.Controls.Add(this.searchByPackageNameLabel);
            this.packagesTabPage.Controls.Add(this.nextButton);
            this.packagesTabPage.Controls.Add(this.prevButton);
            this.packagesTabPage.Controls.Add(this.packageIdTextBox);
            this.packagesTabPage.Controls.Add(airfairInclusionLabel);
            this.packagesTabPage.Controls.Add(this.airfairInclusionCheckBox);
            this.packagesTabPage.Controls.Add(imagePathLabel);
            this.packagesTabPage.Controls.Add(packageAgencyCommissionLabel);
            this.packagesTabPage.Controls.Add(this.packageAgencyCommissionTextBox);
            this.packagesTabPage.Controls.Add(packageBasePriceLabel);
            this.packagesTabPage.Controls.Add(this.packageBasePriceTextBox);
            this.packagesTabPage.Controls.Add(packageDescriptionLabel);
            this.packagesTabPage.Controls.Add(this.packageDescriptionTextBox);
            this.packagesTabPage.Controls.Add(packageEndDateLabel);
            this.packagesTabPage.Controls.Add(this.packageEndDateDateTimePicker);
            this.packagesTabPage.Controls.Add(packageIdLabel);
            this.packagesTabPage.Controls.Add(packageStartDateLabel);
            this.packagesTabPage.Controls.Add(this.packageStartDateDateTimePicker);
            this.packagesTabPage.Controls.Add(partnerURLLabel);
            this.packagesTabPage.Controls.Add(this.partnerURLTextBox);
            this.packagesTabPage.Location = new System.Drawing.Point(4, 22);
            this.packagesTabPage.Name = "packagesTabPage";
            this.packagesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.packagesTabPage.Size = new System.Drawing.Size(563, 425);
            this.packagesTabPage.TabIndex = 0;
            this.packagesTabPage.Text = "Packages";
            this.packagesTabPage.UseVisualStyleBackColor = true;
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(483, 390);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(63, 23);
            this.exitButton.TabIndex = 25;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(483, 299);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(63, 23);
            this.editButton.TabIndex = 25;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(404, 299);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(63, 23);
            this.addButton.TabIndex = 24;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // searchByPackageNameComboBox
            // 
            this.searchByPackageNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchByPackageNameComboBox.FormattingEnabled = true;
            this.searchByPackageNameComboBox.Location = new System.Drawing.Point(183, 22);
            this.searchByPackageNameComboBox.Name = "searchByPackageNameComboBox";
            this.searchByPackageNameComboBox.Size = new System.Drawing.Size(200, 21);
            this.searchByPackageNameComboBox.TabIndex = 23;
            this.searchByPackageNameComboBox.SelectedIndexChanged += new System.EventHandler(this.searchByPackageNameComboBox_SelectedIndexChanged);
            // 
            // searchByPackageNameLabel
            // 
            this.searchByPackageNameLabel.AutoSize = true;
            this.searchByPackageNameLabel.Location = new System.Drawing.Point(27, 25);
            this.searchByPackageNameLabel.Name = "searchByPackageNameLabel";
            this.searchByPackageNameLabel.Size = new System.Drawing.Size(138, 13);
            this.searchByPackageNameLabel.TabIndex = 22;
            this.searchByPackageNameLabel.Text = "Search by Package Name: ";
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(483, 20);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(63, 23);
            this.nextButton.TabIndex = 21;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(404, 21);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(63, 23);
            this.prevButton.TabIndex = 21;
            this.prevButton.Text = "Prev";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
            // 
            // packageIdTextBox
            // 
            this.packageIdTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.packageBindingSource, "PackageId", true));
            this.packageIdTextBox.Location = new System.Drawing.Point(183, 55);
            this.packageIdTextBox.Name = "packageIdTextBox";
            this.packageIdTextBox.ReadOnly = true;
            this.packageIdTextBox.Size = new System.Drawing.Size(200, 20);
            this.packageIdTextBox.TabIndex = 20;
            // 
            // airfairInclusionCheckBox
            // 
            this.airfairInclusionCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.packageBindingSource, "AirfairInclusion", true));
            this.airfairInclusionCheckBox.Enabled = false;
            this.airfairInclusionCheckBox.Location = new System.Drawing.Point(183, 201);
            this.airfairInclusionCheckBox.Name = "airfairInclusionCheckBox";
            this.airfairInclusionCheckBox.Size = new System.Drawing.Size(200, 24);
            this.airfairInclusionCheckBox.TabIndex = 1;
            this.airfairInclusionCheckBox.UseVisualStyleBackColor = true;
            // 
            // packageAgencyCommissionTextBox
            // 
            this.packageAgencyCommissionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.packageBindingSource, "PackageAgencyCommission", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.packageAgencyCommissionTextBox.Location = new System.Drawing.Point(183, 175);
            this.packageAgencyCommissionTextBox.Name = "packageAgencyCommissionTextBox";
            this.packageAgencyCommissionTextBox.ReadOnly = true;
            this.packageAgencyCommissionTextBox.Size = new System.Drawing.Size(200, 20);
            this.packageAgencyCommissionTextBox.TabIndex = 5;
            // 
            // packageBasePriceTextBox
            // 
            this.packageBasePriceTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.packageBindingSource, "PackageBasePrice", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.packageBasePriceTextBox.Location = new System.Drawing.Point(183, 149);
            this.packageBasePriceTextBox.Name = "packageBasePriceTextBox";
            this.packageBasePriceTextBox.ReadOnly = true;
            this.packageBasePriceTextBox.Size = new System.Drawing.Size(200, 20);
            this.packageBasePriceTextBox.TabIndex = 7;
            // 
            // packageDescriptionTextBox
            // 
            this.packageDescriptionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.packageBindingSource, "PackageDescription", true));
            this.packageDescriptionTextBox.Location = new System.Drawing.Point(183, 231);
            this.packageDescriptionTextBox.Multiline = true;
            this.packageDescriptionTextBox.Name = "packageDescriptionTextBox";
            this.packageDescriptionTextBox.ReadOnly = true;
            this.packageDescriptionTextBox.Size = new System.Drawing.Size(200, 62);
            this.packageDescriptionTextBox.TabIndex = 9;
            // 
            // packageEndDateDateTimePicker
            // 
            this.packageEndDateDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.packageBindingSource, "PackageEndDate", true));
            this.packageEndDateDateTimePicker.Enabled = false;
            this.packageEndDateDateTimePicker.Location = new System.Drawing.Point(183, 111);
            this.packageEndDateDateTimePicker.Name = "packageEndDateDateTimePicker";
            this.packageEndDateDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.packageEndDateDateTimePicker.TabIndex = 11;
            // 
            // packageStartDateDateTimePicker
            // 
            this.packageStartDateDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.packageBindingSource, "PackageStartDate", true));
            this.packageStartDateDateTimePicker.Enabled = false;
            this.packageStartDateDateTimePicker.Location = new System.Drawing.Point(183, 85);
            this.packageStartDateDateTimePicker.Name = "packageStartDateDateTimePicker";
            this.packageStartDateDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.packageStartDateDateTimePicker.TabIndex = 17;
            // 
            // partnerURLTextBox
            // 
            this.partnerURLTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.packageBindingSource, "PartnerURL", true));
            this.partnerURLTextBox.Location = new System.Drawing.Point(183, 387);
            this.partnerURLTextBox.Name = "partnerURLTextBox";
            this.partnerURLTextBox.ReadOnly = true;
            this.partnerURLTextBox.Size = new System.Drawing.Size(200, 20);
            this.partnerURLTextBox.TabIndex = 19;
            // 
            // productsTabPage
            // 
            this.productsTabPage.Controls.Add(this.productComboBox);
            this.productsTabPage.Controls.Add(this.searchProductLabel);
            this.productsTabPage.Controls.Add(this.productNextButton);
            this.productsTabPage.Controls.Add(this.productPrevButton);
            this.productsTabPage.Controls.Add(this.productEditButton);
            this.productsTabPage.Controls.Add(this.productAddButton);
            this.productsTabPage.Controls.Add(productNameLabel);
            this.productsTabPage.Controls.Add(this.productNameTextBox);
            this.productsTabPage.Controls.Add(productIdLabel);
            this.productsTabPage.Controls.Add(this.productIdTextBox);
            this.productsTabPage.Location = new System.Drawing.Point(4, 22);
            this.productsTabPage.Name = "productsTabPage";
            this.productsTabPage.Size = new System.Drawing.Size(563, 373);
            this.productsTabPage.TabIndex = 2;
            this.productsTabPage.Text = "Products";
            this.productsTabPage.UseVisualStyleBackColor = true;
            // 
            // productComboBox
            // 
            this.productComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.productComboBox.FormattingEnabled = true;
            this.productComboBox.Location = new System.Drawing.Point(180, 21);
            this.productComboBox.Name = "productComboBox";
            this.productComboBox.Size = new System.Drawing.Size(200, 21);
            this.productComboBox.TabIndex = 27;
            this.productComboBox.SelectedIndexChanged += new System.EventHandler(this.productComboBox_SelectedIndexChanged);
            // 
            // searchProductLabel
            // 
            this.searchProductLabel.AutoSize = true;
            this.searchProductLabel.Location = new System.Drawing.Point(24, 24);
            this.searchProductLabel.Name = "searchProductLabel";
            this.searchProductLabel.Size = new System.Drawing.Size(132, 13);
            this.searchProductLabel.TabIndex = 26;
            this.searchProductLabel.Text = "Search by Product Name: ";
            // 
            // productNextButton
            // 
            this.productNextButton.Location = new System.Drawing.Point(470, 20);
            this.productNextButton.Name = "productNextButton";
            this.productNextButton.Size = new System.Drawing.Size(63, 23);
            this.productNextButton.TabIndex = 24;
            this.productNextButton.Text = "Next";
            this.productNextButton.UseVisualStyleBackColor = true;
            this.productNextButton.Click += new System.EventHandler(this.productNextButton_Click);
            // 
            // productPrevButton
            // 
            this.productPrevButton.Location = new System.Drawing.Point(401, 20);
            this.productPrevButton.Name = "productPrevButton";
            this.productPrevButton.Size = new System.Drawing.Size(63, 23);
            this.productPrevButton.TabIndex = 25;
            this.productPrevButton.Text = "Prev";
            this.productPrevButton.UseVisualStyleBackColor = true;
            this.productPrevButton.Click += new System.EventHandler(this.productPrevButton_Click);
            // 
            // productEditButton
            // 
            this.productEditButton.Location = new System.Drawing.Point(472, 344);
            this.productEditButton.Name = "productEditButton";
            this.productEditButton.Size = new System.Drawing.Size(63, 23);
            this.productEditButton.TabIndex = 5;
            this.productEditButton.Text = "Edit";
            this.productEditButton.UseVisualStyleBackColor = true;
            this.productEditButton.Click += new System.EventHandler(this.ProductEditButton_Click);
            // 
            // productAddButton
            // 
            this.productAddButton.Location = new System.Drawing.Point(400, 344);
            this.productAddButton.Name = "productAddButton";
            this.productAddButton.Size = new System.Drawing.Size(63, 23);
            this.productAddButton.TabIndex = 4;
            this.productAddButton.Text = "Add";
            this.productAddButton.UseVisualStyleBackColor = true;
            this.productAddButton.Click += new System.EventHandler(this.ProductAddButton_Click);
            // 
            // productNameTextBox
            // 
            this.productNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.productBindingSource, "ProductName", true));
            this.productNameTextBox.Location = new System.Drawing.Point(109, 87);
            this.productNameTextBox.Name = "productNameTextBox";
            this.productNameTextBox.ReadOnly = true;
            this.productNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.productNameTextBox.TabIndex = 3;
            // 
            // productIdTextBox
            // 
            this.productIdTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.productBindingSource, "ProductId", true));
            this.productIdTextBox.Location = new System.Drawing.Point(109, 61);
            this.productIdTextBox.Name = "productIdTextBox";
            this.productIdTextBox.ReadOnly = true;
            this.productIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.productIdTextBox.TabIndex = 1;
            // 
            // suppliersTabPage
            // 
            this.suppliersTabPage.Controls.Add(this.supplierComboBox);
            this.suppliersTabPage.Controls.Add(this.searchSupplierLabel);
            this.suppliersTabPage.Controls.Add(this.supplierNextButton);
            this.suppliersTabPage.Controls.Add(this.supplierPrevButton);
            this.suppliersTabPage.Controls.Add(this.supplierEditButton);
            this.suppliersTabPage.Controls.Add(this.supplierAddButton);
            this.suppliersTabPage.Controls.Add(supplierNameLabel);
            this.suppliersTabPage.Controls.Add(this.supplierNameTextBox);
            this.suppliersTabPage.Controls.Add(supplierIdLabel);
            this.suppliersTabPage.Controls.Add(this.supplierIdTextBox);
            this.suppliersTabPage.Location = new System.Drawing.Point(4, 22);
            this.suppliersTabPage.Name = "suppliersTabPage";
            this.suppliersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.suppliersTabPage.Size = new System.Drawing.Size(563, 373);
            this.suppliersTabPage.TabIndex = 1;
            this.suppliersTabPage.Text = "Suppliers";
            this.suppliersTabPage.UseVisualStyleBackColor = true;
            // 
            // supplierComboBox
            // 
            this.supplierComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.supplierComboBox.FormattingEnabled = true;
            this.supplierComboBox.Location = new System.Drawing.Point(180, 21);
            this.supplierComboBox.Name = "supplierComboBox";
            this.supplierComboBox.Size = new System.Drawing.Size(200, 21);
            this.supplierComboBox.TabIndex = 31;
            this.supplierComboBox.SelectedIndexChanged += new System.EventHandler(this.supplierComboBox_SelectedIndexChanged);
            // 
            // searchSupplierLabel
            // 
            this.searchSupplierLabel.AutoSize = true;
            this.searchSupplierLabel.Location = new System.Drawing.Point(24, 24);
            this.searchSupplierLabel.Name = "searchSupplierLabel";
            this.searchSupplierLabel.Size = new System.Drawing.Size(133, 13);
            this.searchSupplierLabel.TabIndex = 30;
            this.searchSupplierLabel.Text = "Search by Supplier Name: ";
            // 
            // supplierNextButton
            // 
            this.supplierNextButton.Location = new System.Drawing.Point(470, 20);
            this.supplierNextButton.Name = "supplierNextButton";
            this.supplierNextButton.Size = new System.Drawing.Size(63, 23);
            this.supplierNextButton.TabIndex = 28;
            this.supplierNextButton.Text = "Next";
            this.supplierNextButton.UseVisualStyleBackColor = true;
            this.supplierNextButton.Click += new System.EventHandler(this.supplierNextButton_Click);
            // 
            // supplierPrevButton
            // 
            this.supplierPrevButton.Location = new System.Drawing.Point(401, 20);
            this.supplierPrevButton.Name = "supplierPrevButton";
            this.supplierPrevButton.Size = new System.Drawing.Size(63, 23);
            this.supplierPrevButton.TabIndex = 29;
            this.supplierPrevButton.Text = "Prev";
            this.supplierPrevButton.UseVisualStyleBackColor = true;
            this.supplierPrevButton.Click += new System.EventHandler(this.supplierPrevButton_Click);
            // 
            // supplierEditButton
            // 
            this.supplierEditButton.Location = new System.Drawing.Point(464, 344);
            this.supplierEditButton.Name = "supplierEditButton";
            this.supplierEditButton.Size = new System.Drawing.Size(75, 23);
            this.supplierEditButton.TabIndex = 5;
            this.supplierEditButton.Text = "Edit";
            this.supplierEditButton.UseVisualStyleBackColor = true;
            this.supplierEditButton.Click += new System.EventHandler(this.SupplierEditButton_Click);
            // 
            // supplierAddButton
            // 
            this.supplierAddButton.Location = new System.Drawing.Point(383, 344);
            this.supplierAddButton.Name = "supplierAddButton";
            this.supplierAddButton.Size = new System.Drawing.Size(75, 23);
            this.supplierAddButton.TabIndex = 4;
            this.supplierAddButton.Text = "Add";
            this.supplierAddButton.UseVisualStyleBackColor = true;
            this.supplierAddButton.Click += new System.EventHandler(this.SupplierAddButton_Click);
            // 
            // supplierNameTextBox
            // 
            this.supplierNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.supplierBindingSource, "SupplierName", true));
            this.supplierNameTextBox.Location = new System.Drawing.Point(109, 87);
            this.supplierNameTextBox.Name = "supplierNameTextBox";
            this.supplierNameTextBox.ReadOnly = true;
            this.supplierNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.supplierNameTextBox.TabIndex = 3;
            // 
            // supplierIdTextBox
            // 
            this.supplierIdTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.supplierBindingSource, "SupplierId", true));
            this.supplierIdTextBox.Location = new System.Drawing.Point(109, 61);
            this.supplierIdTextBox.Name = "supplierIdTextBox";
            this.supplierIdTextBox.ReadOnly = true;
            this.supplierIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.supplierIdTextBox.TabIndex = 1;
            // 
            // packageBindingSource
            // 
            this.packageBindingSource.DataSource = typeof(TravelExpertsData.Package);
            // 
            // productBindingSource
            // 
            this.productBindingSource.DataSource = typeof(TravelExpertsData.Product);
            // 
            // supplierBindingSource
            // 

            this.supplierBindingSource.DataSource = typeof(TravelExpertsData.Supplier);

            // 
            // imageForPictureBoxPictureBox
            // 
            this.imageForPictureBoxPictureBox.DataBindings.Add(new System.Windows.Forms.Binding("Image", this.packageBindingSource, "ImageForPictureBox", true));
            this.imageForPictureBoxPictureBox.Location = new System.Drawing.Point(183, 299);
            this.imageForPictureBoxPictureBox.Name = "imageForPictureBoxPictureBox";
            this.imageForPictureBoxPictureBox.Size = new System.Drawing.Size(200, 82);
            this.imageForPictureBoxPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageForPictureBoxPictureBox.TabIndex = 26;
            this.imageForPictureBoxPictureBox.TabStop = false;
            // 
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(483, 341);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(63, 23);
            this.deleteButton.TabIndex = 25;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 502);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Travel Experts Database Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainTabControl.ResumeLayout(false);
            this.packagesTabPage.ResumeLayout(false);
            this.packagesTabPage.PerformLayout();

            this.productsTabPage.ResumeLayout(false);
            this.productsTabPage.PerformLayout();
            this.suppliersTabPage.ResumeLayout(false);
            this.suppliersTabPage.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(this.imageForPictureBoxPictureBox)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.packageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.supplierBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage packagesTabPage;
        private System.Windows.Forms.TabPage suppliersTabPage;
        private System.Windows.Forms.CheckBox airfairInclusionCheckBox;
        private System.Windows.Forms.BindingSource packageBindingSource;
        private System.Windows.Forms.TextBox packageAgencyCommissionTextBox;
        private System.Windows.Forms.TextBox packageBasePriceTextBox;
        private System.Windows.Forms.TextBox packageDescriptionTextBox;
        private System.Windows.Forms.DateTimePicker packageEndDateDateTimePicker;
        private System.Windows.Forms.DateTimePicker packageStartDateDateTimePicker;
        private System.Windows.Forms.TextBox partnerURLTextBox;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.TextBox packageIdTextBox;
        private System.Windows.Forms.ComboBox searchByPackageNameComboBox;
        private System.Windows.Forms.Label searchByPackageNameLabel;

        private System.Windows.Forms.TabPage productsTabPage;
        private System.Windows.Forms.TextBox productNameTextBox;
        private System.Windows.Forms.BindingSource productBindingSource;
        private System.Windows.Forms.TextBox productIdTextBox;
        private System.Windows.Forms.TextBox supplierNameTextBox;
        private System.Windows.Forms.BindingSource supplierBindingSource;
        private System.Windows.Forms.TextBox supplierIdTextBox;
        private System.Windows.Forms.Button supplierEditButton;
        private System.Windows.Forms.Button supplierAddButton;
        private System.Windows.Forms.Button productEditButton;
        private System.Windows.Forms.Button productAddButton;
        private System.Windows.Forms.ComboBox productComboBox;
        private System.Windows.Forms.Label searchProductLabel;
        private System.Windows.Forms.Button productNextButton;
        private System.Windows.Forms.Button productPrevButton;
        private System.Windows.Forms.ComboBox supplierComboBox;
        private System.Windows.Forms.Label searchSupplierLabel;
        private System.Windows.Forms.Button supplierNextButton;
        private System.Windows.Forms.Button supplierPrevButton;

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.PictureBox imageForPictureBoxPictureBox;
        private System.Windows.Forms.Button deleteButton;

    }
}

