using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;
using static Guna.UI2.Native.WinApi;

namespace QL_ThuVien
{
    public partial class NhanVien : Form
    {
        public Form RefToForm1 { get; set; }
        private string _message;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        DataSet ds_Sach = new DataSet();

        public NhanVien()
        {
            InitializeComponent();
        }

        //Lấy tên đăng nhập bên form đăng nhập
        public NhanVien(string Message) : this()
        {
            _message = Message;
        }

        //Lấy tên đọc giả từ tên đăng nhập
        public string Get_TenNV(string TDN)
        {
            conn.Open();
            string ten = "";
            string select = "select HoTen from NHANVIEN where TenDangNhap = N'" + TDN + "'";
            SqlCommand cmd1 = new SqlCommand(select, conn);
            SqlDataReader rd = cmd1.ExecuteReader();

            if (rd.Read())
                ten = rd.GetString(0);
            conn.Close();
            return ten;
        }

        //Chỉnh vị trí chữ trên header của datagridview
        public void HeaderCell_Alignment(DataGridView dagrid, int SoCot)
        {
            for (int i = 0; i < SoCot; i++)
                dagrid.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        //Ẩn các panel
        public void Hide_Panel()
        {
            Panel_ThongTinTaiKhoan.Visible = false;
            Panel_MuonTraSach.Visible = false;
            Panel_ThuPhi.Visible = false;
        }

        public void load_DSSach()
        {
            string Sach = "select MaSach, TenSach, TenTacGia, TenTheLoai, TenNXB, NamXB, SoLuong from SACH S, TACGIA TG, NXB, THELOAI TL where S.MaTacGia = TG.MaTacGia and S.MaTheLoai = TL.MaTheLoai and S.MaNXB = NXB.MaNXB";
            SqlDataAdapter da_Sach = new SqlDataAdapter(Sach, conn);
            da_Sach.Fill(ds_Sach, "DSSACH");
            HeaderCell_Alignment(DataGridView_DSSach, 7);
            //Đếm số sách
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from SACH", conn);
            int count = (int)cmd.ExecuteScalar();
            for (int i = 0; i < count; i++)
                DataGridView_DSSach.Rows.Add(ds_Sach.Tables["DSSACH"].Rows[i][0], ds_Sach.Tables["DSSACH"].Rows[i][1], ds_Sach.Tables["DSSACH"].Rows[i][2], ds_Sach.Tables["DSSACH"].Rows[i][3], ds_Sach.Tables["DSSACH"].Rows[i][4], ds_Sach.Tables["DSSACH"].Rows[i][5], ds_Sach.Tables["DSSACH"].Rows[i][6]);
            conn.Close();
        }

        //Load các thông tin trên trang thông tin tài khoản
        public void load_TrangThongTinTaiKhoan()
        {
            txt_MaNV.Enabled = false;
            radioButton_Nam.Checked = false;
            radioButton_Nu.Checked = false;
            txt_TenDN.Text = _message;
            string ttcn = "select * from NHANVIEN where TenDangNhap = N'" + txt_TenDN.Text + "'";
            string tttk = "select * from ACCOUNT where TenDangNhap = N'" + txt_TenDN.Text + "'";
            //load thông tin cá nhân
            conn.Open();
            SqlCommand cmd1 = new SqlCommand(ttcn, conn);
            SqlDataReader rd1 = cmd1.ExecuteReader();
            if (rd1.Read())
            {
                txt_MaNV.Text = rd1["MaNV"].ToString();
                txt_HoTen.Text = rd1["HoTen"].ToString();
                if (rd1["GioiTinh"].ToString() == "Nam")
                    radioButton_Nam.Checked = true;
                else
                    radioButton_Nu.Checked = true;
                DateTime date = Convert.ToDateTime(rd1["NgaySinh"]);
                mtxt_NgaySinh.Text = date.ToString("dd/MM/yyyy");
                txt_DiaChi.Text = rd1["DiaChi"].ToString();
                txt_SDT.Text = rd1["SDT"].ToString();
            }
            conn.Close();
            //load thông tin tài khoản
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(tttk, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();
            if (rd2.Read())
                txt_Email.Text = rd2.GetString(2);
            conn.Close();
        }

        public void load_TrangMuonTraSach()
        {
            //load tìm kiếm
            DataSet ds = new DataSet();
            string selectTL = "Select * from THELOAI";
            string selectTG = "Select * from TACGIA";
            string selectNXB = "Select * from NXB";
            SqlDataAdapter da = new SqlDataAdapter(selectTL, conn);
            SqlDataAdapter da1 = new SqlDataAdapter(selectTG, conn);
            SqlDataAdapter da2 = new SqlDataAdapter(selectNXB, conn);
            da.Fill(ds, "THELOAI");
            da1.Fill(ds, "TACGIA");
            da2.Fill(ds, "NXB");

            DataRow inset_New = ds.Tables["THELOAI"].NewRow();
            inset_New["TenTheLoai"] = "All";
            inset_New["MaTheLoai"] = "' or 1 = 1 or S.MaTheLoai = '";
            ds.Tables["THELOAI"].Rows.InsertAt(inset_New, 0);
            cbb_TL.DataSource = ds.Tables["THELOAI"];
            cbb_TL.DisplayMember = "TenTheLoai";
            cbb_TL.ValueMember = "MaTheLoai";

            DataRow inset_New1 = ds.Tables["TACGIA"].NewRow();
            inset_New1["TenTacGia"] = "All";
            inset_New1["MaTacGia"] = "' or 1 = 1 or S.MaTacGia = '";
            ds.Tables["TACGIA"].Rows.InsertAt(inset_New1, 0);
            cbb_TG.DataSource = ds.Tables["TACGIA"];
            cbb_TG.DisplayMember = "TenTacGia";
            cbb_TG.ValueMember = "MaTacGia";

            DataRow inset_New2 = ds.Tables["NXB"].NewRow();
            inset_New2["TenNXB"] = "All";
            inset_New2["MaNXB"] = "' or 1 = 1 or S.MaNXB = '";
            ds.Tables["NXB"].Rows.InsertAt(inset_New2, 0);
            cbb_NXB.DataSource = ds.Tables["NXB"];
            cbb_NXB.DisplayMember = "TenNXB";
            cbb_NXB.ValueMember = "MaNXB";
            
            txt_TenSachTra.Enabled = mtxt_NgayMuon.Enabled = false;
        }

        public void load_TrangThuPhi()
        {
            conn.Open();
            string MaDG = "select MaDocGia from DOCGIA where MaDocGia in (select MaDocGia from MUONSACH)";
            SqlCommand cmd = new SqlCommand(MaDG, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
                cbb_MaDGThuPhi.Items.Add(rd.GetString(0));
            conn.Close();
        }

        private void User_Load(object sender, EventArgs e)
        {
            //Gán tên nhân viên
            label_Ten.Text = Get_TenNV(_message);

            //Cho mấy cái panel khác tàng hình ngoại trừ cái thông tin tài khoản
            Hide_Panel();
            Panel_ThongTinTaiKhoan.Visible = true;

            //disable vài thứ
            dateTimePicker_NamXB.Enabled = false;
            txt_DiaChi.Enabled = txt_Email.Enabled = txt_HoTen.Enabled = txt_SDT.Enabled = false;
            txt_TenDN.Enabled = mtxt_NgaySinh.Enabled = radioButton_Nam.Enabled = radioButton_Nu.Enabled = false;

            //Đổi Datetime picker format thành chỉ được chọn năm
            dateTimePicker_NamXB.Format = DateTimePickerFormat.Custom;
            dateTimePicker_NamXB.CustomFormat = "yyyy";
            dateTimePicker_NamXB.ShowUpDown = true;

            //Load dữ liệu từ database
            load_DSSach();
            load_TrangThongTinTaiKhoan();
            load_TrangMuonTraSach();
        }

        private void User_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btn_ThongTinTaiKhoan_Click(object sender, EventArgs e)
        {
            Hide_Panel();
            Panel_ThongTinTaiKhoan.Visible = true;
        }

        private void btn_MuonTraSach_Click(object sender, EventArgs e)
        {
            Hide_Panel();
            Panel_MuonTraSach.Visible = true;
            //load trả sách
            cbb_MaDG.Items.Clear();
            conn.Open();
            string MaDG = "select MaDocGia from DOCGIA";
            SqlCommand cmd = new SqlCommand(MaDG, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
                cbb_MaDG.Items.Add(rd.GetString(0));
            conn.Close();
        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.RefToForm1.Show();
        }

        private void cbb_MaSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            string select = "select NgayMuon, TenSach from MUONSACH, SACH where MUONSACH.MaSach = SACH.MaSach and MUONSACH.MaSach = '" + cbb_MaSach.Text + "' and MUONSACH.MaDocGia = '" + cbb_MaDG.Text + "' and NgayTra is null";
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                DateTime date = Convert.ToDateTime(rd["NgayMuon"]);
                mtxt_NgayMuon.Text = date.ToString("dd/MM/yyyy");
                txt_TenSachTra.Text = rd.GetString(1);
            }
            conn.Close();
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            ds_Sach.Tables["DSSACH"].Clear();
            string NamXB = "";
            if (cb_NamXB.Checked == false)
                NamXB = "'' or 1 = 1";
            else
                NamXB = dateTimePicker_NamXB.Text;

            string TimKiem = "select MaSach, TenSach, TenTacGia, TenTheLoai, TenNXB, NamXB, SoLuong from SACH S, TACGIA TG, NXB, THELOAI TL where S.MaTacGia = TG.MaTacGia and S.MaTheLoai = TL.MaTheLoai and S.MaNXB = NXB.MaNXB and (S.MaTheLoai = '" + cbb_TL.SelectedValue + "') and (S.MaTacGia = '" + cbb_TG.SelectedValue + "') and (S.MaNXB = '" + cbb_NXB.SelectedValue + "') and (S.NamXB = " + NamXB + ") and S.TenSach like (N'%" + txt_TenSach.Text + "%')";
            SqlDataAdapter da_Sach = new SqlDataAdapter(TimKiem, conn);
            da_Sach.Fill(ds_Sach, "DSSACH");
            HeaderCell_Alignment(DataGridView_DSSach, 7);

            conn.Open();
            //Đếm số sách
            SqlCommand cmd = new SqlCommand("select count(*) from SACH S where (S.MaTheLoai = '" + cbb_TL.SelectedValue + "') and (S.MaTacGia = '" + cbb_TG.SelectedValue + "') and (S.MaNXB = '" + cbb_NXB.SelectedValue + "') and (S.NamXB = " + NamXB + ") and S.TenSach like (N'%" + txt_TenSach.Text + "%')", conn);
            int count = (int)cmd.ExecuteScalar();
            DataGridView_DSSach.Rows.Clear();
            for (int i = 0; i < count; i++)
                DataGridView_DSSach.Rows.Add(ds_Sach.Tables["DSSACH"].Rows[i][0], ds_Sach.Tables["DSSACH"].Rows[i][1], ds_Sach.Tables["DSSACH"].Rows[i][2], ds_Sach.Tables["DSSACH"].Rows[i][3], ds_Sach.Tables["DSSACH"].Rows[i][4], ds_Sach.Tables["DSSACH"].Rows[i][5], ds_Sach.Tables["DSSACH"].Rows[i][6]);
            conn.Close();
        }

        private void btn_DangKi_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms["DangKy"];
            if (form != null)
                form.Show();
            else
            {
                DangKy DK = new DangKy();
                DK.Show();
            }
        }

        private void cbb_MaDG_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbb_MaSach.Items.Clear();
            conn.Open();
            string selectMaSach = "select MaSach from MUONSACH where NgayTra is null and MaDocGia = '" + cbb_MaDG.Text + "'";
            SqlCommand cmd = new SqlCommand(selectMaSach, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                cbb_MaSach.Items.Add(rd.GetString(0));
            }
            conn.Close();
        }

