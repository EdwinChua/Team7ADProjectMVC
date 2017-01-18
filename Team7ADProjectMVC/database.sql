
----------------------------------------- Run this code first to create the database -----------------------------------------
use master
IF EXISTS(SELECT * FROM sys.databases where name = 'Project')
DROP DATABASE Project

CREATE DATABASE Project

---------------------------------------- Tne run this till the end to create tables -----------------------------------------
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
(1,1,1,1,1,1),(1,1,1,1,1,1);


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
HeadName VARCHAR(100),
CollectionPointId INT,
RepresentativeId INT,
CONSTRAINT CollectionPointId FOREIGN KEY(CollectionPointId) REFERENCES CollectionPoints(CollectionPointId)
)
----------------------------------------- End of Department -----------------------------------------

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

INSERT INTO Department
VALUES ('ENGL','English Department','Mrs Pamela Kow','874 2234','892 2234','Prof Ezra Pound',1,11)
INSERT INTO Department
VALUES ('CPSC','Computer Science','Mr. Wee Kian Fatt','890 1235','892 1457','Dr. Soh Kian Wee',1,12)
INSERT INTO Department
VALUES ('COMM','Commerce Department','MrMohd. Azman','874 1284','892 1256','Dr. Chia Leow Bee',1,13)
INSERT INTO Department
VALUES ('REGR','Registrar Department','Ms Helen Ho','890 1266','892 1465','Mrs Low Kway Boo',1,14)
INSERT INTO Department
VALUES ('ZOOL','Zoology Department','Mr. Peter Tan Ah Meng','890 1266','892 1465','Prof Tan',1,15)
INSERT INTO Department
VALUES ('STO','STORE','Mr. Dino Thunder','890 6656','891 9912','Mr. Sander',1,1)


INSERT INTO Employee (EmployeeName,Email,DepartmentId, RoleId, PermissionId,PhNo)
VALUES ('Jenny Wong Mei Lin','jenny@logicuniversity',4,1,1,'11111111') -- registrar

INSERT INTO Employee
VALUES ('Feng Teng','ft@logicuniversity',6,2,2,'22222222') -- store 6 store clerk 2
INSERT INTO Employee
VALUES ('Min Yew','@logicuniversity',6,2,3,'33333333')
INSERT INTO Employee
VALUES ('Lao Lao','@logicuniversity',6,2,4,'44444444')

INSERT INTO Employee
VALUES ('Mrs Pamela Kow','@logicuniversity',1,3,5,'12345678') -- english 1 Head 3
INSERT INTO Employee
VALUES ('Mr. Wee Kian Fatt','@logicuniversity',2,3,6,'55555555')-- com science 2 
INSERT INTO Employee
VALUES ('MrMohd. Azman','@logicuniversity',3,3,7,'66666666') -- commerce 3
INSERT INTO Employee
VALUES ('Ms Helen Ho','@logicuniversity',4,3,8,'77777777') -- registrar 4
INSERT INTO Employee
VALUES ('Mr. Peter Tan Ah Meng','@logicuniversity',5,3,9,'88888888') -- zoo 5
INSERT INTO Employee
VALUES ('Mr. Dino Thunder','@logicuniversity',6,3,10,'99999999') -- store 6

INSERT INTO Employee
VALUES ('Mr. Paw Taw Taw','@logicuniversity',1,4,11,'00000000') -- eng 1 rep 4
INSERT INTO Employee
VALUES ('Mr. Ar Phyan Kwee','@logicuniversity',2,4,12,'87654321') -- com science 2
INSERT INTO Employee
VALUES ('Mr. Shout Arr','@logicuniversity',3,4,12,'87654321') -- commerce 3
INSERT INTO Employee
VALUES ('Mr. Ah Lay Lite','@logicuniversity',4,4,12,'87654321') -- registrar 4
INSERT INTO Employee
VALUES ('Mr. Tay Shout Pann','@logicuniversity',5,4,12,'87654321') -- zoo 5

ALTER TABLE Employee
ADD CONSTRAINT DepartmentId FOREIGN KEY(DepartmentId) REFERENCES Department(DepartmentId)

ALTER TABLE CollectionPoints
ADD CONSTRAINT eid FOREIGN KEY(EmployeeId) REFERENCES Employee(EmployeeId)

ALTER TABLE Department
ADD CONSTRAINT EmployeeId FOREIGN KEY(RepresentativeId) REFERENCES Employee(EmployeeId)

-------------------------------------------------- Requisition ----------------------------------------
CREATE TABLE Requisition
(
RequisitionId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
EmployeeId INT,
DepartmentId INT,
ApprovedBy INT,
ApprovedDate Date,
OrderedDate Date,
RequisitionStatus VARCHAR(50),
Comment VARCHAR(250),
CONSTRAINT RequisitionEmployeeId FOREIGN KEY(EmployeeId) REFERENCES Employee(EmployeeId)
)

