use GARA;
----------------------------------
insert into GaraInfo values(100,30,'0919467684','Gara@gmail.com' , 'TPHCM')

insert into Supply values( N'Lốp', 300000, 10)
insert into Supply values( N'Dầu nhớt', 300000, 100)
insert into Supply values( N'Nước làm mát', 100000, 200)
insert into Supply values( N'Nắp Capo', 1000000, 5)

insert into CarBrand values(N'BMW')
insert into CarBrand values(N'Mercedes')
insert into CarBrand values(N'Toyota')
insert into CarBrand values(N'Vinfast')
insert into CarBrand values(N'Ford')

insert into Customer values(N'Dat' , N'Tien Giang' , '091923921')
insert into Customer values(N'Kiet' , N'Quang Ngai' , '091923921')
insert into Customer values(N'Hung' , N'TPHCM', '091923921')

insert into CarStatus values(N'Đang khám xe')
insert into CarStatus values(N'Đang sửa chữa')
insert into CarStatus values(N'Đã sửa chữa xong')
insert into CarStatus values(N'Hoàn Thành')

insert into CarReception values('1',N'63-BB-9999',4, '05-03-2020',4)
insert into CarReception values('2',N'51-OO-8888',2, '09-01-2020',2)
insert into CarReception values('3',N'71-KL-3123',1, '03-01-2020',2)

insert into Pay values(N'Rửa xe', 50000)
insert into Pay values(N'Thay phụ tùng', 30000)
insert into Pay values(N'Thay nhớt', 10000)

insert into RepairForm values(1,'05-03-2020')

insert into RepairInfo values(1,N'Thay lốp',1,1,2,330000)
insert into RepairInfo values(1,N'Thay lốp',1,1,2,330000)

insert into Receipt values(1,1,'05-10-2020', 660000)

insert into UserRole values('admin')
insert into Users values('admin', '123', 1)
insert into UserInfo values(1, 'Huynh Anh Kiet', 'TP.HCM', '11/24/2001', '0945253415', '212861998')