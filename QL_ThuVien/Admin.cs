using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using System.Windows.Forms;

namespace QL_ThuVien
{
    public partial class Admin : Form
    {
        public Form RefToForm1 { get; set; }
        private string _message;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        DataSet QL_ThuVien = new DataSet();
        public Admin()
        {
            InitializeComponent();
        }

        public Admin(string Message) : this()
        {
            _message = Message;
            label_Ten.Text = _message;
        }

        //Chỉnh vị trí chữ trên header của datagridview
        public void HeaderCell_Alignment(DataGridView dagrid, int SoCot)
        {
            for (int i = 0; i < SoCot; i++)
                dagrid.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void load_TrangThongKe()
        {
            //Số sách
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from SACH", conn);
            int SoSach = (int)cmd.ExecuteScalar();
            label_Sach.Text = SoSach.ToString();
            conn.Close();

            //Số thể loại
            conn.Open();
            SqlCommand cmd1 = new SqlCommand("select count(*) from THELOAI", conn);
            int SoTL = (int)cmd1.ExecuteScalar();
            label_TL.Text = SoTL.ToString();
            conn.Close();

            //Số tác giả
            conn.Open();
            SqlCommand cmd2 = new SqlCommand("select count(*) from TACGIA", conn);
            int SoTG = (int)cmd2.ExecuteScalar();
            label_TG.Text = SoTG.ToString();
            conn.Close();

            //Số nhà xuất bản
            conn.Open();
            SqlCommand cmd3 = new SqlCommand("select count(*) from NXB", conn);
            int SoNXB = (int)cmd3.ExecuteScalar();
            label_NXB.Text = SoNXB.ToString();
            conn.Close();

            //Số đọc giả
            conn.Open();
            SqlCommand cmd4 = new SqlCommand("select count(*) from DOCGIA", conn);
            int SoDG = (int)cmd4.ExecuteScalar();
            label_DG.Text = SoDG.ToString();
            conn.Close();

            //Số nhân viên
            conn.Open();
            SqlCommand cmd5 = new SqlCommand("select count(*) from NHANVIEN", conn);
            int SoNV = (int)cmd5.ExecuteScalar();
            label_NV.Text = SoNV.ToString();
            conn.Close();

            //Số sách chưa trả
            conn.Open();
            SqlCommand cmd6 = new SqlCommand("select count(*) from MUONSACH where NgayTra is null", conn);
            int SoSachChuTra = (int)cmd6.ExecuteScalar();
            label_SachChuaTra.Text = SoSachChuTra.ToString();
            conn.Close();

            //Số người thiếu nợ
            conn.Open();
            SqlCommand cmd7 = new SqlCommand("select count(*) from DOCGIA where MaDocGia in (select MaDocGia from MUONSACH where GhiChu like N'%trễ%' or GhiChu like N'%đền%')", conn);
            int SoNguoiThieuNo = (int)cmd7.ExecuteScalar();
            label_NguoiThieuNo.Text = SoNguoiThieuNo.ToString();
            conn.Close();
        }

        public void load_DSNV()
        {
            string DSNV = "select * from NHANVIEN";
            SqlDataAdapter da_NV = new SqlDataAdapter(DSNV, conn);
            da_NV.Fill(QL_ThuVien, "DSNV");
            HeaderCell_Alignment(DataGridView_DSNV, 6);
            //Đếm số nhân viên
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from NHANVIEN", conn);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            for (int i = 0; i < count; i++)
            {
                DateTime date = Convert.ToDateTime(QL_ThuVien.Tables["DSNV"].Rows[i][4]);
                DataGridView_DSNV.Rows.Add(QL_ThuVien.Tables["DSNV"].Rows[i][0], QL_ThuVien.Tables["DSNV"].Rows[i][2], QL_ThuVien.Tables["DSNV"].Rows[i][3], date.ToString("dd/MM/yyyy"), QL_ThuVien.Tables["DSNV"].Rows[i][5], QL_ThuVien.Tables["DSNV"].Rows[i][6]);
            }
        }

        public void load_DSDG()
        {
            string DSDG = "select * from DOCGIA";
            SqlDataAdapter da_DG = new SqlDataAdapter(DSDG, conn);
            da_DG.Fill(QL_ThuVien, "DSDG");
            HeaderCell_Alignment(DataGridView_DSDG, 6);
            //Đếm số nhân viên
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from DOCGIA", conn);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            for (int i = 0; i < count; i++)
            {
                DateTime date = Convert.ToDateTime(QL_ThuVien.Tables["DSDG"].Rows[i][3]);
                DataGridView_DSDG.Rows.Add(QL_ThuVien.Tables["DSDG"].Rows[i][0], QL_ThuVien.Tables["DSDG"].Rows[i][1], QL_ThuVien.Tables["DSDG"].Rows[i][2], date.ToString("dd/MM/yyyy"), QL_ThuVien.Tables["DSDG"].Rows[i][4], QL_ThuVien.Tables["DSDG"].Rows[i][5]);
            }
        }

        public void load_DSTL()
        {
            string DSTL = "select * from TheLoai";
            SqlDataAdapter da_TL = new SqlDataAdapter(DSTL, conn);
            da_TL.Fill(QL_ThuVien, "DSTL");
            HeaderCell_Alignment(DataGridView_DSTL, 2);
            //Đếm số nhân viên
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from TheLoai", conn);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            for (int i = 0; i < count; i++)
            {
                DataGridView_DSTL.Rows.Add(QL_ThuVien.Tables["DSTL"].Rows[i][0], QL_ThuVien.Tables["DSTL"].Rows[i][1]);
            }
        }

        public void load_DSTG()
        {
            string DSTG = "select * from TACGIA";
            SqlDataAdapter da_TG = new SqlDataAdapter(DSTG, conn);
            da_TG.Fill(QL_ThuVien, "DSTG");
            HeaderCell_Alignment(DataGridView_DSTG, 2);
            //Đếm số nhân viên
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from TACGIA", conn);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            for (int i = 0; i < count; i++)
            {
                DataGridView_DSTG.Rows.Add(QL_ThuVien.Tables["DSTG"].Rows[i][0], QL_ThuVien.Tables["DSTG"].Rows[i][1]);
            }
        }

        public void load_DSNXB()
        {
            string DSNXB = "select * from NXB";
            SqlDataAdapter da_NXB = new SqlDataAdapter(DSNXB, conn);
            da_NXB.Fill(QL_ThuVien, "DSNXB");
            HeaderCell_Alignment(DataGridView_DSNXB, 2);
            //Đếm số nhân viên
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from NXB", conn);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            for (int i = 0; i < count; i++)
            {
                DataGridView_DSNXB.Rows.Add(QL_ThuVien.Tables["DSNXB"].Rows[i][0], QL_ThuVien.Tables["DSNXB"].Rows[i][1]);
            }
        }

        public void load_DSS()
        {
            string DSSACH = "select * from SACH";
            SqlDataAdapter da_SACH = new SqlDataAdapter(DSSACH, conn);
            da_SACH.Fill(QL_ThuVien, "DSSACH");
            HeaderCell_Alignment(DataGridView_DSS, 8);
            //Đếm số nhân viên
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from SACH", conn);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            for (int i = 0; i < count; i++)
            {
                DataGridView_DSS.Rows.Add(QL_ThuVien.Tables["DSSACH"].Rows[i][0], QL_ThuVien.Tables["DSSACH"].Rows[i][1], QL_ThuVien.Tables["DSSACH"].Rows[i][2], QL_ThuVien.Tables["DSSACH"].Rows[i][3], QL_ThuVien.Tables["DSSACH"].Rows[i][4], QL_ThuVien.Tables["DSSACH"].Rows[i][5], QL_ThuVien.Tables["DSSACH"].Rows[i][6], QL_ThuVien.Tables["DSSACH"].Rows[i][7]);
            }
        }

        public void load_TrangDSS()
        {
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

            cbb_TL.DataSource = ds.Tables["THELOAI"];
            cbb_TL.DisplayMember = "TenTheLoai";
            cbb_TL.ValueMember = "MaTheLoai";

            cbb_TG.DataSource = ds.Tables["TACGIA"];
            cbb_TG.DisplayMember = "TenTacGia";
            cbb_TG.ValueMember = "MaTacGia";

            cbb_NXB.DataSource = ds.Tables["NXB"];
            cbb_NXB.DisplayMember = "TenNXB";
            cbb_NXB.ValueMember = "MaNXB";
        }

        public void Hide_Panel()
        {
            Panel_ThongKe.Visible = false;
            Panel_DSDuLieu.Visible = false;
            Panel_DSNV.Visible = false;
            Panel_DSDG.Visible = false;
            Panel_DSTL.Visible = false;
            Panel_DSTG.Visible = false;
            Panel_DSNXB.Visible = false;
            Panel_DSSACH.Visible = false;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            //Load trang đầu
            Hide_Panel();
            Panel_ThongKe.Visible = true;
            //Disable một số thứ
            btn_Luu_DSNV.Enabled = false;
            btn_Luu_DSDG.Enabled = false;
            //Load dữ liệu từ database
            load_TrangThongKe();
            load_DSNV();
            load_DSDG();
            load_DSTL();
            load_DSTG();
            load_DSNXB();
            load_DSS();
            load_TrangDSS();

        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.RefToForm1.Show();
        }

        private void btn_ThongKe_Click(object sender, EventArgs e)
        {
            Hide_Panel();
            Panel_ThongKe.Visible = true;
        }

        private void btn_QLDLHT_Click(object sender, EventArgs e)
        {
            if (Panel_DSDuLieu.Visible == true)
                Panel_DSDuLieu.Visible = false;
            else
                Panel_DSDuLieu.Visible = true;
        }

        private void btn_NV_Click(object sender, EventArgs e)
        {
            Hide_Panel();
            Panel_DSNV.Visible = true;
        }

        private void DataGridView_DSNV_SelectionChanged(object sender, EventArgs e)
        {
            string MaNV = "";
            if (DataGridView_DSNV.SelectedRows.Count > 0)
                if (DataGridView_DSNV.SelectedRows[0].Cells[0].Value != null)
                    MaNV = DataGridView_DSNV.SelectedRows[0].Cells[0].Value.ToString();

            conn.Open();
            SqlCommand cmd = new SqlCommand("select TenDangNhap from NHANVIEN where MaNV = '" + MaNV + "'", conn);
            SqlDataReader rd = cmd.ExecuteReader();
            string TenDN = "";
            if (rd.Read())
            {
                if (rd.IsDBNull("TenDangNhap"))
                {
                    txt_TenDN.Text = "";
                    txt_MK.Text = "";
                    txt_Email.Text = "";
                }
                else
                    TenDN = rd.GetString(0);
            }
            conn.Close();

            if (TenDN != "")
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("select * from ACCOUNT where TenDangNhap = N'" + TenDN + "'", conn);
                SqlDataReader rd1 = cmd1.ExecuteReader();
                if (rd1.Read())
                {
                    txt_TenDN.Text = rd1.GetString(0);
                    txt_MK.Text = rd1.GetString(1);
                    txt_Email.Text = rd1.GetString(2);
                }
                conn.Close();
            }
        }

