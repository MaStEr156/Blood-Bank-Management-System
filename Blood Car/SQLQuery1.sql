Select DonorID ,Name,BType,LastDate,Age,Gender,Phone,Address,Diabetes,BPressure from Donors;
Delete from Donors where DonorID=25876587922184;
insert into Donors values(30303011680768,'soha ahmed','A+',20,12-222,'male',1128709613,'32s helmey',0,0);
CREATE TABLE Donor (
	DonorID VARCHAR(14) PRIMARY KEY,
    Name VARCHAR(255), 
    BType VARCHAR(3), 
    LastDate DATE, 
    Age INT,
    Gender VARCHAR(10), 
    Phone VARCHAR(15), 
    Address VARCHAR(MAX), 
    Diabetes VARCHAR(10), 
    BPressure VARCHAR(10),
	state varchar(10)

);
UPDATE Donor
SET state = 'valid'
FROM Donor
JOIN Stock ON Donor.DonorID = Stock.DonorID
WHERE Donor.DonorID =30303011608768;
Select DonorID ,Name,BType,LastDate,Age,Gender,Phone,Address,Diabetes,BPressure from Donors where Name like 'a%';
            Select * from Donor ;
update Donors set Name = 'sohaa', Phone=01128709615 , Address='mahallla', Age=21  where DonorID= 30303011608768;
update Donors set Name = 'sohaaa'where DonorID=30303011608768;
CREATE TABLE Stock (
    StockID INT PRIMARY KEY IDENTITY(1,1),
    BType VARCHAR(3),
    BDate DATE,
    DonorID VARCHAR(14) FOREIGN KEY REFERENCES Donors(DonorID),
    BSource VARCHAR(50)
);
Select * from Stock; 
Select DonorID from Stock;
drop table Stock;
CREATE TABLE Patients(
	PatientID VARCHAR(14) PRIMARY KEY,
	Name VARCHAR(255),
	BType VARCHAR(3),
	LastDate DATE,
	Age INT, 
	Gender VARCHAR(6),
	Phone VARCHAR(15),
	Address VARCHAR(MAX)
);
select BType  ,count(*) Quantity from stock  group by BType
select *from Patients;
insert into Patients(PatientID, Name, Age, Phone, Address, BType, LastDate, Gender) values(30303011680768,'soha ahmed','A+',20,12-222,'male',1128709613,'32s helmey');

CREATE TABLE Requests(
	ReqID INT PRIMARY KEY IDENTITY(1,1),
	HosName VARCHAR(255),
	SubDate DATE,
	ReqDate DATE,
	Note VARCHAR(MAX),
	Phone VARCHAR(14),
	Email VARCHAR(255),
	APos INT,
	ANeg INT,
	BPos INT, 
	BNeg INT,
	OPos INT,
	ONeg INT,
	ABPos INT,
	ABNeg INT
);

CREATE TABLE HStock (
    StockID INT PRIMARY KEY IDENTITY(1,1),
    BType VARCHAR(3),
    BDate DATE,
    DonorID VARCHAR(14) FOREIGN KEY REFERENCES Donors(DonorID),
    BSource VARCHAR(50)
);