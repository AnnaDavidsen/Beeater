use master;
go

drop database MovieBeeater;
go

create database MovieBeeater;
go

use MovieBeeater
go

create table [Authorization](
id int primary key not null identity(1,1),
userauth binary,
movieauth binary,
showingauth binary,
sceneauth binary,
bookingauth binary);

create table [User](
id int primary key not null identity(1,1),
firstname nvarchar(50) not null,
lastname nvarchar(50) not null,
email nvarchar(100) not null,
birthdate date not null,
bonusPoints int);

create table Employee(
id int primary key not null identity(1,1),
firstname nvarchar(50) not null,
lastname nvarchar(50) not null,
email nvarchar(100) not null,
authorizationId int foreign key references [Authorization](id),
title nvarchar(20));

create table Genre(
id int primary key not null identity(1,1),
name nvarchar(50) unique not null);

create table Preferences(
id int primary key not null identity(1,1),
userId int not null foreign key references [User](id),
genreId int not null foreign key references Genre(id));

create table Movie(
id int primary key not null identity(1,1),
title nvarchar(200) not null,
duration time not null,
releaseDate date not null,
ageRating int,
genreId int not null);

create table Rating(
id int primary key not null identity(1,1),
rating int not null check(rating between 1 and 5),
userId int not null foreign key references [User](id),
movieId int not null foreign key references Movie(id)
);


create table Trailer(
id int primary key not null identity(1,1),
movieid int foreign key references Movie(id),
path nvarchar(250) not null)

create table Theater(
id int primary key not null identity(1,1),
[name] nvarchar(10) not null)

create table Show(
id int primary key not null identity(1,1),
movieId int foreign key references Movie(id),
theaterId int foreign key references Theater(id),
showTime date not null);

create table Seat(
id int primary key not null identity(1,1),
row int not null,
number int not null,
theaterId int foreign key references Theater(id));

create table Booking(
id int primary key not null identity(1,1),
userId int foreign key references [User](id),
showId int foreign key references Show(id),
seatId int foreign key references Seat(id));