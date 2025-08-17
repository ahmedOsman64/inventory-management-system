using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IMS
{
    public partial class Customers : Form
    {
        DBConnection objcon = new DBConnection();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd;
        SqlDataAdapter da;
        private int CustomerId;
        public Customers()
        {
            InitializeComponent();
            con = objcon.GetConnection(); // Ensure this method returns a valid SqlConnection object.

        }

        private void ClearFields()
        {
            txtCustomername.Clear();
            txtphone.Clear();
            cmbcustomertype.SelectedIndex = -1;
            cmbGender.SelectedIndex = -1;
            dtpDor.Value = DateTime.Today;
            btnAdd.Text = "Save";
            CustomerId = 0;
        }

        private void FillCustomersData()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string query = "SELECT CustomerId, CustomerName, CustomerType, Phone, Gender, Dor FROM Customers";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvcustomers.DataSource = dt;

                // Optionally format column headers
                dgvcustomers.Columns["CustomerId"].HeaderText = "Customer ID";
                dgvcustomers.Columns["CustomerName"].HeaderText = "Customer Name";
                dgvcustomers.Columns["CustomerType"].HeaderText = "Customer Type";
                dgvcustomers.Columns["Phone"].HeaderText = "Phone";
                dgvcustomers.Columns["Gender"].HeaderText = "Gender";
                dgvcustomers.Columns["Dor"].HeaderText = "Date of Registration";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtCustomername.Text))
            {
                MessageBox.Show("Enter Customer Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomername.Focus();
                return false;
            }

            if (cmbcustomertype.SelectedIndex == -1)
            {
                MessageBox.Show("Select Customer Type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbcustomertype.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtphone.Text) || !long.TryParse(txtphone.Text, out _))
            {
                MessageBox.Show("Enter a valid Phone Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtphone.Focus();
                return false;
            }

            if (cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Select Gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGender.Focus();
                return false;
            }

            return true;
        }






        private void Customers_Load(object sender, EventArgs e)
        {

            FillCustomersData();
            ClearFields();
            cmbcustomertype.SelectedIndex = 0;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFields()) return;

                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (btnAdd.Text == "Save")
                {
                    // Check for duplicate Customer Name or Phone
                    string checkQuery = "SELECT COUNT(*) FROM Customers WHERE CustomerName = @CustomerName OR Phone = @Phone";
                    cmd = new SqlCommand(checkQuery, con);
                    cmd.Parameters.AddWithValue("@CustomerName", txtCustomername.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtphone.Text);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Customer Name or Phone Number already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Insert new customer without specifying the CustomerId (auto-increment)
                    string insertQuery = "INSERT INTO Customers (CustomerName, CustomerType, Phone, Gender, Dor) " +
                                         "VALUES (@CustomerName, @CustomerType, @Phone, @Gender, @Dor)";

                    cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@CustomerName", txtCustomername.Text);
                    cmd.Parameters.AddWithValue("@CustomerType", cmbcustomertype.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtphone.Text);
                    cmd.Parameters.AddWithValue("@Gender", cmbGender.Text);
                    cmd.Parameters.AddWithValue("@Dor", dtpDor.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnAdd.Text == "Update")
                {
                    // Update existing customer
                    string updateQuery = "UPDATE Customers SET CustomerName = @CustomerName, CustomerType = @CustomerType, " +
                                         "Phone = @Phone, Gender = @Gender, Dor = @Dor WHERE CustomerId = @CustomerId";

                    cmd = new SqlCommand(updateQuery, con);
                    cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
                    cmd.Parameters.AddWithValue("@CustomerName", txtCustomername.Text);
                    cmd.Parameters.AddWithValue("@CustomerType", cmbcustomertype.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtphone.Text);
                    cmd.Parameters.AddWithValue("@Gender", cmbGender.Text);
                    cmd.Parameters.AddWithValue("@Dor", dtpDor.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                FillCustomersData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void Dgvcustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvcustomers.Rows[e.RowIndex];
                CustomerId = Convert.ToInt32(row.Cells["CustomerId"].Value);
                txtCustomername.Text = row.Cells["CustomerName"].Value.ToString();
                cmbcustomertype.Text = row.Cells["CustomerType"].Value.ToString();
                txtphone.Text = row.Cells["Phone"].Value.ToString();
                cmbGender.Text = row.Cells["Gender"].Value.ToString();
                dtpDor.Value = Convert.ToDateTime(row.Cells["Dor"].Value);

                btnAdd.Text = "Update";
            }
        }

        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string searchQuery = "SELECT CustomerId, CustomerName, Phone, CustomerType, Gender, Dor FROM Customers " +
                                     "WHERE CustomerName LIKE @SearchText OR Phone LIKE @SearchText OR CustomerType LIKE @SearchText";

                cmd = new SqlCommand(searchQuery, con);
                cmd.Parameters.AddWithValue("@SearchText", "%" + txtsearch.Text + "%");

                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvcustomers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void Btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cmbcustomertype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Butclose_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void Btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure a customer is selected
                if (CustomerId == 0)
                {
                    MessageBox.Show("Please select a customer to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirm deletion
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    // Delete the selected customer
                    string deleteQuery = "DELETE FROM Customers WHERE CustomerId = @CustomerId";
                    cmd = new SqlCommand(deleteQuery, con);
                    cmd.Parameters.AddWithValue("@CustomerId", CustomerId);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the customer list and clear the form
                    FillCustomersData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

    }
}
