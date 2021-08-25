drop database DPSPdb;
create database DPSPdb;
use DPSPdb;

create table PersonalInfo(
	id int primary key auto_increment,
	lastName nvarchar(50) not null,
	firstName nvarchar(50) not null,
	patronymic nvarchar (50) null
);

create table Contacts(
	id int primary key auto_increment,
	phone varchar(20) not null,
	email varchar(50) null
);

create table Client(
	id int primary key auto_increment,
	login varchar(50) not null,
	password varbinary(64) not null,
	idPersonalInfo int not null,
	foreign key (idPersonalInfo) references PersonalInfo (id),
	idContacts int not null,
	foreign key (idContacts) references Contacts (id)
);

create table Courier(
	id int primary key auto_increment,
	login varchar(50) not null,
	password varbinary(64) not null,
	orderQuantity int default 0,
	idPersonalInfo int not null,
	foreign key (idPersonalInfo) references PersonalInfo (id),
	idContacts int not null,
	foreign key (idContacts) references Contacts (id)
);

create table OrderStatus(
	id int primary key auto_increment,
	name nvarchar(50)
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
	priority int default 5,
	codeToFinish varchar(4) default '0000',
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
	id int primary key auto_increment,
	clientScore int null,
	commentary nvarchar(500) null,
	idOrder int not null,
	foreign key (idOrder) references Ordered (id)
);

create table StoreInfo(
	id int primary key auto_increment,
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
	id int primary key auto_increment,
	name nvarchar(50) not null
);

create table ProductCategory(
	id int primary key auto_increment,
	name nvarchar(50) not null,
	imageUrl nvarchar(300) default '',
	idParentCategory int null,
	foreign key (idParentCategory) references ProductCategory (id)
);

/*alter table ProductCategory add imageUrl nvarchar(300) default ''*/

create table Product(
	id int primary key auto_increment,
	name nvarchar(50) not null,
	cost float not null,
	rating float default 0.0,
	avail bool default 1,
	idCategory int not null,
	foreign key (idCategory) references ProductCategory (id),
	idStoreInfo int not null,
	foreign key (idStoreInfo) references StoreInfo (id)
);

create table ProductImages(
	id int primary key auto_increment,
	imageUrl nvarchar(1000) not null,
	idProduct int not null,
	foreign key (idProduct) references Product (id)
);

create table ProductDescription(
	id int primary key auto_increment,
	attrValue nvarchar(1000) not null,
	idProduct int not null,
	foreign key (idProduct) references Product (id),
	idProductAttribute int not null,
	foreign key (idProductAttribute) references ProductAttribute(id)
);

create table ProductCompos(
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
	foreign key (idClient) references Client (id)
);

create table ClientAddress(
	id int primary key auto_increment,
	idClient int not null,
	foreign key (idClient) references Client (id),
	idAddress int not null,
	foreign key (idAddress) references AddressDelivery (id)
);


insert into PersonalInfo(lastName, firstName, patronymic) values
(N'Иванов', N'Иван', N'Иванович'),
(N'Суслин', N'Александр', N'Михайлович'),
(N'Комаров', N'Алексей', N'Олегович'),
(N'Железов', N'Андрей', null);

insert into Contacts(phone, email) values
('88005553535', 'example@mail.com'),
('89992222556', 'mail@example.ru'),
('89526545654', 'gmail@mpt.ru'),
('89255233535', 'examp13@gmail.com');

insert into Courier(login, password, idPersonalInfo, idContacts) values
('log1', sha2('pass1', 256), 1, 1),
('log2', sha2('pass2', 256), 2, 2),
('log3', sha2('pass3', 256), 3, 3),
('log4', sha2('pass4', 256), 4, 4);

insert into OrderStatus(name) values
(N'Обрабатывается'),
(N'Выдан курьеру'),
(N'Завершен'),
(N'Отменён');

insert into Client(login, password, idPersonalInfo, idContacts) values
('log1', sha2('pass1', 256), 1, 1),
('log2', sha2('pass2', 256), 2, 2),
('log3', sha2('pass3', 256), 3, 3);

insert into AddressDelivery(address, frontDoor, apartmentNum, floorNum, intercom) values
(N'г. Москва, ул. Центральная, д. 3', 2, 77, 8, N'77к2353'),
(N'г. Москва, ул. Пушкинская, д. 12', 3, 43, 3, N'53к4253'),
(N'г. Подольск, ул. Климовская, д. 36а', 1, 13, 1, N'66к1483');

insert into Ordered(idAddress, orderDateTime, deliveryDate, deliveryTimeFrom, deliveryTimeTo, summ, idClient, idOrderStatus) values
(1, now(), '2021-03-30', '09:00', '22:00', 9500.0, 1, 1),
(2, now(), '2021-04-02', '09:00', '21:00', 10500.0, 2, 1),
(3, now(), '2021-04-01', '10:00', '20:00', 3200.0, 3, 1);

