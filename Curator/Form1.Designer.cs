namespace Curator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private global::System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.steamShortcutsFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DeleteConsole_Button = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.consoleBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.curatorDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CuratorDataSet = new Curator.Data.CuratorDataSet();
            this.AddConsole_Button = new MetroFramework.Controls.MetroButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.romFolderListBox = new System.Windows.Forms.ListBox();
            this.systemDetailsName = new MetroFramework.Controls.MetroTextBox();
            this.AddRomFolder_Button = new MetroFramework.Controls.MetroButton();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.romArgsTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.emulatorArgsTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.AddEmulatorPath_Button = new MetroFramework.Controls.MetroButton();
            this.emulatorPathTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.romListView = new MetroFramework.Controls.MetroListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.romDetailsOverride = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.romDetailsEnabledToggle = new MetroFramework.Controls.MetroToggle();
            this.romDetailsCustomArgs = new System.Windows.Forms.TextBox();
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.romDetailsFolder = new System.Windows.Forms.TextBox();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.romDetailsName = new System.Windows.Forms.TextBox();
            this.romDetailsLabel = new MetroFramework.Controls.MetroLabel();
            this.emulatorPathFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.setShortcutsvdfFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToSteamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.rOMDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.steamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getGridPicturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.romFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.metroContextMenu1 = new MetroFramework.Controls.MetroContextMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.consoleBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curatorDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CuratorDataSet)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // steamShortcutsFileDialog
            // 
            this.steamShortcutsFileDialog.Filter = "\"Steam Shortcuts File (shortcuts.vdf)| shortcuts.vdf";
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.94737F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.05263F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 80);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 477F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1185, 477);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(455, 471);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DeleteConsole_Button);
            this.panel1.Controls.Add(this.metroLabel1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.AddConsole_Button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.MaximumSize = new System.Drawing.Size(10000, 57);
            this.panel1.MinimumSize = new System.Drawing.Size(2, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 57);
            this.panel1.TabIndex = 0;
            // 
            // DeleteConsole_Button
            // 
            this.DeleteConsole_Button.Location = new System.Drawing.Point(212, 29);
            this.DeleteConsole_Button.Name = "DeleteConsole_Button";
            this.DeleteConsole_Button.Size = new System.Drawing.Size(26, 21);
            this.DeleteConsole_Button.TabIndex = 19;
            this.DeleteConsole_Button.Text = "-";
            this.DeleteConsole_Button.UseSelectable = true;
            this.DeleteConsole_Button.Click += new System.EventHandler(this.DeleteConsole_Button_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(-1, -2);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(114, 25);
            this.metroLabel1.TabIndex = 18;
            this.metroLabel1.Text = "Select System";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.Silver;
            this.comboBox1.DataSource = this.consoleBindingSource2;
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 29);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(168, 21);
            this.comboBox1.TabIndex = 17;
            // 
            // consoleBindingSource2
            // 
            this.consoleBindingSource2.DataMember = "Console";
            this.consoleBindingSource2.DataSource = this.curatorDataSetBindingSource;
            // 
            // curatorDataSetBindingSource
            // 
            this.curatorDataSetBindingSource.DataSource = this.CuratorDataSet;
            this.curatorDataSetBindingSource.Position = 0;
            // 
            // CuratorDataSet
            // 
            this.CuratorDataSet.DataSetName = "CuratorDataSet";
            this.CuratorDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // AddConsole_Button
            // 
            this.AddConsole_Button.Location = new System.Drawing.Point(177, 29);
            this.AddConsole_Button.Name = "AddConsole_Button";
            this.AddConsole_Button.Size = new System.Drawing.Size(26, 21);
            this.AddConsole_Button.TabIndex = 15;
            this.AddConsole_Button.Text = "+";
            this.AddConsole_Button.UseSelectable = true;
            this.AddConsole_Button.Click += new System.EventHandler(this.AddConsole_Button_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.metroLabel7);
            this.panel2.Controls.Add(this.romFolderListBox);
            this.panel2.Controls.Add(this.systemDetailsName);
            this.panel2.Controls.Add(this.AddRomFolder_Button);
            this.panel2.Controls.Add(this.metroLabel6);
            this.panel2.Controls.Add(this.romArgsTextBox);
            this.panel2.Controls.Add(this.metroLabel5);
            this.panel2.Controls.Add(this.emulatorArgsTextBox);
            this.panel2.Controls.Add(this.metroLabel4);
            this.panel2.Controls.Add(this.AddEmulatorPath_Button);
            this.panel2.Controls.Add(this.emulatorPathTextBox);
            this.panel2.Controls.Add(this.metroLabel3);
            this.panel2.Controls.Add(this.metroLabel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 65);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(449, 403);
            this.panel2.TabIndex = 1;
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(4, 35);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(45, 19);
            this.metroLabel7.TabIndex = 32;
            this.metroLabel7.Text = "Name";
            // 
            // romFolderListBox
            // 
            this.romFolderListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.romFolderListBox.FormattingEnabled = true;
            this.romFolderListBox.Location = new System.Drawing.Point(8, 300);
            this.romFolderListBox.Name = "romFolderListBox";
            this.romFolderListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.romFolderListBox.Size = new System.Drawing.Size(431, 82);
            this.romFolderListBox.TabIndex = 31;
            this.romFolderListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.romFolderListBox_KeyDown);
            // 
            // systemDetailsName
            // 
            // 
            // 
            // 
            this.systemDetailsName.CustomButton.Image = null;
            this.systemDetailsName.CustomButton.Location = new System.Drawing.Point(217, 1);
            this.systemDetailsName.CustomButton.Name = "";
            this.systemDetailsName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.systemDetailsName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.systemDetailsName.CustomButton.TabIndex = 1;
            this.systemDetailsName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.systemDetailsName.CustomButton.UseSelectable = true;
            this.systemDetailsName.CustomButton.Visible = false;
            this.systemDetailsName.Lines = new string[0];
            this.systemDetailsName.Location = new System.Drawing.Point(8, 57);
            this.systemDetailsName.MaxLength = 32767;
            this.systemDetailsName.Name = "systemDetailsName";
            this.systemDetailsName.PasswordChar = '\0';
            this.systemDetailsName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.systemDetailsName.SelectedText = "";
            this.systemDetailsName.SelectionLength = 0;
            this.systemDetailsName.SelectionStart = 0;
            this.systemDetailsName.ShortcutsEnabled = true;
            this.systemDetailsName.Size = new System.Drawing.Size(239, 23);
            this.systemDetailsName.TabIndex = 30;
            this.systemDetailsName.UseSelectable = true;
            this.systemDetailsName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.systemDetailsName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.systemDetailsName.Leave += new System.EventHandler(this.systemDetailsName_Leave);
            // 
            // AddRomFolder_Button
            // 
            this.AddRomFolder_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddRomFolder_Button.Location = new System.Drawing.Point(405, 272);
            this.AddRomFolder_Button.Name = "AddRomFolder_Button";
            this.AddRomFolder_Button.Size = new System.Drawing.Size(34, 24);
            this.AddRomFolder_Button.TabIndex = 29;
            this.AddRomFolder_Button.Text = "+";
            this.AddRomFolder_Button.UseSelectable = true;
            this.AddRomFolder_Button.Click += new System.EventHandler(this.AddRomFolder_Button_Click);
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(8, 272);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(87, 19);
            this.metroLabel6.TabIndex = 28;
            this.metroLabel6.Text = "ROM Folders";
            // 
            // romArgsTextBox
            // 
            this.romArgsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.romArgsTextBox.CustomButton.Image = null;
            this.romArgsTextBox.CustomButton.Location = new System.Drawing.Point(373, 1);
            this.romArgsTextBox.CustomButton.Name = "";
            this.romArgsTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.romArgsTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.romArgsTextBox.CustomButton.TabIndex = 1;
            this.romArgsTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.romArgsTextBox.CustomButton.UseSelectable = true;
            this.romArgsTextBox.CustomButton.Visible = false;
            this.romArgsTextBox.Lines = new string[0];
            this.romArgsTextBox.Location = new System.Drawing.Point(8, 226);
            this.romArgsTextBox.MaxLength = 32767;
            this.romArgsTextBox.Name = "romArgsTextBox";
            this.romArgsTextBox.PasswordChar = '\0';
            this.romArgsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.romArgsTextBox.SelectedText = "";
            this.romArgsTextBox.SelectionLength = 0;
            this.romArgsTextBox.SelectionStart = 0;
            this.romArgsTextBox.ShortcutsEnabled = true;
            this.romArgsTextBox.Size = new System.Drawing.Size(395, 23);
            this.romArgsTextBox.TabIndex = 26;
            this.romArgsTextBox.UseSelectable = true;
            this.romArgsTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.romArgsTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.romArgsTextBox.Leave += new System.EventHandler(this.romArgsTextBox_Leave);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(3, 204);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(71, 19);
            this.metroLabel5.TabIndex = 25;
            this.metroLabel5.Text = "ROM Args";
            // 
            // emulatorArgsTextBox
            // 
            this.emulatorArgsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.emulatorArgsTextBox.CustomButton.Image = null;
            this.emulatorArgsTextBox.CustomButton.Location = new System.Drawing.Point(373, 1);
            this.emulatorArgsTextBox.CustomButton.Name = "";
            this.emulatorArgsTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.emulatorArgsTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.emulatorArgsTextBox.CustomButton.TabIndex = 1;
            this.emulatorArgsTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.emulatorArgsTextBox.CustomButton.UseSelectable = true;
            this.emulatorArgsTextBox.CustomButton.Visible = false;
            this.emulatorArgsTextBox.Lines = new string[0];
            this.emulatorArgsTextBox.Location = new System.Drawing.Point(8, 169);
            this.emulatorArgsTextBox.MaxLength = 32767;
            this.emulatorArgsTextBox.Name = "emulatorArgsTextBox";
            this.emulatorArgsTextBox.PasswordChar = '\0';
            this.emulatorArgsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.emulatorArgsTextBox.SelectedText = "";
            this.emulatorArgsTextBox.SelectionLength = 0;
            this.emulatorArgsTextBox.SelectionStart = 0;
            this.emulatorArgsTextBox.ShortcutsEnabled = true;
            this.emulatorArgsTextBox.Size = new System.Drawing.Size(395, 23);
            this.emulatorArgsTextBox.TabIndex = 24;
            this.emulatorArgsTextBox.UseSelectable = true;
            this.emulatorArgsTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.emulatorArgsTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.emulatorArgsTextBox.Leave += new System.EventHandler(this.emulatorArgsTextBox_Leave);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(3, 147);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(93, 19);
            this.metroLabel4.TabIndex = 23;
            this.metroLabel4.Text = "Emulator Args";
            // 
            // AddEmulatorPath_Button
            // 
            this.AddEmulatorPath_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddEmulatorPath_Button.Location = new System.Drawing.Point(405, 111);
            this.AddEmulatorPath_Button.Name = "AddEmulatorPath_Button";
            this.AddEmulatorPath_Button.Size = new System.Drawing.Size(34, 24);
            this.AddEmulatorPath_Button.TabIndex = 22;
            this.AddEmulatorPath_Button.Text = "+";
            this.AddEmulatorPath_Button.UseSelectable = true;
            this.AddEmulatorPath_Button.Click += new System.EventHandler(this.AddEmulatorPath_Button_Click);
            // 
            // emulatorPathTextBox
            // 
            this.emulatorPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.emulatorPathTextBox.CustomButton.Image = null;
            this.emulatorPathTextBox.CustomButton.Location = new System.Drawing.Point(373, 2);
            this.emulatorPathTextBox.CustomButton.Name = "";
            this.emulatorPathTextBox.CustomButton.Size = new System.Drawing.Size(19, 19);
            this.emulatorPathTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.emulatorPathTextBox.CustomButton.TabIndex = 1;
            this.emulatorPathTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.emulatorPathTextBox.CustomButton.UseSelectable = true;
            this.emulatorPathTextBox.CustomButton.Visible = false;
            this.emulatorPathTextBox.Enabled = false;
            this.emulatorPathTextBox.Lines = new string[0];
            this.emulatorPathTextBox.Location = new System.Drawing.Point(8, 111);
            this.emulatorPathTextBox.MaxLength = 32767;
            this.emulatorPathTextBox.Name = "emulatorPathTextBox";
            this.emulatorPathTextBox.PasswordChar = '\0';
            this.emulatorPathTextBox.ReadOnly = true;
            this.emulatorPathTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.emulatorPathTextBox.SelectedText = "";
            this.emulatorPathTextBox.SelectionLength = 0;
            this.emulatorPathTextBox.SelectionStart = 0;
            this.emulatorPathTextBox.ShortcutsEnabled = true;
            this.emulatorPathTextBox.Size = new System.Drawing.Size(395, 24);
            this.emulatorPathTextBox.TabIndex = 21;
            this.emulatorPathTextBox.UseSelectable = true;
            this.emulatorPathTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.emulatorPathTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(3, 89);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(91, 19);
            this.metroLabel3.TabIndex = 20;
            this.metroLabel3.Text = "Emulator Path";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.Location = new System.Drawing.Point(-1, 2);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(120, 25);
            this.metroLabel2.TabIndex = 19;
            this.metroLabel2.Text = "System Details";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoScroll = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.1797F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.8203F));
            this.tableLayoutPanel3.Controls.Add(this.romListView, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(464, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(718, 471);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // romListView
            // 
            this.romListView.CheckBoxes = true;
            this.romListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.romListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.romListView.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.romListView.FullRowSelect = true;
            this.romListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.romListView.HideSelection = false;
            this.romListView.LabelWrap = false;
            this.romListView.Location = new System.Drawing.Point(3, 3);
            this.romListView.MultiSelect = false;
            this.romListView.Name = "romListView";
            this.romListView.OwnerDraw = true;
            this.romListView.Size = new System.Drawing.Size(390, 465);
            this.romListView.TabIndex = 0;
            this.romListView.UseCompatibleStateImageBehavior = false;
            this.romListView.UseSelectable = true;
            this.romListView.View = System.Windows.Forms.View.Details;
            this.romListView.SelectedIndexChanged += new System.EventHandler(this.romListView_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ROMs";
            this.columnHeader2.Width = 244;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.metroButton2);
            this.panel3.Controls.Add(this.metroButton1);
            this.panel3.Controls.Add(this.romDetailsOverride);
            this.panel3.Controls.Add(this.metroLabel10);
            this.panel3.Controls.Add(this.romDetailsEnabledToggle);
            this.panel3.Controls.Add(this.romDetailsCustomArgs);
            this.panel3.Controls.Add(this.metroLabel11);
            this.panel3.Controls.Add(this.romDetailsFolder);
            this.panel3.Controls.Add(this.metroLabel9);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.metroLabel8);
            this.panel3.Controls.Add(this.romDetailsName);
            this.panel3.Controls.Add(this.romDetailsLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(399, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(316, 465);
            this.panel3.TabIndex = 1;
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(84, 159);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(34, 23);
            this.metroButton2.TabIndex = 45;
            this.metroButton2.Text = ">";
            this.metroButton2.UseSelectable = true;
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(44, 159);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(34, 23);
            this.metroButton1.TabIndex = 44;
            this.metroButton1.Text = "<";
            this.metroButton1.UseSelectable = true;
            // 
            // romDetailsOverride
            // 
            this.romDetailsOverride.AutoSize = true;
            this.romDetailsOverride.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.romDetailsOverride.Location = new System.Drawing.Point(14, 378);
            this.romDetailsOverride.Name = "romDetailsOverride";
            this.romDetailsOverride.Size = new System.Drawing.Size(194, 19);
            this.romDetailsOverride.TabIndex = 43;
            this.romDetailsOverride.Text = "Override System ROM Args";
            this.romDetailsOverride.UseSelectable = true;
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel10.Location = new System.Drawing.Point(14, 419);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(73, 25);
            this.metroLabel10.TabIndex = 42;
            this.metroLabel10.Text = "Enabled";
            // 
            // romDetailsEnabledToggle
            // 
            this.romDetailsEnabledToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.romDetailsEnabledToggle.AutoSize = true;
            this.romDetailsEnabledToggle.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.romDetailsEnabledToggle.Location = new System.Drawing.Point(94, 421);
            this.romDetailsEnabledToggle.Name = "romDetailsEnabledToggle";
            this.romDetailsEnabledToggle.Size = new System.Drawing.Size(80, 23);
            this.romDetailsEnabledToggle.TabIndex = 41;
            this.romDetailsEnabledToggle.Text = "Off";
            this.romDetailsEnabledToggle.UseSelectable = true;
            // 
            // romDetailsCustomArgs
            // 
            this.romDetailsCustomArgs.Location = new System.Drawing.Point(14, 351);
            this.romDetailsCustomArgs.Name = "romDetailsCustomArgs";
            this.romDetailsCustomArgs.Size = new System.Drawing.Size(280, 20);
            this.romDetailsCustomArgs.TabIndex = 40;
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.Location = new System.Drawing.Point(14, 328);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(120, 19);
            this.metroLabel11.TabIndex = 39;
            this.metroLabel11.Text = "Custom ROM Args";
            // 
            // romDetailsFolder
            // 
            this.romDetailsFolder.Location = new System.Drawing.Point(14, 286);
            this.romDetailsFolder.Name = "romDetailsFolder";
            this.romDetailsFolder.Size = new System.Drawing.Size(280, 20);
            this.romDetailsFolder.TabIndex = 36;
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.Location = new System.Drawing.Point(14, 264);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(47, 19);
            this.metroLabel9.TabIndex = 35;
            this.metroLabel9.Text = "Folder";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(44, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(230, 108);
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(14, 198);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(45, 19);
            this.metroLabel8.TabIndex = 33;
            this.metroLabel8.Text = "Name";
            // 
            // romDetailsName
            // 
            this.romDetailsName.Location = new System.Drawing.Point(14, 220);
            this.romDetailsName.Name = "romDetailsName";
            this.romDetailsName.Size = new System.Drawing.Size(280, 20);
            this.romDetailsName.TabIndex = 1;
            // 
            // romDetailsLabel
            // 
            this.romDetailsLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.romDetailsLabel.AutoSize = true;
            this.romDetailsLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.romDetailsLabel.Location = new System.Drawing.Point(94, 0);
            this.romDetailsLabel.Name = "romDetailsLabel";
            this.romDetailsLabel.Size = new System.Drawing.Size(106, 25);
            this.romDetailsLabel.TabIndex = 0;
            this.romDetailsLabel.Text = "ROM Details";
            // 
            // emulatorPathFileDialog
            // 
            this.emulatorPathFileDialog.Filter = "\"Exe Files (.exe)|*.exe";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.steamToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(26, 53);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(217, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator,
            this.setShortcutsvdfFileToolStripMenuItem,
            this.exportToSteamToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(181, 6);
            // 
            // setShortcutsvdfFileToolStripMenuItem
            // 
            this.setShortcutsvdfFileToolStripMenuItem.Name = "setShortcutsvdfFileToolStripMenuItem";
            this.setShortcutsvdfFileToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.setShortcutsvdfFileToolStripMenuItem.Text = "Set Shortcuts.vdf File";
            this.setShortcutsvdfFileToolStripMenuItem.Click += new System.EventHandler(this.SetShortcutsvdfFileToolStripMenuItem_Click);
            // 
            // exportToSteamToolStripMenuItem
            // 
            this.exportToSteamToolStripMenuItem.Name = "exportToSteamToolStripMenuItem";
            this.exportToSteamToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exportToSteamToolStripMenuItem.Text = "Export to Steam";
            this.exportToSteamToolStripMenuItem.Click += new System.EventHandler(this.exportToSteamToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAllToolStripMenuItem,
            this.toolStripSeparator2,
            this.rOMDetailsToolStripMenuItem,
            this.systemDetailsToolStripMenuItem});
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.ShowShortcutKeys = false;
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.saveAllToolStripMenuItem.Text = "All Details";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(147, 6);
            // 
            // rOMDetailsToolStripMenuItem
            // 
            this.rOMDetailsToolStripMenuItem.Name = "rOMDetailsToolStripMenuItem";
            this.rOMDetailsToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.rOMDetailsToolStripMenuItem.Text = "ROM Details";
            // 
            // systemDetailsToolStripMenuItem
            // 
            this.systemDetailsToolStripMenuItem.Name = "systemDetailsToolStripMenuItem";
            this.systemDetailsToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.systemDetailsToolStripMenuItem.Text = "System Details";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // steamToolStripMenuItem
            // 
            this.steamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getGridPicturesToolStripMenuItem});
            this.steamToolStripMenuItem.Name = "steamToolStripMenuItem";
            this.steamToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.steamToolStripMenuItem.Text = "Steam";
            // 
            // getGridPicturesToolStripMenuItem
            // 
            this.getGridPicturesToolStripMenuItem.Name = "getGridPicturesToolStripMenuItem";
            this.getGridPicturesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.getGridPicturesToolStripMenuItem.Text = "Get Grid Pictures";
            this.getGridPicturesToolStripMenuItem.Click += new System.EventHandler(this.getGridPicturesToolStripMenuItem_Click);
            // 
            // metroContextMenu1
            // 
            this.metroContextMenu1.Name = "metroContextMenu1";
            this.metroContextMenu1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 577);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(20, 80, 20, 20);
            this.Text = "Curator";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.consoleBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curatorDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CuratorDataSet)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private global::System.Windows.Forms.OpenFileDialog steamShortcutsFileDialog;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton AddConsole_Button;
        private MetroFramework.Controls.MetroTextBox emulatorPathTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroButton AddEmulatorPath_Button;
        private MetroFramework.Controls.MetroTextBox romArgsTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroTextBox emulatorArgsTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroButton AddRomFolder_Button;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private System.Windows.Forms.ComboBox comboBox1;
        private MetroFramework.Controls.MetroButton DeleteConsole_Button;
        private MetroFramework.Controls.MetroTextBox systemDetailsName;
        private System.Windows.Forms.OpenFileDialog emulatorPathFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rOMDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ListBox romFolderListBox;
        private System.Windows.Forms.FolderBrowserDialog romFolderDialog;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private MetroFramework.Controls.MetroListView romListView;
        public System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem setShortcutsvdfFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToSteamToolStripMenuItem;
        private Data.CuratorDataSet CuratorDataSet;
        private System.Windows.Forms.BindingSource curatorDataSetBindingSource;
        private System.Windows.Forms.BindingSource consoleBindingSource2;
        private System.Windows.Forms.Panel panel3;
        private MetroFramework.Controls.MetroLabel romDetailsLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private System.Windows.Forms.TextBox romDetailsName;
        private MetroFramework.Controls.MetroContextMenu metroContextMenu1;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroCheckBox romDetailsOverride;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroToggle romDetailsEnabledToggle;
        private System.Windows.Forms.TextBox romDetailsCustomArgs;
        private MetroFramework.Controls.MetroLabel metroLabel11;
        private System.Windows.Forms.TextBox romDetailsFolder;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private System.Windows.Forms.ToolStripMenuItem steamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getGridPicturesToolStripMenuItem;
    }
}

