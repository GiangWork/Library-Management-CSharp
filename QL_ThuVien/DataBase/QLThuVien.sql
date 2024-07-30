create database QLThuVien
go
use QLThuVien
go
--use master
-----Đổi kiểu nhập ngày tháng năm
set dateformat dmy
-----
create table ACCOUNT
(
	TenDangNhap nvarchar(50),
	MatKhau nvarchar(30),
	Email nvarchar(50),
	PRIMARY KEY (TenDangNhap)
)

create table NHANVIEN
(
	MaNV varchar(10),
	TenDangNhap nvarchar(50),
	HoTen nvarchar(50),
	GioiTinh nvarchar(5),
	NgaySinh date,
	DiaChi nvarchar(100),
	SDT char(10),
	PRIMARY KEY (MaNV),
	constraint FK_NHANVIEN_ACCOUNT foreign key (TenDangNhap) references ACCOUNT(TenDangNhap)
)


create table DOCGIA
(
	MaDocGia varchar(10),
	HoTen nvarchar(50),
	GioiTinh nvarchar(5),
	NgaySinh date,
	DiaChi nvarchar(100),
	SDT char(10),
	PRIMARY KEY (MaDocGia)
)

create table THELOAI
(
	MaTheLoai varchar(10),
	TenTheLoai nvarchar(50),

	PRIMARY KEY (MaTheLoai)
)

create table TACGIA
(
	MaTacGia varchar(10),
	TenTacGia nvarchar(50),

	PRIMARY KEY (MaTacGia)
)

create table NXB
(
	MaNXB varchar(10),
	TenNXB nvarchar(50),

	PRIMARY KEY (MaNXB)
)

create table SACH
(
	MaSach varchar(10),
	TenSach nvarchar(50),
	MaTacGia varchar(10),
	MaTheLoai varchar(10),
	MaNXB varchar(10),
	NamXB int,
	SoLuong int,
	Gia int,
	PRIMARY KEY (MaSach),
	constraint FK_SACH_THELOAI foreign key (MaTheLoai) references THELOAI (MaTheLoai),
	constraint FK_SACH_TACGIA foreign key (MaTacGia) references TACGIA (MaTacGia),
	constraint FK_SACH_NXB foreign key (MaNXB) references NXB (MaNXB)
)

create table MUONSACH
(
	MaDocGia varchar(10),
	MaSach varchar(10),
	NgayMuon date,
	NgayTra date,
	GhiChu nvarchar(100),
	PRIMARY KEY (MaDocGia, MaSach, NgayMuon),
	constraint FK_MUONSACH_DOCGIA foreign key (MaDocGia) references DOCGIA (MaDocGia),
	constraint FK_MUONSACH_SACH foreign key (MaSach) references SACH (MaSach)
)

--Tạo constraint
alter table ACCOUNT
add constraint DOCNHAT_ACCOUNT unique (Email)

alter table ACCOUNT
add constraint KT_Email check (Email like '%_@_%.__%')

alter table SACH
add constraint MD_SoLuong default 0 for SoLuong

alter table DOCGIA
add constraint DOCNHAT_DOCGIA unique (SDT)

alter table NHANVIEN
add constraint DOCNHAT_NHANVIEN unique (SDT)

-- Tạo trigger
GO
CREATE TRIGGER SLSACH_SAUKHIMUON
ON MUONSACH
AFTER INSERT
AS
BEGIN  
    UPDATE SACH
    SET SoLuong = SoLuong - 1
    FROM inserted, SACH
    WHERE inserted.MaSach = SACH.MaSach
END;
GO

GO
CREATE TRIGGER SLSACH_SAUKHITRA
ON MUONSACH
AFTER UPDATE
AS
BEGIN  
    UPDATE SACH
    SET SoLuong = SoLuong + 1
    FROM inserted, SACH
    WHERE inserted.MaSach = SACH.MaSach and NgayTra is not null
END;
GO

