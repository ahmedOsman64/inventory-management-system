using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;

namespace IMS
{
    public partial class Dashboard : Form
    {
        DBConnection objcon = new DBConnection();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd;
        SqlDataAdapter da;

        private Form activeForm = null; // Reference to the currently open form

        public Dashboard()
        {
            InitializeComponent();
            con = objcon.GetConnection();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Any initialization logic can be placed here.
        }

        private void Btnclose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btnusers_Click_1(object sender, EventArgs e)
        {
            OpenForm(new Users(), "Users");
        }

        private void Btnfull_Click(object sender, EventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized
                ? FormWindowState.Normal
                : FormWindowState.Maximized;
        }

        private void Btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnProducts_Click(object sender, EventArgs e)
        {
            OpenForm(new ProductForm(), "Products");
        }

        private void BtnCategry_Click(object sender, EventArgs e)
        {
            OpenForm(new Category(), "Category");
        }

        private void BtnchangeStatus_Click(object sender, EventArgs e)
        {
            if (activeForm == null || !(activeForm is ChangeStatus))
            {
                OpenForm(new Customers(), "Customers");
            }
        }

        private void Guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            // Optional: Add custom panel paint logic here if needed
        }

        private void BtnSuppliers_Click(object sender, EventArgs e)
        {
            OpenForm(new Suppliers(), "Suppliers");
        }

        private void BtnStoreReport_Click(object sender, EventArgs e)
        {
            OpenForm(new AddProducts(), "Add Products");
        }

        private void Btnoders_Click_1(object sender, EventArgs e)
        {
            OpenForm(new Sales(), "Sales");
        }

        private void OpenForm(Form newForm, string tableName)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm = null;
            }

            activeForm = newForm;
            activeForm.Text = tableName;
            activeForm.FormClosed += (s, args) => activeForm = null;
            activeForm.Show();
        }

        private void Btnimage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Upload Your Picture";
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    if (addimage != null)
                    {
                        addimage.Image = Image.FromFile(selectedFilePath);
                    }
                    MessageBox.Show("Image uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No image selected. Please upload your picture.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Guna2Button2_Click(object sender, EventArgs e)
        {
            OpenForm(new ChangeStatus(), "Change Status");
        }
    }
}
