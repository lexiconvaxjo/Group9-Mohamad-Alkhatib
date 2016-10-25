using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace POS
{
    public partial class frmLogin : Form
    {
        ConnectionString cs = new ConnectionString();
        SqlDataReader rdr = null;
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            try
            {
                SqlConnection myConnection = default(SqlConnection);
                myConnection = new SqlConnection(cs.DBConn);

                SqlCommand myCommand = default(SqlCommand);

                myCommand = new SqlCommand("SELECT Username,password FROM Registration WHERE Username = @username AND password = @UserPassword", myConnection);
                SqlParameter uName = new SqlParameter("@username", SqlDbType.VarChar);
                SqlParameter uPassword = new SqlParameter("@UserPassword", SqlDbType.VarChar);
                uName.Value = txtUserName.Text;
                uPassword.Value = txtPassword.Text;
                myCommand.Parameters.Add(uName);
                myCommand.Parameters.Add(uPassword);

                myCommand.Connection.Open();

                SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

                if (myReader.Read() == true)
                {
                    int i;
                    ProgressBar1.Visible = true;
                    ProgressBar1.Maximum = 5000;
                    ProgressBar1.Minimum = 0;
                    ProgressBar1.Value = 4;
                    ProgressBar1.Step = 1;

                    for (i = 0; i <= 5000; i++)
                    {
                        ProgressBar1.PerformStep();
                    }
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct = "select usertype from Registration where Username='" + txtUserName.Text + "' and Password='" + txtPassword.Text + "'";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        txtUserType.Text = (rdr.GetString(0));
                    }
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                       
                    if (txtUserType.Text.Trim()== "Admin")
                    {

                        this.Hide();
                        frmMainMenu frm = new frmMainMenu();
                        frm.masterEntryToolStripMenuItem.Enabled = true;
                        frm.usersToolStripMenuItem.Enabled = true;
                        frm.customerToolStripMenuItem1.Enabled = true;
                        frm.suppliersToolStripMenuItem.Enabled = true;
                        frm.productsToolStripMenuItem.Enabled = true;
                        frm.recordsToolStripMenuItem.Enabled = true;
                        frm.registrationToolStripMenuItem.Enabled = true;
                        frm.customerToolStripMenuItem.Enabled = true;
                        frm.supplierToolStripMenuItem.Enabled = true;
                        frm.productToolStripMenuItem.Enabled = true;
                        frm.stockToolStripMenuItem.Enabled = true;
                        frm.invoiceToolStripMenuItem.Enabled = true;
                        frm.Show();
                        frm.lblUser.Text = txtUserName.Text;
                        frm.lblUserType.Text = txtUserType.Text;
                    }
                    if (txtUserType.Text.Trim() == "Sales Person")
                    {
                        this.Hide();
                        frmMainMenu frm = new frmMainMenu();
                        frm.masterEntryToolStripMenuItem.Enabled = false;
                        frm.usersToolStripMenuItem.Enabled = false;
                        frm.customerToolStripMenuItem1.Enabled = true;
                        frm.suppliersToolStripMenuItem.Enabled = false;
                        frm.productsToolStripMenuItem.Enabled = false;
                        frm.recordsToolStripMenuItem.Enabled = false;
                        frm.registrationToolStripMenuItem.Enabled = false;
                        frm.customerToolStripMenuItem.Enabled = true;
                        frm.supplierToolStripMenuItem.Enabled = false;
                        frm.productToolStripMenuItem.Enabled = false;
                        frm.stockToolStripMenuItem.Enabled = false;
                        frm.invoiceToolStripMenuItem.Enabled = true;
                        frm.Show();
                        frm.lblUser.Text = txtUserName.Text;
                        frm.lblUserType.Text = txtUserType.Text;
                    }
                    if (txtUserType.Text.Trim() == "Store Manager")
                    {
                        this.Hide();
                        frmMainMenu frm = new frmMainMenu();
                        frm.masterEntryToolStripMenuItem.Enabled = false;
                        frm.usersToolStripMenuItem.Enabled = false;
                        frm.customerToolStripMenuItem1.Enabled = false;
                        frm.suppliersToolStripMenuItem.Enabled = false;
                        frm.productsToolStripMenuItem.Enabled = false;
                        frm.recordsToolStripMenuItem.Enabled = true;
                        frm.registrationToolStripMenuItem.Enabled = false;
                        frm.customerToolStripMenuItem.Enabled = false;
                        frm.supplierToolStripMenuItem.Enabled = false;
                        frm.productToolStripMenuItem.Enabled = true;
                        frm.stockToolStripMenuItem.Enabled = true;
                        frm.invoiceToolStripMenuItem.Enabled = false;
                        frm.Show();
                        frm.lblUser.Text = txtUserName.Text;
                        frm.lblUserType.Text = txtUserType.Text;
                    }
                }
                

                else
                {
                    MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();

                }
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Dispose();
                }

              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
            ProgressBar1.Visible = false;
            txtUserName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmChangePassword frm = new frmChangePassword();
            frm.Show();
            frm.txtUserName.Text = "";
            frm.txtNewPassword.Text = "";
            frm.txtOldPassword.Text = "";
            frm.txtConfirmPassword.Text = "";
        }

     
      
    }
}
