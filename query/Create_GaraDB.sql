-- Khoi tao database
GO
create database GARA;
GO
use GARA;
GO
---------------------DON HANG CUA KHACH HANG-----------------------------------

-- Thiet lap danh sach cac hang xe
create table CarBrand
(
    Id int primary key not NULL IDENTITY(1, 1),
    Name varchar(max) not NULL
);
GO

-- Thiet lap danh sach khach hang
create table Customer
(
    Id int primary key not NULL IDENTITY(1, 1),
    Name varchar(100) not NULL,
    Address varchar(max),
    Telephone varchar(20) not NULL
);
GO

-- Thiet lap danh sach loai tien cong
create table Pay
(
    Id int primary key not NULL IDENTITY(1, 1),
    Name varchar(1000) not NULL,
    Price int not NULL
);
GO

-- Thiet lap danh sach cac dich vu tai gara

-- Thiet lap danh sach vat tu, phu tung
create table Supply
(
    Id int IDENTITY(1, 1) primary key not NULL,
    Name varchar(1000) not NULL,
    Price int,
    Amount int
);
GO

-- Thiet lap trang thai xe
create table CarStatus
(
    Id int primary key not NULL IDENTITY(1, 1),
    Name varchar(200) not NULL
);
GO

-- Thiet lap phieu tiep nhan xe
create table CarReception
(
    Id int primary key not NULL IDENTITY(1, 1),
    IdCustomer int not NULL,
    LicensePlate varchar(50) not NULL,
    IdBrand int not NULL,
    ReceptionDate datetime not NULL,
	IdStatus int not null,
    constraint FK_CarReception_Customer foreign key (IdCustomer) references Customer(Id),
    constraint FK_CarReception_CarBrand foreign key (IdBrand) references CarBrand(Id),
	constraint FK_CarReception_CarStatus foreign key (IdStatus) references CarStatus(Id)
);
GO

-- Thiet lap phieu sua chua
create table RepairForm
(
    Id int primary key not NULL IDENTITY(1, 1),
    IdCarReception int not NULL,
    RepairDate datetime not NULL,
	constraint FK_RepairForm_CarReception foreign key (IdCarReception) references CarReception(Id),
);
GO
-- Thiet lap danh sach noi dung sua chua
create table RepairInfo 
(
	Id int identity(1,1) not null primary key,
	IdRepairForm int not null,
	Content varchar(max) not NULL,
    IdSupply int,
    SuppliesAmount int,
    IdPay int not NULL,
    TotalMoney int not NULL,
	constraint FK_RepairInfo_Supply foreign key (IdSupply) references Supply(Id),
    constraint FK_RepairInfo_Pay foreign key (IdPay) references Pay(Id),
	constraint FK_RepairInfo_RepairForm foreign key (IdRepairForm) references RepairForm(Id)
)
GO
-- drop table REPAIR_TICKET;

-- Thiet lap danh sach cac xe dang trong qua trinh thu no
--create table CAR_IN_DEBT
--(
--    License_ID int primary key not null,
--    Debt int,
--    constraint FK_CARLIST_LICENSEPLATE foreign key (License_ID) references LICENSE_PLATE(ID)
--);
-- drop table CAR_IN_DEBT;

-- Thiet lap danh sach thong tin cua gara
create table GaraInfo
(
	Id int primary key identity (1,1) not null,
	MaxCar int,
	MaxCarReception int,
	Phone varchar(20),
	Email varchar(200),
	Address varchar(max),
);
Go
-- Thiet lap danh sach cac hoa don -> doanh so cua thang
create table Recept
(
	Id int primary key identity(1,1) not null,
    IdCarReception int not NULL,
    IdGaraInfo int not null,
    ReceptDate datetime not NULL,
    TotalMoney int not NULL,
    constraint FK_Recept_CarReception foreign key (IdCarReception) references CarReception(Id),
	constraint FK_Recept_GaraInfo foreign key (IdGaraInfo) references GaraInfo(Id)
);
GO

-- Thiet lap bao cao ton

--------------------NHAN SU CUA GARA--------------------------------

-- Thiet lap danh sach cac role bao gom staff va admin
create table UserRole
(
	Id int identity(1,1) primary key,
	Name nvarchar(max),
)
go
-- Thiet lap danh sach tai khoan cua nguoi dung
create table Users
(
    Id int primary key not NULL IDENTITY(1, 1),
    UserName varchar(100) not NULL,
    Password varchar(100) not NULL,
    IdRole int not NULL,
	foreign key(IdRole) references UserRole(Id),
);
GO
-- Thiet lap thong tin cua nguoi dung
create table UserInfo
(
    Id int primary key not NULL IDENTITY(1, 1),
	IdUser int not null,
    Name varchar(200) not NULL,
    Address varchar(max),
    BirthDate datetime,
    Telephone varchar(20) not NULL,
    CMND varchar(100) not NULL,
    constraint FK_UserInfo_Users foreign key (IdUser) references Users(Id)
);


