
----------------------------------------- Run this code first to create the database -----------------------------------------
use master
IF EXISTS(SELECT * FROM sys.databases where name = 'Project')
DROP DATABASE Project

CREATE DATABASE Project

-------------------------------------- Tne run this till the end to create tables -----------------------------------------
USE Project

--------------------------------------- Supplier -----------------------------------------
CREATE TABLE Supplier
(
SupplierId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
SupplierCode VARCHAR(30),
SupplierName VARCHAR(100),
ContactName VARCHAR(100),
PhNo VARCHAR(50),
FaxNo VARCHAR(50),
[Address] VARCHAR(250),
GstRegistrationNo VARCHAR(100)
)

INSERT INTO Supplier
VALUES ('AlPA','ALPHA Office Supplies', 'Ms Irene Tan', '461 9928', '461 2238', 
		'Blk 1128, Ang Mo Kio Industrial Park, #02-1108 Ang Mo Kio Street 62, Singapore 622262',
		'MR-8500440-2')
INSERT INTO Supplier
VALUES ('CHEP','Cheap Stationer', 'Mr Soh Kway Koh', '354 3234', '474 2434', 
		'Blk 34, Blk 34, Clementi road, #02-70 Ban Ban Soh Building, Singapore 110525',
		'Nil')
INSERT INTO Supplier
VALUES ('BANE','BANES Shop', 'Mr Loh Ah Pek', '478 1234', '479 2434', 
		'Blk 124, Alexandra Road, #03-04 Banes Building, Singapore 550315',
		'MR-8200420-2')
INSERT INTO Supplier
VALUES ('OMEG','OMEGA Stationery Supplier', 'Mr Ronnie ho', '767 1233', '767 1234', 
		'Blk 11 Hillview Avenue, #03-04, Singapore 679036',
		'MR-8555330-1')

----------------------------------------- Role -----------------------------------------
CREATE TABLE [Role]
(
RoleId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(100),
[Description] VARCHAR(250)
)

INSERT INTO [Role]
VALUES ('Store Clerk', 'Store Clerk')
INSERT INTO [Role]
VALUES ('Department Head', 'Head')
INSERT INTO [Role]
VALUES ('Employee', 'Normal Staff')
INSERT INTO [Role]
VALUES ('Representative', 'Department Representative')
INSERT INTO [Role]
VALUES ('Store Supervisor', 'Store sup')

----------------------------------------- Permission -----------------------------------------
CREATE TABLE Permission
(
PermissionId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
ApproveRequisition BIT,
ChangeCollectionPoint BIT,
ViewRequisition BIT,
MakeRequisition BIT,
DelegateRole BIT,
ViewCollectionDetails BIT
)

INSERT INTO Permission
VALUES (1,1,0,0,1,1),(1,0,1,0,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),
(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),
(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1);


----------------------------------------- Collection Points -----------------------------------------
CREATE TABLE CollectionPoints
(
CollectionPointId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
PlaceName VARCHAR(100),
CollectTime TIME,
EmployeeId INT
)

----------------------------------------- Employee -----------------------------------------
CREATE TABLE Employee
(
EmployeeId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
EmployeeName VARCHAR(100),
Email VARCHAR(50),
DepartmentId INT,
RoleId INT,
PermissionId INT,
PhNo VARCHAR(50),
Token VARCHAR(200)
CONSTRAINT RoleId FOREIGN KEY(RoleId) REFERENCES [Role](RoleId),
CONSTRAINT PermissionId FOREIGN KEY(PermissionId) REFERENCES Permission(PermissionId)
)

----------------------------------------- Department -----------------------------------------
CREATE TABLE Department
(
DepartmentId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
DepartmentCode VARCHAR(30),
DepartmentName VARCHAR(100),
ContactName VARCHAR(100),
PhNo VARCHAR(50),
FaxNo VARCHAR(50),
HeadId INT,
CollectionPointId INT,
RepresentativeId INT,
CONSTRAINT CollectionPointId FOREIGN KEY(CollectionPointId) REFERENCES CollectionPoints(CollectionPointId)
)
----------------------------------------- End of Department -----------------------------------------

----------------------------------------- Add Collection Points-----------------------------------------
INSERT INTO CollectionPoints
VALUES ('Stationert Store','9:30 am',1)
INSERT INTO CollectionPoints
VALUES ('Management School','11:00 am',1)
INSERT INTO CollectionPoints
VALUES ('Medical School','9:30 am',1)
INSERT INTO CollectionPoints
VALUES ('Engineering School','11:00 am',1)
INSERT INTO CollectionPoints
VALUES ('Science School','9:30 am',1)
INSERT INTO CollectionPoints
VALUES ('University Hospital','11:00 am',1)

