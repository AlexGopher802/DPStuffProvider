drop database [DPSP Api_db]
create database [DPSP Api_db]
use [DPSP Api_db]

create table PersonalInfo(
	id int primary key identity,
	lastName nvarchar(50) not null,
	firstName nvarchar(50) not null,
	patronymic nvarchar (50) null
);

create table Contacts(
	id int primary key identity,
	phone varchar(20) not null,
	email varchar(50) null
);

create table Client(
	id int primary key identity,
	login varchar(50) not null,
	password varbinary(32) not null,
	idPersonalInfo int not null,
	foreign key (idPersonalInfo) references PersonalInfo (id),
	idContacts int not null,
	foreign key (idContacts) references Contacts (id)
);

create table Courier(
	id int primary key identity,
	login varchar(50) not null,
	password varbinary(32) not null,
	orderQuantity int default 0,
	idPersonalInfo int not null,
	foreign key (idPersonalInfo) references PersonalInfo (id),
	idContacts int not null,
	foreign key (idContacts) references Contacts (id)
);

create table OrderStatus(
	id int primary key identity,
	name nvarchar(50)
);

create table AddressDelivery(
	id int primary key identity,
	address nvarchar(100) not null,
	frontDoor int null,
	apartmentNum int null,
	floorNum int null,
	intercom nvarchar(20) null
);

create table Ordered(
	id int primary key identity,
	orderDateTime datetime not null,
	deliveryDate date not null,
	deliveryTimeFrom time not null,
	deliveryTimeTo time not null,
	commentary nvarchar(max) null,
	summ float default 0,
	priority int default 5,
	idAddress int not null,
	foreign key (idAddress) references AddressDelivery (id),
	idClient int not null,
	foreign key (idClient) references Client (id),
	idCourier int null,
	foreign key (idCourier) references Courier (id),
	idOrderStatus int not null,
	foreign key (idOrderStatus) references OrderStatus (id)
);

create table OrderFinished(
	id int primary key identity,
	clientScore int null,
	commentary nvarchar(max) null,
	idOrder int not null,
	foreign key (idOrder) references Ordered (id)
);

create table StoreInfo(
	id int primary key identity,
	name nvarchar(50) not null,
	fullname nvarchar(100) not null,
	tin varchar(12) not null,
	bank nvarchar(100) not null,
	bic varchar(9) not null,
	email varchar(50) not null,
	phone varchar(20) not null,
	address nvarchar(100) not null
);

create table ProductAttribute(
	id int primary key identity,
	name nvarchar(50) not null
);

create table ProductCategory(
	id int primary key identity,
	name nvarchar(50) not null,
	imageUrl nvarchar(300) default '',
	idParentCategory int null,
	foreign key (idParentCategory) references ProductCategory (id)
);
--alter table ProductCategory add imageUrl nvarchar(300) default ''

create table Product(
	id int primary key identity,
	name nvarchar(50) not null,
	cost float not null,
	rating float default 0.0,
	avail bit default 1,
	idCategory int not null,
	foreign key (idCategory) references ProductCategory (id),
	idStoreInfo int not null,
	foreign key (idStoreInfo) references StoreInfo (id)
);

create table ProductImages(
	id int primary key identity,
	imageUrl nvarchar(300) not null,
	idProduct int not null,
	foreign key (idProduct) references Product (id)
);

create table ProductDescription(
	id int primary key identity,
	attrValue nvarchar(1000) not null,
	idProduct int not null,
	foreign key (idProduct) references Product (id),
	idProductAttribute int not null,
	foreign key (idProductAttribute) references ProductAttribute(id)
);

create table ProductCompos(
	id int primary key identity,
	quantity int not null,
	summ float not null,
	idProduct int not null,
	foreign key (idProduct) references Product (id),
	idOrder int not null,
	foreign key (idOrder) references Ordered (id)
);

create table ProductReview(
	id int primary key identity,
	clientScore int not null,
	commentary nvarchar(max) null,
	idProduct int not null,
	foreign key (idProduct) references Product (id),
	idClient int not null,
	foreign key (idClient) references Client (id)
);

create table ClientAddress(
	id int primary key identity,
	idClient int not null,
	foreign key (idClient) references Client (id),
	idAddress int not null,
	foreign key (idAddress) references AddressDelivery (id)
);


insert into PersonalInfo(lastName, firstName, patronymic) values
(N'������', N'����', N'��������'),
(N'������', N'���������', N'����������'),
(N'�������', N'�������', N'��������'),
(N'�������', N'������', null)

