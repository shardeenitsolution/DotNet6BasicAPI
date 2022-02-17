Create Table States
(
Id int identity(100,1) not null primary key,
Name varchar(100) not null unique,
)

insert into States values('Delhi');
insert into States values('Uttar Pradesh');
insert into States values('Goa');
insert into States values('Arunachal Pradesh');
insert into States values('Andhra Pradesh');
insert into States values('Kerala');
insert into States values('Tamil Nadu');
insert into States values('Bihar');
insert into States values('Assam');
insert into States values('Chhattisgarh');
insert into States values('Haryana');
insert into States values('Gujarat');

Create Table Subjects
(
Id int identity(100,1) not null primary key,
Name varchar(100) not null unique,
Code varchar(100) not null unique,
)

Create Table AcademicYears
(
Id int identity(100,1) not null primary key,
Year varchar(100) not null unique,
)

Create Table MasterCourses
(
Id int identity(100,1) not null primary key,
Name varchar(100) not null unique,
Code varchar(100) not null unique,
SemesterNumber int not null,
LaunchDate date not null,
IsActive bit not null default(1),
ModifiedOn datetime not null,
ModifiedBy nvarchar(450) not null foreign key references AspNetUsers(Id),
)

Create Table MasterClasses
(
Id int identity(100,1) not null primary key,
MasterCourseId int not null foreign key references MasterCourses(Id),
Name varchar(100) not null unique,
IsActive bit not null default(1),
ModifiedOn datetime not null,
ModifiedBy nvarchar(450) not null foreign key references AspNetUsers(Id),
)
