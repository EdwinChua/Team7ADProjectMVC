
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
VALUES ('Store Clerk', 'Staff')
INSERT INTO [Role]
VALUES ('Department Head', 'Head')
INSERT INTO [Role]
VALUES ('Supervisor', 'Staff')
INSERT INTO [Role]
VALUES ('Representative', 'Rep')
INSERT INTO [Role]
VALUES ('Tutor', 'Department Employee')
INSERT INTO [Role]
VALUES ('Cleaner', 'Staff')
INSERT INTO [Role]
VALUES ('Admin', 'Staff')
INSERT INTO [Role]
VALUES ('Registrar', 'Staff')

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
VALUES (1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),(1,1,1,1,1,1),
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
VALUES ('COMM','Commerce Department','MrMohd. Azman','874 1284','892 1256',7,1,13)
INSERT INTO Department
VALUES ('REGR','Registrar Department','Ms Helen Ho','890 1266','892 1465',8,1,14)
INSERT INTO Department
VALUES ('ZOOL','Zoology Department','Mr. Peter Tan Ah Meng','890 1266','892 1465',9,1,15)
INSERT INTO Department
VALUES ('STO','STORE','Mr. Dino Thunder','890 6656','891 9912',10,1,1)

----------------------------------------- Add Employee -----------------------------------------
INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Jenny Wong Mei Lin','jenny@logicuniversity',4,1,1,'11111111') -- registrar

INSERT INTO Employee
VALUES ('Feng Teng','ft@logicuniversity',6,2,2,'22222222') -- store 6 store clerk 2
INSERT INTO Employee
VALUES ('Min Yew','@logicuniversity',6,2,3,'33333333')
INSERT INTO Employee
VALUES ('Lao Lao','@logicuniversity',6,2,4,'44444444')

INSERT INTO Employee
VALUES ('Mrs Pamela Kow','@logicuniversity',1,3,5,'55555555') -- english 1 Head 3
INSERT INTO Employee
VALUES ('Dr. Soh Kian Wee','@logicuniversity',2,3,6,'66666666')-- com science 2 
INSERT INTO Employee
VALUES ('MrMohd. Azman','@logicuniversity',3,3,7,'77777777') -- commerce 3
INSERT INTO Employee
VALUES ('Mrs Low Kway Boo','@logicuniversity',4,3,8,'88888888') -- registrar 4
INSERT INTO Employee
VALUES ('Mr. Peter Tan Ah Meng','@logicuniversity',5,3,9,'99999999') -- zoo 5
INSERT INTO Employee
VALUES ('Mr. Sander','@logicuniversity',6,3,10,'10101010') -- store 6

INSERT INTO Employee
VALUES ('Prof Ezra Pound','@logicuniversity',1,4,11,'11111111') -- eng 1 rep 4
INSERT INTO Employee
VALUES ('Mr. Ar Phyan Kwee','@logicuniversity',2,4,12,'12121212') -- com science 2
INSERT INTO Employee
VALUES ('Dr. Chia Leow Bee','@logicuniversity',3,4,13,'13131313') -- commerce 3
INSERT INTO Employee
VALUES ('Prof Tan','@logicuniversity',4,4,14,'14141414') -- registrar 4
INSERT INTO Employee
VALUES ('Mr. Tay Shout Pann','@logicuniversity',5,4,15,'15151515') -- zoo 5

INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo) -- employeeid 16 dept zoo 5
VALUES ('Tang Wo Long','tan@logicuniversity',5,3,16,'16161616')

INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo) -- employee 17 dept commerce 3
VALUES ('Ngyuen Feng','ngfg@logicuniversity',3,3,17,'17171717')

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
VALUES ('C001',1,'Clips Double 1"','A7', 50,30, 1, 100,0,	1,2, 2,2.3, 3,2.4),
		('C002',1,'Clips Double 2"', 'A9',50,30, 1, 100,0, 1,2.2, 2,2.4, 3,2.5),
		('E001',3,'Envelope Brown (3"x6")', 'C2', 600,400, 3, 1000,0, 1,0.5, 2,0.6, 3,0.7),
		('E020',3,'Eraser(hard)', 'C4', 50,20, 3, 100,0, 1,1, 2,1.1, 3,1.2),
		('F020',5,'File Separator', 'C5', 100,50, 4, 100,0, 1,0.7, 2,0.8, 3,0.9);