insert into Contacts(phone, email) values
('88005553535', 'example@mail.com'),
('89992222556', 'mail@example.ru'),
('89526545654', 'gmail@mpt.ru'),
('89255233535', 'examp13@gmail.com')

insert into Courier(login, password, idPersonalInfo, idContacts) values
('log1', hashbytes('SHA2_256', 'pass1'), 1, 1),
('log2', hashbytes('SHA2_256', 'pass2'), 2, 2),
('log3', hashbytes('SHA2_256', 'pass3'), 3, 3),
('log4', hashbytes('SHA2_256', 'pass4'), 4, 4)

insert into OrderStatus(name) values
(N'��������������'),
(N'����� �������'),
(N'��������'),
(N'������')

insert into Client(login, password, idPersonalInfo, idContacts) values
('log1', hashbytes('SHA2_256', 'pass1'), 1, 1),
('log2', hashbytes('SHA2_256', 'pass2'), 2, 2),
('log3', hashbytes('SHA2_256', 'pass3'), 3, 3)

insert into AddressDelivery(address, frontDoor, apartmentNum, floorNum, intercom) values
(N'�. ������, ��. �����������, �. 3', 2, 77, 8, N'77�2353'),
(N'�. ������, ��. ����������, �. 12', 3, 43, 3, N'53�4253'),
(N'�. ��������, ��. ����������, �. 36�', 1, 13, 1, N'66�1483')

insert into Ordered(idAddress, orderDateTime, deliveryDate, deliveryTimeFrom, deliveryTimeTo, summ, idClient, idOrderStatus) values
(1, getdate(), '2021-03-30', '09:00', '22:00', 9500.0, 1, 1),
(2, getdate(), '2021-04-02', '09:00', '21:00', 10500.0, 2, 1),
(3, getdate(), '2021-04-01', '10:00', '20:00', 3200.0, 3, 1)

insert into StoreInfo(name, fullname, tin, bank, bic, email, phone, address) values
(N'Pleer.ru', N'��� ����� ����� ��', '8003745819', N'��������', '567435440', 'mail@pleer.ru', '89526545654', N'�. ������, ��. ������������, �. 19'),
(N'���������', N'��� ���������', '1287003819', N'�����-����', '900435440', 'mail@el.ru', '89251228456', N'�. ������, ��. �������� �������, �. 123'),
(N'DNS', N'��� ��� ����', '8999745819', N'��������', '567923440', 'mail@dns.ru', '84300545654', N'�. ������, ��. ��������, �. 27'),
(N'��������', N'��� Citylink', '8926745819', N'Chease', '110025440', 'mail@cit.ru', '83423545654', N'�. ������, ��. ������, �. 3'),
(N'������', N'��� ������ ����', '8666745819', N'��������', '137435440', 'mail@mv.ru', '82286545654', N'�. ������, ��. ���������, �. 6')

--delete from ProductCategory
insert into ProductCategory(name, idParentCategory) values
(N'�����������', null),
(N'������������ �������', null),
(N'������� �������', null),
(N'�����', null),
(N'������', null),
(N'���������', null)

update ProductCategory set imageUrl = N'http://clipart-library.com/img/1604549.png' where name = N'�����������'

insert into ProductCategory(name, idParentCategory) values
(N'��������� � ����������', (select id from ProductCategory where name=N'�����������')),
(N'�������', (select id from ProductCategory where name=N'�����������')),
(N'��������', (select id from ProductCategory where name=N'�����������')),
(N'����������', (select id from ProductCategory where name=N'�����������')),
(N'������������ ����������', (select id from ProductCategory where name=N'������������ �������')),
(N'�������� � ����������', (select id from ProductCategory where name=N'������������ �������')),
(N'������� ��� �����', (select id from ProductCategory where name=N'������� �������')),
(N'������� ��� ����', (select id from ProductCategory where name=N'������� �������')),
(N'����� ���', (select id from ProductCategory where name=N'������� �������')),
(N'������� � �����', (select id from ProductCategory where name=N'�����')),
(N'�������������� ����������', (select id from ProductCategory where name=N'�����')),
(N'���������� � ������������', (select id from ProductCategory where name=N'�����')),
(N'����� � ������', (select id from ProductCategory where name=N'������')),
(N'������ ��� �������', (select id from ProductCategory where name=N'������')),
(N'������ ��� �����', (select id from ProductCategory where name=N'������')),
(N'��� �����', (select id from ProductCategory where name=N'���������')),
(N'��� �����', (select id from ProductCategory where name=N'���������')),
(N'��� ����', (select id from ProductCategory where name=N'���������'))
insert into ProductCategory(name, idParentCategory) values
(N'Playstation 4', (select id from ProductCategory where name=N'�������')),
(N'Playstation 5', (select id from ProductCategory where name=N'�������')),
(N'�����', (select id from ProductCategory where name=N'������������ ����������')),
(N'��������', (select id from ProductCategory where name=N'�������� � ����������'))

