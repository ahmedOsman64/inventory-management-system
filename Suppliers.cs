using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IMS
{
    public partial class Suppliers : Form
    {
        DBConnection objcon = new DBConnection();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd;
        SqlDataAdapter da;
        private int SupplierId;

        public Suppliers()
        {
            InitializeComponent();
            con = objcon.GetConnection(); // Ensure this method returns a valid SqlConnection object.
        }

        private void ClearFields()
        {
            txtSupplierName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            dtpDateRegister.Value = DateTime.Today;
            btnAdd.Text = "Save";
            SupplierId = 0;
        }

        private void FillSuppliersData()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string query = "SELECT SupplierID, SupplierName, PhoneNumber, Email, Address, CreatedDate FROM Suppliers";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvSuppliers.DataSource = dt;

                // Optionally format column headers
                dgvSuppliers.Columns["SupplierID"].HeaderText = "Supplier ID";
                dgvSuppliers.Columns["SupplierName"].HeaderText = "Supplier Name";
                dgvSuppliers.Columns["PhoneNumber"].HeaderText = "Phone Number";
                dgvSuppliers.Columns["Email"].HeaderText = "Email";
                dgvSuppliers.Columns["Address"].HeaderText = "Address";
                dgvSuppliers.Columns["CreatedDate"].HeaderText = "Created Date";
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
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Enter Supplier Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text) || !long.TryParse(txtPhone.Text, out _))
            {
                MessageBox.Show("Enter a valid Phone Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Enter Email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void Suppliers_Load(object sender, EventArgs e)
        {
            FillSuppliersData();
            ClearFields();
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
                    // Check for duplicate Supplier Name or Email
                    string checkQuery = "SELECT COUNT(*) FROM Suppliers WHERE SupplierName = @SupplierName OR Email = @Email";
                    cmd = new SqlCommand(checkQuery, con);
                    cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Supplier Name or Email already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Insert new supplier (no need to include SupplierID since it's auto-incremented)
                    string insertQuery = "INSERT INTO Suppliers (SupplierName, PhoneNumber, Email, Address, CreatedDate) " +
                                         "VALUES (@SupplierName, @PhoneNumber, @Email, @Address, @CreatedDate)";

                    cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@CreatedDate", dtpDateRegister.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Supplier added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnAdd.Text == "Update")
                {
                    // Update existing supplier
                    string updateQuery = "UPDATE Suppliers SET SupplierName = @SupplierName, PhoneNumber = @PhoneNumber, Email = @Email, " +
                                         "Address = @Address, CreatedDate = @CreatedDate WHERE SupplierID = @SupplierID";

                    cmd = new SqlCommand(updateQuery, con);
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierId);
                    cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@CreatedDate", dtpDateRegister.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Supplier updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                FillSuppliersData();
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

        private void DgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSuppliers.Rows[e.RowIndex];
                //SupplierId = Convert.ToInt32(row.Cells["SupplierID"].Value);
                txtSupplierName.Text = row.Cells["SupplierName"].Value.ToString();
                txtPhone.Text = row.Cells["PhoneNumber"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                dtpDateRegister.Value = Convert.ToDateTime(row.Cells["CreatedDate"].Value);

                btnAdd.Text = "Update";
            }
        }

        private void Txtsearch_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string searchQuery = "SELECT SupplierID, SupplierName, PhoneNumber, Email, Address, CreatedDate FROM Suppliers " +
                                     "WHERE SupplierName LIKE @SearchText OR PhoneNumber LIKE @SearchText OR Email LIKE @SearchText";

                cmd = new SqlCommand(searchQuery, con);
                cmd.Parameters.AddWithValue("@SearchText", "%" + txtsearch.Text + "%");

                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvSuppliers.DataSource = dt;
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
    }
}
