CREATE TABLE Donors (
	DonorID VARCHAR(14) PRIMARY KEY,
    Name VARCHAR(255), 
    BType VARCHAR(3), 
    LastDate DATE, 
    Age INT,
    Gender VARCHAR(6), 
    Phone VARCHAR(15), 
    Address VARCHAR(MAX), 
    Diabetes VARCHAR(10), 
    BPressure VARCHAR(10)
);
CREATE TABLE Stock (
    StockID INT PRIMARY KEY IDENTITY(1,1),
    BType VARCHAR(3),
    BDate DATE,
    DonorID VARCHAR(14) FOREIGN KEY REFERENCES Donors(DonorID),
    BSource VARCHAR(50)
);
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
    BType VARCHAR(3) PRIMARY KEY,
    BCount INT
);
insert into HStock values('A+',4);
insert into HStock values('B+',8);
insert into HStock values('AB+',3);
insert into HStock values('O+',1);
insert into HStock values('B-',10);
insert into HStock values('A-',15);
insert into HStock values('AB-',5);
insert into HStock values('O-',0);


CREATE TABLE CarStock (
    StockID INT PRIMARY KEY IDENTITY(1,1),
    BType VARCHAR(3),
    BDate DATE,
    DonorID VARCHAR(14) FOREIGN KEY REFERENCES Donors(DonorID),
    BSource VARCHAR(50)
);
INSERT INTO Requests (HosName, SubDate, ReqDate, Note, Phone, Email, APos, ANeg, BPos, BNeg, OPos, ONeg, ABPos, ABNeg)
VALUES
('Hospital1', '2023-01-01', '2023-01-05', 'Note1', '123-456-7890', 'email1@example.com', 2, 3, 1, 0, 4, 2, 1, 0),
('Hospital2', '2023-02-02', '2023-02-08', 'Note2', '987-654-3210', 'email2@example.com', 1, 2, 0, 1, 3, 1, 0, 1),
('Hospital3', '2023-03-03', '2023-03-10', 'Note3', '555-123-4567', 'email3@example.com', 3, 1, 2, 0, 5, 3, 2, 1),
-- Add 7 more rows with similar syntax
('Hospital4', '2023-04-04', '2023-04-12', 'Note4', '111-222-3333', 'email4@example.com', 0, 4, 1, 2, 1, 0, 3, 2),
('Hospital5', '2023-05-05', '2023-05-15', 'Note5', '444-555-6666', 'email5@example.com', 2, 0, 3, 1, 2, 1, 0, 4),
('Hospital6', '2023-06-06', '2023-06-20', 'Note6', '999-888-7777', 'email6@example.com', 1, 3, 0, 4, 1, 0, 2, 3),
('Hospital7', '2023-07-07', '2023-07-25', 'Note7', '777-888-9999', 'email7@example.com', 4, 2, 1, 0, 3, 2, 1, 0),
('Hospital8', '2023-08-08', '2023-08-30', 'Note8', '333-222-1111', 'email8@example.com', 0, 1, 2, 3, 4, 1, 0, 2),
('Hospital9', '2023-09-09', '2023-09-10', 'Note9', '222-333-4444', 'email9@example.com', 3, 0, 4, 2, 1, 0, 3, 1),
('Hospital10', '2023-10-10', '2023-10-18', 'Note10', '666-555-4444', 'email10@example.com', 1, 2, 3, 0, 2, 1, 4, 0);


INSERT INTO Stock (BType, BDate, DonorID, BSource)
VALUES 
('O+', '2023-01-15', 'D12345678901', 'Hospital A'),
('A-', '2023-02-20', 'D23456789012', 'Clinic B'),
('B+', '2023-03-10', 'D34567890123', 'Blood Drive 1'),
('AB+', '2023-04-05', 'D45678901234', 'Hospital C'),
('O-', '2023-05-12', 'D56789012345', 'Blood Drive 2'),
('A+', '2023-06-08', 'D67890123456', 'Clinic D'),
('B-', '2023-07-17', 'D78901234567', 'Hospital E'),
('AB-', '2023-08-23', 'D89012345678', 'Blood Drive 3'),
('O+', '2023-09-30', 'D90123456789', 'Hospital F'),
('A+', '2023-10-14', 'D01234567890', 'Clinic G');
INSERT INTO Patients (PatientID, Name, BType, LastDate, Age, Gender, Phone, Address)
VALUES 
('12345678901', 'Alice Johnson', 'AB-', '2023-01-10', 32, 'Female', '+98765432101', '123 Oak St, Cityville'),
('23456789012', 'Bob Smith', 'O+', '2023-02-15', 45, 'Male', '+98765432102', '456 Pine St, Townsville'),
('34567890123', 'Catherine Davis', 'A+', '2023-03-20', 28, 'Female', '+98765432103', '789 Maple St, Villagetown'),
('45678901234', 'Daniel Wilson', 'B-', '2023-04-25', 35, 'Male', '+98765432104', '101 Elm St, Hamletville'),
('56789012345', 'Eva Brown', 'O-', '2023-05-30', 40, 'Female', '+98765432105', '222 Birch St, Countryside'),
('67890123456', 'Frank Miller', 'AB+', '2023-06-05', 55, 'Male', '+98765432106', '333 Cedar St, Suburbia'),
('78901234567', 'Grace White', 'B+', '2023-07-10', 27, 'Female', '+98765432107', '444 Walnut St, Uptown'),
('89012345678', 'Henry Taylor', 'A-', '2023-08-15', 38, 'Male', '+98765432108', '555 Pineapple St, Downtown'),
('90123456789', 'Isabel Anderson', 'O+', '2023-09-20', 33, 'Female', '+98765432109', '666 Mango St, Outskirts'),
('01234567890', 'James Brown', 'B+', '2023-10-25', 30, 'Male', '+98765432110', '777 Peach St, Farmland');
select * from HStock;
select * from Stock;
select * from CarStock;
select* from Employee;
select *from Donors;