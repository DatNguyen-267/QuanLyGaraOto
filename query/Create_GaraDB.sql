-- Khoi tao database
GO
create database GARA;
GO
use GARA;
GO
-- drop database GARA;
---------------------DON HANG CUA KHACH HANG-----------------------------------

-- Thiet lap danh sach cac hang xe
create table CAR_BRAND
(
    CarBrand_Id int primary key not NULL IDENTITY(1, 1),
    CarBrand_Name nvarchar(max) not NULL
);
GO

-- Thiet lap danh sach khach hang
create table CUSTOMER
(
    Customer_Id int primary key not NULL IDENTITY(1, 1),
    Customer_Name nvarchar(100) not NULL,
    Customer_Address nvarchar(max),
    Customer_Phone nvarchar(20) not NULL
);
GO

-- Thiet lap danh sach loai tien cong
create table WAGE
(
    Wage_Id int primary key not NULL IDENTITY(1, 1),
    Wage_Name nvarchar(1000) not NULL,
    Wage_Value int not NULL
);
GO

-- Thiet lap danh sach cac dich vu tais gara

-- Thiet lap danh sach vat tu, phu tung
create table SUPPLIES
(
    Supplies_Id int IDENTITY(1, 1) primary key not NULL,
    Supplies_Name nvarchar(1000) not NULL,
    Supplies_Price int,
    Supplies_Amount int
);
GO
create table IMPORT_GOODS
(
    ImportGoods_Id int IDENTITY(1, 1) primary key not NULL,
    ImportGoods_Amount int not null,
	ImportGoods_Date date not null,
	ImportGoods_Price int not null,
	ImportGoods_Total int not null,
	IdSupplies int not null,

	constraint FK_IMPORTGOODS_SUPPLIES foreign key (IdSupplies) references SUPPLIES(Supplies_Id)
);
GO
create table INVENTORY_REPORT
(
	InventoryReport_Id int IDENTITY(1, 1) primary key not NULL,
	InventoryReport_Date date not null,
);
GO
create table INVENTORY_REPORT_DETAIL
(
	InventoryReportDetail_Id int IDENTITY(1, 1) primary key not NULL,
	IdInventoryReport int not null,
	IdSupplies int not NULL,
	TonDau int not null,
	PhatSinh int not null,
	TonCuoi int not null,
	constraint FK_INVENTORYREPORTDETAIL_INVENTORYREPORT foreign key (IdInventoryReport) references INVENTORY_REPORT(InventoryReport_Id),
	constraint FK_INVENTORYREPORTDETAIL_SUPPLIES foreign key (IdSupplies) references SUPPLIES(Supplies_Id)
);
GO
create table SALES_REPORT
(
	SalesReport_Id int IDENTITY(1, 1) primary key not NULL,
	SalesReport_Revenue int not null,
	SalesReport_Date date not null,
);
GO
create table SALES_REPORT_DETAIL
(
	SalesReportDetail_Id int IDENTITY(1, 1) primary key not NULL,
	IdSalesReport int not null,
    IdBrand int not NULL,
    AmountOfTurn int,
    TotalMoney int,
    Rate float,
	constraint FK_SALESREPORTDETAIL_SALESREPORT foreign key (IdSalesReport) references SALES_REPORT(SalesReport_Id),
	constraint FK_SALESREPORTDETAIL_CARBRAND foreign key (IdBrand) references CAR_BRAND(CarBrand_Id)
);
GO
-- Thiet lap trang thai xe
create table CAR_STATUS
(
    CarStatus_Id int primary key not NULL IDENTITY(1, 1),
    CarStatus_Name nvarchar(200) not NULL
);
GO

