

-- Khoi tao database
create database GARA;
use GARA;
-- drop database GARA;
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

-- Thiet lap danh sach loai tien cong
create table PAY
(
    ID int primary key not NULL IDENTITY(1, 1),
    Name varchar(256) not NULL,
    Price int not NULL
);
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

-- Thiet lap trang thai xe
create table CAR_STATUS
(
    ID int primary key not NULL IDENTITY(1, 1),
    Name varchar(50) not NULL
);
-- drop table CAR_STATUS;

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

-- Thiet lap phieu tiep nhan xe
create table RECEIVING_TICKET
(
    ID int primary key not NULL IDENTITY(1, 1),
    Customer_ID int not NULL,
    License_ID int not NULL,
    Brand_ID int not NULL,
    Receiving_Date datetime not NULL,

    constraint FK_RECEIVINGTICKET_CUSTOMER foreign key (Customer_ID) references CUSTOMER(ID),
    constraint FK_RECEIVINGTICKET_LICENSEPLATE foreign key (License_ID) references LICENSE_PLATE(ID),
    constraint FK_RECEIVINGTICKET_CARBRAND foreign key (Brand_ID) references CAR_BRAND(ID)
);
-- drop table RECEIVING_TICKET;

-- Thiet lap phieu sua chua
create table REPAIR_TICKET
(
    ID int primary key not NULL IDENTITY(1, 1),
    Receiving_ID int not NULL,
    Repair_Date datetime not NULL,
    Content varchar(256) not NULL,
    Supply_ID varchar(255) not NULL,
    Supplies_Amount int not NULL,
    Pay_ID int not NULL,
    Total_Money int not NULL,

    constraint FK_TICKET_SUPPLIES foreign key (Supply_ID) references SUPPLIES(ID),
    constraint FK_REPAIRTICKET_RECEIVINGTICKET foreign key (Receiving_ID) references RECEIVING_TICKET(ID),
    constraint FK_REPAIRTICKET_PAY foreign key (Pay_ID) references PAY(ID)
);
-- drop table REPAIR_TICKET;

-- Thiet lap danh sach cac xe dang trong qua trinh thu no
create table CAR_IN_DEBT
(
    License_ID int primary key not null,
    Debt int,
    constraint FK_CARLIST_LICENSEPLATE foreign key (License_ID) references LICENSE_PLATE(ID)
);
-- drop table CAR_IN_DEBT;

-- Thiet lap danh sach cac hoa don -> doanh so cua thang
create table RECEIPT
(
    License_ID int primary key not NULL,
    Email_Address varchar(50),
    Receipt_Date datetime not NULL,
    Total_Money int not NULL,
    IsPayed bit not NULL,
    constraint FK_RECEIPT_LICENSEPLATE foreign key (License_ID) references LICENSE_PLATE(ID)
);
-- drop table RECEIPT;

-- Thiet lap bao cao ton


--------------------NHAN SU CUA GARA--------------------------------

-- Thiet lap danh sach tai khoan cua nhan vien va admin
create table STAFF_ACCOUNT
(
    ID int primary key not NULL IDENTITY(1, 1),
    User_Name varchar(50) not NULL,
    Password varchar(256) not NULL,
    Role varchar(50) not NULL
);
-- drop table STAFF_ACCOUNT;

-- Thiet lap thong tin nhan vien
create table STAFF_INFORMATION
(
    Staff_ID int primary key not NULL,
    Name varchar(50) not NULL,
    Address varchar(50),
    Birthdate datetime,
    Telephone varchar(20) not NULL,
    CMND varchar(50) not NULL,
    constraint FK_INFORMATION_ACCOUNT foreign key (Staff_ID) references STAFF_ACCOUNT(ID)
);