        private void btn_Them_DSNV_Click(object sender, EventArgs e)
        {
            btn_Luu_DSNV.Enabled = true;
            //+ Cho phép thêm các dòng tiếp theo trên datagridview
            DataGridView_DSNV.AllowUserToAddRows = true;
            DataGridView_DSNV.ReadOnly = false;


            //Không được sửa các dòng trên datagridview đã có dữ liệu
            for (int i = 0; i < DataGridView_DSNV.Rows.Count - 1; i++)
            {
                DataGridView_DSNV.Rows[i].ReadOnly = true;
            }
            DataGridView_DSNV.FirstDisplayedScrollingRowIndex = DataGridView_DSNV.Rows.Count - 1;
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            txt_TenDN.Enabled = txt_MK.Enabled = txt_Email.Enabled = true;
            //+Button Lưu có hiệu lực
            btn_Luu_DSNV.Enabled = true;
            //+Cho phép sửa các thông tin trên Datagrid
            DataGridView_DSNV.ReadOnly = false;
            for (int i = 0; i < DataGridView_DSNV.Rows.Count - 1; i++)
                DataGridView_DSNV.Rows[i].ReadOnly = false;
            DataGridView_DSNV.Columns[0].ReadOnly = true;
            //+Lưu ý: không cho phép gõ thêm các dòng mới
            DataGridView_DSNV.AllowUserToAddRows = false;
        }