----------------------------------------- Add Department ----------------------------------------
INSERT INTO Department
VALUES ('ENGL','English Department','Mrs Pamela Kow','874 2234','892 2234',5,1,11)
INSERT INTO Department
VALUES ('CPSC','Computer Science','Mr. Wee Kian Fatt','890 1235','892 1457',6,1,12)
INSERT INTO Department
VALUES ('COMM','Commerce Department','Mr Mohd. Azman','874 1284','892 1256',7,1,13)
INSERT INTO Department
VALUES ('REGR','Registrar Department','Ms Helen Ho','890 1266','892 1465',8,1,14)
INSERT INTO Department
VALUES ('ZOOL','Zoology Department','Mr. Peter Tan Ah Meng','890 1266','892 1465',9,1,15)
INSERT INTO Department
VALUES ('STO','STORE','Jenny Wong Mei Lin','890 6656','891 9912',10,1,1)

----------------------------------------- Add Employee -----------------------------------------
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Jenny Wong Mei Lin','youngmountain7@gmail.com',6,4,1,'11111111') -- registrar

INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Feng Teng','youngmountain7@gmail.com',6,1,2,'22222222') -- store 6 store clerk 2
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Min Yew','youngmountain7@gmail.com',6,1,3,'33333333')
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Lao Lao','youngmountain7@gmail.com',6,1,4,'44444444')

INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Mrs Pamela Kow','youngmountain7@gmail.com',1,2,5,'55555555') -- english 1 Head 3
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Dr. Soh Kian Wee','youngmountain7@gmail.com',2,2,6,'66666666')-- com science 2 
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('MrMohd. Azman','youngmountain7@gmail.com',3,2,7,'77777777') -- commerce 3
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Mrs Low Kway Boo','youngmountain7@gmail.com',4,2,8,'88888888') -- registrar 4
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Mr. Peter Tan Ah Meng','youngmountain7@gmail.com',5,2,9,'99999999') -- zoo 5
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Mr. Sander','youngmountain7@gmail.com',6,2,10,'10101010') -- store 6

INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Prof Ezra Pound','youngmountain7@gmail.com',1,4,11,'11111111') -- eng 1 rep 4
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Mr. Ar Phyan Kwee','youngmountain7@gmail.com',2,4,12,'12121212') -- com science 2
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Dr. Chia Leow Bee','youngmountain7@gmail.com',3,4,13,'13131313') -- commerce 3
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Prof Tan','youngmountain7@gmail.com',4,4,14,'14141414') -- registrar 4
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Mr. Tay Shout Pann','youngmountain7@gmail.com',5,4,15,'15151515') -- zoo 5

INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Mr Alan','youngmountain7@gmail.com',1,3,5,'55555555') -- english 1 Head 3
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Mr Bob','youngmountain7@gmail.com',2,3,6,'66666666')-- com science 2 
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Mr Charlie','youngmountain7@gmail.com',3,3,7,'77777777') -- commerce 3
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Ms Delia','youngmountain7@gmail.com',4,3,8,'88888888') -- registrar 4
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Ms Eve','youngmountain7@gmail.com',5,3,9,'99999999') -- zoo 5


----------------------------------------- End of Adding Employee ----------------------------------------------------------------------------

ALTER TABLE Employee
ADD CONSTRAINT DepartmentId FOREIGN KEY(DepartmentId) REFERENCES Department(DepartmentId)

ALTER TABLE CollectionPoints
ADD CONSTRAINT eid FOREIGN KEY(EmployeeId) REFERENCES Employee(EmployeeId)

ALTER TABLE Department
ADD CONSTRAINT EmployeeId FOREIGN KEY(RepresentativeId) REFERENCES Employee(EmployeeId)
ALTER TABLE Department
ADD CONSTRAINT DepartmentHeadId FOREIGN KEY(HeadId) REFERENCES Employee(EmployeeId)

-------------------------------------------------- Requisition ----------------------------------------
CREATE TABLE Requisition
(
RequisitionId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

EmployeeId INT,
DepartmentId INT,
ApprovedBy INT,
OrderedDate Date,
ApprovedDate Date,
RequisitionStatus VARCHAR(50),
Comment VARCHAR(250),
RetrievalId INT,
CONSTRAINT RequisitionEmployeeId FOREIGN KEY(EmployeeId) REFERENCES Employee(EmployeeId)
)

-------------------------------------------------- Category ----------------------------------------
CREATE TABLE Category
(
CategoryId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
CategoryName VARCHAR(100)
)