-------------------------------------------------- RequisitionDetail ----------------------------------------

CREATE TABLE RequisitionDetail
(
RequisitionDetailId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
RequisitionId INT,
ItemNo VARCHAR(50),
Quantity INT,
OutstandingQuantity INT,
DeliveryStatus VARCHAR(50),
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
CONSTRAINT AdjustmentDetailItemNo FOREIGN KEY(ItemNo) REFERENCES Inventory(ItemNo)
)

INSERT INTO AdjustmentDetail
VALUES (1,'C001',1,'Broken'), (2,'C002',1,'Damamged'),(3,'E001',1,'Damamged');

-------------------------------------------------- PruchaseOrder ----------------------------------------
CREATE TABLE PurchaseOrder
(
PurchaseOrderId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
OrderDate DATE,
DeliveredDate DATE,
SupplierId INT,
EmployeeId INT,
ReceivedBy INT,
AuthorizedBy INT,
AuthorizedDate DATE,
CONSTRAINT PurchaseOrderEmployeeId FOREIGN KEY(EmployeeId) REFERENCES Employee(EmployeeId),
CONSTRAINT PurchaseOrderReceivedBy FOREIGN KEY(ReceivedBy) REFERENCES Employee(EmployeeId)
)

INSERT INTO PurchaseOrder
VALUES ('2016-12-30','2017-01-02',1,3,2,10,'2017-12-30'),
		('2016-12-31','2017-01-04',1,2,2,10,'2017-01-04'),
		('2016-01-06','2017-01-10',1,2,3,10,'2017-01-10');

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
(2,'2017-01-04'),
(3,'2017-01-05'),
(2,'2017-01-06'),
(3,'2017-01-13'),
(2,'2017-01-14');

-------------------------------------------------- Add data to Requisition ------------------------------
ALTER TABLE Requisition
ADD CONSTRAINT RequisitionRetrievalId FOREIGN KEY(RetrievalId) REFERENCES Retrieval(RetrievalId)

INSERT INTO Requisition
VALUES (1,4,8,'2017-01-03','2017-01-03','Approved','',1) -- registrar
INSERT INTO Requisition
VALUES (1,4,8,'2017-01-01','2017-01-01','Approved','',2)
INSERT INTO Requisition
VALUES (1,4,8,'2017-01-04','2017-01-04','Approved','',3)

INSERT INTO Requisition
VALUES (16,5,9,'2017-01-10','2017-01-11','Approved','',4) -- zoo

INSERT INTO Requisition
VALUES (17,3,7,'2017-01-11','2017-01-12','Approved','',5) -- commerce

INSERT INTO Requisition
VALUES (1,4,8,'','2017-01-14','Pending','',null)
INSERT INTO Requisition
VALUES (1,4,8,'','2017-01-15','Pending','',null)
INSERT INTO Requisition
VALUES (1,4,'','','2017-01-15','Pending','',null)


-------------------------------------------------- Add data to Requisition Detail ------------------------------
INSERT INTO RequisitionDetail
VALUES	(1,'C001',10,0,'Delivered'), (1,'C002',10,0,'Delivered'), (1,'E001',10,0,'Delivered'),
		(2,'C001',10,0,'Delivered'), (2,'C002',10,0,'Delivered'), (2,'E001',10,0,'Delivered'),
		(3,'C001',10,0,'Delivered'), (3,'C002',10,0,'Delivered'), (3,'E001',10,0,'Delivered'),

		(4,'C001',10,0,'Preparing'), (4,'C002',10,0,'Preparing'), (4,'E001',10,0,'Preparing'),
		(5,'C001',5,0,'Preparing'), (5,'C002',5,0,'Preparing'), (5,'E001',5,0,'Preparing'),

		(6,'C002',10,0,'Delivered'), (6,'E020',10,0,'Delivered'), (6,'F020',10,0,'Delivered'),

		(7,'E020',10,0,'Delivered'), (7,'F020',10,0,'Delivered'), (7,'E001',10,0,'Delivered');

