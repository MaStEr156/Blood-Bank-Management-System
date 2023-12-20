CREATE TABLE Don(
	DonorID VARCHAR(14) PRIMARY KEY,
    Name VARCHAR(255), 
    BType VARCHAR(3), 
    LastDate DATE, 
    Age INT,
    Gender VARCHAR(10),
    Phone VARCHAR(15), 
    Address VARCHAR(MAX), 
    Diabetes VARCHAR(10), 
    BPressure VARCHAR(10)
);
Select DonorID ,Name,BType,LastDate,Age,Gender,Phone,Address,Diabetes,BPressure from Don;
insert into Donors values(30303011680768,'soha ahmed','A+',20,12-222,'male',1128709613,'32s helmey',0,0);
CREATE TABLE Stockk(
    StockID INT PRIMARY KEY IDENTITY(1,1),
    BType VARCHAR(3),
    BDate DATE,
    DonorID VARCHAR(14) FOREIGN KEY REFERENCES Donors(DonorID),
    BSource VARCHAR(50)
);
select  BType  from stockk;
CREATE TABLE Patien(
	PatientID VARCHAR(14) PRIMARY KEY,
	Name VARCHAR(255),
	BType VARCHAR(3),
	LastDate DATE,
	Age INT, 
	Gender BIT,
	Phone VARCHAR(15),
	Address VARCHAR(MAX)
);

CREATE TABLE Requess(
	ReqID INT PRIMARY KEY IDENTITY(1,1),
	HosName VARCHAR(255),
	ReqDate DATE,
	APos INT,
	ANeg INT,
	BPos INT, 
	BNeg INT,
	OPos INT,
	ONeg INT,
	ABPos INT,
	ABNeg INT
);