        private void btn_Luu_DSNV_Click(object sender, EventArgs e)
        {
            if (DataGridView_DSNV.Rows.Count > QL_ThuVien.Tables["DSNV"].Rows.Count)
            {
                //Lưu sau khi thêm
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select count(*) from NHANVIEN", conn);
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();

                    DateTime date = Convert.ToDateTime(DataGridView_DSNV.Rows[count].Cells[3].Value);
                    DataRow Insert_New = QL_ThuVien.Tables["DSNV"].NewRow();
                    Insert_New["MaNV"] = DataGridView_DSNV.Rows[count].Cells[0].Value.ToString();
                    Insert_New["HoTen"] = DataGridView_DSNV.Rows[count].Cells[1].Value.ToString();
                    Insert_New["GioiTinh"] = DataGridView_DSNV.Rows[count].Cells[2].Value.ToString();
                    Insert_New["NgaySinh"] = date.ToString("dd/MM/yyyy");
                    Insert_New["DiaChi"] = DataGridView_DSNV.Rows[count].Cells[4].Value.ToString();
                    Insert_New["SDT"] = DataGridView_DSNV.Rows[count].Cells[5].Value.ToString();
                    QL_ThuVien.Tables["DSNV"].Rows.Add(Insert_New);

                    SqlDataAdapter da_Account = new SqlDataAdapter("select * from NHANVIEN", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da_Account);
                    da_Account.Update(QL_ThuVien, "DSNV");
                    MessageBox.Show("Lưu thành công");
                    DataGridView_DSNV.Rows.Clear();
                    QL_ThuVien.Tables["DSNV"].Clear();
                    load_DSNV();
                    btn_Luu_DSNV.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lưu thất bại");
                }
            }
            else
            {
                //Lưu sau khi sửa
                try
                {
                    for (int i = 0; i < DataGridView_DSNV.Rows.Count; i++)
                    {
                        QL_ThuVien.Tables["DSNV"].Rows[i]["MaNV"] = DataGridView_DSNV.Rows[i].Cells[0].Value.ToString();
                        QL_ThuVien.Tables["DSNV"].Rows[i]["HoTen"] = DataGridView_DSNV.Rows[i].Cells[1].Value.ToString();
                        QL_ThuVien.Tables["DSNV"].Rows[i]["GioiTinh"] = DataGridView_DSNV.Rows[i].Cells[2].Value.ToString();
                        QL_ThuVien.Tables["DSNV"].Rows[i]["NgaySinh"] = DataGridView_DSNV.Rows[i].Cells[3].Value.ToString();
                        QL_ThuVien.Tables["DSNV"].Rows[i]["DiaChi"] = DataGridView_DSNV.Rows[i].Cells[4].Value.ToString();
                        QL_ThuVien.Tables["DSNV"].Rows[i]["SDT"] = DataGridView_DSNV.Rows[i].Cells[5].Value.ToString();
                    }
                    SqlDataAdapter da_Account = new SqlDataAdapter("select * from NHANVIEN", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da_Account);
                    da_Account.Update(QL_ThuVien, "DSNV");
                    DataGridView_DSNV.Rows.Clear();
                    QL_ThuVien.Tables["DSNV"].Clear();
                    load_DSNV();
                    MessageBox.Show("Lưu thành công");
                    btn_Luu_DSNV.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lưu thất bại");
                }
            }
        }

        //Kiểm tra email
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private void btn_TaoTK_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select TenDangNhap from NHANVIEN where MaNV = '" + DataGridView_DSNV.SelectedRows[0].Cells[0].Value.ToString() + "'", conn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    if (rd.IsDBNull("TenDangNhap"))
                    {
                        conn.Close();
                        if (IsValidEmail(txt_Email.Text) == true && txt_TenDN.Text != string.Empty && txt_MK.Text != string.Empty)
                        {
                            SqlDataAdapter da_Account = new SqlDataAdapter("select * from ACCOUNT", conn);
                            da_Account.Fill(QL_ThuVien, "ACCOUNT");
                            DataColumn[] key = new DataColumn[1];
                            key[0] = QL_ThuVien.Tables["ACCOUNT"].Columns[0];
                            QL_ThuVien.Tables["ACCOUNT"].PrimaryKey = key;
                            SqlCommandBuilder builder = new SqlCommandBuilder(da_Account);
                            DataRow find = QL_ThuVien.Tables["ACCOUNT"].Rows.Find(txt_TenDN.Text);
                            if (find == null) //Xem tên đăng nhập có bị trùng không
                            {
                                DataRow Insert_New = QL_ThuVien.Tables["ACCOUNT"].NewRow();
                                Insert_New["TenDangNhap"] = txt_TenDN.Text;
                                Insert_New["MatKhau"] = txt_MK.Text;
                                Insert_New["Email"] = txt_Email.Text;
                                QL_ThuVien.Tables["ACCOUNT"].Rows.Add(Insert_New);
                                da_Account.Update(QL_ThuVien, "ACCOUNT");
                                QL_ThuVien.Tables["ACCOUNT"].Clear();
                            }
                            else
                            {
                                MessageBox.Show("Tạo tài khoản thất bại");
                                QL_ThuVien.Tables["ACCOUNT"].Clear();
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tạo tài khoản thất bại");
                            QL_ThuVien.Tables["ACCOUNT"].Clear();
                            return;
                        }

                        string MaNV = DataGridView_DSNV.SelectedRows[0].Cells[0].Value.ToString();
                        int find_row = 0;
                        foreach (DataRow row in QL_ThuVien.Tables["DSNV"].Rows)
                        {
                            if (row["MaNV"].ToString() == MaNV)
                            {
                                find_row = QL_ThuVien.Tables["DSNV"].Rows.IndexOf(row);
                            }
                        }
                        QL_ThuVien.Tables["DSNV"].Rows[find_row]["TenDangNhap"] = txt_TenDN.Text;
                        SqlDataAdapter da_NV = new SqlDataAdapter("select * from NHANVIEN", conn);
                        SqlCommandBuilder builder1 = new SqlCommandBuilder(da_NV);
                        da_NV.Update(QL_ThuVien, "DSNV");
                        MessageBox.Show("Tạo tài khoản thành công");
                        DataGridView_DSNV.Rows.Clear();
                        QL_ThuVien.Tables["DSNV"].Clear();
                        load_DSNV();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tạo tài khoản thất bại");
            }
            conn.Close();
        }

