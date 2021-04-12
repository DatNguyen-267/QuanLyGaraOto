

-- Khoi tao database
create database GARA;
use GARA;
---------------------DON HANG CUA KHACH HANG-----------------------------------

-- Thiet lap danh sach cac hang xe
create table CAR_BRAND
(
    ID int primary key not NULL IDENTITY(1, 1),
    Name varchar(255) not NULL
);
-- drop table CAR_BRAND;

-- Thiet lap danh sach khach hang
create table CUSTOMER
(
    ID int primary key not NULL IDENTITY(1, 1),
    Name varchar(50) not NULL,
    Address varchar(256),
    Telephone varchar(20) not NULL
);
-- drop table CUSTOMER;

-- Thiet lap danh sach cac dich vu tai gara
create table GARA_SERVICE
(
    ID int primary key not NULL IDENTITY(1, 1),
    Name varchar(50) not NULL
);
-- drop table GARA_SERVICE;

-- Thiet lap danh sach vat tu, phu tung
create table SUPPLIES
(
    ID varchar(255) primary key not NULL,
    Name varchar(50) not NULL,
    Price int,
    Amount int
);
-- drop table SUPPLIES;

-- Thiet lap danh sach cac xe co tai gara
create table LICENSE_PLATE
(
    ID int primary key not NULL IDENTITY(1, 1),
    License_Number varchar(50) not NULL,
    Brand_ID int not NULL,
    Customer_ID int not NULL,
    Status int not NULL,
    constraint FK_LICENSEPLATE_BRANDID foreign key (Brand_ID) references CAR_BRAND(ID),
    constraint FK_LICENSEPLATE_CUSTOMER foreign key (Customer_ID) references CUSTOMER(ID),
    constraint FK_LICENSEPLATE_STATUS foreign key (Status) references CAR_STATUS(ID)
);
-- drop table LICENSE_PLATE;
-- Thiet lap phieu sua chua
create table REPAIR_TICKET
(
    ID int primary key not NULL IDENTITY(1, 1),
    Date datetime not NULL,
    License_ID int not NULL,
    Content varchar(256) not NULL,
    Supply_ID int not NULL,
    Supplies_Amount int not NULL,
    Pay int not NULL,
    constraint FK_TICKET_LICENSEPLATE foreign key (License_ID) references LICENSE_PLATE(ID),
    constraint FK_TICKET_SUPPLIES foreign key (Supply_ID) references SUPPLIES(ID)
);
-- Thiet lap trang thai cua cac xe 
create table CAR_STATUS
(
    ID int primary key not NULL IDENTITY(1, 1),
    Name varchar(50) not NULL
);
-- drop table CAR_STATUS;

-- Thiet lap danh sach cac xe dang trong qua trinh thu no
create table CAR_IN_DEBT
(
    License_ID int primary key not null,
    Debt int,
    constraint FK_CARLIST_LICENSEPLATE foreign key (License_ID) references LICENSE_PLATE(ID)
);
-- drop table CAR_IN_DEBT;

-- Thiet lap danh sach phieu thu tien -> doanh so cua thang
create table RECEIPT
(
    License_ID int primary key not NULL,
    Email_Address varchar(50), -- ?
    Receipt_Date datetime,
    Amount int,
    constraint FK_RECEIPT_LICENSEPLATE foreign key (License_ID) references LICENSE_PLATE(ID)
);

-- Thiet lap bao cao ton


--------------------NHAN SU CUA GARA--------------------------------

-- Thiet lap danh sach tai khoan cua nhan vien va admin
create table STAFF_ACCOUNT
(
    User_Name varchar(50) primary key not NULL,
    Password varchar(256) not NULL,
    Role varchar(50) not NULL
);

create table STAFF_INFORMATION
(
    ID varchar(50) primary key not NULL,
    Name varchar(50) not NULL,
    Address varchar(50),
    Birthdate datetime,
    Telephone varchar(20) not NULL,
    CMND varchar(50) not NULL,
    constraint FK_INFORMATION_ACCOUNT foreign key (ID) references STAFF_ACCOUNT(ID)
);

