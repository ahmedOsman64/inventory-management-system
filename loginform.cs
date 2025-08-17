using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace IMS
{
    public partial class loginform : Form
    {
        // Connection string
        private string connectionString = "server=DESKTOP-8N9FM70\\AHMEDOSMAN;database=IMS;integrated security=true";

        public loginform()
        {
            InitializeComponent();
        }

        private void Btnlogin_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(txtusername.Text))
            {
                MessageBox.Show("Please enter a username.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtusername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtpassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtpassword.Focus();
                return;
            }

            try
            {
                // Establish connection to the database
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // SQL query to check username, password, and status
                    string query = "SELECT Status FROM Users WHERE UserName = @UserName AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@UserName", txtusername.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password",txtpassword.Text.Trim());

                        // Execute the query
                        object statusObj = cmd.ExecuteScalar();

                        // Check if the user exists
                        if (statusObj != null)
                        {
                            string status = statusObj.ToString();

                            // Check user status
                            if (status == "Blocked")
                            {
                                MessageBox.Show("This user has been blocked. Please contact the administrator.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                // Clear the fields and focus on username
                                txtusername.Clear();
                                txtusername.Clear();
                                txtusername.Focus();
                            }
                            else if (status == "Active")
                            {
                                //MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Redirect to another table/form
                                this.Hide();
                                Dashboard anotherForm = new Dashboard();
                                anotherForm.Show();
                            }
                            else
                            {
                                MessageBox.Show("This user has been blocked.Please contact the administrator.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                // Clear the fields
                                txtusername.Clear();
                                txtpassword.Clear();
                                txtusername.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            // Clear the fields
                            txtusername.Clear();
                            txtpassword.Clear();
                            txtusername.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            // Exit the application
            Application.Exit();
        }

        private void Loginform_Load(object sender, EventArgs e)
        {

        }
    }
}