        private void btn_Xoa_DSNV_Click(object sender, EventArgs e)
        {
            try
            {
                string MaNV = DataGridView_DSNV.SelectedRows[0].Cells[0].Value.ToString();
                string TenDN = "";
                SqlDataAdapter da = new SqlDataAdapter("select * from NHANVIEN", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                SqlDataAdapter da1 = new SqlDataAdapter("select * from ACCOUNT", conn);
                da1.Fill(QL_ThuVien, "ACCOUNT");
                SqlCommandBuilder builder1 = new SqlCommandBuilder(da1);
                //Tìm dòng trong table có mã nhân viên đã được chọn trong datagridview
                int find = 0;
                foreach (DataRow row in QL_ThuVien.Tables["DSNV"].Rows)
                {
                    if (row["MaNV"].ToString() == MaNV)
                        find = QL_ThuVien.Tables["DSNV"].Rows.IndexOf(row);
                }
                //Tìm dòng trong table có tên đăng nhập đã được chọn trong datagridview
                conn.Open();
                SqlCommand cmd = new SqlCommand("select TenDangNhap from NHANVIEN where MaNV = '" + MaNV + "'", conn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    if (rd.IsDBNull("TenDangNhap"))
                    {
                        conn.Close();
                        QL_ThuVien.Tables["DSNV"].Rows[find].Delete();
                        da.Update(QL_ThuVien, "DSNV");
                        MessageBox.Show("Xoá thành công");
                        DataGridView_DSNV.Rows.Clear();
                        QL_ThuVien.Tables["DSNV"].Clear();
                        load_DSNV();
                        return;
                    }
                    else
                    {
                        TenDN = rd.GetString(0);
                        conn.Close();
                        QL_ThuVien.Tables["DSNV"].Rows[find].Delete();
                        da.Update(QL_ThuVien, "DSNV");


                        int find1 = 0;
                        foreach (DataRow row in QL_ThuVien.Tables["ACCOUNT"].Rows)
                        {
                            if (row["TenDangNhap"].ToString() == TenDN)
                                find1 = QL_ThuVien.Tables["ACCOUNT"].Rows.IndexOf(row);
                        }
                        QL_ThuVien.Tables["ACCOUNT"].Rows[find1].Delete();
                        da1.Update(QL_ThuVien, "ACCOUNT");
                        MessageBox.Show("Xoá thành công");
                        DataGridView_DSNV.Rows.Clear();
                        QL_ThuVien.Tables["DSNV"].Clear();
                        load_DSNV();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xoá thất bại");
            }
        }

        private void btn_SuaTK_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from ACCOUNT", conn);
                da.Fill(QL_ThuVien, "ACCOUNT");
                SqlCommandBuilder builder = new SqlCommandBuilder(da);

                conn.Open();
                SqlCommand cmd = new SqlCommand("select TenDangNhap from NHANVIEN where MaNV = '" + DataGridView_DSNV.SelectedRows[0].Cells[0].Value.ToString() + "'", conn);
                SqlDataReader rd = cmd.ExecuteReader();
                string TenDN = "";
                if (rd.Read())
                    TenDN = rd.GetString(0);
                conn.Close();

                conn.Open();
                SqlCommand cmd1 = new SqlCommand("update NHANVIEN set TenDangNhap = null where MaNV = '" + DataGridView_DSNV.SelectedRows[0].Cells[0].Value.ToString() + "'", conn);
                cmd1.ExecuteNonQuery();
                conn.Close();

                int find = 0;
                foreach (DataRow row in QL_ThuVien.Tables["ACCOUNT"].Rows)
                {
                    if (row["TenDangNhap"].ToString() == TenDN)
                    {
                        find = QL_ThuVien.Tables["ACCOUNT"].Rows.IndexOf(row);
                    }
                }

                QL_ThuVien.Tables["ACCOUNT"].Rows[find].Delete();
                da.Update(QL_ThuVien, "ACCOUNT");
                MessageBox.Show("Xoá tài khoản thành công");
                DataGridView_DSNV.Rows.Clear();
                QL_ThuVien.Tables["DSNV"].Clear();
                load_DSNV();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                MessageBox.Show("Xoá tài khoản thất bại");
            }

        }

        private void btn_DG_Click(object sender, EventArgs e)
        {
            Hide_Panel();
            Panel_DSDG.Visible = true;
        }

        private void btn_Them_DSDG_Click(object sender, EventArgs e)
        {
            btn_Luu_DSDG.Enabled = true;
            //+ Cho phép thêm các dòng tiếp theo trên datagridview
            DataGridView_DSDG.AllowUserToAddRows = true;
            DataGridView_DSDG.ReadOnly = false;


            //Không được sửa các dòng trên datagridview đã có dữ liệu
            for (int i = 0; i < DataGridView_DSDG.Rows.Count - 1; i++)
            {
                DataGridView_DSDG.Rows[i].ReadOnly = true;
            }
            DataGridView_DSDG.FirstDisplayedScrollingRowIndex = DataGridView_DSDG.Rows.Count - 1;
        }

        private void btn_Sua_DSDG_Click(object sender, EventArgs e)
        {
            //+Button Lưu có hiệu lực
            btn_Luu_DSDG.Enabled = true;
            //+Cho phép sửa các thông tin trên Datagrid
            DataGridView_DSDG.ReadOnly = false;
            for (int i = 0; i < DataGridView_DSDG.Rows.Count - 1; i++)
                DataGridView_DSDG.Rows[i].ReadOnly = false;
            DataGridView_DSDG.Columns[0].ReadOnly = true;
            //+Lưu ý: không cho phép gõ thêm các dòng mới
            DataGridView_DSDG.AllowUserToAddRows = false;
        }

        private void btn_Xoa_DSDG_Click(object sender, EventArgs e)
        {
            try
            {
                string MaDG = DataGridView_DSDG.SelectedRows[0].Cells[0].Value.ToString();
                SqlDataAdapter da = new SqlDataAdapter("select * from DOCGIA", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                int find = 0;
                foreach (DataRow row in QL_ThuVien.Tables["DSDG"].Rows)
                {
                    if (row["MaDocGia"].ToString() == MaDG)
                        find = QL_ThuVien.Tables["DSDG"].Rows.IndexOf(row);
                }

                QL_ThuVien.Tables["DSDG"].Rows[find].Delete();
                da.Update(QL_ThuVien, "DSDG");
                MessageBox.Show("Xoá thành công");
                DataGridView_DSDG.Rows.Clear();
                QL_ThuVien.Tables["DSDG"].Clear();
                load_DSDG();
            }
            catch (Exception)
            {
                MessageBox.Show("Xoá thất bại");
            }
        }

        private void btn_Luu_DSDG_Click(object sender, EventArgs e)
        {
            if (DataGridView_DSDG.Rows.Count > QL_ThuVien.Tables["DSDG"].Rows.Count)
            {
                //Lưu sau khi thêm
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select count(*) from DOCGIA", conn);
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();

                    DateTime date = Convert.ToDateTime(DataGridView_DSDG.Rows[count].Cells[3].Value);
                    DataRow Insert_New = QL_ThuVien.Tables["DSDG"].NewRow();
                    Insert_New["MaDocGia"] = DataGridView_DSDG.Rows[count].Cells[0].Value.ToString();
                    Insert_New["HoTen"] = DataGridView_DSDG.Rows[count].Cells[1].Value.ToString();
                    Insert_New["GioiTinh"] = DataGridView_DSDG.Rows[count].Cells[2].Value.ToString();
                    Insert_New["NgaySinh"] = date.ToString("dd/MM/yyyy");
                    Insert_New["DiaChi"] = DataGridView_DSDG.Rows[count].Cells[4].Value.ToString();
                    Insert_New["SDT"] = DataGridView_DSDG.Rows[count].Cells[5].Value.ToString();
                    QL_ThuVien.Tables["DSDG"].Rows.Add(Insert_New);

                    SqlDataAdapter da_DOCGIA = new SqlDataAdapter("select * from DOCGIA", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da_DOCGIA);
                    da_DOCGIA.Update(QL_ThuVien, "DSDG");
                    MessageBox.Show("Lưu thành công");
                    DataGridView_DSDG.Rows.Clear();
                    QL_ThuVien.Tables["DSDG"].Clear();
                    load_DSDG();
                    btn_Luu_DSDG.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lưu thất bại");
                }
            }
            else
            {
                //Lưu sau khi sửa
                try
                {
                    for (int i = 0; i < DataGridView_DSDG.Rows.Count; i++)
                    {
                        QL_ThuVien.Tables["DSDG"].Rows[i]["MaDocGia"] = DataGridView_DSDG.Rows[i].Cells[0].Value.ToString();
                        QL_ThuVien.Tables["DSDG"].Rows[i]["HoTen"] = DataGridView_DSDG.Rows[i].Cells[1].Value.ToString();
                        QL_ThuVien.Tables["DSDG"].Rows[i]["GioiTinh"] = DataGridView_DSDG.Rows[i].Cells[2].Value.ToString();
                        QL_ThuVien.Tables["DSDG"].Rows[i]["NgaySinh"] = DataGridView_DSDG.Rows[i].Cells[3].Value.ToString();
                        QL_ThuVien.Tables["DSDG"].Rows[i]["DiaChi"] = DataGridView_DSDG.Rows[i].Cells[4].Value.ToString();
                        QL_ThuVien.Tables["DSDG"].Rows[i]["SDT"] = DataGridView_DSDG.Rows[i].Cells[5].Value.ToString();
                    }
                    SqlDataAdapter da_DOCGIA = new SqlDataAdapter("select * from DOCGIA", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da_DOCGIA);
                    da_DOCGIA.Update(QL_ThuVien, "DSDG");
                    DataGridView_DSDG.Rows.Clear();
                    QL_ThuVien.Tables["DSDG"].Clear();
                    load_DSDG();
                    MessageBox.Show("Lưu thành công");
                    btn_Luu_DSDG.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lưu thất bại");
                }
            }
        }

        private void btn_TheLoai_Click(object sender, EventArgs e)
        {
            Hide_Panel();
            Panel_DSTL.Visible = true;
        }

        private void btn_Them_DSTL_Click(object sender, EventArgs e)
        {
            btn_Luu_DSTL.Enabled = true;
            //+ Cho phép thêm các dòng tiếp theo trên datagridview
            DataGridView_DSTL.AllowUserToAddRows = true;
            DataGridView_DSTL.ReadOnly = false;


            //Không được sửa các dòng trên datagridview đã có dữ liệu
            for (int i = 0; i < DataGridView_DSTL.Rows.Count - 1; i++)
            {
                DataGridView_DSTL.Rows[i].ReadOnly = true;
            }
            DataGridView_DSTL.FirstDisplayedScrollingRowIndex = DataGridView_DSTL.Rows.Count - 1;
        }

        private void btn_Sua_DSTL_Click(object sender, EventArgs e)
        {
            //+Button Lưu có hiệu lực
            btn_Luu_DSTL.Enabled = true;
            //+Cho phép sửa các thông tin trên Datagrid
            DataGridView_DSTL.ReadOnly = false;
            for (int i = 0; i < DataGridView_DSTL.Rows.Count - 1; i++)
                DataGridView_DSTL.Rows[i].ReadOnly = false;
            DataGridView_DSTL.Columns[0].ReadOnly = true;
            //+Lưu ý: không cho phép gõ thêm các dòng mới
            DataGridView_DSTL.AllowUserToAddRows = false;
        }

        private void btn_Luu_DSTL_Click(object sender, EventArgs e)
        {
            if (DataGridView_DSTL.Rows.Count > QL_ThuVien.Tables["DSTL"].Rows.Count)
            {
                //Lưu sau khi thêm
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select count(*) from THELOAI", conn);
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();

                    for (int i = count; i < DataGridView_DSTL.Rows.Count - 1; i++)
                    {
                        DataRow Insert_New = QL_ThuVien.Tables["DSTL"].NewRow();
                        Insert_New["MaTheLoai"] = DataGridView_DSTL.Rows[count].Cells[0].Value.ToString();
                        Insert_New["TenTheLoai"] = DataGridView_DSTL.Rows[count].Cells[1].Value.ToString();
                        QL_ThuVien.Tables["DSTL"].Rows.Add(Insert_New);
                    }
                    SqlDataAdapter da_THELOAI = new SqlDataAdapter("select * from THELOAI", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da_THELOAI);
                    da_THELOAI.Update(QL_ThuVien, "DSTL");
                    MessageBox.Show("Lưu thành công");
                    DataGridView_DSTL.Rows.Clear();
                    QL_ThuVien.Tables["DSTL"].Clear();
                    load_DSTL();
                    btn_Luu_DSTL.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lưu thất bại");
                }
            }
            else
            {
                //Lưu sau khi sửa
                try
                {
                    for (int i = 0; i < DataGridView_DSTL.Rows.Count; i++)
                    {
                        QL_ThuVien.Tables["DSTL"].Rows[i]["MaTheLoai"] = DataGridView_DSTL.Rows[i].Cells[0].Value.ToString();
                        QL_ThuVien.Tables["DSTL"].Rows[i]["TenTheLoai"] = DataGridView_DSTL.Rows[i].Cells[1].Value.ToString();
                    }
                    SqlDataAdapter da_THELOAI = new SqlDataAdapter("select * from THELOAI", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da_THELOAI);
                    da_THELOAI.Update(QL_ThuVien, "DSTL");
                    DataGridView_DSTL.Rows.Clear();
                    QL_ThuVien.Tables["DSTL"].Clear();
                    load_DSTL();
                    MessageBox.Show("Lưu thành công");
                    btn_Luu_DSTL.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lưu thất bại");
                }
            }
        }

