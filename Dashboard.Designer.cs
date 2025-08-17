namespace IMS
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.btnfull = new System.Windows.Forms.Button();
            this.btnmini = new System.Windows.Forms.Button();
            this.btnclose = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnimage = new Guna.UI2.WinForms.Guna2Button();
            this.btnoders = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.btnStoreReport = new Guna.UI2.WinForms.Guna2Button();
            this.btnSuppliers = new Guna.UI2.WinForms.Guna2Button();
            this.addimage = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.btncustomer = new Guna.UI2.WinForms.Guna2Button();
            this.btnCategry = new Guna.UI2.WinForms.Guna2Button();
            this.btnProducts = new Guna.UI2.WinForms.Guna2Button();
            this.btnusers = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addimage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnfull
            // 
            this.btnfull.AutoSize = true;
            this.btnfull.BackColor = System.Drawing.Color.Silver;
            this.btnfull.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnfull.ForeColor = System.Drawing.Color.Black;
            this.btnfull.Location = new System.Drawing.Point(1153, -3);
            this.btnfull.Name = "btnfull";
            this.btnfull.Size = new System.Drawing.Size(78, 46);
            this.btnfull.TabIndex = 3;
            this.btnfull.Text = "/";
            this.btnfull.UseVisualStyleBackColor = false;
            this.btnfull.Click += new System.EventHandler(this.Btnfull_Click);
            // 
            // btnmini
            // 
            this.btnmini.AutoSize = true;
            this.btnmini.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnmini.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(1069, -3);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(78, 46);
            this.btnmini.TabIndex = 4;
            this.btnmini.Text = "-";
            this.btnmini.UseVisualStyleBackColor = false;
            this.btnmini.Click += new System.EventHandler(this.Btnmini_Click);
            // 
            // btnclose
            // 
            this.btnclose.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.ToogleButton;
            this.btnclose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnclose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnclose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnclose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnclose.FillColor = System.Drawing.Color.Red;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(1237, -2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(73, 45);
            this.btnclose.TabIndex = 5;
            this.btnclose.Text = "x";
            this.btnclose.Click += new System.EventHandler(this.Btnclose_Click_1);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.DarkMagenta;
            this.guna2Panel1.Controls.Add(this.btnimage);
            this.guna2Panel1.Controls.Add(this.btnoders);
            this.guna2Panel1.Controls.Add(this.guna2Button2);
            this.guna2Panel1.Controls.Add(this.btnStoreReport);
            this.guna2Panel1.Controls.Add(this.btnSuppliers);
            this.guna2Panel1.Controls.Add(this.addimage);
            this.guna2Panel1.Controls.Add(this.btncustomer);
            this.guna2Panel1.Controls.Add(this.btnCategry);
            this.guna2Panel1.Controls.Add(this.btnProducts);
            this.guna2Panel1.Controls.Add(this.btnusers);
            this.guna2Panel1.Controls.Add(this.guna2Button1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(265, 1046);
            this.guna2Panel1.TabIndex = 6;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Guna2Panel1_Paint);
            // 
            // btnimage
            // 
            this.btnimage.BackColor = System.Drawing.Color.Indigo;
            this.btnimage.BorderColor = System.Drawing.Color.Indigo;
            this.btnimage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnimage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnimage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnimage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnimage.FillColor = System.Drawing.Color.DarkMagenta;
            this.btnimage.Font = new System.Drawing.Font("Verdana", 12F);
            this.btnimage.ForeColor = System.Drawing.Color.White;
            this.btnimage.Location = new System.Drawing.Point(55, 988);
            this.btnimage.Name = "btnimage";
            this.btnimage.Size = new System.Drawing.Size(137, 45);
            this.btnimage.TabIndex = 20;
            this.btnimage.Text = "Picture";
            this.btnimage.Click += new System.EventHandler(this.Btnimage_Click);
            // 
            // btnoders
            // 
            this.btnoders.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnoders.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnoders.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnoders.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnoders.FillColor = System.Drawing.Color.DarkMagenta;
            this.btnoders.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnoders.ForeColor = System.Drawing.Color.White;
            this.btnoders.Image = ((System.Drawing.Image)(resources.GetObject("btnoders.Image")));
            this.btnoders.Location = new System.Drawing.Point(40, 814);
            this.btnoders.Name = "btnoders";
            this.btnoders.Size = new System.Drawing.Size(180, 45);
            this.btnoders.TabIndex = 10;
            this.btnoders.Text = "Sales";
            this.btnoders.Click += new System.EventHandler(this.Btnoders_Click_1);
            // 
            // guna2Button2
            // 
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.FillColor = System.Drawing.Color.DarkMagenta;
            this.guna2Button2.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button2.ForeColor = System.Drawing.Color.White;
            this.guna2Button2.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button2.Image")));
            this.guna2Button2.Location = new System.Drawing.Point(40, 751);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(176, 45);
            this.guna2Button2.TabIndex = 9;
            this.guna2Button2.Text = "Status";
            this.guna2Button2.Click += new System.EventHandler(this.Guna2Button2_Click);
            // 
            // btnStoreReport
            // 
            this.btnStoreReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnStoreReport.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnStoreReport.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnStoreReport.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnStoreReport.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnStoreReport.FillColor = System.Drawing.Color.DarkMagenta;
            this.btnStoreReport.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStoreReport.ForeColor = System.Drawing.Color.White;
            this.btnStoreReport.Image = ((System.Drawing.Image)(resources.GetObject("btnStoreReport.Image")));
            this.btnStoreReport.Location = new System.Drawing.Point(40, 629);
            this.btnStoreReport.Name = "btnStoreReport";
            this.btnStoreReport.Size = new System.Drawing.Size(222, 45);
            this.btnStoreReport.TabIndex = 8;
            this.btnStoreReport.Text = "Add Products";
            this.btnStoreReport.Click += new System.EventHandler(this.BtnStoreReport_Click);
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSuppliers.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSuppliers.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSuppliers.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSuppliers.FillColor = System.Drawing.Color.DarkMagenta;
            this.btnSuppliers.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuppliers.ForeColor = System.Drawing.Color.White;
            this.btnSuppliers.Image = ((System.Drawing.Image)(resources.GetObject("btnSuppliers.Image")));
            this.btnSuppliers.Location = new System.Drawing.Point(40, 563);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(176, 45);
            this.btnSuppliers.TabIndex = 7;
            this.btnSuppliers.Text = "Suppliers";
            this.btnSuppliers.Click += new System.EventHandler(this.BtnSuppliers_Click);
            // 
            // addimage
            // 
            this.addimage.ImageRotate = 0F;
            this.addimage.Location = new System.Drawing.Point(40, 44);
            this.addimage.Name = "addimage";
            this.addimage.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.addimage.Size = new System.Drawing.Size(172, 164);
            this.addimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.addimage.TabIndex = 6;
            this.addimage.TabStop = false;
            // 
            // btncustomer
            // 
            this.btncustomer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btncustomer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btncustomer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btncustomer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btncustomer.FillColor = System.Drawing.Color.DarkMagenta;
            this.btncustomer.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncustomer.ForeColor = System.Drawing.Color.White;
            this.btncustomer.Image = ((System.Drawing.Image)(resources.GetObject("btncustomer.Image")));
            this.btncustomer.Location = new System.Drawing.Point(40, 501);
            this.btncustomer.Name = "btncustomer";
            this.btncustomer.Size = new System.Drawing.Size(172, 45);
            this.btncustomer.TabIndex = 4;
            this.btncustomer.Text = "Customers";
            this.btncustomer.Click += new System.EventHandler(this.BtnchangeStatus_Click);
            // 
            // btnCategry
            // 
            this.btnCategry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCategry.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCategry.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCategry.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCategry.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCategry.FillColor = System.Drawing.Color.DarkMagenta;
            this.btnCategry.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCategry.ForeColor = System.Drawing.Color.White;
            this.btnCategry.Image = ((System.Drawing.Image)(resources.GetObject("btnCategry.Image")));
            this.btnCategry.Location = new System.Drawing.Point(40, 380);
            this.btnCategry.Name = "btnCategry";
            this.btnCategry.Size = new System.Drawing.Size(176, 45);
            this.btnCategry.TabIndex = 3;
            this.btnCategry.Text = "Category";
            this.btnCategry.Click += new System.EventHandler(this.BtnCategry_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnProducts.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnProducts.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnProducts.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnProducts.FillColor = System.Drawing.Color.DarkMagenta;
            this.btnProducts.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProducts.ForeColor = System.Drawing.Color.White;
            this.btnProducts.Image = ((System.Drawing.Image)(resources.GetObject("btnProducts.Image")));
            this.btnProducts.Location = new System.Drawing.Point(40, 440);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(176, 45);
            this.btnProducts.TabIndex = 2;
            this.btnProducts.Text = "Products";
            this.btnProducts.Click += new System.EventHandler(this.BtnProducts_Click);
            // 
            // btnusers
            // 
            this.btnusers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnusers.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnusers.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnusers.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnusers.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnusers.FillColor = System.Drawing.Color.DarkMagenta;
            this.btnusers.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnusers.ForeColor = System.Drawing.Color.White;
            this.btnusers.Image = ((System.Drawing.Image)(resources.GetObject("btnusers.Image")));
            this.btnusers.Location = new System.Drawing.Point(40, 693);
            this.btnusers.Name = "btnusers";
            this.btnusers.Size = new System.Drawing.Size(176, 45);
            this.btnusers.TabIndex = 1;
            this.btnusers.Text = "Users";
            this.btnusers.Click += new System.EventHandler(this.Btnusers_Click_1);
            // 
            // guna2Button1
            // 
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.DarkMagenta;
            this.guna2Button1.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button1.Image")));
            this.guna2Button1.Location = new System.Drawing.Point(40, 326);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(180, 45);
            this.guna2Button1.TabIndex = 0;
            this.guna2Button1.Text = "Home";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1307, 1046);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnmini);
            this.Controls.Add(this.btnfull);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.addimage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnfull;
        private System.Windows.Forms.Button btnmini;
        private Guna.UI2.WinForms.Guna2Button btnclose;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnusers;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button btnCategry;
        private Guna.UI2.WinForms.Guna2Button btnProducts;
        private Guna.UI2.WinForms.Guna2Button btncustomer;
        public Guna.UI2.WinForms.Guna2CirclePictureBox addimage;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private Guna.UI2.WinForms.Guna2Button btnStoreReport;
        private Guna.UI2.WinForms.Guna2Button btnSuppliers;
        private Guna.UI2.WinForms.Guna2Button btnoders;
        private Guna.UI2.WinForms.Guna2Button btnimage;
    }
}