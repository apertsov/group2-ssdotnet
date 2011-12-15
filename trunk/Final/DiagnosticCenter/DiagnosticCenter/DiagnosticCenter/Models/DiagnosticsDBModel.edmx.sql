
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/01/2011 21:48:51
-- Generated from EDMX file: E:\Repositories\SSTrainingRep\DiagnosticCenter\DiagnosticCenter\Models\DiagnosticsDBModel.edmx
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

IF OBJECT_ID(N'[dbo].[FK_CabinetEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_CabinetEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentCabinet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cabinets] DROP CONSTRAINT [FK_DepartmentCabinet];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_DepartmentEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentReferral]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Referrals] DROP CONSTRAINT [FK_DepartmentReferral];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeExamination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Examinations] DROP CONSTRAINT [FK_EmployeeExamination];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeExaminationTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExaminationTemplates] DROP CONSTRAINT [FK_EmployeeExaminationTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeNews]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[News] DROP CONSTRAINT [FK_EmployeeNews];
GO
IF OBJECT_ID(N'[dbo].[FK_ExaminationTypeExamination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Examinations] DROP CONSTRAINT [FK_ExaminationTypeExamination];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientExamination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Examinations] DROP CONSTRAINT [FK_PatientExamination];
GO
IF OBJECT_ID(N'[dbo].[FK_ReferralExamination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Examinations] DROP CONSTRAINT [FK_ReferralExamination];
GO
IF OBJECT_ID(N'[dbo].[FK_ExaminationTypeExaminationTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExaminationTemplates] DROP CONSTRAINT [FK_ExaminationTypeExaminationTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_DayEmployee_Days]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DayEmployee] DROP CONSTRAINT [FK_DayEmployee_Days];
GO
IF OBJECT_ID(N'[dbo].[FK_DayEmployee_Employees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DayEmployee] DROP CONSTRAINT [FK_DayEmployee_Employees];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Cabinets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cabinets];
GO
IF OBJECT_ID(N'[dbo].[Days]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Days];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Examinations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Examinations];
GO
IF OBJECT_ID(N'[dbo].[ExaminationTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExaminationTemplates];
GO
IF OBJECT_ID(N'[dbo].[ExaminationTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExaminationTypes];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO
IF OBJECT_ID(N'[dbo].[Patients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patients];
GO
IF OBJECT_ID(N'[dbo].[Positions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Positions];
GO
IF OBJECT_ID(N'[dbo].[Referrals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Referrals];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Settings];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[DayEmployee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DayEmployee];
GO
IF OBJECT_ID(N'[dbo].[UserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRole];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Cabinets'
CREATE TABLE [dbo].[Cabinets] (
    [ID_Cabinet] int IDENTITY(1,1) NOT NULL,
    [Number] int  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ID_Dept] int  NOT NULL
);
GO

-- Creating table 'Days'
CREATE TABLE [dbo].[Days] (
    [ID_Day] int IDENTITY(1,1) NOT NULL,
    [Date] datetime2(7)  NOT NULL,
    [StartTime] datetime2(7)  NOT NULL,
    [EndTime] datetime2(7)  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [ID_Dept] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
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
    [ID_Cabinet] int  NOT NULL,
    [AtWork] nvarchar(3)  NULL,
    [ID_User] int  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
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
    [StartTime] datetime2(7)  NOT NULL,
    [ID_Employee] int  NOT NULL,
    [ID_Patient] int  NOT NULL,
    [ID_ExmType] int  NOT NULL,
    [Referral_ID_Referral] int  NULL,
    [ExaminationType_ID_ExmType] int  NOT NULL
);
GO

-- Creating table 'ExaminationTemplates'
CREATE TABLE [dbo].[ExaminationTemplates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [ExaminationTypeID_ExmType] int  NOT NULL,
    [EmployeeID_Employee] int  NOT NULL,
    [IsPrivate] bit  NOT NULL
);
GO

-- Creating table 'ExaminationTypes'
CREATE TABLE [dbo].[ExaminationTypes] (
    [ID_ExmType] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [Duration] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [ID_News] int IDENTITY(1,1) NOT NULL,
    [ID_Dept] int  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Type] tinyint  NOT NULL,
    [ID_Employee] int  NOT NULL,
    [Topic] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [ID_Patient] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(20)  NOT NULL,
    [Specialty] nvarchar(50)  NOT NULL,
    [Address] nvarchar(50)  NOT NULL,
    [Phone] nvarchar(20)  NOT NULL,
    [Comment] nvarchar(max)  NULL,
    [Workplace] bit  NOT NULL,
    [Civil_Servant] bit  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NOT NULL,
    [Email] nvarchar(50)  NULL,
    [Password] nvarchar(16)  NOT NULL,
    [BirthDate] datetime2(7)  NOT NULL,
    [Sex] nvarchar(10)  NOT NULL,
    [City] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Positions'
CREATE TABLE [dbo].[Positions] (
    [ID_Position] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Referrals'
CREATE TABLE [dbo].[Referrals] (
    [ID_Referral] int IDENTITY(1,1) NOT NULL,
    [CreationDate] datetime2(7)  NOT NULL,
    [VisitDate] datetime2(7)  NOT NULL,
    [ID_Patient] int  NOT NULL,
    [ID_Employee] int  NOT NULL,
    [ID_Examination] int  NULL,
    [ID_Dept] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleID] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [ID_Settings] int  NOT NULL,
    [CenterName] nvarchar(max)  NOT NULL,
    [CenterDetails] nvarchar(max)  NOT NULL,
    [CenterAddress] nvarchar(max)  NOT NULL,
    [CenterPhoneNo] nvarchar(50)  NOT NULL,
    [LanguageId] nchar(10)  NOT NULL,
    [ThemeId] nvarchar(50)  NOT NULL,
    [WorkDayStartHour] int  NOT NULL,
    [WorkDayEndHour] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(30)  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [Password] nvarchar(128)  NOT NULL,
    [PasswordSalt] nvarchar(128)  NOT NULL,
    [Comments] nvarchar(256)  NULL,
    [CreatedDate] datetime2(7)  NOT NULL,
    [LastLoginDate] datetime2(7)  NOT NULL,
    [IsActivated] bit  NOT NULL,
    [IsLockedOut] bit  NOT NULL,
    [LastLockedOutDate] datetime2(7)  NULL
);
GO

-- Creating table 'UserSettingsSet'
CREATE TABLE [dbo].[UserSettingsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [PropName] nvarchar(max)  NOT NULL,
    [PropValue] nvarchar(max)  NOT NULL,
    [DefaultValue] nvarchar(max)  NULL
);
GO

-- Creating table 'DayEmployee'
CREATE TABLE [dbo].[DayEmployee] (
    [Day_ID_Day] int  NOT NULL,
    [Employee_ID_Employee] int  NOT NULL
);
GO

-- Creating table 'UserRole'
CREATE TABLE [dbo].[UserRole] (
    [Roles_RoleID] int  NOT NULL,
    [Users_UserID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID_Cabinet] in table 'Cabinets'
ALTER TABLE [dbo].[Cabinets]
ADD CONSTRAINT [PK_Cabinets]
    PRIMARY KEY CLUSTERED ([ID_Cabinet] ASC);
GO

-- Creating primary key on [ID_Day] in table 'Days'
ALTER TABLE [dbo].[Days]
ADD CONSTRAINT [PK_Days]
    PRIMARY KEY CLUSTERED ([ID_Day] ASC);
GO

-- Creating primary key on [ID_Dept] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([ID_Dept] ASC);
GO

-- Creating primary key on [ID_Employee] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([ID_Employee] ASC);
GO

-- Creating primary key on [ID_Examination] in table 'Examinations'
ALTER TABLE [dbo].[Examinations]
ADD CONSTRAINT [PK_Examinations]
    PRIMARY KEY CLUSTERED ([ID_Examination] ASC);
GO

-- Creating primary key on [Id] in table 'ExaminationTemplates'
ALTER TABLE [dbo].[ExaminationTemplates]
ADD CONSTRAINT [PK_ExaminationTemplates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID_ExmType] in table 'ExaminationTypes'
ALTER TABLE [dbo].[ExaminationTypes]
ADD CONSTRAINT [PK_ExaminationTypes]
    PRIMARY KEY CLUSTERED ([ID_ExmType] ASC);
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

-- Creating primary key on [ID_Position] in table 'Positions'
ALTER TABLE [dbo].[Positions]
ADD CONSTRAINT [PK_Positions]
    PRIMARY KEY CLUSTERED ([ID_Position] ASC);
GO

-- Creating primary key on [ID_Referral] in table 'Referrals'
ALTER TABLE [dbo].[Referrals]
ADD CONSTRAINT [PK_Referrals]
    PRIMARY KEY CLUSTERED ([ID_Referral] ASC);
GO

-- Creating primary key on [RoleID] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleID] ASC);
GO

-- Creating primary key on [ID_Settings] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [PK_Settings]
    PRIMARY KEY CLUSTERED ([ID_Settings] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [Id] in table 'UserSettingsSet'
ALTER TABLE [dbo].[UserSettingsSet]
ADD CONSTRAINT [PK_UserSettingsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Day_ID_Day], [Employee_ID_Employee] in table 'DayEmployee'
ALTER TABLE [dbo].[DayEmployee]
ADD CONSTRAINT [PK_DayEmployee]
    PRIMARY KEY NONCLUSTERED ([Day_ID_Day], [Employee_ID_Employee] ASC);
GO

-- Creating primary key on [Roles_RoleID], [Users_UserID] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [PK_UserRole]
    PRIMARY KEY NONCLUSTERED ([Roles_RoleID], [Users_UserID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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

-- Creating foreign key on [ID_Dept] in table 'Cabinets'
ALTER TABLE [dbo].[Cabinets]
ADD CONSTRAINT [FK_DepartmentCabinet]
    FOREIGN KEY ([ID_Dept])
    REFERENCES [dbo].[Departments]
        ([ID_Dept])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentCabinet'
CREATE INDEX [IX_FK_DepartmentCabinet]
ON [dbo].[Cabinets]
    ([ID_Dept]);
GO

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

-- Creating foreign key on [ID_Dept] in table 'Referrals'
ALTER TABLE [dbo].[Referrals]
ADD CONSTRAINT [FK_DepartmentReferral]
    FOREIGN KEY ([ID_Dept])
    REFERENCES [dbo].[Departments]
        ([ID_Dept])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentReferral'
CREATE INDEX [IX_FK_DepartmentReferral]
ON [dbo].[Referrals]
    ([ID_Dept]);
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

-- Creating foreign key on [EmployeeID_Employee] in table 'ExaminationTemplates'
ALTER TABLE [dbo].[ExaminationTemplates]
ADD CONSTRAINT [FK_EmployeeExaminationTemplate]
    FOREIGN KEY ([EmployeeID_Employee])
    REFERENCES [dbo].[Employees]
        ([ID_Employee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeExaminationTemplate'
CREATE INDEX [IX_FK_EmployeeExaminationTemplate]
ON [dbo].[ExaminationTemplates]
    ([EmployeeID_Employee]);
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

-- Creating foreign key on [ExaminationType_ID_ExmType] in table 'Examinations'
ALTER TABLE [dbo].[Examinations]
ADD CONSTRAINT [FK_ExaminationTypeExamination]
    FOREIGN KEY ([ExaminationType_ID_ExmType])
    REFERENCES [dbo].[ExaminationTypes]
        ([ID_ExmType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ExaminationTypeExamination'
CREATE INDEX [IX_FK_ExaminationTypeExamination]
ON [dbo].[Examinations]
    ([ExaminationType_ID_ExmType]);
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

-- Creating foreign key on [Referral_ID_Referral] in table 'Examinations'
ALTER TABLE [dbo].[Examinations]
ADD CONSTRAINT [FK_ReferralExamination]
    FOREIGN KEY ([Referral_ID_Referral])
    REFERENCES [dbo].[Referrals]
        ([ID_Referral])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReferralExamination'
CREATE INDEX [IX_FK_ReferralExamination]
ON [dbo].[Examinations]
    ([Referral_ID_Referral]);
GO

-- Creating foreign key on [ExaminationTypeID_ExmType] in table 'ExaminationTemplates'
ALTER TABLE [dbo].[ExaminationTemplates]
ADD CONSTRAINT [FK_ExaminationTypeExaminationTemplate]
    FOREIGN KEY ([ExaminationTypeID_ExmType])
    REFERENCES [dbo].[ExaminationTypes]
        ([ID_ExmType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ExaminationTypeExaminationTemplate'
CREATE INDEX [IX_FK_ExaminationTypeExaminationTemplate]
ON [dbo].[ExaminationTemplates]
    ([ExaminationTypeID_ExmType]);
GO

-- Creating foreign key on [Day_ID_Day] in table 'DayEmployee'
ALTER TABLE [dbo].[DayEmployee]
ADD CONSTRAINT [FK_DayEmployee_Days]
    FOREIGN KEY ([Day_ID_Day])
    REFERENCES [dbo].[Days]
        ([ID_Day])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Employee_ID_Employee] in table 'DayEmployee'
ALTER TABLE [dbo].[DayEmployee]
ADD CONSTRAINT [FK_DayEmployee_Employees]
    FOREIGN KEY ([Employee_ID_Employee])
    REFERENCES [dbo].[Employees]
        ([ID_Employee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DayEmployee_Employees'
CREATE INDEX [IX_FK_DayEmployee_Employees]
ON [dbo].[DayEmployee]
    ([Employee_ID_Employee]);
GO

-- Creating foreign key on [Roles_RoleID] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_Roles]
    FOREIGN KEY ([Roles_RoleID])
    REFERENCES [dbo].[Roles]
        ([RoleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_UserID] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_Users]
    FOREIGN KEY ([Users_UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRole_Users'
CREATE INDEX [IX_FK_UserRole_Users]
ON [dbo].[UserRole]
    ([Users_UserID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------