INSERT INTO Category
VALUES ('Clip'), ('Envelope'), ('Eraser'), ('Exercise'), ('File'), 
		('Pen') , ('Puncher'), ('Pad'), ('Paper'),
		 ('Ruler'), ('Scissors'), ('Tape'), ('Sharpener'), ('Shorthand'), 
		 ('Stapler'), ('Tacks'), ('Tparency'), ('Tray');

-------------------------------------------------- Measurement ----------------------------------------
CREATE TABLE Measurement
(
MeasurementId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
UnitOfMeasurement VARCHAR(50)
)

INSERT INTO Measurement
VALUES ('Dozen'), ('Box'), ('Each'), ('Set'),  ('Packet');

-------------------------------------------------- Inventory ----------------------------------------

CREATE TABLE Inventory
(
ItemNo VARCHAR(50) NOT NULL PRIMARY KEY,
CategoryId INT,
[Description] VARCHAR(250),
BinNo VARCHAR(50),
ReorderLevel INT,
ReorderQuantity INT,
MeasurementId INT,
Quantity INT,
HoldQuantity INT,
SupplierId1 INT,
Price1 DECIMAL(8,2),
SupplierId2 INT,
Price2 DECIMAL(8,2),
SupplierId3 INT,
Price3 DECIMAL(8,2),
CONSTRAINT CategoryId FOREIGN KEY(CategoryId) REFERENCES Category(CategoryId),
CONSTRAINT MeasurementId FOREIGN KEY(MeasurementId) REFERENCES Measurement(MeasurementId),
CONSTRAINT SupplierId1 FOREIGN KEY(SupplierId1) REFERENCES Supplier(SupplierId),
CONSTRAINT SupplierId2 FOREIGN KEY(SupplierId2) REFERENCES Supplier(SupplierId),
CONSTRAINT SupplierId3 FOREIGN KEY(SupplierId3) REFERENCES Supplier(SupplierId)
)

INSERT INTO Inventory
VALUES 
('C001',1,'Clips Double 1"','C1',50,30,1,100,0,1,2,2,2.3,3,2.4),
('C002',1,'Clips Double 2"','C2',50,30,1,100,0,1,2.2,2,2.4,3,2.5),
('E001',2,'Envelope Brown (3"x6")','E1',600,400,3,1000,0,1,0.5,2,0.6,3,0.7),
('E002',3,'Eraser(hard)','E2',50,20,3,100,0,1,1,2,1.1,3,1.2),
('F001',5,'File Separator','F1',100,50,4,150,0,1,0.7,2,0.8,3,0.9),
('E003',4,'Exercise Book (100pg)','E3',100,50,3,150,0,1,5.7,2,5.8,3,5.9),
('H001',6,'Highlighter Blue','H1',100,80,2,150,0,1,12,2,13,3,14),
('H002',7,'Hole Puncher 2 holes','H2',50,20,3,100,0,1,5,2,6,3,7),
('P001',8,'Pad Postit Memo 1"X2"','P1',100,60,5,150,0,1,5,2,5.5,3,6),
('P002',9,'Paper Photostat A3','P2',500,500,2,800,0,1,5,2,5.5,3,6),
('P003',6,'Pen Ballpoint Black','P3',100,50,1,150,0,1,12,2,13,3,14),
('P004',6,'Pen Ballpoint Blue','P4',100,50,1,150,0,1,12,2,13,3,14),
('P005',6,'Pencil 2B','P5',100,50,1,80,0,1,12,2,13,3,14),
('R001',10,'Ruler 6"','R1',50,20,1,20,0,1,5,2,6,3,7),
('S001',11,'Scissors','S1',50,20,3,20,0,1,1.5,2,2,3,2.2),
('T001',12,'Scotch Tape','T1',50,20,3,20,0,1,1.5,2,2,3,2.2),
('S002',15,'Stapler No.28','S2',50,20,1,20,0,1,5,2,6,3,7);

-------------------------------------------------- RequisitionDetail ----------------------------------------

CREATE TABLE RequisitionDetail
(
RequisitionDetailId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
RequisitionId INT,
ItemNo VARCHAR(50),
Quantity INT,
OutstandingQuantity INT,
CONSTRAINT RequisitionId FOREIGN KEY(RequisitionId) REFERENCES Requisition(RequisitionId),
CONSTRAINT RequisitionDetailItemNo FOREIGN KEY(ItemNo) REFERENCES Inventory(ItemNo)
)



