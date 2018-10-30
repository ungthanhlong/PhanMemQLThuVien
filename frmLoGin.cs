using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyThuVien
{
    public partial class frmLoGin : Form
    {
        public frmLoGin()
        {
            InitializeComponent();
        }
        private bool Login(string user, string pass)
        {
            string cnStr = @"Data Source=UngThanhLong\SQLEXPRESS;Initial Catalog=QLTV;Integrated Security=True";
            SqlConnection cn = new SqlConnection(cnStr);
            cn.Open();
            string sql = "SELECT COUNT(UserName) FROM DangNhap  WHERE UserName = '" + user + "' AND PassWord = '" + pass + "'";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = sql;
            int count = (int)cmd.ExecuteScalar();
            if (count == 1)
                return true;
            else return false;
        }
        private void btnLoGin_Click(object sender, EventArgs e)
        {
            string user = txtUserName.Text;
            string pass = txtPassWord.Text;
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Thong tin khong đủ", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (Login(user, pass) == true)
                {
                    frmTable f = new frmTable();
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                }
                else
                {
                    DialogResult result = MessageBox.Show("User or pass khong đúng", "Login", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Cancel)
                        Application.Exit();
                    else
                    {
                        txtUserName.Clear();
                        txtPassWord.Clear();
                        txtUserName.Focus();
                    }
                }
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void frmLoGin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát chương trình", "Thông Báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
