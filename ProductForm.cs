using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace IMS
{
    public partial class ProductForm : Form
    {
        DBConnection objcon = new DBConnection();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd;
        SqlDataAdapter da;
        int ProductID;

        public ProductForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            try
            {
                con = objcon.GetConnection();
                LoadCategories();
                LoadSuppliers();
                FillProductsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                // Query to fetch categories and add a default entry
                string query = "SELECT '--- Select Category ---' AS CategoryName " +
                               "UNION " +
                               "SELECT DISTINCT CategoryName FROM Category";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the ComboBox to the data source
                cmbCategory.DataSource = dt;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryName";

                // Set the default selected index to the first item
                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                // Query to fetch suppliers and add a default entry
                string query = "SELECT 0 AS SupplierID, '--- Select Supplier ---' AS SupplierName " +
                               "UNION " +
                               "SELECT SupplierID, SupplierName FROM Suppliers";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the ComboBox to the data source
                cmbSupplier.DataSource = dt;
                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "SupplierID";

                // Set the default selected index to the first item
                cmbSupplier.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void FillProductsData()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                // Query to fetch product details
                string query = @"SELECT p.ProductID, p.CategoryName, p.ProductName, p.Quantity, 
                                p.UnitPrice, 
                                CASE 
                                    WHEN p.Quantity = 0 THEN 'Empty' 
                                    ELSE p.Status 
                                END AS Status, 
                                p.DateAdded, 
                                s.SupplierName 
                         FROM Products p
                         LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID";

                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvProducts.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (btnAdd.Text == "Add")
                {
                    // Logic for adding a new product (unchanged)
                    if (string.IsNullOrWhiteSpace(txtProductName.Text))
                    {
                        MessageBox.Show("Enter Product Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtProductName.Focus();
                        return;
                    }

                    if (cmbCategory.SelectedIndex == -1 || cmbCategory.SelectedIndex == 0)
                    {
                        MessageBox.Show("Select a Category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbCategory.Focus();
                        return;
                    }

                    if (cmbSupplier.SelectedIndex == -1 || cmbSupplier.SelectedIndex == 0)
                    {
                        MessageBox.Show("Select a Supplier", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbSupplier.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out int quantity))
                    {
                        MessageBox.Show("Enter a valid Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtQuantity.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtUnitPrice.Text) || !decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice))
                    {
                        MessageBox.Show("Enter a valid Unit Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUnitPrice.Focus();
                        return;
                    }

                    cmd = new SqlCommand(@"INSERT INTO Products (CategoryName, ProductName, Quantity, UnitPrice, Status, DateAdded, SupplierID) 
                                   VALUES (@CategoryName, @ProductName, @Quantity, @UnitPrice, @Status, @DateAdded, @SupplierID)", con);

                    cmd.Parameters.AddWithValue("@CategoryName", cmbCategory.Text);
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@SupplierID", cmbSupplier.SelectedValue);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillProductsData();
                        Clear(); // Clears all textboxes and combo boxes after successful addition
                    }
                }
                else if (btnAdd.Text == "Update")
                {
                    // New logic: Only update the UnitPrice
                    if (string.IsNullOrWhiteSpace(txtUnitPrice.Text) || !decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice))
                    {
                        MessageBox.Show("Enter a valid Unit Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUnitPrice.Focus();
                        return;
                    }

                    cmd = new SqlCommand(@"UPDATE Products 
                                   SET UnitPrice = @UnitPrice 
                                   WHERE ProductID = @ProductID", con);

                    cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Product unit price updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillProductsData();
                        Clear(); // Clears all textboxes and combo boxes after successful update
                    }
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProductID == 0)
                {
                    MessageBox.Show("Please select a product to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Confirm deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete this product?",
                                                      "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string deleteQuery = "DELETE FROM Products WHERE ProductID = @ProductID";
                    cmd = new SqlCommand(deleteQuery, con);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillProductsData(); // Refresh the product list
                        Clear();            // Reset the form inputs
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the product. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Database error: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void CmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSupplier.SelectedIndex != -1)
                {
                    string selectedSupplier = cmbSupplier.Text;
                    //MessageBox.Show($"Supplier '{selectedSupplier}' selected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Additional logic based on Supplier selection can be added here.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in processing supplier selection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Clear()
        {
            txtProductName.Clear();
            txtQuantity.Clear();
            txtUnitPrice.Clear();
            cmbCategory.SelectedIndex = -1;
            cmbSupplier.SelectedIndex = -1;
            btnAdd.Text = "Add";
            ProductID = 0;
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
                ProductID = Convert.ToInt32(row.Cells["ProductID"].Value);
                cmbCategory.Text = row.Cells["CategoryName"].Value.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
                txtUnitPrice.Text = row.Cells["UnitPrice"].Value.ToString();
                cmbSupplier.Text = row.Cells["SupplierName"].Value.ToString();
                btnAdd.Text = "Update";
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string searchQuery = txtsearch.Text.Trim();
                string query = @"SELECT p.ProductID, p.CategoryName, p.ProductName, p.Quantity, 
                                p.UnitPrice, p.Status, p.DateAdded, s.SupplierName 
                         FROM Products p
                         LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID
                         WHERE p.ProductName LIKE @Search OR p.CategoryName LIKE @Search";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Search", "%" + searchQuery + "%");
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvProducts.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while searching: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void Butclose_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void Btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}