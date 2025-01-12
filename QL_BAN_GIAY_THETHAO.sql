CREATE DATABASE QL_BAN_GIAY_THETHEO
GO
USE QL_BAN_GIAY_THETHEO
GO


CREATE TABLE SANPHAM
(
	MASP CHAR(10) NOT NULL PRIMARY KEY,
	TENSP NVARCHAR(50) NOT NULL,
	MOTASANPHAM NVARCHAR(50),
	GIASP FLOAT,
	SOLUONGTRONGKHO INT,
	MANHACUNGCAP CHAR(10) NOT NULL,
	HINHANHSP VARCHAR(50) NOT NULL,
)
GO
CREATE TABLE DONHANG
(
	MADH CHAR(10) NOT NULL PRIMARY KEY,
	MANGUOIDUNG CHAR(10) NOT NULL,
	
	NGAYDATHANG DATE,
	TONGTIEN FLOAT
)
GO
CREATE TABLE CHITIETDONHANG
(
	MACTDH CHAR(10) NOT NULL PRIMARY KEY,
	MADH CHAR(10) NOT NULL,
	MASP CHAR(10) NOT NULL,
	SOLUONG INT,


)
GO
CREATE TABLE NHACUNGCAP
(
	MANHACUNGCAP CHAR(10) NOT NULL PRIMARY KEY,
	TENNHACUNGCAP NVARCHAR(50) NOT NULL,
	DIACHI NVARCHAR(50),
	SDT CHAR(11),
	EMAIL NVARCHAR(50)
)
GO
CREATE TABLE NGUOIDUNG
(
	MANGUOIDUNG CHAR(10) NOT NULL PRIMARY KEY,
	TENNGUOIDUNG NVARCHAR(50) NOT NULL,
	SDT CHAR(10),
	EMAIL NVARCHAR(50),
	MATKHAU NVARCHAR(50),
	MAQUYEN INT,

)
GO
CREATE TABLE PHANQUYEN
(
	MAQUYEN INT PRIMARY KEY,
	TENQUYEN NVARCHAR(50)
)
GO
ALTER TABLE DONHANG
ADD FOREIGN KEY(MANGUOIDUNG) REFERENCES NGUOIDUNG(MANGUOIDUNG)
GO
ALTER TABLE NGUOIDUNG 
ADD FOREIGN KEY(MAQUYEN) REFERENCES PHANQUYEN(MAQUYEN)
GO
ALTER TABLE CHITIETDONHANG
ADD FOREIGN KEY(MADH) REFERENCES DONHANG(MADH) ON DELETE CASCADE
GO
ALTER TABLE CHITIETDONHANG
ADD FOREIGN KEY(MASP) REFERENCES SANPHAM(MASP) ON DELETE CASCADE
GO
ALTER TABLE SANPHAM
ADD FOREIGN KEY(MANHACUNGCAP) REFERENCES NHACUNGCAP(MANHACUNGCAP)



GO
INSERT INTO NHACUNGCAP(MANHACUNGCAP, TENNHACUNGCAP, DIACHI, SDT, EMAIL)
VALUES 
('NCC01', 'Cong ty TNHH ABC', '123 Le Loi, Q1, TP.HCM', '0123456789', 'abc@gmail.com'),
('NCC02', 'Cong ty TNHH DEF', '456 Nguyen Hue, Q1, TP.HCM', '0123456790', 'def@gmail.com'),
('NCC03', 'Cong ty TNHH GHI', '789 Hai Ba Trung, Q1, TP.HCM', '0123456791', 'ghi@gmail.com'),
('NCC04', 'Cong ty TNHH JKL', '321 Le Lai, Q1, TP.HCM', '0123456792', 'jkl@gmail.com'),
('NCC05', 'Cong ty TNHH MNO', '654 Tran Hung Dao, Q1, TP.HCM', '0123456793', 'mno@gmail.com');
GO
INSERT INTO SANPHAM(MASP, TENSP, MOTASANPHAM, GIASP, SOLUONGTRONGKHO, MANHACUNGCAP,HINHANHSP)
VALUES 
('SP01', 'Nike Aiir Force 1', 'Mau den, size 42', 500000, 10, 'NCC01',N'nikeairforce1.jpg'),
('SP02', 'Nike Air Max', 'Mau trang, size 43', 600000, 15, 'NCC02',N'NikeAirMax.jpg'),
('SP03', 'Nike Air Max 95', 'Mau xanh, size 44', 700000, 20, 'NCC03',N'NikeAirMax95.jpg'),
('SP04', 'Nike Air Max Plus', 'Mau do, size 45', 800000, 25, 'NCC04',N'NikeAirMaxPlus.jpg'),
('SP05', 'Nike Zoom Fly', 'Mau vang, size 46', 900000, 30, 'NCC05',N'NikeZoomFly.jpg'),
('SP06', 'Nike Aiir Force 1', 'Mau den, size 42', 500000, 10, 'NCC01',N'nikeairforce1.jpg'),
('SP07', 'Nike Air Max', 'Mau trang, size 43', 600000, 15, 'NCC02',N'NikeAirMax.jpg'),
('SP08', 'Nike Air Max 95', 'Mau xanh, size 44', 700000, 20, 'NCC03',N'NikeAirMax95.jpg'),
('SP09', 'Nike Air Max Plus', 'Mau do, size 45', 800000, 25, 'NCC04',N'NikeAirMaxPlus.jpg'),
('SP10', 'Nike Zoom Fly', 'Mau vang, size 46', 900000, 30, 'NCC05',N'NikeZoomFly.jpg');
GO
GO
INSERT INTO PHANQUYEN(MAQUYEN, TENQUYEN)
VALUES(1,'Admin'),
(2,N'User')
GO
INSERT INTO NGUOIDUNG(MANGUOIDUNG, TENNGUOIDUNG, SDT, EMAIL, MATKHAU, MAQUYEN)
VALUES 
('ND01', 'Nguyen Van F', '0123456789', 'Admin@gmail.com','12345',1),
('ND02', 'Tran Thi G', '0123456790', 'ttg@gmail.com','123',2),
('ND03', 'Le Van H',  '0123456791', 'lvh@gmail.com','123',2),
('ND04', 'Pham Thi I', '0123456792', 'pti@gmail.com','123',2),
('ND05', 'Hoang Van J', '0123456793', 'hvj@gmail.com','123',2);
GO
INSERT INTO DONHANG(MADH, MANGUOIDUNG, NGAYDATHANG, TONGTIEN)
VALUES 
('DH01',  'ND02', '2023-11-01', 1000000),
('DH02',  'ND02', '2023-11-02', 2000000),
('DH03',  'ND03', '2023-11-03', 3000000),	
('DH04',  'ND04', '2023-11-04', 4000000),
('DH05',  'ND05', '2023-11-05', 5000000);
GO
INSERT INTO CHITIETDONHANG(MACTDH, MADH, MASP, SOLUONG)
VALUES 
('CT01', 'DH01', 'SP01', 2),
('CT02', 'DH02', 'SP02', 3),
('CT03', 'DH03', 'SP03', 4),
('CT04', 'DH04', 'SP04', 5),
('CT05', 'DH05', 'SP05', 6);
GO
SELECT *FROM [dbo].[CHITIETDONHANG]
SELECT *FROM [dbo].[DONHANG]
SELECT *FROM [dbo].[NGUOIDUNG]
SELECT *FROM [dbo].[NHACUNGCAP]
SELECT *FROM [dbo].[SANPHAM]
SELECT *FROM [dbo].[NGUOIDUNG]
SELECT *FROM [dbo].[PHANQUYEN]