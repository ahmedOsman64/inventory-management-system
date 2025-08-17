using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IMS
{
    public partial class Users : Form
    {
        DBConnection objcon = new DBConnection();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd;
        SqlDataAdapter da;
        int UserId;

        void clear()
        {
            txtfullname.Clear();
            txtPhoneNomber.Clear();
            txtemail.Clear();
            txtusername.Clear();
            txtpassword.Clear();
            txtconfirm.Clear();
            dtpdate.Text = System.DateTime.Today.ToShortDateString();
            txtusername.Enabled = true;
            btnAction.Text = "Save";
        }

        public void FillUsersData()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                cmd = new SqlCommand("SELECT UserId, FullName, Phone, Email, UserName, Password, ConfirmPassword, Status, DataRegister FROM Users", con);

                da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUsers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Users()
        {
            InitializeComponent();
        }

        private void BtnAction_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                // =============== Validations ================
                if (txtfullname.Text == "")
                {
                    MessageBox.Show("Enter Full Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtfullname.Focus();
                    return;
                }

                if (txtPhoneNomber.Text == "")
                {
                    MessageBox.Show("Enter Phone Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhoneNomber.Focus();
                    return;
                }

                if (txtemail.Text == "")
                {
                    MessageBox.Show("Enter Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtemail.Focus();
                    return;
                }

                if (txtpassword.Text == "")
                {
                    MessageBox.Show("Enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtpassword.Focus();
                    return;
                }

                if (txtconfirm.Text == "")
                {
                    MessageBox.Show("Confirm Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtconfirm.Focus();
                    return;
                }

                if (txtpassword.Text != txtconfirm.Text)
                {
                    MessageBox.Show("Password and Confirm Password do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtpassword.Clear();
                    txtconfirm.Clear();
                    txtpassword.Focus();
                    return;
                }

                if (!int.TryParse(txtPhoneNomber.Text, out int phoneNumber))
                {
                    MessageBox.Show("Enter a valid Phone Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhoneNomber.Clear();
                    txtPhoneNomber.Focus();
                    return;
                }

                if (btnAction.Text == "Save")
                {
                    cmd = new SqlCommand("SELECT ISNULL(MAX(UserId), 0) + 1 FROM Users", con);
                    int nextUserId = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Phone = @Phone OR Email = @Email OR UserName = @UserName", con);
                    cmd.Parameters.AddWithValue("@Phone", txtPhoneNomber.Text);
                    cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                    cmd.Parameters.AddWithValue("@UserName", txtusername.Text);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("User with the same Phone, Email, or Username already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    cmd = new SqlCommand("INSERT INTO Users (UserId, FullName, Phone, Email, UserName, Password, ConfirmPassword, Status, DataRegister) " +
                                         "VALUES (@UserId, @FullName, @Phone, @Email, @UserName, @Password, @ConfirmPassword, @Status, @DataRegister)", con);
                    cmd.Parameters.AddWithValue("@UserId", nextUserId);
                    cmd.Parameters.AddWithValue("@FullName", txtfullname.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhoneNomber.Text);
                    cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                    cmd.Parameters.AddWithValue("@UserName", txtusername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtpassword.Text);
                    cmd.Parameters.AddWithValue("@ConfirmPassword", txtconfirm.Text);
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.Parameters.AddWithValue("@DataRegister", dtpdate.Value);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("User Information Has Been Saved Successfully", "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillUsersData();
                        clear();
                    }
                }
                else if (btnAction.Text == "Update")
                {
                    cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE (Phone = @Phone OR Email = @Email) AND UserId != @UserId", con);
                    cmd.Parameters.AddWithValue("@Phone", txtPhoneNomber.Text);
                    cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Phone or Email already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update user information (excluding Photo)
                    cmd = new SqlCommand("UPDATE Users SET FullName = @FullName, Phone = @Phone, Email = @Email, Password = @Password, ConfirmPassword = @ConfirmPassword, DataRegister = @DataRegister WHERE UserId = @UserId", con);
                    cmd.Parameters.AddWithValue("@FullName", txtfullname.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhoneNomber.Text);
                    cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                    cmd.Parameters.AddWithValue("@Password", txtpassword.Text);
                    cmd.Parameters.AddWithValue("@ConfirmPassword", txtconfirm.Text);
                    cmd.Parameters.AddWithValue("@DataRegister", dtpdate.Value);
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("User Information Has Been Updated Successfully", "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillUsersData();
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            try
            {
                con = objcon.GetConnection();
                FillUsersData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvUsers_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvUsers.CurrentRow != null)
                {
                    UserId = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value);

                    txtfullname.Text = dgvUsers.CurrentRow.Cells[1].Value?.ToString();
                    txtPhoneNomber.Text = dgvUsers.CurrentRow.Cells[2].Value?.ToString();
                    txtemail.Text = dgvUsers.CurrentRow.Cells[3].Value?.ToString();
                    txtusername.Text = dgvUsers.CurrentRow.Cells[4].Value?.ToString();
                    txtpassword.Text = dgvUsers.CurrentRow.Cells[5].Value?.ToString();
                    txtconfirm.Text = dgvUsers.CurrentRow.Cells[6].Value?.ToString();

                    string dateValue = dgvUsers.CurrentRow.Cells[7].Value?.ToString();

                    if (DateTime.TryParse(dateValue, out DateTime parsedDate))
                    {
                        dtpdate.Value = parsedDate;
                    }
                    else
                    {
                        dtpdate.Value = DateTime.Today;
                    }

                    btnAction.Text = "Update";
                    txtusername.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsers.CurrentRow == null || dgvUsers.CurrentRow.Cells[0].Value == null)
                {
                    MessageBox.Show("Please select a user to delete.", "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int selectedUserId = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value);
                string selectedUserName = dgvUsers.CurrentRow.Cells[1].Value?.ToString();

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete the user '{selectedUserName}'?",
                    "INVENTORY MANAGEMENT SYSTEM",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    // Delete user
                    cmd = new SqlCommand("DELETE FROM Users WHERE UserId = @UserId", con);
                    cmd.Parameters.AddWithValue("@UserId", selectedUserId);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show($"User '{selectedUserName}' has been deleted successfully.", "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillUsersData();
                        clear();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to delete the user '{selectedUserName}'. Please try again.", "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string searchQuery = "SELECT UserId, FullName, Phone, Email, UserName, Status, DataRegister FROM Users " +
                                     "WHERE FullName LIKE @SearchText OR Phone LIKE @SearchText OR Email LIKE @SearchText OR UserName LIKE @SearchText";

                cmd = new SqlCommand(searchQuery, con);
                cmd.Parameters.AddWithValue("@SearchText", "%" + txtsearch.Text + "%");

                da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUsers.DataSource = dt; // Update the DataGridView with the filtered data
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

        private void Btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btnfull_Click(object sender, EventArgs e)
        {
        }

        private void DgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