INSERT INTO Requisition
VALUES (1,4,8,'2017-01-03','2017-01-03','Approved')
INSERT INTO Requisition
VALUES (1,4,15,'2017-01-01','2017-01-01','Approved')
INSERT INTO Requisition
VALUES (1,4,15,'2017-01-04','2017-01-04','Approved')
INSERT INTO Requisition
VALUES (1,4,1,'','2017-01-14','Pending')
INSERT INTO Requisition
VALUES (1,4,15,'','2017-01-15','Pending')


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
CONSTRAINT RequisitionDetailItemNo FOREIGN KEY(ItemNo) REFERENCES Inventory(ItemNo),
)

INSERT INTO RequisitionDetail
VALUES	(1,'C001',10,0,'Delivered'), (1,'C002',10,0,'Delivered'), (1,'E001',10,0,'Delivered'),
		(2,'C001',10,0,'Delivered'), (2,'C002',10,0,'Delivered'), (2,'E001',10,0,'Delivered'),
		(3,'C001',10,0,'Delivered'), (3,'C002',10,0,'Delivered'), (3,'E001',10,0,'Delivered'),
		(4,'C001',10,0,'Preparing'), (4,'C002',10,0,'Preparing'), (4,'E001',10,0,'Preparing'),
		(5,'C001',5,0,'Preparing'), (5,'C002',5,0,'Preparing'), (5,'E001',5,0,'Preparing');

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
HeadAuthorizedDate DATE
CONSTRAINT AdjustmentEmployeeId FOREIGN KEY(EmployeeId) REFERENCES Employee(EmployeeId)
)

INSERT INTO Adjustment
VALUES ('2016-12-22',2,'','','',''), ('2016-12-31',2,'','','','');

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
VALUES (1,'C001',1,'Broken'), (2,'C002',1,'Damamged');

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
(2,'2017-01-05'),
(2,'2017-01-06');

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
VALUES	(1, 4, '2017-01-03', '2017-01-05', 1, 'Delivered'), (1, 4, '2017-01-01', '2017-01-03', 1, 'Delivered'), (1, 4, '2017-01-03', '2017-01-06', 1, 'Delivered');

-------------------------------------------------- DisbursementDetail ----------------------------------------
CREATE TABLE DisbursementDetail
(
DisbursementDetailId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
DisbursementListId INT,
RequisitionDetailId INT,
OrderedQuantity INT,
DeliveredQuantity INT,
Remark VARCHAR(250),
CONSTRAINT DisbursementListId FOREIGN KEY (DisbursementListId) REFERENCES DisbursementList (DisbursementListId),
CONSTRAINT RequisitionDetailId FOREIGN KEY (RequisitionDetailId) REFERENCES RequisitionDetail (RequisitionDetailId)
)

INSERT INTO DisbursementDetail
VALUES	(1,1,10,'',''),(1,2,10,'',''),(1,3,10,'',''),
		(2,4,10,'',''),(2,5,10,'',''),(2,6,10,'',''),
		(3,7,10,'',''),(3,8,10,'',''),(3,9,10,'','');

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

-------------------------------------- Stock Card View ----------------------------------------
CREATE VIEW StockCard AS

SELECT ISNULL(a.AdjustmentDate,'1900-01-01') AS [Date], e.EmployeeName AS [Dept/Supplier], '-' + CAST(ad.Quantity AS VARCHAR(100)) As AdjustedQuantity, i.ItemNo, i.Description FROM AdjustmentDetail ad
INNER JOIN Adjustment a on a.AdjustmentId = ad.AdjustmentId
INNER JOIN Employee e on e.EmployeeId = a.EmployeeId
INNER JOIN Inventory i on i.ItemNo = ad.ItemNo

UNION
SELECT ISNULL(dl.DeliveryDate,'1900-01-01') AS [Date], d.DepartmentName , '-' + CAST(dd.Quantity AS VARCHAR(100)) As DeliveredQuantity, rd.ItemNo, i.Description FROM DisbursementDetail dd
LEFT JOIN RequisitionDetail rd on rd.RequisitionId = dd.RequisitionDetailId
INNER JOIN DisbursementList dl on dl.DisbursementListId = dd.DisbursementListId
INNER JOIN Department d on d.DepartmentId = dl.DepartmentId
INNER JOIN Inventory i on i.ItemNo = rd.ItemNo
WHERE Dl.DeliveryDate <> '1900-01-01'

UNION
SELECT ISNULL(po.OrderDate,'1900-01-01') AS [Date], s.SupplierName,'+' + CAST(pd.Quantity AS VARCHAR(100)) As ReceiveedQuantity, i.ItemNo, i.Description FROM PurchaseDetail pd
INNER JOIN PurchaseOrder po on po.PurchaseOrderId = pd.PurchaseOrderId
INNER JOIN Inventory i on i.ItemNo = pd.ItemNo
INNER JOIN Supplier s on s.SupplierId = po.SupplierId

-------------------------------------- CrystalReports Views ----------------------------------------
create view disbAnalysis as
select d.DepartmentName, c.CategoryName, r.ApprovedDate, dd.Quantity, e.EmployeeName, r.RequisitionId
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