-------------------------------------------------- PruchaseDetail ----------------------------------------
CREATE TABLE PurchaseDetail
(
PurchaseDetailId  INT NOT NULL IDENTITY(1,1)  PRIMARY KEY,
PurchaseOrderId INT,
ItemNo VARCHAR(50),
Quantity INT,
Price DECIMAL(8,2),
Amount INT,
SupplierId INT,
CONSTRAINT PurchaseDetailItemNo FOREIGN KEY(ItemNo) REFERENCES Inventory(ItemNo),
CONSTRAINT PurchaseDetailSupplierId FOREIGN KEY(SupplierId) REFERENCES Supplier(SupplierId),
)

INSERT INTO PurchaseDetail
VALUES (1,'C001',100, 2,200,1), (1,'E001',100, 0.5,50,1), 
		(2,'C002',100, 2.2,220,1),(2,'F020',100, 0.8,80,1);

-------------------------------------------------- DisbursementList ----------------------------------------
CREATE TABLE DisbursementList
(
DisbursementListId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
RetrievalId INT,
DepartmentId INT,
OrderedDate DATE,
DeliveryDate DATE,
CollectionPointId INT,
[Status] VARCHAR(50),
CONSTRAINT RetrievalId FOREIGN KEY (RetrievalId) REFERENCES Retrieval (RetrievalId),
CONSTRAINT DisbursementListDepartmentId FOREIGN KEY (DepartmentId) REFERENCES Department (DepartmentId),
CONSTRAINT DisbursementListCollectionPointId FOREIGN KEY (CollectionPointId) REFERENCES CollectionPoints (CollectionPointId)
)

INSERT INTO DisbursementList(RetrievalId, DepartmentId,  OrderedDate, DeliveryDate, CollectionPointId, [Status])
VALUES	(1, 4, '2017-01-03', '2017-01-05', 1, 'Delivered'), 
		(2, 4, '2017-01-01', '2017-01-03', 2, 'Delivered'), 
		(3, 4, '2017-01-03', '2017-01-06', 1, 'Delivered'),
		(4, 5, '2017-01-10', '2017-01-13', 3, 'Delivered'),
		(5, 3, '2017-01-11', '2017-01-14', 5, 'Delivered');

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
VALUES	(1,'C001',10,10,''),(1,'C002',10,10,''),(1,'E001',10,10,''),
		(2,'C001',10,10,''),(2,'C002',10,10,''),(2,'E001',10,10,''),
		(3,'C001',10,10,''),(3,'C002',10,10,''),(3,'E001',10,10,''),
		(4,'C002',10,10,''),(4,'E020',10,10,''),(4,'F020',10,10,''),
		(4,'E020',10,10,''),(4,'F020',10,10,''),(4,'E001',10,9,'One item damaged');

-------------------------------------------------- Delivery ----------------------------------------
CREATE TABLE Delivery
(
DeliveryId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
PurchaseOrderId INT,
DeliveredDate DATE,
ReceivedBy INT,
CONSTRAINT PurchaseOrderId FOREIGN KEY (PurchaseOrderId ) REFERENCES PurchaseOrder (PurchaseOrderId ),
CONSTRAINT DeliveryEmployeeId FOREIGN KEY (ReceivedBy ) REFERENCES Employee (EmployeeId)
)

INSERT INTO Delivery
(PurchaseOrderId,DeliveredDate,ReceivedBy)
VALUES
(1,'2017-01-02',3),
(2,'2017-01-04',2),
(3,'2017-01-10',2);

-------------------------------------------------- DeliveryDetail ----------------------------------------
CREATE TABLE DeliveryDetail
(
DeliveryDetailid INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
ItemNo VARCHAR(50),
Quantity INT,
Remarks VARCHAR(250),
DeliveryId INT,
CONSTRAINT DeliveryDetailItemNo FOREIGN KEY (ItemNo) REFERENCES Inventory (ItemNo)
)

INSERT INTO DeliveryDetail
VALUES
('C001',100,'',1),
('E001',100,'',1),
('C002',100,'',2),
('F020',100,'',2);

-------------------------------------------------- DeliveryDetail ----------------------------------------
CREATE TABLE ItemCodeGenerator
(
Letter Char(1) NOT NULL  PRIMARY KEY,
itemcount int
)

Insert into ItemCodeGenerator values
('a',1),('b',1),('c',3),('d',1),('e',2),('f',1),
('g',1),('h',1),('i',1),('j',1),('k',1),('l',1),
('m',1),('n',1),('o',1),('p',1),('q',1),('r',1),
('s',1),('t',1),('u',1),('v',1),('w',1),('x',1),
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