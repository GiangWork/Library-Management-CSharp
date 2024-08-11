# App quản lý thư viện
## Công nghệ sử dụng
Winform, GunaUI
## Cài đặt
1. **Cài đặt Visual Studio 2022**
2. **Import `sql file` trong thư mục `Database`**
3. **Chỉnh sửa file `App.config`**
    ```XML
    <?xml version="1.0" encoding="utf-8" ?>
    <configuration>
	<!-- Chỉnh sửa lại connectionString phù hợp -->
	<connectionStrings>
		<add name="connect" connectionString="Data Source=DESKTOP-1HLJNV7;Initial Catalog=QLThuVien;Integrated Security=True;TrustServerCertificate=True"/>
	</connectionStrings>
    </configuration>
    ```
## Tính năng
### Admin
- Thống kê
- Quản lý tài khoản nhân viên (Thêm, sửa, xoá)
- Quản lý đọc giả (Thêm, sửa, xoá)
- Quản lý sách (Thêm, sửa, xoá)
- Quản lý thể loại (Thêm, sửa, xoá)
- Quản lý tác giả (Thêm, sửa, xoá)
- Quản lý nhà xuất bản (Thêm, sửa, xoá)
### Nhân viên
- Đăng nhập
- Xem thông tin tài khoản
- Mượn, trả sách
- Thu phí
- Đăng ký tài khoản cho đọc giả
## Admin account
- Username: Admin
- Password: Admin
## Hình ảnh
- Sơ đồ database
![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20220737.png)
- Đăng nhập
![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20222108.png)
- Thống kê
![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20222149.png)
- Quản lý dữ liệu hệ thống
![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20222210.png)
![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20222228.png)
![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20222243.png)
![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20222305.png)
![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20222328.png)
![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20222341.png)
- Thông tin tài khoản
![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20222412.png)
- Mượn trả sách
![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20222426.png)
- Thu phí
![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20222438.png)
- Đăng ký tài khoản đọc giả

![ScreenShot](QL_ThuVien/ScreenShot/Screenshot%202024-08-11%20222453.png)
