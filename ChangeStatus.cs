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
    public partial class ChangeStatus : Form
    {
        DBConnection objcon = new DBConnection();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd;
        SqlDataAdapter da;
        string Status;
        void filllistofUsername()
        {
            try
            {

                // Ensure that database con`nection is open
                if (con.State == ConnectionState.Closed)
                    con.Open();

                // exclude 
                cmd = new SqlCommand(" select 0 as UserId, '---- Select User Name----' as UserName from Users Union Select UserId, UserName from Users ", con);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbusernames.DataSource = dt;
                cmbusernames.DisplayMember = "Username";
                cmbusernames.ValueMember = "UserId";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public ChangeStatus()
        {
            InitializeComponent();
        }

        private void ChangeStatus_Load(object sender, EventArgs e)
        {
            try
            {
                con = objcon.GetConnection();
                filllistofUsername();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnChangeStatus_Click(object sender, EventArgs e)
        {
            try
            {

                // Validation: Ensure a user is selected
                if (cmbusernames.SelectedValue == null || (int)cmbusernames.SelectedValue == 0)
                {
                    MessageBox.Show(
                        "Please select a valid user.",
                        "INVENTORY MANAGEMENT SYSTEM",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Validation: Ensure a status option is selected
                if (!rbtnactive.Checked && !rbtnblock.Checked)
                {
                    MessageBox.Show(
                        "Please select a status (Active or Block).",
                        "INVENTORY MANAGEMENT SYSTEM",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }




                if (rbtnactive.Checked)
                    Status = rbtnactive.Text;
                if (rbtnblock.Checked)
                    Status = rbtnblock.Text;
                // Ensure that database con`nection is open
                if (con.State == ConnectionState.Closed)
                    con.Open();

                // Check current status of the user
                string currentStatus = string.Empty;
                cmd = new SqlCommand("SELECT Status FROM Users WHERE UserId = @UserId", con);
                cmd.Parameters.AddWithValue("@UserId", cmbusernames.SelectedValue);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    currentStatus = result.ToString();
                }

                // Validation: Check if the user is already in the selected status
                if (currentStatus.Equals(Status, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        $"The user is already {Status}.",
                        "INVENTORY MANAGEMENT SYSTEM",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }


                cmd = new SqlCommand("Update Users set Status='" + Status + "' where UserId= '" + cmbusernames.SelectedValue + "'     ", con);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("User Status Has Been Update Successfully", "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INVENTORY MANAGEMENT SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