-------------------------------------------------- Delegate ----------------------------------------
CREATE TABLE Delegate
(
DelegateId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
EmployeeId INT,
StartDate DATE,
EndDate DATE,
ActualEndDate DATE,
ApprovedBy INT,
ApprovedDate DATE,
CONSTRAINT DelegateEmployeeId FOREIGN KEY(EmployeeId) REFERENCES Employee(EmployeeId)
)

INSERT INTO Delegate
VALUES (13,'2017-01-03', '2017-01-06', '2017-01-06', 7, '2017-01-05')

-------------------------------------------------- Adjustment ----------------------------------------
CREATE TABLE Adjustment
(
AdjustmentId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
AdjustmentDate DATE,
EmployeeId INT,
SupervisorId INT,
SupervisorAuthorizedDate DATE,
HeadId INT,
HeadAuthorizedDate DATE,
[Status] VarChar(50)
CONSTRAINT AdjustmentEmployeeId FOREIGN KEY(EmployeeId) REFERENCES Employee(EmployeeId)
)

INSERT INTO Adjustment
VALUES ('2016-12-22',2,'','','','','Approved'), ('2016-12-31',3,'','','','','Pending Final Approval'), ('2017-01-14',2,'','','','','Pending Approval');

-------------------------------------------------- AdjustmentDetail ----------------------------------------
CREATE TABLE AdjustmentDetail
(
AdjustmentDetailId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
AdjustmentId INT,
ItemNo VARCHAR(50),
Quantity INT,
Reason VARCHAR(250),
CONSTRAINT AdjustmentDetailItemNo FOREIGN KEY(ItemNo) REFERENCES Inventory(ItemNo),
CONSTRAINT AdjustmentDetailAdjustmentId FOREIGN KEY(AdjustmentId) REFERENCES Adjustment(AdjustmentId)
)

INSERT INTO AdjustmentDetail
VALUES (1,'C001',1,'Broken'), (2,'C002',1,'Damamged'),(3,'E001',1,'Damamged');

-------------------------------------------------- PruchaseOrder ----------------------------------------
CREATE TABLE PurchaseOrder
(
PurchaseOrderId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
OrderDate DATE,
SupplierId INT,
EmployeeId INT,
OrderStatus varchar(20),
AuthorizedBy INT,
AuthorizedDate DATE,
CONSTRAINT PurchaseOrderEmployeeId FOREIGN KEY(EmployeeId) REFERENCES Employee(EmployeeId),
CONSTRAINT PurchaseOrderSupplierId FOREIGN KEY(SupplierId) REFERENCES Supplier(SupplierId),
CONSTRAINT PurchaseOrderAuthorizedBy FOREIGN KEY(AuthorizedBy) REFERENCES Employee(EmployeeId)
)

INSERT INTO PurchaseOrder
VALUES ('2016-12-30',1,3,'Pending',10,'2017-12-30'),
		('2016-12-31',1,2,'Approved',10,'2017-01-04'),
		('2016-01-06',1,2,'Rejected',10,'2017-01-10');

-------------------------------------------------- Retrieval ----------------------------------------
CREATE TABLE Retrieval
(
RetrievalId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
EmployeeId INT,
RetrievalDate DATE,
CONSTRAINT RetrievalEmployeeId FOREIGN KEY (EmployeeId) REFERENCES Employee (EmployeeId)
)

INSERT INTO Retrieval(EmployeeId, RetrievalDate)
VALUES
(2,'2016-06-21'),
(2,'2016-07-21'),
(2,'2016-08-21'),
(2,'2016-09-21'),
(2,'2016-10-21'),
(2,'2016-11-21'),
(2,'2016-12-21');

-------------------------------------------------- Add data to Requisition ------------------------------
ALTER TABLE Requisition
ADD CONSTRAINT RequisitionRetrievalId FOREIGN KEY(RetrievalId) REFERENCES Retrieval(RetrievalId)

