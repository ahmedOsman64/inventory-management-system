using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace IMS
{
    public partial class Sales : Form
    {
        private readonly DBConnection objcon = new DBConnection();
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter da;

        public Sales()
        {
            InitializeComponent();
            con = objcon.GetConnection(); // Get the SQL connection from DBConnection class
            CmbProductName.SelectedIndexChanged += CmbProductName_SelectedIndexChanged;
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            try
            {
                FillSalesData();
                FillProductData();
                FillProductNames();

                txtUnitPrice.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillSalesData()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string query = @"
                SELECT 
                    S.SaleID, 
                    P.ProductName, 
                    S.Quantity, 
                    P.UnitPrice, 
                    S.TotalAmount, 
                    S.SaleDate
                FROM Sales S
                INNER JOIN Products P ON S.ProductID = P.ProductID";

                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvSales.DataSource = dt;

                dgvSales.Columns["SaleID"].HeaderText = "Sale ID";
                dgvSales.Columns["ProductName"].HeaderText = "Product Name";
                dgvSales.Columns["Quantity"].HeaderText = "Quantity";
                dgvSales.Columns["UnitPrice"].HeaderText = "Unit Price";
                dgvSales.Columns["TotalAmount"].HeaderText = "Total Amount";
                dgvSales.Columns["SaleDate"].HeaderText = "Sale Date";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching sales data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void FillProductData()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string query = "SELECT ProductID, ProductName, UnitPrice, Quantity FROM Products";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvProducts.DataSource = dt;

                dgvProducts.Columns["ProductID"].HeaderText = "Product ID";
                dgvProducts.Columns["ProductName"].HeaderText = "Product Name";
                dgvProducts.Columns["UnitPrice"].HeaderText = "Unit Price";
                dgvProducts.Columns["Quantity"].HeaderText = "Quantity";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching product data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void FillProductNames()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string query = "SELECT ProductID, ProductName FROM Products";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                CmbProductName.DataSource = dt;
                CmbProductName.DisplayMember = "ProductName";
                CmbProductName.ValueMember = "ProductID";
                CmbProductName.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load product names: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrWhiteSpace(CmbProductName.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) || unitPrice <= 0)
                {
                    MessageBox.Show("Unit price is invalid. Please select a valid product.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(CmbProductName.SelectedValue?.ToString(), out int productID))
                {
                    MessageBox.Show("Invalid product selected.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check available product quantity
                int availableQuantity = GetAvailableQuantity(productID);
                if (quantity > availableQuantity)
                {
                    MessageBox.Show($"The entered quantity exceeds available stock. Available quantity: {availableQuantity}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal totalAmount = quantity * unitPrice;

                string insertQuery = @"
                INSERT INTO Sales (ProductID, Quantity, TotalAmount, SaleDate) 
                VALUES (@ProductID, @Quantity, @TotalAmount, @SaleDate)";
                string updateQuery = @"
                UPDATE Products 
                SET Quantity = Quantity - @Quantity 
                WHERE ProductID = @ProductID";

                if (con.State == ConnectionState.Closed)
                    con.Open();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        // Insert the sales record
                        cmd = new SqlCommand(insertQuery, con, transaction);
                        cmd.Parameters.AddWithValue("@ProductID", productID);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        cmd.Parameters.AddWithValue("@SaleDate", DateTime.Now);
                        cmd.ExecuteNonQuery();

                        // Update the product quantity
                        cmd = new SqlCommand(updateQuery, con, transaction);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@ProductID", productID);
                        cmd.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Sales record saved successfully and product quantity updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        FillSalesData();
                        FillProductData();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error saving sales data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private int GetAvailableQuantity(int productID)
        {
            try
            {
                string query = "SELECT Quantity FROM Products WHERE ProductID = @ProductID";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductID", productID);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                object result = cmd.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching available quantity: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void CmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CmbProductName.SelectedIndex != -1 && CmbProductName.SelectedValue != null)
                {
                    if (int.TryParse(CmbProductName.SelectedValue.ToString(), out int productID))
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        string query = "SELECT UnitPrice FROM Products WHERE ProductID = @ProductID";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ProductID", productID);

                        object result = cmd.ExecuteScalar();

                        txtUnitPrice.Text = result != null ? result.ToString() : "0.00";
                    }
                    else
                    {
                        txtUnitPrice.Text = "0.00";
                    }
                }
                else
                {
                    txtUnitPrice.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching unit price: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void Btnprint_Click(object sender, EventArgs e)
        {
        }



        private void Btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Lbltotal_Click(object sender, EventArgs e)
        {

        }
    }
}