insert into StoreInfo(name, fullname, tin, bank, bic, email, phone, address) values
(N'Pleer.ru', N'ООО Плеер точка ру', '8003745819', N'Сбербанк', '567435440', 'mail@pleer.ru', '89526545654', N'г. Москва, ул. Серпуховская, д. 19'),
(N'Эльдорадо', N'ООО Эльдорадо', '1287003819', N'Альфа-банк', '900435440', 'mail@el.ru', '89251228456', N'г. Москва, ул. Красныйх фонарей, д. 123'),
(N'DNS', N'ООО ДНС груп', '8999745819', N'Сбербанк', '567923440', 'mail@dns.ru', '84300545654', N'г. Москва, ул. Камугная, д. 27'),
(N'Ситилинк', N'ООО Citylink', '8926745819', N'Chease', '110025440', 'mail@cit.ru', '83423545654', N'г. Москва, ул. Зелёная, д. 3'),
(N'Мвидео', N'ООО Мвидео груп', '8666745819', N'Сбербанк', '137435440', 'mail@mv.ru', '82286545654', N'г. Москва, ул. Меруэмная, д. 6');

/*delete from ProductCategory*/
insert into ProductCategory(name, idParentCategory) values
(N'Электроника', null),
(N'Компьютерная техника', null),
(N'Бытовая техника', null),
(N'Книги', null),
(N'Мебель', null),
(N'Зоотовары', null);

/*SET SQL_SAFE_UPDATES = 0;*/
update ProductCategory set imageUrl = N'https://avatars.mds.yandex.net/get-pdb/2852074/ac8b053e-59d4-40cb-8c23-6ce9b8cd5bc4/s1200' where name = N'Электроника';
update ProductCategory set imageUrl = N'https://avatars.mds.yandex.net/get-pdb/4516377/799f20f8-23ac-4c79-ba98-605ef500a007/s1200' where name = N'Компьютерная техника';
update ProductCategory set imageUrl = N'https://avatars.mds.yandex.net/get-pdb/4571085/553bef76-e3f8-45d0-934a-57cc401502d0/s1200' where name = N'Бытовая техника';
update ProductCategory set imageUrl = N'https://avatars.mds.yandex.net/get-pdb/2434617/150b9b90-f1c3-4a07-8d86-4f5e91969560/s1200' where name = N'Книги';
update ProductCategory set imageUrl = N'https://avatars.mds.yandex.net/get-pdb/4265498/f6fb9e7d-b7b8-4ad4-9a06-6b4decbe9125/s1200' where name = N'Мебель';
update ProductCategory set imageUrl = N'https://avatars.mds.yandex.net/get-pdb/4263207/0c82c267-27d8-4325-8b96-ed3d85ed8d6c/s1200' where name = N'Зоотовары';

insert into ProductCategory(name, idParentCategory) values
(N'Смартфоны и аксессуары', 1),
(N'Гейминг', 1),
(N'Наушники', 1),
(N'Телевизоры', 1),
(N'Переферийные устройства', 2),
(N'Мониторы и аксессуары', 2),
(N'Техника для кухни', 3),
(N'Техника для дома', 3),
(N'Умный дом', 3),
(N'Комиксы и манга', 4),
(N'Художественная литература', 4),
(N'Психология и саморазвитие', 4),
(N'Столы и стулья', 5),
(N'Мебель для спальни', 5),
(N'Мебель для кухни', 5),
(N'Для кошек', 6),
(N'Для собак', 6),
(N'Для птиц', 6);
insert into ProductCategory(name, idParentCategory) values
(N'Playstation 4', 8),
(N'Playstation 5', 8),
(N'Мышки', 11),
(N'Мониторы', 12);

