-- Khoi tao database
GO
create database GARA;
GO
use GARA;
GO
-- drop database GARA;
---------------------DON HANG CUA KHACH HANG-----------------------------------

create table CAR_BRAND
(
    CarBrand_Id int primary key not NULL IDENTITY(1, 1),
    CarBrand_Name nvarchar(max) not NULL
);
GO

create table CUSTOMER
(
    Customer_Id int primary key not NULL IDENTITY(1, 1),
    Customer_Name nvarchar(100) not NULL,
    Customer_Address nvarchar(max),
    Customer_Phone nvarchar(20) not NULL
);
GO

create table WAGE
(
    Wage_Id int primary key not NULL IDENTITY(1, 1),
    Wage_Name nvarchar(1000) not NULL,
    Wage_Value int not NULL
);
GO
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
	ImportGoods_Date date not null,
	ImportGoods_TotalMoney int not null
);
GO
create table IMPORT_GOODS_DETAIL
(
    IdImportGood int not NULL,
	IdSupplies int not null,
	Amount int not null,
	Price int not null,
	TotalMoney int not null,
	constraint PR_IMPORTGOODS_SUPPLIES Primary key (IdImportGood,IdSupplies),
	constraint FK_IMPORTGOODSDETAIL_SUPPLIES foreign key (IdSupplies) references SUPPLIES(Supplies_Id),
	constraint FK_IMPORTGOODSDETAIL_IMPORTGOODS foreign key (IdImportGood) references IMPORT_GOODS(ImportGoods_Id)
);
GO

create table INVENTORY_REPORT
(
	InventoryReport_Id int primary key not null,
	InventoryReport_Date date not null,
);
GO

create table INVENTORY_REPORT_DETAIL
(
	IdInventoryReport int not null,
	IdSupplies int not NULL,
	TonDau int not null,
	PhatSinh int not null,
	TonCuoi int not null,
	constraint PR_INVENTORYREPORT_SUPPLIES primary key(IdInventoryReport,IdSupplies),
	constraint FK_INVENTORYREPORTDETAIL_INVENTORYREPORT foreign key (IdInventoryReport) references INVENTORY_REPORT(InventoryReport_Id),
	constraint FK_INVENTORYREPORTDETAIL_SUPPLIES foreign key (IdSupplies) references SUPPLIES(Supplies_Id)
);
GO

create table SALES_REPORT
(
	SalesReport_Id int IDENTITY(1, 1) primary key not NULL,
	SalesReport_Date date not null,
	SalesReport_Revenue int not null default('0'),
);
GO

create table SALES_REPORT_DETAIL
(
	IdSalesReport int not null,
    IdCarBrand int not NULL,
    AmountOfTurn int,
    TotalMoney int,
    Rate float,
	constraint PR_SALESREPORT_CARBRAND primary key(IdSalesReport,IdCarBrand) ,
	constraint FK_SALESREPORTDETAIL_SALESREPORT foreign key (IdSalesReport) references SALES_REPORT(SalesReport_Id),
	constraint FK_SALESREPORTDETAIL_CARBRAND foreign key (IdCarBrand) references CAR_BRAND(CarBrand_Id)
);
GO

--create table CAR_STATUS
--(
--    CarStatus_Id int primary key not NULL IDENTITY(1, 1),
--    CarStatus_Name nvarchar(200) not NULL
--);
--GO

create table RECEPTION
(
    Reception_Id int primary key not NULL IDENTITY(1, 1),   
	LicensePlate nvarchar(50) not NULL,
    ReceptionDate date not NULL,
	Debt int not null Default('0'),
	IdCarBrand int not NULL,
	IdCustomer int not NULL,
    constraint FK_RECEPTION_CUSTOMER foreign key (IdCustomer) references CUSTOMER(Customer_Id),
    constraint FK_RECEPTION_CARBRAND foreign key (IdCarBrand) references CAR_BRAND(CarBrand_Id),
);
GO

create table REPAIR
(
    Repair_Id int primary key not NULL IDENTITY(1, 1),
    IdReception int not NULL,
    RepairDate date not NULL,
	Repair_TotalMoney int not null default('0'),
	constraint FK_REPAIRFORM_CARRECEPTION foreign key (IdReception) references RECEPTION(Reception_Id),
);
GO

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
	MaxCarReception int not null,
	IsOverPay bit not null,
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
	Phone nvarchar(20),
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
create table PREMISSION_ITEM
(
	PermissionItem_Id int identity(1,1) primary key,
	PermissionItem_Name nvarchar(max) not null,
)
go
create table ROLE_DETAIL
(
	RoleDetail_Id int identity(1,1) primary key,
	IdRole int not null,
	IdPermissionItem int not null,
	Permission bit not null default('False'),
	constraint FK_ROLEDETAIL_ROLE foreign key (IdRole) references ROLE(Role_Id),
	constraint FK_ROLEDETAIL_PREMISSIONITEM foreign key (IdPermissionItem) references PREMISSION_ITEM(PermissionItem_Id)
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