using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace IMS
{
    public partial class AddProducts : Form
    {
        private readonly DBConnection objcon = new DBConnection();
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter da;

        public AddProducts()
        {
            InitializeComponent();
        }

        private void ClearFields()
        {
            cmbProductName.SelectedIndex = 0;
            txtQuantity.Clear();
            txtUnitPrice.Clear();
            dtpAddedDate.Value = DateTime.Today;
            txtUnitPrice.ReadOnly = true;
        }

        private void FillProductData()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query = "SELECT AddProducts.AddProductId, Products.ProductName, AddProducts.QuantityAdded, AddProducts.AddedDate " +
                               "FROM AddProducts " +
                               "INNER JOIN Products ON AddProducts.ProductID = Products.ProductID " +
                               "ORDER BY AddProducts.AddedDate DESC;";

                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvaddprducts.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Populate product names in ComboBox
        private void FillProductNames()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query = "SELECT 0 AS ProductID, '--- Select Product ---' AS ProductName " +
                               "UNION " +
                               "SELECT ProductID, ProductName FROM Products;";

                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbProductName.DataSource = dt;
                cmbProductName.DisplayMember = "ProductName";
                cmbProductName.ValueMember = "ProductID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update UnitPrice when a product is selected
        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProductName.SelectedIndex > 0)
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT UnitPrice FROM Products WHERE ProductID = @ProductID;";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ProductID", cmbProductName.SelectedValue);

                    object result = cmd.ExecuteScalar();
                    txtUnitPrice.Text = result != null ? result.ToString() : "0.00";
                }
                else
                {
                    txtUnitPrice.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Form Load 
        private void AddProducts_Load(object sender, EventArgs e)
        {
            try
            {
                con = objcon.GetConnection();
                FillProductData();
                FillProductNames();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validations
                if (cmbProductName.SelectedIndex == 0)
                {
                    MessageBox.Show("Please select a product.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out int quantity))
                {
                    MessageBox.Show("Please enter a valid numeric quantity.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd = new SqlCommand("SELECT ISNULL(MAX(AddProductID), 0) + 1 FROM AddProducts;", con);
                int addProductId = Convert.ToInt32(cmd.ExecuteScalar());

                // Begin a transaction to ensure atomicity
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        // Insert into AddProducts
                        string insertQuery = "INSERT INTO AddProducts (AddProductId, ProductID, QuantityAdded, AddedDate) " +
                                             "VALUES (@AddProductId, @ProductID, @QuantityAdded, @AddedDate);";

                        cmd = new SqlCommand(insertQuery, con, transaction);
                        cmd.Parameters.AddWithValue("@AddProductId", addProductId);
                        cmd.Parameters.AddWithValue("@ProductID", cmbProductName.SelectedValue);
                        cmd.Parameters.AddWithValue("@QuantityAdded", quantity);
                        cmd.Parameters.AddWithValue("@AddedDate", dtpAddedDate.Value);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            // Update the quantity in the Products table
                            string updateQuery = "UPDATE Products SET Quantity = Quantity + @Quantity WHERE ProductID = @ProductID;";
                            cmd = new SqlCommand(updateQuery, con, transaction);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@ProductID", cmbProductName.SelectedValue);

                            cmd.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Product added successfully.", "IMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillProductData(); // Refresh data grid
                            ClearFields();
                        }
                        else
                        {
                            throw new Exception("Failed to insert into AddProducts.");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"An error occurred: {ex.Message}", "IMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        // Close button
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