INSERT INTO Requisition
VALUES 
(11,1,5,'2016-06-20','2016-06-20','Completed','',1),
(12,2,6,'2016-06-20','2016-06-20','Completed','',1),
(13,3,7,'2016-06-20','2016-06-20','Completed','',1),
(14,4,8,'2016-06-20','2016-06-20','Completed','',1),
(15,5,9,'2016-06-20','2016-06-20','Completed','',1),
(11,1,5,'2016-07-20','2016-07-20','Completed','',2),
(12,2,6,'2016-07-20','2016-07-20','Completed','',2),
(13,3,7,'2016-07-20','2016-07-20','Completed','',2),
(14,4,8,'2016-07-20','2016-07-20','Completed','',2),
(15,5,9,'2016-07-20','2016-07-20','Completed','',2),
(11,1,5,'2016-08-20','2016-08-20','Completed','',3),
(12,2,6,'2016-08-20','2016-08-20','Completed','',3),
(13,3,7,'2016-08-20','2016-08-20','Completed','',3),
(14,4,8,'2016-08-20','2016-08-20','Completed','',3),
(15,5,9,'2016-08-20','2016-08-20','Completed','',3),
(11,1,5,'2016-09-20','2016-09-20','Completed','',4),
(12,2,6,'2016-09-20','2016-09-20','Completed','',4),
(13,3,7,'2016-09-20','2016-09-20','Completed','',4),
(14,4,8,'2016-09-20','2016-09-20','Completed','',4),
(15,5,9,'2016-09-20','2016-09-20','Completed','',4),
(11,1,5,'2016-10-20','2016-10-20','Completed','',5),
(12,2,6,'2016-10-20','2016-10-20','Completed','',5),
(13,3,7,'2016-10-20','2016-10-20','Completed','',5),
(14,4,8,'2016-10-20','2016-10-20','Completed','',5),
(15,5,9,'2016-10-20','2016-10-20','Completed','',5),
(11,1,5,'2016-11-20','2016-11-20','Completed','',6),
(12,2,6,'2016-11-20','2016-11-20','Completed','',6),
(13,3,7,'2016-11-20','2016-11-20','Completed','',6),
(14,4,8,'2016-11-20','2016-11-20','Completed','',6),
(15,5,9,'2016-11-20','2016-11-20','Completed','',6),
(11,1,5,'2016-12-20','2016-12-20','Completed','',7),
(12,2,6,'2016-12-20','2016-12-20','Completed','',7),
(13,3,7,'2016-12-20','2016-12-20','Completed','',7),
(14,4,8,'2016-12-20','2016-12-20','Outstanding','',7),
(15,5,9,'2016-12-20','2016-12-20','Outstanding','',7),
(11,1,null,'2017-01-07',null,'Pending Approval','',null),
(12,2,null,'2017-01-07',null,'Pending Approval','',null),
(13,3,7,'2017-01-07','2017-01-07','Approved','',null),
(14,4,8,'2017-01-07','2017-01-07','Approved','',null),
(15,5,9,'2017-01-07','2017-01-07','Rejected','',null);



-------------------------------------------------- Add data to Requisition Detail ------------------------------
INSERT INTO RequisitionDetail
VALUES	
(1,'F001',25,0),
(1,'P002',5,0),
(1,'P005',15,0),
(2,'T001',20,0),
(2,'P005',25,0),
(2,'P003',25,0),
(3,'P002',25,0),
(3,'H001',15,0),
(3,'H002',10,0),
(4,'P002',10,0),
(4,'P003',20,0),
(4,'C001',25,0),
(5,'P003',25,0),
(5,'T001',5,0),
(5,'C001',20,0),
(6,'H002',15,0),
(6,'P003',10,0),
(6,'P002',25,0),
(7,'C001',5,0),
(7,'P001',25,0),
(7,'P002',25,0),
(8,'R001',10,0),
(8,'P002',10,0),
(8,'T001',20,0),
(9,'P001',10,0),
(9,'H001',10,0),
(9,'E002',5,0),
(10,'E003',5,0),
(10,'S002',15,0),
(10,'C002',15,0),
(11,'H001',10,0),
(11,'P001',15,0),
(11,'S002',25,0),
(12,'E002',25,0),
(12,'E003',25,0),
(12,'P001',10,0),
(13,'C001',25,0),
(13,'R001',5,0),
(13,'E003',5,0),
(14,'C001',10,0),
(14,'P003',20,0),
(14,'T001',10,0),
(15,'E003',15,0),
(15,'P001',10,0),
(15,'P002',15,0),
(16,'P002',25,0),
(16,'P001',5,0),
(16,'P003',5,0),
(17,'S001',10,0),
(17,'P003',20,0),
(17,'F001',5,0),
(18,'T001',15,0),
(18,'P003',25,0),
(18,'E003',15,0),
(19,'P003',5,0),
(19,'H002',25,0),
(19,'P002',20,0),
(20,'E003',10,0),
(20,'P003',20,0),
(20,'P002',20,0),
(21,'E003',20,0),
(21,'C002',20,0),
(21,'P003',25,0),
(22,'R001',10,0),
(22,'P004',5,0),
(22,'P003',25,0),
(23,'R001',25,0),
(23,'T001',10,0),
(23,'P001',10,0),
(24,'S001',20,0),
(24,'P003',5,0),
(24,'H001',10,0),
(25,'P005',10,0),
(25,'P003',5,0),
(25,'P002',20,0),
(26,'R001',15,0),
(26,'P001',25,0),
(26,'E002',10,0),
(27,'F001',5,0),
(27,'C002',10,0),
(27,'R001',15,0),
(28,'P005',5,0),
(28,'C002',10,0),
(28,'E001',15,0),
(29,'P004',15,0),
(29,'E001',20,0),
(29,'P003',5,0),
(30,'C002',15,0),
(30,'P004',5,0),
(30,'S001',5,0),
(31,'E003',20,0),
(31,'P005',10,0),
(31,'P002',5,0),
(32,'P003',15,0),
(32,'S002',20,0),
(32,'S001',15,0),
(33,'S002',25,0),
(33,'E001',5,0),
(33,'P003',15,0),
(34,'C001',25,0),
(34,'E002',10,0),
(34,'P003',10,5),
(35,'P003',15,5),
(35,'S001',25,0),
(35,'R001',25,10),
(36,'F001',25,25),
(36,'P002',5,5),
(36,'P005',15,15),
(37,'T001',20,20),
(37,'P005',25,25),
(37,'P003',25,25),
(38,'P002',25,25),
(38,'H001',15,15),
(39,'H002',10,10),
(39,'P003',10,10),
(39,'P002',20,20),
(40,'P002',25,25),
(40,'P003',25,25),
(40,'T001',5,5),
(40,'C001',20,20);

