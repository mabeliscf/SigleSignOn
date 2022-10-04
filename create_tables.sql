
Create database QRAchallenge;


use QRAchallenge
CREATE SCHEMA  qra;

Create table qra.tenants (
idTenant bigint identity(1,1) primary key not null,
fullname varchar(150) not null,
email varchar(50) not null,
phone varchar(20) not null
)

create table qra.tenantsLogin (
idTenantLogin bigint identity(1,1) primary key not null,
idTenant bigint not null,
username nvarchar(50) not null,
passwordEncrypted nvarchar(50) not null,
loginType int not null,
token nvarchar(255) 
foreign key (idTenant) REFERENCES qra.tenants(idTenant)
)

create table qra.tenantsRole(
idTenantRole bigint identity(1,1) primary key not null,
idTenant bigint not null,
roleDescription nvarchar(100) not null,
roleFather bigint not null,
foreign key (idTenant) REFERENCES qra.tenants(idTenant)
)

create table qra.db (
idDB bigint identity(1,1) primary key not null,
dbSchema nvarchar(10) not null,
dbName nvarchar(50) not null,
serverName nvarchar(50) not null,
serverRoute nvarchar(100) not null
)


create table qra.dbAccess (
idDbAccess bigint identity(1,1) primary key not null,
idTenant bigint not null,
idDB bigint not null,
foreign key (idTenant) REFERENCES qra.tenants(idTenant),
foreign key (idDB) REFERENCES qra.db(idDB)
)

create table qra.users (
idUser bigint identity(1,1) primary key not null,
idTenant  bigint not null,
fullname varchar(150) not null,
email varchar(50) not null,
phone varchar(20) not null
foreign key (idTenant) REFERENCES qra.tenants(idTenant),
)

create table qra.userRole (
idUserRole bigint identity(1,1) primary key not null,
idUser bigint not null,
roleDescription nvarchar(100) not null,
roleFather bigint not null,
foreign key (idUser) REFERENCES qra.users(idUser)
)