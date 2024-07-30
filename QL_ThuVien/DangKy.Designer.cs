namespace QL_ThuVien
{
    partial class DangKy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            txt_SDT = new TextBox();
            txt_HoTen = new TextBox();
            txt_DiaChi = new TextBox();
            radio_Nam = new RadioButton();
            radio_Nu = new RadioButton();
            mtxt_NgaySinh = new MaskedTextBox();
            btn_DangKy = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(224, 19);
            label1.Name = "label1";
            label1.Size = new Size(151, 46);
            label1.TabIndex = 0;
            label1.Text = "Đăng ký";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(110, 387);
            label4.Name = "label4";
            label4.Size = new Size(35, 20);
            label4.TabIndex = 3;
            label4.Text = "SDT";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(90, 111);
            label5.Name = "label5";
            label5.Size = new Size(54, 20);
            label5.TabIndex = 4;
            label5.Text = "Họ tên";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(80, 176);
            label6.Name = "label6";
            label6.Size = new Size(65, 20);
            label6.TabIndex = 5;
            label6.Text = "Giới tính";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(71, 237);
            label7.Name = "label7";
            label7.Size = new Size(74, 20);
            label7.TabIndex = 6;
            label7.Text = "Ngày sinh";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(90, 316);
            label8.Name = "label8";
            label8.Size = new Size(55, 20);
            label8.TabIndex = 7;
            label8.Text = "Địa chỉ";
            // 
            // txt_SDT
            // 
            txt_SDT.Location = new Point(161, 383);
            txt_SDT.Multiline = true;
            txt_SDT.Name = "txt_SDT";
            txt_SDT.Size = new Size(284, 33);
            txt_SDT.TabIndex = 10;
            txt_SDT.Leave += txt_SDT_Leave;
            // 
            // txt_HoTen
            // 
            txt_HoTen.Location = new Point(161, 107);
            txt_HoTen.Multiline = true;
            txt_HoTen.Name = "txt_HoTen";
            txt_HoTen.Size = new Size(284, 33);
            txt_HoTen.TabIndex = 11;
            // 
            // txt_DiaChi
            // 
            txt_DiaChi.Location = new Point(161, 312);
            txt_DiaChi.Multiline = true;
            txt_DiaChi.Name = "txt_DiaChi";
            txt_DiaChi.Size = new Size(284, 33);
            txt_DiaChi.TabIndex = 12;
            // 
            // radio_Nam
            // 
            radio_Nam.AutoSize = true;
            radio_Nam.Location = new Point(161, 173);
            radio_Nam.Name = "radio_Nam";
            radio_Nam.Size = new Size(62, 24);
            radio_Nam.TabIndex = 13;
            radio_Nam.TabStop = true;
            radio_Nam.Text = "Nam";
            radio_Nam.UseVisualStyleBackColor = true;
            // 
            // radio_Nu
            // 
            radio_Nu.AutoSize = true;
            radio_Nu.Location = new Point(255, 173);
            radio_Nu.Name = "radio_Nu";
            radio_Nu.Size = new Size(50, 24);
            radio_Nu.TabIndex = 14;
            radio_Nu.TabStop = true;
            radio_Nu.Text = "Nữ";
            radio_Nu.UseVisualStyleBackColor = true;
            // 
            // mtxt_NgaySinh
            // 
            mtxt_NgaySinh.Location = new Point(161, 233);
            mtxt_NgaySinh.Mask = "00/00/0000";
            mtxt_NgaySinh.Name = "mtxt_NgaySinh";
            mtxt_NgaySinh.Size = new Size(284, 27);
            mtxt_NgaySinh.TabIndex = 15;
            mtxt_NgaySinh.ValidatingType = typeof(DateTime);
            // 
            // btn_DangKy
            // 
            btn_DangKy.Location = new Point(224, 448);
            btn_DangKy.Name = "btn_DangKy";
            btn_DangKy.Size = new Size(134, 45);
            btn_DangKy.TabIndex = 16;
            btn_DangKy.Text = "Đăng ký";
            btn_DangKy.UseVisualStyleBackColor = true;
            btn_DangKy.Click += btn_DangKy_Click;
            // 
            // DangKy
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(587, 528);
            Controls.Add(radio_Nu);
            Controls.Add(radio_Nam);
            Controls.Add(btn_DangKy);
            Controls.Add(mtxt_NgaySinh);
            Controls.Add(txt_DiaChi);
            Controls.Add(txt_HoTen);
            Controls.Add(txt_SDT);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label1);
            Name = "DangKy";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng ký";
            FormClosing += DangKy_FormClosing;
            Load += DangKy_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox txt_SDT;
        private TextBox txt_HoTen;
        private TextBox txt_DiaChi;
        private RadioButton radio_Nam;
        private RadioButton radio_Nu;
        private MaskedTextBox mtxt_NgaySinh;
        private Button btn_DangKy;
    }
}