-------------------------------------------------- PruchaseDetail ----------------------------------------
CREATE TABLE PurchaseDetail
(
PurchaseDetailId  INT NOT NULL IDENTITY(1,1)  PRIMARY KEY,
PurchaseOrderId INT,
ItemNo VARCHAR(50),
Quantity INT,
SupplierId INT,
CONSTRAINT PurchaseDetailItemNo FOREIGN KEY(ItemNo) REFERENCES Inventory(ItemNo),
CONSTRAINT PurchaseDetailSupplierId FOREIGN KEY(SupplierId) REFERENCES Supplier(SupplierId),
CONSTRAINT PurchaseDetailPurchaseOrderId FOREIGN KEY(PurchaseOrderId) REFERENCES PurchaseOrder(PurchaseOrderId)
)

INSERT INTO PurchaseDetail
VALUES (1,'C001',100,1), (1,'E001',100,1), 
		(2,'C002',100,1),(2,'F001',100,1);

-------------------------------------------------- DisbursementList ----------------------------------------
CREATE TABLE DisbursementList
(
DisbursementListId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
RetrievalId INT,
DepartmentId INT,
DeliveryDate DATE,
[Status] VARCHAR(50),
CONSTRAINT RetrievalId FOREIGN KEY (RetrievalId) REFERENCES Retrieval (RetrievalId),
CONSTRAINT DisbursementListDepartmentId FOREIGN KEY (DepartmentId) REFERENCES Department (DepartmentId),
)

INSERT INTO DisbursementList(RetrievalId, DepartmentId,DeliveryDate, [Status])
VALUES
(1,1,'2016-06-21','Complete'),
(1,2,'2016-07-21','Complete'),
(1,3,'2016-08-21','Complete'),
(1,4,'2016-09-21','Complete'),
(1,5,'2016-10-21','Complete'),
(2,1,'2016-11-21','Complete'),
(2,2,'2016-12-21','Complete'),
(2,3,'2016-06-21','Complete'),
(2,4,'2016-07-21','Complete'),
(2,5,'2016-08-21','Complete'),
(3,1,'2016-09-21','Complete'),
(3,2,'2016-10-21','Complete'),
(3,3,'2016-11-21','Complete'),
(3,4,'2016-12-21','Complete'),
(3,5,'2016-06-21','Complete'),
(4,1,'2016-07-21','Complete'),
(4,2,'2016-08-21','Complete'),
(4,3,'2016-09-21','Complete'),
(4,4,'2016-10-21','Complete'),
(4,5,'2016-11-21','Complete'),
(5,1,'2016-12-21','Complete'),
(5,2,'2016-06-21','Complete'),
(5,3,'2016-07-21','Complete'),
(5,4,'2016-08-21','Complete'),
(5,5,'2016-09-21','Complete'),
(6,1,'2016-10-21','Complete'),
(6,2,'2016-11-21','Complete'),
(6,3,'2016-12-21','Complete'),
(6,4,'2016-06-21','Complete'),
(6,5,'2016-07-21','Complete'),
(7,1,'2016-08-21','Complete'),
(7,2,'2016-09-21','Complete'),
(7,3,'2016-10-21','Complete'),
(7,4,'2016-11-21','Complete'),
(7,5,'2016-12-21','Complete');

