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
    public partial class Category : Form
    
        {
            DBConnection objcon = new DBConnection();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd;
            SqlDataAdapter da;
            int CategoryId;

        void clear()
        {
            txtcategoryName.Clear();
            btnSave.Text = "Save";
        }

        public void FillCategoryData()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                cmd = new SqlCommand("SELECT CategoryId, CategoryName FROM Category", con);
                da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvCategory.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Category()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (txtcategoryName.Text == "")
                {
                    MessageBox.Show("Enter Category Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtcategoryName.Focus();
                    return;
                }

                if (btnSave.Text == "Save")
                {
                    cmd = new SqlCommand("SELECT ISNULL(MAX(CategoryId), 0) + 1 FROM Category", con);
                    int nextCategoryId = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand("SELECT COUNT(*) FROM Category WHERE CategoryName = @CategoryName", con);
                    cmd.Parameters.AddWithValue("@CategoryName", txtcategoryName.Text);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Category Name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    cmd = new SqlCommand("INSERT INTO Category (CategoryId, CategoryName) VALUES (@CategoryId, @CategoryName)", con);
                    cmd.Parameters.AddWithValue("@CategoryId", nextCategoryId);
                    cmd.Parameters.AddWithValue("@CategoryName", txtcategoryName.Text);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Category has been saved successfully", "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillCategoryData();
                        clear();
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    cmd = new SqlCommand("UPDATE Category SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId", con);
                    cmd.Parameters.AddWithValue("@CategoryName", txtcategoryName.Text);
                    cmd.Parameters.AddWithValue("@CategoryId", CategoryId);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Category has been updated successfully", "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillCategoryData();
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Category_Load(object sender, EventArgs e)
        {

            try
            {
                con = objcon.GetConnection();
                FillCategoryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvCategory_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvCategory.CurrentRow != null)
                {
                    CategoryId = Convert.ToInt32(dgvCategory.CurrentRow.Cells[0].Value);
                    txtcategoryName.Text = dgvCategory.CurrentRow.Cells[1].Value?.ToString();

                    btnSave.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btnclose_Click(object sender, EventArgs e)
        {

            this.Close();            
        }


        private void Btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure a category is selected
                if (dgvCategory.CurrentRow == null || dgvCategory.CurrentRow.Cells[0].Value == null)
                {
                    MessageBox.Show("Please select a valid category to delete.", "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the selected CategoryId
                if (!int.TryParse(dgvCategory.CurrentRow.Cells[0].Value.ToString(), out int selectedCategoryId))
                {
                    MessageBox.Show("Invalid CategoryId. Please select a valid category.", "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the selected CategoryName
                string selectedCategoryName = dgvCategory.CurrentRow.Cells[1].Value?.ToString();

                // Confirm deletion
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete the category '{selectedCategoryName}'?",
                    "INVENTORY MANAGEMENT SYSTEM",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Ensure database connection is open
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    // Delete the selected category
                    cmd = new SqlCommand("DELETE FROM Category WHERE CategoryId = @CategoryId", con);
                    cmd.Parameters.AddWithValue("@CategoryId", selectedCategoryId);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show($"Category '{selectedCategoryName}' has been deleted successfully.", "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Re-sequence the CategoryId
                        ReorderCategoryIds();

                        // Refresh the data
                        FillCategoryData();
                        clear();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to delete the category '{selectedCategoryName}'. Please try again.", "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReorderCategoryIds()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                cmd = new SqlCommand(@"
            WITH OrderedCategories AS (
                SELECT CategoryId, ROW_NUMBER() OVER (ORDER BY CategoryId) AS NewId
                FROM Category
            )
            UPDATE Category
            SET CategoryId = oc.NewId
            FROM Category c
            INNER JOIN OrderedCategories oc
            ON c.CategoryId = oc.CategoryId;", con);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
