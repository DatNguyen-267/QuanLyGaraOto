-- Khoi tao database
GO
create database GARA;
GO
use GARA;
GO
-- drop database GARA;
---------------------DON HANG CUA KHACH HANG-----------------------------------

-- Thiet lap danh sach cac hang xe
create table CARBRAND
(
    Id int primary key not NULL IDENTITY(1, 1),
    CarBrand_Name varchar(max) not NULL
);
GO

-- Thiet lap danh sach khach hang
create table CUSTOMER
(
    Id int primary key not NULL IDENTITY(1, 1),
    Customer_Name varchar(100) not NULL,
    Customer_Address varchar(max),
    Customer_Phone varchar(20) not NULL
);
GO

-- Thiet lap danh sach loai tien cong
create table PAY
(
    Id int primary key not NULL IDENTITY(1, 1),
    Pay_Name varchar(1000) not NULL,
    Pay_Price int not NULL
);
GO

-- Thiet lap danh sach cac dich vu tais gara

-- Thiet lap danh sach vat tu, phu tung
create table SUPPLIES
(
    Id int IDENTITY(1, 1) primary key not NULL,
    Supplies_Name varchar(1000) not NULL,
    Supplies_Price int,
    Supplies_Amount int
);
GO

-- Thiet lap trang thai xe
create table CARSTATUS
(
    Id int primary key not NULL IDENTITY(1, 1),
    Status_Name varchar(200) not NULL
);
GO

-- Thiet lap phieu tiep nhan xe
create table CARRECEPTION
(
    Id int primary key not NULL IDENTITY(1, 1),
    IdCustomer int not NULL,
    LicensePlate varchar(50) not NULL,
    IdBrand int not NULL,
    ReceptionDate date not NULL,
	IdStatus int not null,
    constraint FK_CARRECEPTION_CUSTOMER foreign key (IdCustomer) references CUSTOMER(Id),
    constraint FK_CARRECEPTION_CARBRAND foreign key (IdBrand) references CARBRAND(Id),
	constraint FK_CARRECEPTION_CARSTATUS foreign key (IdStatus) references CARSTATUS(Id)
);
GO

-- Thiet lap phieu sua chua
create table REPAIRFORM
(
    Id int primary key not NULL IDENTITY(1, 1),
    IdCarReception int not NULL,
    RepairDate date not NULL,
	constraint FK_REPAIRFORM_CARRECEPTION foreign key (IdCarReception) references CARRECEPTION(Id),
);
GO
-- Thiet lap danh sach noi dung sua chua
create table REPAIRINFO 
(
	Id int identity(1,1) not null primary key,
	IdRepairForm int not null,
	Content varchar(max),
    IdSupply int,
    SuppliesAmount int,
    IdPay int not NULL,
    TotalMoney int not NULL,
	constraint FK_REPAIRINFO_SUPPLIES foreign key (IdSupply) references SUPPLIES(Id),
    constraint FK_REPAIRINFO_PAY foreign key (IdPay) references PAY(Id),
	constraint FK_REPAIRINFO_REPAIRFORM foreign key (IdRepairForm) references REPAIRFORM(Id)
)
GO
-- Thiet lap danh sach thong tin cua gara
create table GARAINFO
(
	Id int primary key identity (1,1) not null,
	MaxCar int,
	MaxCarReception int,
	GaraInfo_Phone varchar(20),
	GaraInfo_Email varchar(200),
	GaraInfo_Address varchar(max),
);
Go
-- Thiet lap danh sach cac hoa don -> doanh so cua thang
create table RECEIPT
(
	Id int primary key identity(1,1) not null,
    IdCarReception int not NULL,
    IdGaraInfo int not null,
    ReceptDate date not NULL,
    TotalMoney int not NULL,
    constraint FK_RECEIPT_CARRECEPTION foreign key (IdCarReception) references CARRECEPTION(Id),
	constraint FK_RECEIPT_GARAINFO foreign key (IdGaraInfo) references GARAINFO(Id)
);
GO

-- Thiet lap bao cao ton

--------------------NHAN SU CUA GARA--------------------------------

-- Thiet lap danh sach cac role bao gom staff va admin
create table ROLE
(
	Id int identity(1,1) primary key,
	Role_Name nvarchar(max),
)
go
-- Thiet lap danh sach tai khoan cua nguoi dung
create table USERS
(
    Id int primary key not NULL IDENTITY(1, 1),
    UserName varchar(100) not NULL,
    Password varchar(100) not NULL,
    IdRole int not NULL,
	IsActive int,
	foreign key(IdRole) references ROLE(Id),
);
GO
-- Thiet lap thong tin cua nguoi dung
create table USERINFO
(
    Id int primary key not NULL IDENTITY(1, 1),
	IdUser int,
    UserInfo_Name varchar(200) not NULL,
    UserInfo_Address varchar(max),
    UserInfo_BirthDate date,
    UserInfo_Telephone varchar(20) not NULL,
    UserInfo_CMND varchar(100) not NULL,
    constraint FK_USERINFO_USERS foreign key (IdUser) references USERS(Id)
);

insert into ROLE(Role_Name) values ('admin');
insert into USERS(UserName,Password,IdRole) values ('admin','db69fc039dcbd2962cb4d28f5891aae1',1);