-------------------------------------------------- DisbursementDetail ----------------------------------------
CREATE TABLE DisbursementDetail
(
DisbursementDetailId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

DisbursementListId INT,
ItemNo VARCHAR(50),
PreparedQuantity INT,
DeliveredQuantity INT,
Remark VARCHAR(250),
CONSTRAINT DisbursementListId FOREIGN KEY (DisbursementListId) REFERENCES DisbursementList (DisbursementListId),
CONSTRAINT DisbursementDetailItemNo FOREIGN KEY(ItemNo) REFERENCES Inventory(ItemNo)
)

INSERT INTO DisbursementDetail
VALUES	
(1,'F001',25,25,''),
(1,'P002',5,5,''),
(1,'P005',15,15,''),
(2,'T001',20,20,''),
(2,'P005',25,25,''),
(2,'P003',25,25,''),
(3,'P002',25,25,''),
(3,'H001',15,15,''),
(3,'H002',10,10,''),
(4,'P002',10,10,''),
(4,'P003',20,20,''),
(4,'C001',25,25,''),
(5,'P003',25,25,''),
(5,'T001',5,5,''),
(5,'C001',20,20,''),
(6,'H002',15,15,''),
(6,'P003',10,10,''),
(6,'P002',25,25,''),
(7,'C001',5,5,''),
(7,'P001',25,25,''),
(7,'P002',25,25,''),
(8,'R001',10,10,''),
(8,'P002',10,10,''),
(8,'T001',20,20,''),
(9,'P001',10,10,''),
(9,'H001',10,10,''),
(9,'E002',5,5,''),
(10,'E003',5,5,''),
(10,'S002',15,15,''),
(10,'C002',15,15,''),
(11,'H001',10,10,''),
(11,'P001',15,15,''),
(11,'S002',25,25,''),
(12,'E002',25,25,''),
(12,'E003',25,25,''),
(12,'P001',10,10,''),
(13,'C001',25,25,''),
(13,'R001',5,5,''),
(13,'E003',5,5,''),
(14,'C001',10,10,''),
(14,'P003',20,20,''),
(14,'T001',10,10,''),
(15,'E003',15,15,''),
(15,'P001',10,10,''),
(15,'P002',15,15,''),
(16,'P002',25,25,''),
(16,'P001',5,5,''),
(16,'P003',5,5,''),
(17,'S001',10,10,''),
(17,'P003',20,20,''),
(17,'F001',5,5,''),
(18,'T001',15,15,''),
(18,'P003',25,25,''),
(18,'E003',15,15,''),
(19,'P003',5,5,''),
(19,'H002',25,25,''),
(19,'P002',20,20,''),
(20,'E003',10,10,''),
(20,'P003',20,20,''),
(20,'P002',20,20,''),
(21,'E003',20,20,''),
(21,'C002',20,20,''),
(21,'P003',25,25,''),
(22,'R001',10,10,''),
(22,'P004',5,5,''),
(22,'P003',25,25,''),
(23,'R001',25,25,''),
(23,'T001',10,10,''),
(23,'P001',10,10,''),
(24,'S001',20,20,''),
(24,'P003',5,5,''),
(24,'H001',10,10,''),
(25,'P005',10,10,''),
(25,'P003',5,5,''),
(25,'P002',20,20,''),
(26,'R001',15,15,''),
(26,'P001',25,25,''),
(26,'E002',10,10,''),
(27,'F001',5,5,''),
(27,'C002',10,10,''),
(27,'R001',15,15,''),
(28,'P005',5,5,''),
(28,'C002',10,10,''),
(28,'E001',15,15,''),
(29,'P004',15,15,''),
(29,'E001',20,20,''),
(29,'P003',5,5,''),
(30,'C002',15,15,''),
(30,'P004',5,5,''),
(30,'S001',5,5,''),
(31,'E003',20,20,''),
(31,'P005',10,10,''),
(31,'P002',5,5,''),
(32,'P003',15,15,''),
(32,'S002',20,20,''),
(32,'S001',15,15,''),
(33,'S002',25,25,''),
(33,'E001',5,5,''),
(33,'P003',15,15,''),
(34,'C001',25,25,''),
(34,'E002',10,10,''),
(34,'P003',5,5,''),
(35,'P003',10,10,''),
(35,'S001',25,25,''),
(35,'R001',15,15,'');
-------------------------------------------------- Delivery ----------------------------------------
CREATE TABLE Delivery
(
DeliveryId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
DeliveryOrderNo VARCHAR(50),
PurchaseOrderId INT,
DeliveredDate DATE,
ReceivedBy INT,
CONSTRAINT PurchaseOrderId FOREIGN KEY (PurchaseOrderId ) REFERENCES PurchaseOrder (PurchaseOrderId ),
CONSTRAINT DeliveryEmployeeId FOREIGN KEY (ReceivedBy ) REFERENCES Employee (EmployeeId)
)