--Nhập dữ liệu vào bảng
insert into ACCOUNT values
('Admin', 'Admin', N'Admin@gmail.com'),
('nguyenhuuhoanghieu', '123456', N'Hieu123@gmail.com'),
('TranThanhNha', '123456', N'Nha12$@gmail.com'),
('PhamVanChieu', '123456', N'VanChieu2003@gmail.com'),
('Nguyenminhhoang', '123456', N'Dinner38@gmail.com'),
('NguyenDinhHai', '123456', N'DinhHai@gmail.com')
--
insert into NHANVIEN values
('NV1','TranThanhNha',N'Trần Thanh Nhã', N'Nữ','12-03-2006',N'510 Lê Quang Định', '0654152334'),
('NV2','phamVanChieu',N'Phạm Văn Chiêu', 'Nam','07-08-2004',N'20 Trường Chinh', '0654152333'),
('NV3','Nguyenminhhoang',N'Nguyễn Minh Hoàng', 'Nam','09-04-2003',N'20 Phạm Ngũ Lão', '0654152332'),
('NV4','NguyenDinhHai',N'Nguyễn Đình Hải', 'Nam','04-09-2000',N'201 Lý Thánh Tông', '0654152331'),
('NV5','nguyenhuuhoanghieu',N'Nguyễn Hữu Hoàng Hiếu', N'Nam','01-12-2001',N'74 Lê Trọng Tấn', '0654152330')
----
insert into DOCGIA values
('DG1',N'Lê Hoàng Nam','Nam','12-03-2006',N'510 Lê Quang Định', '0354152334'),
('DG2',N'Nguyễn Văn Châu',N'Nữ','07-08-2004',N'20 Trường Chinh', '0354152333'),
('DG3',N'Lê Thị Kiều Diễm', N'Nữ','09-04-2003',N'20 Phạm Ngũ Lão', '0354152332'),
('DG4',N'Hoàng Đăng Trí','Nam','04-09-2000',N'201 Lý Thánh Tông', '0354152331'),
('DG5',N'Kim Gia Ngô','Nam','01-12-2001',N'74 Lê Trọng Tấn', '0354152330')
----
insert into THELOAI values
('TL1', N'Giáo trình'),
('TL2', N'Chính trị - Pháp luật'),
('TL3', N'Khoa học công nghệ - Kinh tế'),
('TL4', N'Văn hoá xã hội - Lịch sử'),
('TL5', N'Văn học nghệ thuật'),
('TL6', N'Truyện, tiểu thuyết'),
('TL7', N'Tâm lý, tâm linh, tôn giáo'),
('TL8', N'Sách thiếu nhi')
----
insert into TACGIA values
('TG1', N'Trần Ngọc Dũng'),
('TG2', N'Phan Khôi'),
('TG3', N'Yuval Noah Harari'),
('TG4', N'Erik Brynjolfsson và Andrew McAfee'),
('TG5', N'Nguyễn Nhật Ánh'),
('TG6', N'Nguyễn Huy Thiệp'),
('TG7', N'Nguyễn Du'),
('TG8', N'Virginia Woolf'),
('TG9', N'Lê Hữu Kiệt'),
('TG10', N'Trần Trọng Kim'),
('TG11', N'Phạm Công Thiện'),
('TG12', N'Trần Văn Hoàng'),
('TG13', N'Nguyễn Thị Mai Hoa'),
('TG14', N'J.K. Rowling'),
('TG15', N'Paulo Coelho'),
('TG16', N'Sigmund Freud'),
('TG17', N'Đào Duy Anh'),
('TG18', N'Mark Twain'),
('TG19', N'Michael Bond'),
('TG20', N'Fujiko F. Fujio')
----
insert into NXB values
('NXB1', N'NXB Chính Trị Quốc Gia'),
('NXB2', N'NXB Giáo dục'),
('NXB3', N'NXB Lao Động'),
('NXB4', N'NXB Lao Động - Xã Hội'),
('NXB5', N'Nhã Nam'),
('NXB6', N'NXB Trẻ'),
('NXB7', N'Nhiều NXB khác nhau'),
('NXB8', N'NXB Văn Hóa Thông Tin'),
('NXB9', N'NXB Văn Hóa Dân Tộc'),
('NXB10', N'NXB Thông Tin và Truyền Thông'),
('NXB11', N'Harper Voyager'),
('NXB12', N'NXB Kim Đồng')
----
insert into SACH values
('S1', N'Pháp Luật và Đời Sống Xã Hội ở Việt Nam', 'TG1', 'TL2', 'NXB1', 2007, 3, 20000),
('S2', N'Lược Sử Chính Trị Việt Nam', 'TG2', 'TL2', 'NXB2', 2016, 3, 20000),
('S3', N'Sapiens: Lược Sử Loài Người', 'TG3', 'TL3', 'NXB3', 2016, 3, 20000),
('S4', N'Kỹ Thuật Số', 'TG4', 'TL3', 'NXB4', 2015, 3, 20000),
('S5', N'Mắt Biếc', 'TG5', 'TL5', 'NXB5', 1990, 3, 20000),
('S6', N'Nhật Ký Lão Rợ', 'TG6', 'TL5', 'NXB6', 1992, 3, 20000),
('S7', N'Truyện Kiều', 'TG7', 'TL5', 'NXB7', 1820, 3, 20000),
('S8', N'The Wave', 'TG8', 'TL5', 'NXB7', 1931, 3, 20000),
('S9', N'Sống Ở Đà Nẵng Những Năm 1990', 'TG9', 'TL4', 'NXB8', 2019, 3, 20000),
('S10', N'Lịch Sử Việt Nam Từ Gốc Đến Nguyễn', 'TG10', 'TL4', 'NXB2', 1955, 3, 20000),
('S11', N'Văn Hóa Việt Nam: Nhìn Từ Nước Ngoài', 'TG11', 'TL4', 'NXB9', 2002, 3, 20000),
('S12', N'Giáo Trình Java Cơ Bản và Nâng Cao', 'TG12', 'TL1', 'NXB10', 2018, 3, 20000),
('S13', N'Giáo Trình Tiếng Anh Giao Tiếp', 'TG13', 'TL1', 'NXB2', 2013, 3, 20000),
('S14', N'Harry Potter và Hòn Đá Phù Thủy', 'TG14', 'TL6', 'NXB6', 1977, 3, 20000),
('S15', N'Người Tình Ánh Trăng', 'TG5', 'TL6', 'NXB5', 1955, 3, 20000),
('S16', N'Nhà Giả Kim', 'TG15', 'TL6', 'NXB8', 1988, 3, 20000),
('S17', N'Kiến Thức Đau Khổ', 'TG16', 'TL7', 'NXB3', 2016, 3, 20000),
('S18', N'Cây Cầu Tâm Linh', 'TG17', 'TL7', 'NXB5', 2015, 3, 20000),
('S19', N'Cuộc Phiêu Lưu Của Tom Sawyer', 'TG18', 'TL8', 'NXB7', 1876, 3, 20000),
('S20', N'Gấu Paddington series', 'TG19', 'TL8', 'NXB11', 1958, 3, 20000),
('S21', N'Doraemon series', 'TG20', 'TL8', 'NXB12', 1969, 3, 20000)
---
insert into MUONSACH (MaDocGia, MaSach, NgayMuon) values
('DG1', 'S1','12-01-2023'),
('DG1', 'S2','12-01-2023')
---
