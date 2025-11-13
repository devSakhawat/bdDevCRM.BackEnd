-- SQL Script to create CRM Intake and Payment Method tables
-- Created for bdDevCRM application

-- 1. Create CrmIntakeMonth table
CREATE TABLE CrmIntakeMonth (
    IntakeMonthId INT IDENTITY(1,1) NOT NULL,
    MonthName VARCHAR(50) NOT NULL,
    MonthCode VARCHAR(10) NULL,
    MonthNumber INT NOT NULL,
    Description VARCHAR(500) NULL,
    IsActive BIT NULL DEFAULT(1),
    CreatedDate DATETIME NOT NULL DEFAULT(GETDATE()),
    CreatedBy INT NOT NULL,
    UpdatedDate DATETIME NULL,
    UpdatedBy INT NULL,
    CONSTRAINT PK_CrmIntakeMonth PRIMARY KEY (IntakeMonthId)
);

-- 2. Create CrmIntakeYear table
CREATE TABLE CrmIntakeYear (
    IntakeYearId INT IDENTITY(1,1) NOT NULL,
    YearName VARCHAR(10) NOT NULL,
    YearCode VARCHAR(10) NULL,
    YearValue INT NOT NULL,
    Description VARCHAR(500) NULL,
    IsActive BIT NULL DEFAULT(1),
    CreatedDate DATETIME NOT NULL DEFAULT(GETDATE()),
    CreatedBy INT NOT NULL,
    UpdatedDate DATETIME NULL,
    UpdatedBy INT NULL,
    CONSTRAINT PK_CrmIntakeYear PRIMARY KEY (IntakeYearId)
);

-- 3. Create CrmPaymentMethod table
CREATE TABLE CrmPaymentMethod (
    PaymentMethodId INT IDENTITY(1,1) NOT NULL,
    PaymentMethodName VARCHAR(100) NOT NULL,
    PaymentMethodCode VARCHAR(20) NULL,
    Description VARCHAR(500) NULL,
    ProcessingFee DECIMAL(18,2) NULL,
    ProcessingFeeType VARCHAR(20) NULL, -- 'Fixed' or 'Percentage'
    IsOnlinePayment BIT NOT NULL DEFAULT(0),
    IsActive BIT NULL DEFAULT(1),
    CreatedDate DATETIME NOT NULL DEFAULT(GETDATE()),
    CreatedBy INT NOT NULL,
    UpdatedDate DATETIME NULL,
    UpdatedBy INT NULL,
    CONSTRAINT PK_CrmPaymentMethod PRIMARY KEY (PaymentMethodId)
);

-- Insert sample data for CrmIntakeMonth
INSERT INTO CrmIntakeMonth (MonthName, MonthCode, MonthNumber, Description, Status, CreatedBy)
VALUES 
    ('January', 'JAN', 1, 'January intake month', 1, 1),
    ('February', 'FEB', 2, 'February intake month', 1, 1),
    ('March', 'MAR', 3, 'March intake month', 1, 1),
    ('April', 'APR', 4, 'April intake month', 1, 1),
    ('May', 'MAY', 5, 'May intake month', 1, 1),
    ('June', 'JUN', 6, 'June intake month', 1, 1),
    ('July', 'JUL', 7, 'July intake month', 1, 1),
    ('August', 'AUG', 8, 'August intake month', 1, 1),
    ('September', 'SEP', 9, 'September intake month', 1, 1),
    ('October', 'OCT', 10, 'October intake month', 1, 1),
    ('November', 'NOV', 11, 'November intake month', 1, 1),
    ('December', 'DEC', 12, 'December intake month', 1, 1);

-- Insert sample data for CrmIntakeYear
DECLARE @currentYear INT = YEAR(GETDATE());
DECLARE @startYear INT = @currentYear;
DECLARE @endYear INT = @currentYear + 5;
DECLARE @year INT = @startYear;

WHILE @year <= @endYear
BEGIN
    INSERT INTO CrmIntakeYear (YearName, YearCode, YearValue, Description, Status, CreatedBy)
    VALUES 
        (CAST(@year AS VARCHAR(10)), CAST(@year AS VARCHAR(10)), @year, 'Academic year ' + CAST(@year AS VARCHAR(10)), 1, 1);
    
    SET @year = @year + 1;
END;

-- Insert sample data for CrmPaymentMethod
INSERT INTO CrmPaymentMethod (PaymentMethodName, PaymentMethodCode, Description, ProcessingFee, ProcessingFeeType, IsOnlinePayment, Status, CreatedBy)
VALUES 
    ('Credit Card', 'CC', 'Payment via Credit Card', 2.50, 'Percentage', 1, 1, 1),
    ('Debit Card', 'DC', 'Payment via Debit Card', 1.50, 'Percentage', 1, 1, 1),
    ('Bank Transfer', 'BT', 'Direct Bank Transfer', 5.00, 'Fixed', 0, 1, 1),
    ('PayPal', 'PP', 'Payment via PayPal', 3.00, 'Percentage', 1, 1, 1),
    ('Cash', 'CASH', 'Cash Payment', 0.00, 'Fixed', 0, 1, 1),
    ('Cheque', 'CHQ', 'Payment by Cheque', 0.00, 'Fixed', 0, 1, 1),
    ('Online Banking', 'OB', 'Online Banking Transfer', 0.00, 'Fixed', 1, 1, 1),
    ('Mobile Payment', 'MP', 'Mobile Payment (bKash, Rocket, etc.)', 1.00, 'Percentage', 1, 1, 1);

-- Add comments to tables
EXEC sys.sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Stores intake months for CRM applications', 
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE', @level1name = N'CrmIntakeMonth';

EXEC sys.sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Stores intake years for CRM applications', 
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE', @level1name = N'CrmIntakeYear';

EXEC sys.sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Stores payment methods available in CRM system', 
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE', @level1name = N'CrmPaymentMethod';

-- Create indexes for better performance
CREATE INDEX IX_CrmIntakeMonth_Status ON CrmIntakeMonth(Status);
CREATE INDEX IX_CrmIntakeMonth_MonthNumber ON CrmIntakeMonth(MonthNumber);
CREATE INDEX IX_CrmIntakeYear_Status ON CrmIntakeYear(Status);
CREATE INDEX IX_CrmIntakeYear_YearValue ON CrmIntakeYear(YearValue);
CREATE INDEX IX_CrmPaymentMethod_Status ON CrmPaymentMethod(Status);
CREATE INDEX IX_CrmPaymentMethod_IsOnlinePayment ON CrmPaymentMethod(IsOnlinePayment);

PRINT 'Tables created successfully: CrmIntakeMonth, CrmIntakeYear, CrmPaymentMethod';
PRINT 'Sample data inserted successfully';
PRINT 'Indexes created for performance optimization';