        private void btn_Xoa_DSTL_Click(object sender, EventArgs e)
        {
            try
            {
                string MaTL = DataGridView_DSTL.SelectedRows[0].Cells[0].Value.ToString();
                SqlDataAdapter da = new SqlDataAdapter("select * from THELOAI", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                int find = 0;
                foreach (DataRow row in QL_ThuVien.Tables["DSTL"].Rows)
                {
                    if (row["MaTheLoai"].ToString() == MaTL)
                        find = QL_ThuVien.Tables["DSTL"].Rows.IndexOf(row);
                }

                QL_ThuVien.Tables["DSTL"].Rows[find].Delete();
                da.Update(QL_ThuVien, "DSTL");
                MessageBox.Show("Xoá thành công");
                DataGridView_DSTL.Rows.Clear();
                QL_ThuVien.Tables["DSTL"].Clear();
                load_DSTL();
            }
            catch (Exception)
            {
                MessageBox.Show("Xoá thất bại");
            }

        }

        private void btn_TG_Click(object sender, EventArgs e)
        {
            Hide_Panel();
            Panel_DSTG.Visible = true;
        }

        private void btn_Them_DSTG_Click(object sender, EventArgs e)
        {
            btn_Luu_DSTG.Enabled = true;
            //+ Cho phép thêm các dòng tiếp theo trên datagridview
            DataGridView_DSTG.AllowUserToAddRows = true;
            DataGridView_DSTG.ReadOnly = false;


            //Không được sửa các dòng trên datagridview đã có dữ liệu
            for (int i = 0; i < DataGridView_DSTG.Rows.Count - 1; i++)
            {
                DataGridView_DSTG.Rows[i].ReadOnly = true;
            }
            DataGridView_DSTG.FirstDisplayedScrollingRowIndex = DataGridView_DSTG.Rows.Count - 1;
        }

        private void btn_Sua_DSTG_Click(object sender, EventArgs e)
        {
            //+Button Lưu có hiệu lực
            btn_Luu_DSTG.Enabled = true;
            //+Cho phép sửa các thông tin trên Datagrid
            DataGridView_DSTG.ReadOnly = false;
            for (int i = 0; i < DataGridView_DSTG.Rows.Count - 1; i++)
                DataGridView_DSTG.Rows[i].ReadOnly = false;
            DataGridView_DSTG.Columns[0].ReadOnly = true;
            //+Lưu ý: không cho phép gõ thêm các dòng mới
            DataGridView_DSTG.AllowUserToAddRows = false;
        }

        private void btn_Luu_DSTG_Click(object sender, EventArgs e)
        {
            if (DataGridView_DSTG.Rows.Count > QL_ThuVien.Tables["DSTG"].Rows.Count)
            {
                //Lưu sau khi thêm
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select count(*) from TACGIA", conn);
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();

                    DataRow Insert_New = QL_ThuVien.Tables["DSTG"].NewRow();
                    Insert_New["MaTacGia"] = DataGridView_DSTG.Rows[count].Cells[0].Value.ToString();
                    Insert_New["TenTacGia"] = DataGridView_DSTG.Rows[count].Cells[1].Value.ToString();
                    QL_ThuVien.Tables["DSTG"].Rows.Add(Insert_New);

                    SqlDataAdapter da_TACGIA = new SqlDataAdapter("select * from TACGIA", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da_TACGIA);
                    da_TACGIA.Update(QL_ThuVien, "DSTG");
                    MessageBox.Show("Thêm thành công");
                    DataGridView_DSTG.Rows.Clear();
                    QL_ThuVien.Tables["DSTG"].Clear();
                    load_DSTG();
                    btn_Luu_DSTG.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm thất bại");
                }
            }
            else
            {
                //Lưu sau khi sửa
                try
                {
                    for (int i = 0; i < DataGridView_DSTG.Rows.Count; i++)
                    {
                        QL_ThuVien.Tables["DSTG"].Rows[i]["MaTacGia"] = DataGridView_DSTG.Rows[i].Cells[0].Value.ToString();
                        QL_ThuVien.Tables["DSTG"].Rows[i]["TenTacGia"] = DataGridView_DSTG.Rows[i].Cells[1].Value.ToString();
                    }
                    SqlDataAdapter da_TACGIA = new SqlDataAdapter("select * from TACGIA", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da_TACGIA);
                    da_TACGIA.Update(QL_ThuVien, "DSTG");
                    DataGridView_DSTG.Rows.Clear();
                    QL_ThuVien.Tables["DSTG"].Clear();
                    load_DSTG();
                    MessageBox.Show("Sửa thành công");
                    btn_Luu_DSTG.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sửa thất bại");
                }
            }
        }

