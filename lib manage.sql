create database LibraryManagement

create table LoginDetails
(
UserId int primary key,
UserPW varchar(20)
)
insert into LoginDetails values (101,'hema24'),(102,'priya67'),(103,'mary@34')

select * from LoginDetails
--drop table LoginDetails
create table StudentDetails
(
StudentName varchar(30),
StudentRollno int primary key,
StudentEmail varchar(40),
StudentDept varchar(50),

)
select * from StudentDetails
--drop table StudentDetails
create table BookDetails
(
BookId int identity primary key,
BookName varchar(40),
Author varchar(30),
Publications varchar(40),
Quantity int
)
select * from BookDetails
--drop table BookDetails
create table IssueBook
(
StudentRollno int references StudentDetails(StudentRollno),
BookId int references BookDetails(BookId) ,
IssueDate datetime,
primary key (StudentRollno)
)
--Update IssueBook Set StudentRollno=6001 where BookId=111
select * from IssueBook
--drop table IssueBook


select * from LoginDetails

select * from StudentDetails
select * from BookDetails
select * from IssueBook
