
-- Khoi tao database
create database GARA;
use GARA;
-- drop database GARA

---------------------DON HANG CUA KHACH HANG-----------------------------------

-- Thiet lap danh sach cac hang xe
create table CAR_BRAND
(
    Brand_ID varchar(50) primary key not NULL,
    Brand_Name varchar(50) not NULL
);

-- Thiet lap danh sach khach hang
create table CUSTOMER
(
    Customer_ID varchar(50) primary key not NULL,
    Customer_Name varchar(50) not NULL,
    Customer_Address varchar(50),
    Customer_Telephone varchar(20) not NULL
);
-- Thiet lap danh sach cac dich vu tai gara
create table GARA_SERVICE
(
    Servive_ID varchar(50) primary key not NULL,
    Service_Name varchar(50) not NULL
);

-- Thiet lap danh sach vat tu, phu tung
create table SUPPLIES
(
    Supplies_ID varchar(50) primary key not NULL,
    Supplies_Name varchar(50) not NULL,
    Price int,
    Pay int,
    Amount int
);
-- Thiet lap danh sach cac xe co tai gara
create table LICENSE_PLATE
(
    License_Number varchar(50) primary key not NULL,
    Brand_ID varchar(50) not NULL,
    Customer_ID varchar(50) not NULL,
    constraint FK_LICENSEPLATE_BRANDID foreign key (Brand_ID) references CAR_BRAND(Brand_ID),
    constraint FK_LICENSEPLATE_CUSTOMER foreign key (Customer_ID) references CUSTOMER(Customer_ID)
);

-- Thiet lap phieu sua chua

-- Thiet lap danh sach cac xe dang trong qua trinh pending

-- Thiet lap danh sach cac xe dang trong qua trinh sua chua

-- Thiet lap danh sach cac xe dang trong qua trinh thu no
create table CAR_LIST
(
    License_Number varchar(50) primary key not null,
    Debt int,
    constraint FK_CARLIST_LICENSEPLATE foreign key (License_Number) references LICENSE_PLATE(License_Number)
);

-- Thiet lap danh sach phieu thu tien -> doanh so cua thang
create table RECEIPT
(
    License_Number varchar(50) primary key not NULL,
    Email_Address varchar(50), -- ?
    Receipt_Date datetime,
    Amount int,
    constraint FK_RECEIPT_LICENSEPLATE foreign key (License_Number) references LICENSE_PLATE(License_Number)
);

-- Thiet lap bao cao ton


--------------------NHAN SU CUA GARA--------------------------------

-- Thiet lap danh sach tai khoan cua nhan vien va admin
create table STAFF_ACCOUNT
(
    ID varchar(50) primary key not NULL,
    User_Name varchar(50) not NULL,
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