        private void btn_Xoa_DSTG_Click(object sender, EventArgs e)
        {
            try
            {
                string MaTG = DataGridView_DSTG.SelectedRows[0].Cells[0].Value.ToString();
                SqlDataAdapter da = new SqlDataAdapter("select * from TACGIA", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                int find = 0;
                foreach (DataRow row in QL_ThuVien.Tables["DSTG"].Rows)
                {
                    if (row["MaTacGia"].ToString() == MaTG)
                        find = QL_ThuVien.Tables["DSTG"].Rows.IndexOf(row);
                }

                QL_ThuVien.Tables["DSTG"].Rows[find].Delete();
                da.Update(QL_ThuVien, "DSTG");
                MessageBox.Show("Xoá thành công");
                DataGridView_DSTG.Rows.Clear();
                QL_ThuVien.Tables["DSTG"].Clear();
                load_DSTG();
            }
            catch (Exception)
            {
                MessageBox.Show("Xoá thất bại");
            }
        }

        private void btn_NXB_Click(object sender, EventArgs e)
        {
            Hide_Panel();
            Panel_DSNXB.Visible = true;
        }

        private void btn_Them_DSNXB_Click(object sender, EventArgs e)
        {
            btn_Luu_DSNXB.Enabled = true;
            //+ Cho phép thêm các dòng tiếp theo trên datagridview
            DataGridView_DSNXB.AllowUserToAddRows = true;
            DataGridView_DSNXB.ReadOnly = false;


            //Không được sửa các dòng trên datagridview đã có dữ liệu
            for (int i = 0; i < DataGridView_DSNXB.Rows.Count - 1; i++)
            {
                DataGridView_DSNXB.Rows[i].ReadOnly = true;
            }
            DataGridView_DSNXB.FirstDisplayedScrollingRowIndex = DataGridView_DSNXB.Rows.Count - 1;
        }

        private void btn_Sua_DSNXB_Click(object sender, EventArgs e)
        {
            //+Button Lưu có hiệu lực
            btn_Luu_DSNXB.Enabled = true;
            //+Cho phép sửa các thông tin trên Datagrid
            DataGridView_DSNXB.ReadOnly = false;
            for (int i = 0; i < DataGridView_DSNXB.Rows.Count - 1; i++)
                DataGridView_DSNXB.Rows[i].ReadOnly = false;
            DataGridView_DSNXB.Columns[0].ReadOnly = true;
            //+Lưu ý: không cho phép gõ thêm các dòng mới
            DataGridView_DSNXB.AllowUserToAddRows = false;
        }

        private void btn_Luu_DSNXB_Click(object sender, EventArgs e)
        {
            if (DataGridView_DSNXB.Rows.Count > QL_ThuVien.Tables["DSNXB"].Rows.Count)
            {
                //Lưu sau khi thêm
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select count(*) from NXB", conn);
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();

                    DataRow Insert_New = QL_ThuVien.Tables["DSNXB"].NewRow();
                    Insert_New["MaNXB"] = DataGridView_DSNXB.Rows[count].Cells[0].Value.ToString();
                    Insert_New["TenNXB"] = DataGridView_DSNXB.Rows[count].Cells[1].Value.ToString();
                    QL_ThuVien.Tables["DSNXB"].Rows.Add(Insert_New);

                    SqlDataAdapter da_NXB = new SqlDataAdapter("select * from NXB", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da_NXB);
                    da_NXB.Update(QL_ThuVien, "DSNXB");
                    MessageBox.Show("Thêm thành công");
                    DataGridView_DSNXB.Rows.Clear();
                    QL_ThuVien.Tables["DSNXB"].Clear();
                    load_DSNXB();
                    btn_Luu_DSNXB.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm thất bại");
                }
            }
            else
            {
                //Lưu sau khi sửa
                try
                {
                    for (int i = 0; i < DataGridView_DSNXB.Rows.Count; i++)
                    {
                        QL_ThuVien.Tables["DSNXB"].Rows[i]["MaTacGia"] = DataGridView_DSNXB.Rows[i].Cells[0].Value.ToString();
                        QL_ThuVien.Tables["DSNXB"].Rows[i]["TenTacGia"] = DataGridView_DSNXB.Rows[i].Cells[1].Value.ToString();
                    }
                    SqlDataAdapter da_NXB = new SqlDataAdapter("select * from NXB", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da_NXB);
                    da_NXB.Update(QL_ThuVien, "DSNXB");
                    DataGridView_DSNXB.Rows.Clear();
                    QL_ThuVien.Tables["DSNXB"].Clear();
                    load_DSNXB();
                    MessageBox.Show("Sửa thành công");
                    btn_Luu_DSNXB.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sửa thất bại");
                }
            }
        }