--delete from Product
insert into Product(name, cost, idStoreInfo, idCategory) values
(N'������� Dualshock4', 4990.0, 1, (select id from ProductCategory where name=N'Playstation 4')),
(N'������� Dualshock5', 5990.0, 1, (select id from ProductCategory where name=N'Playstation 5')),
(N'����� Bloody Z36P2', 2390.0, 1, (select id from ProductCategory where name=N'�����')),
(N'�������� Jbl M25', 4990.0, 1, (select id from ProductCategory where name=N'��������')),
(N'������� Philips M20J3', 10990.0, 1, (select id from ProductCategory where name=N'��������')),
(N'������� Dualshock4', 4890.0, 2, (select id from ProductCategory where name=N'Playstation 4')),
(N'������� Dualshock5', 6390.0, 2, (select id from ProductCategory where name=N'Playstation 5')),
(N'����� Bloody Z36P2', 1990.0, 2, (select id from ProductCategory where name=N'�����')),
(N'�������� Jbl M25', 4890.0, 2, (select id from ProductCategory where name=N'��������')),
(N'������� Philips M20J3', 10830.0, 2, (select id from ProductCategory where name=N'��������')),
(N'������� Dualshock4', 4890.0, 3, (select id from ProductCategory where name=N'Playstation 4')),
(N'������� Dualshock5', 5890.0, 3, (select id from ProductCategory where name=N'Playstation 5')),
(N'����� Bloody Z36P2', 2290.0, 3, (select id from ProductCategory where name=N'�����')),
(N'�������� Jbl M25', 4990.0, 3, (select id from ProductCategory where name=N'��������')),
(N'������� Philips M20J3', 11390.0, 3, (select id from ProductCategory where name=N'��������')),
(N'������� Dualshock4', 4790.0, 4, (select id from ProductCategory where name=N'Playstation 4')),
(N'������� Dualshock5', 5900.0, 4, (select id from ProductCategory where name=N'Playstation 5')),
(N'����� Bloody Z36P2', 2300.0, 4, (select id from ProductCategory where name=N'�����')),
(N'�������� Jbl M25', 4900.0, 4, (select id from ProductCategory where name=N'��������')),
(N'������� Philips M20J3', 10900.0, 4, (select id from ProductCategory where name=N'��������')),
(N'������� Dualshock4', 4900.0, 5, (select id from ProductCategory where name=N'Playstation 4')),
(N'������� Dualshock5', 5900.0, 5, (select id from ProductCategory where name=N'Playstation 5')),
(N'����� Bloody Z36P2', 2380.0, 5, (select id from ProductCategory where name=N'�����')),
(N'�������� Jbl M25', 4900.0, 5, (select id from ProductCategory where name=N'��������')),
(N'������� Philips M20J3', 10900.0, 5, (select id from ProductCategory where name=N'��������'))

insert into ProductCompos(idOrder, idProduct, quantity, summ) values
(1, (select id from Product where name=N'������� Dualshock4' and idStoreInfo=1), 1, (select cost from Product where name=N'������� Dualshock4' and idStoreInfo=1)*1),
(1, (select id from Product where name=N'����� Bloody Z36P2' and idStoreInfo=2), 2, (select cost from Product where name=N'����� Bloody Z36P2' and idStoreInfo=2)*2),
(2, (select id from Product where name=N'�������� Jbl M25' and idStoreInfo=3), 1, (select cost from Product where name=N'�������� Jbl M25' and idStoreInfo=3)*1),
(2, (select id from Product where name=N'������� Philips M20J3' and idStoreInfo=3), 1, (select cost from Product where name=N'������� Philips M20J3' and idStoreInfo=3)*1),
(3, (select id from Product where name=N'������� Dualshock4' and idStoreInfo=1), 1, (select cost from Product where name=N'������� Dualshock4' and idStoreInfo=1)*1),
(3, (select id from Product where name=N'������� Dualshock5' and idStoreInfo=4), 1, (select cost from Product where name=N'������� Dualshock5' and idStoreInfo=4)*2)

select * from Courier
select * from Ordered
select
	ProductCategory.name as [category],
	parentCategory.name as [parent],
	ProductCategory.imageUrl as [img]
from ProductCategory
	left join ProductCategory as parentCategory on ProductCategory.idParentCategory = parentCategory.id