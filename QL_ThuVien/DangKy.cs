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
using System.Net.Mail;
using System.Runtime.ConstrainedExecution;
using System.Configuration;

namespace QL_ThuVien
{
    public partial class DangKy : Form
    {
        public Form RefToForm1 { get; set; }
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        DataSet ds_DG = new DataSet();
        ErrorProvider errorP = new ErrorProvider();
        ErrorProvider errorP1 = new ErrorProvider();
        ErrorProvider errorP2 = new ErrorProvider();
        DataColumn[] key = new DataColumn[1];
        public DangKy()
        {
            InitializeComponent();
            string Select = "select * from DOCGIA";
            SqlDataAdapter da_DG = new SqlDataAdapter(Select, conn);
            da_DG.Fill(ds_DG, "DOCGIA");
            //Tạo khoá chính
            key[0] = ds_DG.Tables["DOCGIA"].Columns[0];
            ds_DG.Tables["DOCGIA"].PrimaryKey = key;
        }
        private void DangKy_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        //Tạo ErrorProvider để hiển thị các lỗi khi chuyển focus
        private void txt_SDT_Leave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (txt_SDT.TextLength > 10 || txt_SDT.TextLength < 10)
                this.errorP2.SetError(ctr, "Số điện thoại không hợp lệ");
            else
                this.errorP2.Clear();

            DataRow dr = ds_DG.Tables["DOCGIA"].Rows.Find(txt_SDT.Text);
            if (dr != null)
                this.errorP1.SetError(ctr, "Số điện thoại đã tồn tại");
            else
                this.errorP1.Clear();
        }

        private void btn_DangKy_Click(object sender, EventArgs e)
        {
            //Dùng ErrorProvider để hiển thị lỗi khi chưa nhập đủ thông tin
            var boxes = Controls.OfType<TextBox>();
            foreach (var box in boxes)
            {
                if (string.IsNullOrWhiteSpace(box.Text))
                {
                    errorP.SetError(box, "Không được để trống");
                }
            }

            if (mtxt_NgaySinh.MaskCompleted == false)
                this.errorP.SetError(mtxt_NgaySinh, "Không được để trống");
            else
                this.errorP.Clear();

            if (radio_Nam.Checked == false && radio_Nu.Checked == false)
                this.errorP.SetError(radio_Nu, "Không được để trống");
            else
                this.errorP.Clear();
            //Điều chỉnh vị trí icon của ErrorProvider
            errorP.SetIconPadding(radio_Nu, 94);

            //Nhập không đúng yêu cầu thì trả về để nhập lại
            if (txt_SDT.Text == string.Empty || txt_DiaChi.Text == string.Empty || txt_HoTen.Text == string.Empty || mtxt_NgaySinh.MaskCompleted == false || (radio_Nam.Checked == false && radio_Nu.Checked == false))
                return;

            try
            {
                conn.Open();
                //Đếm số lượng đọc giả trong bảng DocGia
                string Count = "select count(*) from DOCGIA";
                SqlCommand cmd2 = new SqlCommand(Count, conn);
                int SoLuongDocGia = (int)cmd2.ExecuteScalar() + 1;
                conn.Close();
                string MDG = "DG" + SoLuongDocGia.ToString();
                //Xác định giới tính
                string GT = "";
                if (radio_Nam.Checked)
                    GT = "Nam";
                else
                    GT = "Nữ";

                //Ghi vào bảng DocGia
                conn.Open();
                DateTime date = Convert.ToDateTime(mtxt_NgaySinh.Text);
                string InsertDocGia = "insert into DOCGIA values ('" + MDG + "', N'" + txt_HoTen.Text + "', N'" + GT + "', '" + date.ToString("yyyy-MM-dd") + "', N'" + txt_DiaChi.Text + "', '" + txt_SDT.Text + "')";
                SqlCommand cmd3 = new SqlCommand(InsertDocGia, conn);
                cmd3.ExecuteNonQuery();
                conn.Close();

                //Đóng trang đăng kí sau khi đăng ký thành công
                MessageBox.Show("Đăng ký Thành công");
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng ký thất bại");
            }
        }

        private void DangKy_Load(object sender, EventArgs e)
        {
            txt_HoTen.Select();
            txt_HoTen.Focus();
        }
    }
}