/*delete from Product*/
insert into Product(name, cost, idStoreInfo, idCategory) values
(N'Геймпад Dualshock4', 4990.0, 1, (select id from ProductCategory where name=N'Playstation 4')),
(N'Геймпад Dualshock5', 5990.0, 1, (select id from ProductCategory where name=N'Playstation 5')),
(N'Мышка Bloody Z36P2', 2390.0, 1, (select id from ProductCategory where name=N'Мышки')),
(N'Наушники Jbl M25', 4990.0, 1, (select id from ProductCategory where name=N'Наушники')),
(N'Монитор Philips M20J3', 10990.0, 1, (select id from ProductCategory where name=N'Мониторы')),
(N'Геймпад Dualshock4', 4890.0, 2, (select id from ProductCategory where name=N'Playstation 4')),
(N'Геймпад Dualshock5', 6390.0, 2, (select id from ProductCategory where name=N'Playstation 5')),
(N'Мышка Bloody Z36P2', 1990.0, 2, (select id from ProductCategory where name=N'Мышки')),
(N'Наушники Jbl M25', 4890.0, 2, (select id from ProductCategory where name=N'Наушники')),
(N'Монитор Philips M20J3', 10830.0, 2, (select id from ProductCategory where name=N'Мониторы')),
(N'Геймпад Dualshock4', 4890.0, 3, (select id from ProductCategory where name=N'Playstation 4')),
(N'Геймпад Dualshock5', 5890.0, 3, (select id from ProductCategory where name=N'Playstation 5')),
(N'Мышка Bloody Z36P2', 2290.0, 3, (select id from ProductCategory where name=N'Мышки')),
(N'Наушники Jbl M25', 4990.0, 3, (select id from ProductCategory where name=N'Наушники')),
(N'Монитор Philips M20J3', 11390.0, 3, (select id from ProductCategory where name=N'Мониторы')),
(N'Геймпад Dualshock4', 4790.0, 4, (select id from ProductCategory where name=N'Playstation 4')),
(N'Геймпад Dualshock5', 5900.0, 4, (select id from ProductCategory where name=N'Playstation 5')),
(N'Мышка Bloody Z36P2', 2300.0, 4, (select id from ProductCategory where name=N'Мышки')),
(N'Наушники Jbl M25', 4900.0, 4, (select id from ProductCategory where name=N'Наушники')),
(N'Монитор Philips M20J3', 10900.0, 4, (select id from ProductCategory where name=N'Мониторы')),
(N'Геймпад Dualshock4', 4900.0, 5, (select id from ProductCategory where name=N'Playstation 4')),
(N'Геймпад Dualshock5', 5900.0, 5, (select id from ProductCategory where name=N'Playstation 5')),
(N'Мышка Bloody Z36P2', 2380.0, 5, (select id from ProductCategory where name=N'Мышки')),
(N'Наушники Jbl M25', 4900.0, 5, (select id from ProductCategory where name=N'Наушники')),
(N'Монитор Philips M20J3', 10900.0, 5, (select id from ProductCategory where name=N'Мониторы'));

insert into ProductCompos(idOrder, idProduct, quantity, summ) values
(1, (select id from Product where name=N'Геймпад Dualshock4' and idStoreInfo=1), 1, (select cost from Product where name=N'Геймпад Dualshock4' and idStoreInfo=1)*1),
(1, (select id from Product where name=N'Мышка Bloody Z36P2' and idStoreInfo=2), 2, (select cost from Product where name=N'Мышка Bloody Z36P2' and idStoreInfo=2)*2),
(2, (select id from Product where name=N'Наушники Jbl M25' and idStoreInfo=3), 1, (select cost from Product where name=N'Наушники Jbl M25' and idStoreInfo=3)*1),
(2, (select id from Product where name=N'Монитор Philips M20J3' and idStoreInfo=3), 1, (select cost from Product where name=N'Монитор Philips M20J3' and idStoreInfo=3)*1),
(3, (select id from Product where name=N'Геймпад Dualshock4' and idStoreInfo=1), 1, (select cost from Product where name=N'Геймпад Dualshock4' and idStoreInfo=1)*1),
(3, (select id from Product where name=N'Геймпад Dualshock5' and idStoreInfo=4), 1, (select cost from Product where name=N'Геймпад Dualshock5' and idStoreInfo=4)*2);