        private void btn_Xoa_DSNXB_Click(object sender, EventArgs e)
        {
            try
            {
                string MaNXB = DataGridView_DSNXB.SelectedRows[0].Cells[0].Value.ToString();
                SqlDataAdapter da = new SqlDataAdapter("select * from NXB", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                int find = 0;
                foreach (DataRow row in QL_ThuVien.Tables["DSNXB"].Rows)
                {
                    if (row["MaNXB"].ToString() == MaNXB)
                        find = QL_ThuVien.Tables["DSNXB"].Rows.IndexOf(row);
                }

                QL_ThuVien.Tables["DSNXB"].Rows[find].Delete();
                da.Update(QL_ThuVien, "DSNXB");
                MessageBox.Show("Xoá thành công");
                DataGridView_DSNXB.Rows.Clear();
                QL_ThuVien.Tables["DSNXB"].Clear();
                load_DSNXB();
            }
            catch (Exception)
            {
                MessageBox.Show("Xoá thất bại");
            }
        }

        private void btn_Sach_Click(object sender, EventArgs e)
        {
            Hide_Panel();
            Panel_DSSACH.Visible = true;
        }

        private void DataGridView_DSS_SelectionChanged(object sender, EventArgs e)
        {
            if (DataGridView_DSS.SelectedRows.Count > 0)
            {
                txt_TenSach.Text = DataGridView_DSS.SelectedRows[0].Cells[1].Value.ToString();
                txt_SoLuong.Text = DataGridView_DSS.SelectedRows[0].Cells[6].Value.ToString();
                txt_Gia.Text = DataGridView_DSS.SelectedRows[0].Cells[7].Value.ToString();
                txt_NamXB.Text = DataGridView_DSS.SelectedRows[0].Cells[5].Value.ToString();

                conn.Open();
                SqlCommand cmd = new SqlCommand("select TenTacGia from TACGIA where MaTacGia = '" + DataGridView_DSS.SelectedRows[0].Cells[2].Value.ToString() + "'", conn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                    cbb_TG.Text = rd.GetString(0);
                conn.Close();

                conn.Open();
                SqlCommand cmd1 = new SqlCommand("select TenTheLoai from THELOAI where MaTheLoai = '" + DataGridView_DSS.SelectedRows[0].Cells[3].Value.ToString() + "'", conn);
                SqlDataReader rd1 = cmd1.ExecuteReader();
                if (rd1.Read())
                    cbb_TL.Text = rd1.GetString(0);
                conn.Close();

                conn.Open();
                SqlCommand cmd2 = new SqlCommand("select TenNXB from NXB where MaNXB = '" + DataGridView_DSS.SelectedRows[0].Cells[4].Value.ToString() + "'", conn);
                SqlDataReader rd2 = cmd2.ExecuteReader();
                if (rd2.Read())
                    cbb_NXB.Text = rd2.GetString(0);
                conn.Close();
            }
        }

        private void btn_Them_DSS_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from SACH", conn);
                SqlCommandBuilder built = new SqlCommandBuilder(da);

                conn.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from SACH", conn);
                int count = (int)cmd.ExecuteScalar() + 1;
                conn.Close();

                DataRow Insert_New = QL_ThuVien.Tables["DSSACH"].NewRow();
                Insert_New["MaSach"] = "S" + count;
                Insert_New["TenSach"] = txt_TenSach.Text;
                Insert_New["MaTacGia"] = cbb_TG.SelectedValue;
                Insert_New["MaTheLoai"] = cbb_TL.SelectedValue;
                Insert_New["MaNXB"] = cbb_NXB.SelectedValue;
                Insert_New["NamXB"] = txt_NamXB.Text;
                Insert_New["SoLuong"] = txt_SoLuong.Text;
                Insert_New["Gia"] = txt_Gia.Text;
                QL_ThuVien.Tables["DSSACH"].Rows.Add(Insert_New);
                da.Update(QL_ThuVien, "DSSACH");
                MessageBox.Show("Thêm thành công");
                DataGridView_DSS.Rows.Clear();
                QL_ThuVien.Tables["DSSACH"].Clear();
                load_DSS();

            }
            catch (Exception)
            {
                MessageBox.Show("Thêm thất bại");
            }
        }

