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
using System.Configuration;

namespace QL_ThuVien
{
    public partial class DangNhap : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        DataSet ds_QLTV = new DataSet();

        public DangNhap()
        {
            InitializeComponent();
        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có chắc là muốn thoát ?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                string Check_DN = "select count(*) from ACCOUNT where TenDangNhap = N'" + txt_TenDangNhap.Text + "' and MatKhau = N'" + txt_MatKhau.Text + "'";
                SqlDataAdapter da_DN = new SqlDataAdapter(Check_DN, conn);
                da_DN.Fill(ds_QLTV, "DN");
                if (ds_QLTV.Tables["DN"].Rows[0][0].ToString() == "1")
                {
                    if (txt_TenDangNhap.Text == "Admin")
                    {
                        var form = Application.OpenForms["Admin"];
                        if (form != null)
                        {
                            this.Hide();
                            form.Show();
                        }
                        else
                        {
                            Admin admin = new Admin(txt_TenDangNhap.Text);
                            admin.RefToForm1 = this;
                            this.Visible = false;
                            admin.Show();
                        }
                    }
                    else
                    {
                        var form = Application.OpenForms["User"];
                        if (form != null)
                        {
                            this.Hide();
                            form.Show();
                        }
                        else
                        {
                            NhanVien user = new NhanVien(txt_TenDangNhap.Text);
                            user.RefToForm1 = this;
                            this.Visible = false;
                            user.Show();
                        }
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Đăng nhập thất bại");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Đăng nhập thất bại");
            }
        }
    }
}