INSERT INTO Delivery
(PurchaseOrderId,DeliveryOrderNo,DeliveredDate,ReceivedBy)
VALUES
(1,'A1234','2017-01-02',3),
(2,'A1235','2017-01-04',2),
(3,'A1236','2017-01-10',2);

-------------------------------------------------- DeliveryDetail ----------------------------------------
CREATE TABLE DeliveryDetail
(
DeliveryDetailid INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
ItemNo VARCHAR(50),
Quantity INT,
Remarks VARCHAR(250),
DeliveryId INT,
CONSTRAINT DeliveryDetailItemNo FOREIGN KEY (ItemNo) REFERENCES Inventory (ItemNo),
CONSTRAINT DeliveryDetailDeliveryId FOREIGN KEY (DeliveryId) REFERENCES Delivery (DeliveryId)
)

INSERT INTO DeliveryDetail
VALUES
('C001',100,'',1),
('E001',100,'',1),
('C002',100,'',2),
('F001',100,'',2);

-------------------------------------------------- DeliveryDetail ----------------------------------------
CREATE TABLE ItemCodeGenerator
(
Letter Char(1) NOT NULL  PRIMARY KEY,
itemcount int
)

Insert into ItemCodeGenerator values
('a',1),('b',1),('c',3),('d',1),('e',4),('f',2),
('g',1),('h',3),('i',1),('j',1),('k',1),('l',1),
('m',1),('n',1),('o',1),('p',6),('q',1),('r',2),
('s',3),('t',2),('u',1),('v',1),('w',1),('x',1),
('y',1),('z',1)


-------------------------------------- Stock Card View ----------------------------------------
CREATE VIEW StockCard AS

SELECT ISNULL(a.AdjustmentDate,'1900-01-01') AS [Date], e.EmployeeName AS [Dept/Supplier], '-' + CAST(ad.Quantity AS VARCHAR(100)) As AdjustedQuantity, i.ItemNo, i.Description
FROM AdjustmentDetail ad
INNER JOIN Adjustment a on a.AdjustmentId = ad.AdjustmentId
INNER JOIN Employee e on e.EmployeeId = a.EmployeeId
INNER JOIN Inventory i on i.ItemNo = ad.ItemNo

UNION
SELECT ISNULL(dl.DeliveryDate,'1900-01-01') AS [Date], d.DepartmentName , '-' + CAST(dd.DeliveredQuantity AS VARCHAR(100)) As DeliveredQuantity, dd.ItemNo, i.Description 
FROM DisbursementDetail dd
INNER JOIN DisbursementList dl on dl.DisbursementListId = dd.DisbursementListId
INNER JOIN Department d on d.DepartmentId = dl.DepartmentId
INNER JOIN Inventory i on i.ItemNo = dd.ItemNo
WHERE Dl.DeliveryDate <> '1900-01-01'

UNION
SELECT ISNULL(po.OrderDate,'1900-01-01') AS [Date], s.SupplierName,'+' + CAST(pd.Quantity AS VARCHAR(100)) As ReceiveedQuantity, i.ItemNo, i.Description
FROM PurchaseDetail pd
INNER JOIN PurchaseOrder po on po.PurchaseOrderId = pd.PurchaseOrderId
INNER JOIN Inventory i on i.ItemNo = pd.ItemNo
INNER JOIN Supplier s on s.SupplierId = po.SupplierId

-------------------------------------- CrystalReports Views (Don't run this view, need to be maintained)----------------------------------------
create view disbAnalysis as
select d.DepartmentName, c.CategoryName, r.ApprovedDate, dd.PreparedQuantity, e.EmployeeName, r.RequisitionId
from 
DisbursementDetail dd, 
RequisitionDetail rd, 
Requisition r, 
Inventory i, 
Employee e, 
Department d, 
Category c
where 
dd.RequisitionDetailId=rd.RequisitionDetailId 
and rd.RequisitionId=r.RequisitionId 
and rd.ItemNo=i.ItemNo 
and i.CategoryId=c.CategoryId
and r.EmployeeId=e.EmployeeId 
and e.DepartmentId=d.DepartmentId 

select e.EmployeeId,e.EmployeeName,d.DepartmentName,r.Name from Employee e, role r, Department d
where e.RoleId=r.RoleId and e.DepartmentId=d.DepartmentId

select * from role

select * from Department

select * from Requisition