insert into ProductImages(idProduct, imageUrl) values
((select id from Product where name=N'Геймпад Dualshock4' and idStoreInfo=1), N'https://www.ucustom.nl/wp-content/uploads/2019/01/PS4-Controller-Skin-Zwart-1.png'),
((select id from Product where name=N'Геймпад Dualshock4' and idStoreInfo=2), N'https://images.wbstatic.net/big/new/20820000/20822789-1.jpg'),
((select id from Product where name=N'Геймпад Dualshock4' and idStoreInfo=3), N'https://images-eu.ssl-images-amazon.com/images/I/41GPLTxGE6L.01_SL120_.jpg'),
((select id from Product where name=N'Геймпад Dualshock4' and idStoreInfo=4), N'https://www.ucustom.nl/wp-content/uploads/2019/01/PS4-Controller-Skin-Zwart-1.png'),
((select id from Product where name=N'Геймпад Dualshock4' and idStoreInfo=5), N'https://images-eu.ssl-images-amazon.com/images/I/41GPLTxGE6L.01_SL120_.jpg');
insert into ProductImages(idProduct, imageUrl) values
((select id from Product where name=N'Мышка Bloody Z36P2' and idStoreInfo=1), N'https://e7.pngegg.com/pngimages/465/617/png-clipart-computer-mouse-a4tech-bloody-gaming-a4-tech-bloody-v7m-a4tech-bloody-v7-computer-mouse-electronics-computer-keyboard.png'),
((select id from Product where name=N'Мышка Bloody Z36P2' and idStoreInfo=2), N'https://e7.pngegg.com/pngimages/465/617/png-clipart-computer-mouse-a4tech-bloody-gaming-a4-tech-bloody-v7m-a4tech-bloody-v7-computer-mouse-electronics-computer-keyboard.png'),
((select id from Product where name=N'Мышка Bloody Z36P2' and idStoreInfo=3), N'https://e7.pngegg.com/pngimages/465/617/png-clipart-computer-mouse-a4tech-bloody-gaming-a4-tech-bloody-v7m-a4tech-bloody-v7-computer-mouse-electronics-computer-keyboard.png'),
((select id from Product where name=N'Мышка Bloody Z36P2' and idStoreInfo=4), N'https://e7.pngegg.com/pngimages/465/617/png-clipart-computer-mouse-a4tech-bloody-gaming-a4-tech-bloody-v7m-a4tech-bloody-v7-computer-mouse-electronics-computer-keyboard.png'),
((select id from Product where name=N'Мышка Bloody Z36P2' and idStoreInfo=5), N'https://e7.pngegg.com/pngimages/465/617/png-clipart-computer-mouse-a4tech-bloody-gaming-a4-tech-bloody-v7m-a4tech-bloody-v7-computer-mouse-electronics-computer-keyboard.png');
insert into ProductImages(idProduct, imageUrl) values
((select id from Product where name=N'Наушники Jbl M25' and idStoreInfo=1), N'https://static.onlinetrade.ru/img/items/b/besprovodnie_naushniki_jbl_e45bt_krasniy_2.jpg'),
((select id from Product where name=N'Наушники Jbl M25' and idStoreInfo=2), N'https://24.lv/images/detailed/484/JBL_LIVE500BT_Product_Image_Fold_White_17273_x1.png'),
((select id from Product where name=N'Наушники Jbl M25' and idStoreInfo=3), N'https://img.mvideo.ru/Pdb/50125455b2.jpg'),
((select id from Product where name=N'Наушники Jbl M25' and idStoreInfo=4), N'https://static.onlinetrade.ru/img/items/b/besprovodnie_naushniki_jbl_e45bt_krasniy_2.jpg'),
((select id from Product where name=N'Наушники Jbl M25' and idStoreInfo=5), N'https://img.mvideo.ru/Pdb/50125455b2.jpg');
insert into ProductImages(idProduct, imageUrl) values
((select id from Product where name=N'Монитор Philips M20J3' and idStoreInfo=1), N'https://www.1sm.ru/upload/iblock/274/00000112446.JPG'),
((select id from Product where name=N'Монитор Philips M20J3' and idStoreInfo=2), N'https://www.1sm.ru/upload/iblock/274/00000112446.JPG'),
((select id from Product where name=N'Монитор Philips M20J3' and idStoreInfo=3), N'https://www.1sm.ru/upload/iblock/274/00000112446.JPG'),
((select id from Product where name=N'Монитор Philips M20J3' and idStoreInfo=4), N'https://www.1sm.ru/upload/iblock/274/00000112446.JPG'),
((select id from Product where name=N'Монитор Philips M20J3' and idStoreInfo=5), N'https://www.1sm.ru/upload/iblock/274/00000112446.JPG');
insert into ProductImages(idProduct, imageUrl) values
((select id from Product where name=N'Геймпад Dualshock5' and idStoreInfo=1), N'https://static-sl.insales.ru/images/products/1/1394/416662898/1.png'),
((select id from Product where name=N'Геймпад Dualshock5' and idStoreInfo=2), N'https://static-sl.insales.ru/images/products/1/1394/416662898/1.png'),
((select id from Product where name=N'Геймпад Dualshock5' and idStoreInfo=3), N'https://static-sl.insales.ru/images/products/1/1394/416662898/1.png'),
((select id from Product where name=N'Геймпад Dualshock5' and idStoreInfo=4), N'https://static-sl.insales.ru/images/products/1/1394/416662898/1.png'),
((select id from Product where name=N'Геймпад Dualshock5' and idStoreInfo=5), N'https://static-sl.insales.ru/images/products/1/1394/416662898/1.png');

/*
select * from ProductImages
select * from Product where name=N'Геймпад Dualshock4'
select * from Courier
select * from Ordered
select
	ProductCategory.name as [category],
	parentCategory.name as [parent],
	ProductCategory.imageUrl as [img]
from ProductCategory
	left join ProductCategory as parentCategory on ProductCategory.idParentCategory = parentCategory.id
    */
    
/*select * from Product;*/