-- Thiet lap phieu tiep nhan xe
create table RECEPTION
(
    Reception_Id int primary key not NULL IDENTITY(1, 1),   
	LicensePlate nvarchar(50) not NULL,
    ReceptionDate date not NULL,
	IdCarBrand int not NULL,
	IdCustomer int not NULL,
    constraint FK_RECEPTION_CUSTOMER foreign key (IdCustomer) references CUSTOMER(Customer_Id),
    constraint FK_RECEPTION_CARBRAND foreign key (IdCarBrand) references CAR_BRAND(CarBrand_Id),
	
);
GO
--create table CAR
--(
--    Car_Id int primary key not NULL IDENTITY(1, 1),   
--	Debt int not null,
--    IdStatus int not null,
--	IdReception int not null,
--	constraint FK_CAR_RECEPTION foreign key (IdReception) references Reception(Reception_Id),
--	constraint FK_CAR_CARSTATUS foreign key (IdStatus) references CAR_STATUS(CarStatus_Id)
--);
GO
-- Thiet lap phieu sua chua
create table REPAIR
(
    Repair_Id int primary key not NULL IDENTITY(1, 1),
    IdReception int not NULL,
    RepairDate date not NULL,
	constraint FK_REPAIRFORM_CARRECEPTION foreign key (IdReception) references RECEPTION(Reception_Id),
);
GO
-- Thiet lap danh sach noi dung sua chua
create table REPAIR_DETAIL 
(
	RepairDetail_Id int identity(1,1) not null primary key,
	Content nvarchar(max),
	TotalMoney int not NULL,
	SuppliesPrice int,
    SuppliesAmount int,
    IdWage int not NULL,
	IdRepair int not null,
	IdSupplies int,
	constraint FK_REPAIRDETAIL_SUPPLIES foreign key (IdSupplies) references SUPPLIES(Supplies_Id),
    constraint FK_REPAIRDETAIL_WAGE foreign key (IdWage) references WAGE(Wage_Id),
	constraint FK_REPAIRDETAIL_REPAIR foreign key (IdRepair) references REPAIR(Repair_Id)
)
GO
-- Thiet lap danh sach thong tin cua gara
create table GARA_INFO
(
	GaraInfo_Id int primary key identity (1,1) not null,
	MaxCarReception int,
	IsOverPay bit,
	--GaraInfo_Phone nvarchar(20),
	--GaraInfo_Email nvarchar(200),
	--GaraInfo_Address nvarchar(max),
);
Go
-- Thiet lap danh sach cac hoa don -> doanh so cua thang
create table RECEIPT
(
	Receipt_Id int primary key identity(1,1) not null,
    ReceiptDate date not NULL,
    MoneyReceived int not NULL,
	Email nvarchar(200),
	IdReception int not NULL,
    constraint FK_RECEIPT_CARRECEPTION foreign key (IdReception) references RECEPTION(Reception_Id),
);
GO

-- Thiet lap bao cao ton

--------------------NHAN SU CUA GARA--------------------------------

-- Thiet lap danh sach cac role bao gom staff va admin
create table ROLE
(
	Role_Id int identity(1,1) primary key,
	Role_Name nvarchar(max),
)
go
create table ROLE_INFO
(
	RoleInfo_Id int identity(1,1) primary key,
	IdRole int not null,
	AccessDashBoard bit default(0) not null,
	AccessService bit default(0) not null,
	AccessEmployee bit default(0)not null,
	AccessSetting bit default(0)not null,
	AccessReport bit default(0)not null,
	AccessInventory bit default(0)not null,
	AllowAddSupplies bit default(0)not null,
	AllowAddCarBrand bit default(0)not null,
	AllowAddAccount bit default(0)not null,
	AllowAddWage bit default(0)not null,
	AllowEditGaraInfo bit default(0)not null,
	constraint FK_ROLEINFO_ROLE foreign key (IdRole) references ROLE(Role_Id)
)
go
-- Thiet lap danh sach tai khoan cua nguoi dung
create table USERS
(
    Users_Id int primary key not NULL IDENTITY(1, 1),
    UserName nvarchar(100) not NULL,
    Password nvarchar(100) not NULL,
    IdRole int not NULL,
	foreign key(IdRole) references ROLE(Role_Id),
);
GO
-- Thiet lap thong tin cua nguoi dung
create table USER_INFO
(
    UserInfo_Id int primary key not NULL IDENTITY(1, 1),
	IdUser int,
    UserInfo_Name nvarchar(200) not NULL,
    UserInfo_Address nvarchar(max),
    UserInfo_BirthDate date,
    UserInfo_Telephone nvarchar(20) not NULL,
    UserInfo_CMND nvarchar(100) not NULL,
    constraint FK_USERINFO_USERS foreign key (IdUser) references USERS(Users_Id)
);

insert into ROLE(Role_Name) values ('admin');
insert into USERS(UserName,Password,IdRole) values ('admin','db69fc039dcbd2962cb4d28f5891aae1',1);