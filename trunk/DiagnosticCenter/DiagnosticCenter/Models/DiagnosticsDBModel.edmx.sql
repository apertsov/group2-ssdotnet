
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/09/2011 23:00:15
-- Generated from EDMX file: D:\DiagnosticCenter\DiagnosticCenter\Models\DiagnosticsDBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DiagnosticsDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DepartmentEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_DepartmentEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_CabinetEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_CabinetEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeNews]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[News] DROP CONSTRAINT [FK_EmployeeNews];
GO
IF OBJECT_ID(N'[dbo].[FK_DayEmployee_Day]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DayEmployee] DROP CONSTRAINT [FK_DayEmployee_Day];
GO
IF OBJECT_ID(N'[dbo].[FK_DayEmployee_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DayEmployee] DROP CONSTRAINT [FK_DayEmployee_Employee];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientReferral]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Referrals] DROP CONSTRAINT [FK_PatientReferral];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeReferral]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Referrals] DROP CONSTRAINT [FK_EmployeeReferral];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeExamination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Examinations] DROP CONSTRAINT [FK_EmployeeExamination];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientExamination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Examinations] DROP CONSTRAINT [FK_PatientExamination];
GO
IF OBJECT_ID(N'[dbo].[FK_ReferralExamination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Referrals] DROP CONSTRAINT [FK_ReferralExamination];
GO
IF OBJECT_ID(N'[dbo].[FK_ExaminationTypeExamination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExaminationTypes] DROP CONSTRAINT [FK_ExaminationTypeExamination];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Days]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Days];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Cabinets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cabinets];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO
IF OBJECT_ID(N'[dbo].[Patients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patients];
GO
IF OBJECT_ID(N'[dbo].[Referrals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Referrals];
GO
IF OBJECT_ID(N'[dbo].[Examinations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Examinations];
GO
IF OBJECT_ID(N'[dbo].[ExaminationTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExaminationTypes];
GO
IF OBJECT_ID(N'[dbo].[DayEmployee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DayEmployee];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [ID_Dept] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Days'
CREATE TABLE [dbo].[Days] (
    [ID_Day] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [EndTime] datetime  NOT NULL,
    [StartBreak] datetime  NOT NULL,
    [EndBreak] datetime  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [ID_Employee] int IDENTITY(1,1) NOT NULL,
    [Category] nvarchar(max)  NOT NULL,
    [Specialty] nvarchar(30)  NOT NULL,
    [Position] nvarchar(30)  NOT NULL,
    [Rate] int  NOT NULL,
    [ID_Dept] int  NOT NULL,
    [ID_Cabinet] int  NOT NULL
);
GO

-- Creating table 'Cabinets'
CREATE TABLE [dbo].[Cabinets] (
    [ID_Cabinet] int IDENTITY(1,1) NOT NULL,
    [Number] int  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [ID_News] int IDENTITY(1,1) NOT NULL,
    [ID_Dept] int  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Type] tinyint  NOT NULL,
    [ID_Employee] int  NOT NULL
);
GO

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [ID_Patient] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Specialty] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [Workplace] tinyint  NOT NULL,
    [Civil_Servant] tinyint  NOT NULL
);
GO

-- Creating table 'Referrals'
CREATE TABLE [dbo].[Referrals] (
    [ID_Referral] int IDENTITY(1,1) NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [VisitDate] datetime  NOT NULL,
    [ID_Patient] int  NOT NULL,
    [ID_Employee] int  NOT NULL,
    [ID_Examination] int  NOT NULL,
    [Examination_ID_Examination] int  NOT NULL
);
GO

-- Creating table 'Examinations'
CREATE TABLE [dbo].[Examinations] (
    [ID_Examination] int IDENTITY(1,1) NOT NULL,
    [Status] tinyint  NOT NULL,
    [Protocol] nvarchar(max)  NOT NULL,
    [Conclusion] nvarchar(max)  NOT NULL,
    [Recommendation] nvarchar(max)  NOT NULL,
    [Consultation] nvarchar(max)  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [ID_Employee] int  NOT NULL,
    [ID_Patient] int  NOT NULL,
    [ID_ExmType] int  NOT NULL
);
GO

-- Creating table 'ExaminationTypes'
CREATE TABLE [dbo].[ExaminationTypes] (
    [ID_ExmType] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [Duration] int  NOT NULL,
    [Examination_ID_Examination] int  NOT NULL
);
GO

-- Creating table 'DayEmployee'
CREATE TABLE [dbo].[DayEmployee] (
    [Day_ID_Day] int  NOT NULL,
    [Employee_ID_Employee] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID_Dept] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([ID_Dept] ASC);
GO

-- Creating primary key on [ID_Day] in table 'Days'
ALTER TABLE [dbo].[Days]
ADD CONSTRAINT [PK_Days]
    PRIMARY KEY CLUSTERED ([ID_Day] ASC);
GO

-- Creating primary key on [ID_Employee] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([ID_Employee] ASC);
GO

-- Creating primary key on [ID_Cabinet] in table 'Cabinets'
ALTER TABLE [dbo].[Cabinets]
ADD CONSTRAINT [PK_Cabinets]
    PRIMARY KEY CLUSTERED ([ID_Cabinet] ASC);
GO

-- Creating primary key on [ID_News] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [PK_News]
    PRIMARY KEY CLUSTERED ([ID_News] ASC);
GO

-- Creating primary key on [ID_Patient] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([ID_Patient] ASC);
GO

-- Creating primary key on [ID_Referral] in table 'Referrals'
ALTER TABLE [dbo].[Referrals]
ADD CONSTRAINT [PK_Referrals]
    PRIMARY KEY CLUSTERED ([ID_Referral] ASC);
GO

-- Creating primary key on [ID_Examination] in table 'Examinations'
ALTER TABLE [dbo].[Examinations]
ADD CONSTRAINT [PK_Examinations]
    PRIMARY KEY CLUSTERED ([ID_Examination] ASC);
GO

-- Creating primary key on [ID_ExmType] in table 'ExaminationTypes'
ALTER TABLE [dbo].[ExaminationTypes]
ADD CONSTRAINT [PK_ExaminationTypes]
    PRIMARY KEY CLUSTERED ([ID_ExmType] ASC);
GO

-- Creating primary key on [Day_ID_Day], [Employee_ID_Employee] in table 'DayEmployee'
ALTER TABLE [dbo].[DayEmployee]
ADD CONSTRAINT [PK_DayEmployee]
    PRIMARY KEY NONCLUSTERED ([Day_ID_Day], [Employee_ID_Employee] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ID_Dept] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_DepartmentEmployee]
    FOREIGN KEY ([ID_Dept])
    REFERENCES [dbo].[Departments]
        ([ID_Dept])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentEmployee'
CREATE INDEX [IX_FK_DepartmentEmployee]
ON [dbo].[Employees]
    ([ID_Dept]);
GO

-- Creating foreign key on [ID_Cabinet] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_CabinetEmployee]
    FOREIGN KEY ([ID_Cabinet])
    REFERENCES [dbo].[Cabinets]
        ([ID_Cabinet])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CabinetEmployee'
CREATE INDEX [IX_FK_CabinetEmployee]
ON [dbo].[Employees]
    ([ID_Cabinet]);
GO

-- Creating foreign key on [ID_Employee] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [FK_EmployeeNews]
    FOREIGN KEY ([ID_Employee])
    REFERENCES [dbo].[Employees]
        ([ID_Employee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeNews'
CREATE INDEX [IX_FK_EmployeeNews]
ON [dbo].[News]
    ([ID_Employee]);
GO

-- Creating foreign key on [Day_ID_Day] in table 'DayEmployee'
ALTER TABLE [dbo].[DayEmployee]
ADD CONSTRAINT [FK_DayEmployee_Day]
    FOREIGN KEY ([Day_ID_Day])
    REFERENCES [dbo].[Days]
        ([ID_Day])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Employee_ID_Employee] in table 'DayEmployee'
ALTER TABLE [dbo].[DayEmployee]
ADD CONSTRAINT [FK_DayEmployee_Employee]
    FOREIGN KEY ([Employee_ID_Employee])
    REFERENCES [dbo].[Employees]
        ([ID_Employee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DayEmployee_Employee'
CREATE INDEX [IX_FK_DayEmployee_Employee]
ON [dbo].[DayEmployee]
    ([Employee_ID_Employee]);
GO

-- Creating foreign key on [ID_Patient] in table 'Referrals'
ALTER TABLE [dbo].[Referrals]
ADD CONSTRAINT [FK_PatientReferral]
    FOREIGN KEY ([ID_Patient])
    REFERENCES [dbo].[Patients]
        ([ID_Patient])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientReferral'
CREATE INDEX [IX_FK_PatientReferral]
ON [dbo].[Referrals]
    ([ID_Patient]);
GO

-- Creating foreign key on [ID_Employee] in table 'Referrals'
ALTER TABLE [dbo].[Referrals]
ADD CONSTRAINT [FK_EmployeeReferral]
    FOREIGN KEY ([ID_Employee])
    REFERENCES [dbo].[Employees]
        ([ID_Employee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeReferral'
CREATE INDEX [IX_FK_EmployeeReferral]
ON [dbo].[Referrals]
    ([ID_Employee]);
GO

-- Creating foreign key on [ID_Employee] in table 'Examinations'
ALTER TABLE [dbo].[Examinations]
ADD CONSTRAINT [FK_EmployeeExamination]
    FOREIGN KEY ([ID_Employee])
    REFERENCES [dbo].[Employees]
        ([ID_Employee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeExamination'
CREATE INDEX [IX_FK_EmployeeExamination]
ON [dbo].[Examinations]
    ([ID_Employee]);
GO

-- Creating foreign key on [ID_Patient] in table 'Examinations'
ALTER TABLE [dbo].[Examinations]
ADD CONSTRAINT [FK_PatientExamination]
    FOREIGN KEY ([ID_Patient])
    REFERENCES [dbo].[Patients]
        ([ID_Patient])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientExamination'
CREATE INDEX [IX_FK_PatientExamination]
ON [dbo].[Examinations]
    ([ID_Patient]);
GO

-- Creating foreign key on [Examination_ID_Examination] in table 'Referrals'
ALTER TABLE [dbo].[Referrals]
ADD CONSTRAINT [FK_ReferralExamination]
    FOREIGN KEY ([Examination_ID_Examination])
    REFERENCES [dbo].[Examinations]
        ([ID_Examination])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReferralExamination'
CREATE INDEX [IX_FK_ReferralExamination]
ON [dbo].[Referrals]
    ([Examination_ID_Examination]);
GO

-- Creating foreign key on [Examination_ID_Examination] in table 'ExaminationTypes'
ALTER TABLE [dbo].[ExaminationTypes]
ADD CONSTRAINT [FK_ExaminationTypeExamination]
    FOREIGN KEY ([Examination_ID_Examination])
    REFERENCES [dbo].[Examinations]
        ([ID_Examination])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ExaminationTypeExamination'
CREATE INDEX [IX_FK_ExaminationTypeExamination]
ON [dbo].[ExaminationTypes]
    ([Examination_ID_Examination]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------