        private void cb_NamXB_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_NamXB.Checked == true)
                dateTimePicker_NamXB.Enabled = true;
            else
                dateTimePicker_NamXB.Enabled = false;
        }

        private void btn_Muon_Click(object sender, EventArgs e)
        {
            SqlDataAdapter Muon = new SqlDataAdapter("select * from MUONSACH", conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(Muon);
            Muon.Fill(ds_Sach, "MUONSACH");
            DateTime date = DateTime.Now;

            if (DataGridView_DSSach.SelectedRows.Count > 0 && cbb_MaDG.Text != string.Empty)
            {
                for (int i = 0; i < DataGridView_DSSach.SelectedRows.Count; i++)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select SoLuong from Sach where MaSach = '" + DataGridView_DSSach.SelectedRows[i].Cells[0] + "'", conn);
                    int SoLuong = 0;
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        SoLuong = (int)rd["SoLuong"];
                        if (SoLuong == 0)
                        {
                            MessageBox.Show("Mượn sách thất bại");
                            return;
                        }
                    }
                    conn.Close();
                    DataRow insert_row = ds_Sach.Tables["MUONSACH"].NewRow();
                    insert_row["MaDocGia"] = cbb_MaDG.Text;
                    insert_row["MaSach"] = DataGridView_DSSach.SelectedRows[i].Cells[0].Value.ToString();
                    insert_row["NgayMuon"] = date.ToString("dd/MM/yyyy");
                    ds_Sach.Tables["MUONSACH"].Rows.Add(insert_row);
                }
                Muon.Update(ds_Sach, "MUONSACH");
                MessageBox.Show("Mượn sách thành công");
                ds_Sach.Tables["MUONSACH"].Clear();
                cbb_MaDG.SelectedItem = null;
                cbb_MaSach.SelectedItem = null;
            }
            else
                MessageBox.Show("Mượn sách thất bại");

            DataGridView_DSSach.Rows.Clear();
            ds_Sach.Tables["DSSACH"].Clear();
            load_DSSach();
        }

        private void btn_GiaHan_Click(object sender, EventArgs e)
        {
            if (cbb_MaDG.Text != string.Empty && cbb_MaSach.Text != string.Empty)
            {
                SqlDataAdapter GiaHan = new SqlDataAdapter("select * from MUONSACH where MaDocGia = '" + cbb_MaDG.Text + "' and MaSach = '" + cbb_MaSach.Text + "'", conn);
                GiaHan.Fill(ds_Sach, "GiaHan");
                SqlCommandBuilder builder = new SqlCommandBuilder(GiaHan);
                int SoNgayTraTre = Math.Abs((Convert.ToDateTime(mtxt_NgayMuon.Text) - DateTime.Now).Days);
                if (SoNgayTraTre > 0)
                {
                    ds_Sach.Tables["GiaHan"].Rows[0]["GhiChu"] = "Gia hạn";
                    GiaHan.Update(ds_Sach, "GiaHan");
                    MessageBox.Show("Gia hạn Thành công");
                    ds_Sach.Tables["GiaHan"].Clear();
                    cbb_MaDG.SelectedIndex = -1;
                    cbb_MaSach.SelectedIndex = -1;
                }
            }
            else
                MessageBox.Show("Gia hạn thất bại");
        }

        private void btn_Tra_Click(object sender, EventArgs e)
        {
            if (cbb_MaDG.Text != string.Empty && cbb_MaSach.Text != string.Empty)
            {
                SqlDataAdapter Tra = new SqlDataAdapter("select * from MUONSACH where MaDocGia = '" + cbb_MaDG.Text + "' and MaSach = '" + cbb_MaSach.Text + "'", conn);
                Tra.Fill(ds_Sach, "TraSach");
                SqlCommandBuilder builder = new SqlCommandBuilder(Tra);
                int SoNgayTraTre = (Convert.ToDateTime(mtxt_NgayMuon.Text) - DateTime.Now).Days + 30;
                if (ds_Sach.Tables["TraSach"].Rows[0]["GhiChu"].ToString() != string.Empty)
                {
                    if (ds_Sach.Tables["TraSach"].Rows[0]["GhiChu"].ToString().Contains("hạn"))
                    {
                        if (SoNgayTraTre < 0)
                            ds_Sach.Tables["TraSach"].Rows[0]["GhiChu"] = ds_Sach.Tables["TraSach"].Rows[0]["GhiChu"].ToString() + ", trả trễ " + (Math.Abs(SoNgayTraTre) - 30) + " ngày";

                    }
                    else
                    {
                        if (SoNgayTraTre < 0)
                            ds_Sach.Tables["TraSach"].Rows[0]["GhiChu"] = ds_Sach.Tables["TraSach"].Rows[0]["GhiChu"].ToString() + ", trả trễ " + (Math.Abs(SoNgayTraTre) - 30) + " ngày";
                    }
                }
                else
                {
                    if (SoNgayTraTre < 0)
                        ds_Sach.Tables["TraSach"].Rows[0]["GhiChu"] = "Trả trễ " + Math.Abs(SoNgayTraTre) + " ngày";
                }

                if (checkBox_HuHai.Checked)
                {
                    if (ds_Sach.Tables["TraSach"].Rows[0]["GhiChu"].ToString() == string.Empty)
                        ds_Sach.Tables["TraSach"].Rows[0]["GhiChu"] = "Đền sách";
                    else
                        ds_Sach.Tables["TraSach"].Rows[0]["GhiChu"] = ds_Sach.Tables["TraSach"].Rows[0]["GhiChu"].ToString() + ", đền sách";
                }

                ds_Sach.Tables["TraSach"].Rows[0]["NgayTra"] = DateTime.Now.ToString("dd/MM/yyyy");
                Tra.Update(ds_Sach, "TraSach");
                MessageBox.Show("Trả sách Thành công");
                ds_Sach.Tables["TraSach"].Clear();
                cbb_MaDG.SelectedItem = null;
                cbb_MaSach.Text = string.Empty;
                cbb_MaSach.SelectedItem = null;
            }
            else
                MessageBox.Show("Trả sách thất bại");
        }

        private void btn_ThuPhi_Click(object sender, EventArgs e)
        {
            Hide_Panel();
            Panel_ThuPhi.Visible = true;
            cbb_MaDGThuPhi.Items.Clear();
            load_TrangThuPhi();
        }

        private void cbb_MaDGThuPhi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ds_Sach.Tables["DSThuPhi"] != null)
                ds_Sach.Tables["DSThuPhi"].Clear();

            DataGridView_DSThuPhi.Rows.Clear();

            string SachMuon = "select * from MUONSACH where (GhiChu like N'%Trả trễ%' or GhiChu like N'%Đền sách%') and MaDocGia = '" + cbb_MaDGThuPhi.Text + "'";
            SqlDataAdapter da_Sach = new SqlDataAdapter(SachMuon, conn);
            da_Sach.Fill(ds_Sach, "DSThuPhi");
            HeaderCell_Alignment(DataGridView_DSThuPhi, 6);

            //Load bảng thu phí
            conn.Open();
            //Đếm đọc giả đúng điều kiện
            SqlCommand cmd = new SqlCommand("select count(*) from MUONSACH where (GhiChu like N'%Trả trễ%' or GhiChu like N'%Đền sách%') and MaDocGia = '" + cbb_MaDGThuPhi.Text + "'", conn);
            int count = (int)cmd.ExecuteScalar();
            for (int i = 0; i < count; i++)
            {
                DateTime NgayMuon = Convert.ToDateTime(ds_Sach.Tables["DSThuPhi"].Rows[i][2].ToString());
                DateTime NgayTra = Convert.ToDateTime(ds_Sach.Tables["DSThuPhi"].Rows[i][3].ToString());
                DataGridView_DSThuPhi.Rows.Add(ds_Sach.Tables["DSThuPhi"].Rows[i][0], ds_Sach.Tables["DSThuPhi"].Rows[i][1], NgayMuon.ToString("dd/MM/yyyy"), NgayTra.ToString("dd/MM/yyyy"), ds_Sach.Tables["DSThuPhi"].Rows[i][4]);
            }
            conn.Close();

            //Tính số tiền phải trả
            for (int i = 0; i < DataGridView_DSThuPhi.Rows.Count; i++)
            {
                int TienPhaiTra = 0;
                if (DataGridView_DSThuPhi.Rows[i].Cells[4].Value.ToString().Contains("trễ"))
                {
                    int SoNgayTre = Convert.ToInt32(getBetween(DataGridView_DSThuPhi.Rows[i].Cells[4].Value.ToString(), "trễ", "ngày"));
                    TienPhaiTra = 2000 * SoNgayTre;
                }

                if (DataGridView_DSThuPhi.Rows[i].Cells[4].Value.ToString().Contains("đền"))
                {
                    string MaSach = DataGridView_DSThuPhi.Rows[i].Cells[1].Value.ToString();
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("select Gia from SACH where MaSach = '" + MaSach + "'", conn);
                    SqlDataReader rd = cmd1.ExecuteReader();
                    if (rd.Read())
                        TienPhaiTra += Convert.ToInt32(rd["Gia"]);
                    conn.Close();

                }

                DataGridView_DSThuPhi.Rows[i].Cells[5].Value = TienPhaiTra.ToString();
            }
        }

        //Lấy từ ở giữa 2 từ
        public string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter ThanhToan = new SqlDataAdapter("select * from MUONSACH where MaSach = '" + DataGridView_DSThuPhi.SelectedRows[0].Cells[1].Value.ToString() + "' and MaDocGia = '" + cbb_MaDGThuPhi.Text + "'", conn);
                ThanhToan.Fill(ds_Sach, "ThanhToan");
                SqlCommandBuilder builder = new SqlCommandBuilder(ThanhToan);
                if (DataGridView_DSThuPhi.SelectedRows.Count > 0)
                {
                    for (int i = 0; i < DataGridView_DSThuPhi.SelectedRows.Count; i++)
                    {
                        ds_Sach.Tables["ThanhToan"].Rows[i]["GhiChu"] = null;
                    }
                    ThanhToan.Update(ds_Sach, "ThanhToan");
                    MessageBox.Show("Thanh toán thành công");
                    ds_Sach.Tables["ThanhToan"].Clear();
                    DataGridView_DSThuPhi.Rows.RemoveAt(DataGridView_DSThuPhi.SelectedRows[0].Index);

                }
                else
                {
                    MessageBox.Show("Thanh toán thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thanh toán thất bại");
            }
            
        }
    }
}
