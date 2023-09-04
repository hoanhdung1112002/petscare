--. Tạo bảng tblMenu để lưu trữ thông tin của Menu (Navigation)
CREATE TABLE tblMenu (
	MenuID int identity(1,1) primary key,
	MenuName nvarchar(50),
	IsActive bit,
	ControllerName nvarchar(50),
	ActionName nvarchar(50),
	Levels int,
	ParentID int,
	Link nvarchar(50),
	MenuOrder int
)

--. Tạo bảng tblFooter để lưu trữ thông tin của phần chân trang (Footer)
CREATE TABLE tblFooter (
	FooterID int identity(1,1) primary key,
	FooterName nvarchar(50),
	IsActive bit,
	ControllerName nvarchar(50),
	ActionName nvarchar(50),
	Levels int,
	ParentID int,
	Link nvarchar(50),
	FooterOrder int,
)

--. Tạo bảng tblAdminMenu để quản lý Menu ở trang Admin
CREATE TABLE tblAdminMenu (
	AdminMenuID BIGINT IDENTITY(1,1) PRIMARY KEY,
	ItemName NVARCHAR(50),
	ItemLevel INT,
	ParentLevel INT,
	ItemOrder INT,
	IsActive BIT,
	ItemTarget NVARCHAR(50),
	AreaName NVARCHAR(50),
	ControllerName NVARCHAR(50),
	ActionName NVARCHAR(50),
	Icon NVARCHAR(50),
	IdName NVARCHAR(50)
)
--. tblAdminUser để lưu trữ thông tin Admin đăng nhập
CREATE TABLE tblAdminUser (
	UserID INT PRIMARY KEY IDENTITY(1,1),
	UserName NVARCHAR(50),
	Email NVARCHAR(50),
	Password NVARCHAR(50),
	IsActive BIT,
	Description NVARCHAR(255)
)

--. tblCategories để quản lý danh mục sản phẩm + bài viết
CREATE TABLE tblCategories (
	CategoryID int identity(1,1) primary key,
	CategoryName nvarchar(50),
	Levels int,
	ParentID int,
	Link nvarchar(50),
	CategoryOrder INT,
	Position INT,
	IsActive bit,
	DataFilter NVARCHAR(50)
)

--. tblProducts để quản lý danh sách sản phẩm
CREATE TABLE tblProducts (
	ProductID int IDENTITY(1,1) PRIMARY KEY,
	ProductName NVARCHAR(255),
	CategoryID INT,
	FOREIGN KEY (CategoryID) REFERENCES dbo.tblCategories (CategoryID),
	Price INT,
	Discount int,
	Image NVARCHAR(255),
	IsActive BIT,
	Description NTEXT
)

-- . Tạo bảng tblBaiViet để lưu thông tin bài viết
CREATE TABLE tblPosts (
	PostID int IDENTITY(1,1) PRIMARY KEY,
	CategoryID INT,
	FOREIGN KEY (CategoryID) REFERENCES tblCategories (CategoryID),
	Title NVARCHAR(255) NOT NULL,
	Abstract NVARCHAR(500) NOT NULL,
	Contents NTEXT,
	Link NVARCHAR(255),
	Thumb nvarchar(255),
	Viewer int,
	PostOrder int,
	IsActive bit,
	CreatedDate DATETIME,
	Author NVARCHAR(50),
	Description NVARCHAR(255)
) 
--. Bảng tblDoctors để lưu thông tin các bác sĩ làm việc
CREATE TABLE tblDoctors (
	DoctorID INT IDENTITY(1,1) PRIMARY KEY,
	DoctorName NVARCHAR(50),
	PositionID INT,--
	foreign key (PositionID) references tblPosition (PositionID),
	DoctorFeeID INT,
	ServiceID INT,
	FOREIGN KEY (ServiceID) REFERENCES dbo.tblServices (ServiceID),
	FOREIGN KEY (DoctorFeeID) REFERENCES dbo.tblDoctorFees (DoctorFeeID),
	Thumb NVARCHAR(255), --Hình ảnh đại diện
	PhoneNumber NVARCHAR(11),
	Email nvarchar(50),
	Address NVARCHAR(255),
	IsActive BIT
)
CREATE TABLE tblDoctorFees (
	DoctorFeeID INT PRIMARY KEY IDENTITY(1,1),
	FeeAmount NVARCHAR(50)
)

--Bảng tblSchedule để lưu xếp lịch cho bác sĩ
CREATE TABLE tblSchedules (
	ScheduleID INT PRIMARY KEY IDENTITY(1,1),
	DoctorID INT,
	FOREIGN KEY (DoctorID) REFERENCES dbo.tblDoctors (DoctorID),
	TimeSlot NVARCHAR(50),
	IsBooked BIT
)

--. Bảng tblChucVu để lưu thông tin các chức vụ trong phòng khám
create table tblPosition (
	PositionID int identity(1,1) primary KEY NOT NULL,
	PositionName nvarchar(50),
	IsActive bit,
	Description nvarchar(250)
)

--. Tạo bảng tblServices để lưu các dịch vụ
CREATE TABLE tblServices (
	ServiceID INT PRIMARY KEY IDENTITY(1,1),
	ServiceName NVARCHAR(150),
	IsActive BIT,
	Description NVARCHAR(500),
)

--. Tạo bảng tblBooking để đặt lịch khám bệnh cho thú cưng

CREATE TABLE tblBookingCare (
	BookingCareID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	FullName NVARCHAR(50),
	ServiceID INT,
	FOREIGN KEY (ServiceID) REFERENCES dbo.tblServices (ServiceID),
	DoctorID INT,
	FOREIGN KEY (DoctorID) REFERENCES dbo.tblDoctors (DoctorID),
	ScheduleID INT,
	FOREIGN KEY (ScheduleID) REFERENCES dbo.tblSchedules (ScheduleID),
	PhoneNumber NVARCHAR(11) NOT NULL,
	Email NVARCHAR(50) NOT NULL,
	PetName NVARCHAR(50),
	Time DATETIME,
	Image NVARCHAR(255), --Hình ảnh về tình trạng thú cưng
	IsActive BIT,
	StatusBooking BIT,
	Description NTEXT
)




