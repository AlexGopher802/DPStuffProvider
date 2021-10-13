create database DPSPdbV2;
use DPSPdbV2;

create table PersonalInfo(
	id int primary key auto_increment,
	lastName nvarchar(50) not null,
	firstName nvarchar(50) not null,
	patronymic nvarchar (50) null
);

create table Contacts(
	id int primary key auto_increment,
	phone varchar(20) not null unique,
	email varchar(50) null
);

create table UserType(
	id int primary key auto_increment,
    name nvarchar(50) not null unique
);

create table UserStatus(
	id int primary key auto_increment,
    name nvarchar(50) not null unique
);

create table User(
	id int primary key auto_increment,
    login varchar(40) not null unique,
    password varchar(64) not null,
    idType int not null,
    foreign key (idType) references UserType (id),
    idStatus int not null,
    foreign key (idStatus) references UserStatus (id)
);

create table ClientInfo(
	id int primary key auto_increment,
	idPersonalInfo int not null,
	foreign key (idPersonalInfo) references PersonalInfo (id),
	idContacts int not null,
	foreign key (idContacts) references Contacts (id),
    idUser int not null,
    foreign key (idUser) references User (id)
);

create table CourierInfo(
	id int primary key auto_increment,
	orderQuantity int default 0,
	idPersonalInfo int not null,
	foreign key (idPersonalInfo) references PersonalInfo (id),
	idContacts int not null,
	foreign key (idContacts) references Contacts (id),
    idUser int not null,
    foreign key (idUser) references User (id)
);

create table OrderStatus(
	id int primary key auto_increment,
	name nvarchar(50) not null unique
);

create table AddressDelivery(
	id int primary key auto_increment,
	address nvarchar(100) not null,
	frontDoor int null,
	apartmentNum int null,
	floorNum int null,
	intercom nvarchar(20) null
);

create table Ordered(
	id int primary key auto_increment,
	orderDateTime datetime not null,
	deliveryDate date not null,
	deliveryTimeFrom time not null,
	deliveryTimeTo time not null,
	commentary nvarchar(300) null,
	summ float default 0,
	codeToFinish varchar(4) default '0000',
	idAddress int not null,
	foreign key (idAddress) references AddressDelivery (id),
	idClient int not null,
	foreign key (idClient) references ClientInfo (id),
	idCourier int null,
	foreign key (idCourier) references CourierInfo (id),
	idOrderStatus int not null,
	foreign key (idOrderStatus) references OrderStatus (id)
);

create table OrderFinished(
	id int primary key auto_increment,
	clientScore int null,
	commentary nvarchar(500) null,
	idOrder int not null,
	foreign key (idOrder) references Ordered (id)
);

create table VendorInfo(
	id int primary key auto_increment,
	name nvarchar(40) not null unique,
	fullname nvarchar(100) not null unique,
	tin varchar(12) not null,
	bank nvarchar(100) not null,
	bic varchar(9) not null,
	email varchar(50) not null,
	phone varchar(20) not null,
	address nvarchar(100) not null,
    idUser int not null,
    foreign key (idUser) references User (id)
);

create table ProductAttribute(
	id int primary key auto_increment,
	name nvarchar(50) not null unique
);

create table ProductCategory(
	id int primary key auto_increment,
	name nvarchar(50) not null,
	imageUrl nvarchar(1000) null,
	idParentCategory int null,
	foreign key (idParentCategory) references ProductCategory (id)
);

create table Product(
	id int primary key auto_increment,
	name nvarchar(50) not null,
	price float not null,
	rating float default 0.0,
	avail bool default 1,
	idCategory int not null,
	foreign key (idCategory) references ProductCategory (id),
	idVendorInfo int not null,
	foreign key (idVendorInfo) references VendorInfo (id)
);

create table ProductImages(
	id int primary key auto_increment,
	imageUrl nvarchar(1000) not null,
	idProduct int not null,
	foreign key (idProduct) references Product (id)
);

create table ProductDescription(
	id int primary key auto_increment,
	value nvarchar(1000) not null,
	idProduct int not null,
	foreign key (idProduct) references Product (id),
	idProductAttribute int not null,
	foreign key (idProductAttribute) references ProductAttribute(id)
);

create table OrderProducts(
	id int primary key auto_increment,
	quantity int not null,
	summ float not null,
	idProduct int not null,
	foreign key (idProduct) references Product (id),
	idOrder int not null,
	foreign key (idOrder) references Ordered (id)
);

create table ProductReview(
	id int primary key auto_increment,
	clientScore int not null,
	commentary nvarchar(500) null,
	idProduct int not null,
	foreign key (idProduct) references Product (id),
	idClient int not null,
	foreign key (idClient) references ClientInfo (id)
);

create table ClientAddress(
	id int primary key auto_increment,
	idClient int not null,
	foreign key (idClient) references ClientInfo (id),
	idAddress int not null,
	foreign key (idAddress) references AddressDelivery (id)
);

create table Warehouse(
	id int primary key auto_increment,
    address nvarchar(100) not null
);

create table WarehouseProducts(
	id int primary key auto_increment,
    idWarehouse int not null,
    foreign key (idWarehouse) references Warehouse (id),
    idProduct int not null,
    foreign key (idProduct) references Product (id)
);



insert into UserType(name) values
(N'Администратор'),
(N'Клиент'),
(N'Курьер'),
(N'Вендор');

insert into UserStatus(name) values
(N'Заблокирован'),
(N'Подтвержден'),
(N'Не подтвержден');

insert into PersonalInfo(lastName, firstName, patronymic) values
(N'Иванов', N'Иван', N'Иванович'),
(N'Суслин', N'Александр', N'Михайлович'),
(N'Комаров', N'Алексей', N'Олегович');

insert into Contacts(phone, email) values
('88005553535', 'example@mail.com'),
('89992222556', 'mail@example.ru'),
('89526545654', 'gmail@mpt.ru');

insert into User(login, password, idType, idStatus) values
('admin', '123123', 1, 2);


select * from UserStatus;
select * from UserType;