        private void btn_Sua_DSS_Click(object sender, EventArgs e)
        {
            try
            {
                string MaSach = DataGridView_DSS.SelectedRows[0].Cells[0].Value.ToString();
                int find = 0;
                foreach (DataRow row in QL_ThuVien.Tables["DSSACH"].Rows)
                {
                    if (row["MaSach"] == MaSach)
                    {
                        find = QL_ThuVien.Tables["DSSACH"].Rows.IndexOf(row);
                    }
                }
                QL_ThuVien.Tables["DSSACH"].Rows[find]["TenSach"] = txt_TenSach.Text;
                QL_ThuVien.Tables["DSSACH"].Rows[find]["MaTacGia"] = cbb_TG.SelectedValue;
                QL_ThuVien.Tables["DSSACH"].Rows[find]["MaTheLoai"] = cbb_TL.SelectedValue;
                QL_ThuVien.Tables["DSSACH"].Rows[find]["MaNXB"] = cbb_NXB.SelectedValue;
                QL_ThuVien.Tables["DSSACH"].Rows[find]["NamXB"] = txt_NamXB.Text;
                QL_ThuVien.Tables["DSSACH"].Rows[find]["SoLuong"] = txt_SoLuong.Text;
                QL_ThuVien.Tables["DSSACH"].Rows[find]["Gia"] = txt_Gia.Text;

                SqlDataAdapter da = new SqlDataAdapter("select * from SACH", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                da.Update(QL_ThuVien, "DSSACH");
                MessageBox.Show("Sửa thành công");
                DataGridView_DSS.Rows.Clear();
                QL_ThuVien.Tables["DSSACH"].Clear();
                load_DSS();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sửa thất bại");
            }
        }

        private void btn_Xoa_DSS_Click(object sender, EventArgs e)
        {
            try
            {
                string MaSach = DataGridView_DSS.SelectedRows[0].Cells[0].Value.ToString();
                int find = 0;
                foreach (DataRow row in QL_ThuVien.Tables["DSSACH"].Rows)
                {
                    if (row["MaSach"] == MaSach)
                    {
                        find = QL_ThuVien.Tables["DSSACH"].Rows.IndexOf(row);
                    }
                }
                QL_ThuVien.Tables["DSSACH"].Rows[find].Delete();
                SqlDataAdapter da = new SqlDataAdapter("select * from SACH", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                da.Update(QL_ThuVien, "DSSACH");
                MessageBox.Show("Xoá thành công");
                DataGridView_DSS.Rows.Clear();
                QL_ThuVien.Tables["DSSACH"].Clear();
                load_DSS();
            }
            catch (Exception)
            {
                MessageBox.Show("Xoá thất bại");
            }
        }
    }
}
