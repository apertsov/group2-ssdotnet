
namespace DCExternalSite.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies CabinetMetadata as the class
    // that carries additional metadata for the Cabinet class.
    [MetadataTypeAttribute(typeof(Cabinet.CabinetMetadata))]
    public partial class Cabinet
    {

        // This class allows you to attach custom attributes to properties
        // of the Cabinet class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CabinetMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CabinetMetadata()
            {
            }

            public Department Department { get; set; }

            public string Description { get; set; }

            public EntityCollection<Employee> Employees { get; set; }

            public int ID_Cabinet { get; set; }

            public int ID_Dept { get; set; }

            public int Number { get; set; }

            public string Phone { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies DayMetadata as the class
    // that carries additional metadata for the Day class.
    [MetadataTypeAttribute(typeof(Day.DayMetadata))]
    public partial class Day
    {

        // This class allows you to attach custom attributes to properties
        // of the Day class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class DayMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private DayMetadata()
            {
            }

            public DateTime Date { get; set; }

            public EntityCollection<Employee> Employees { get; set; }

            public DateTime EndTime { get; set; }

            public int ID_Day { get; set; }

            public DateTime StartTime { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies DepartmentMetadata as the class
    // that carries additional metadata for the Department class.
    [MetadataTypeAttribute(typeof(Department.DepartmentMetadata))]
    public partial class Department
    {

        // This class allows you to attach custom attributes to properties
        // of the Department class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class DepartmentMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private DepartmentMetadata()
            {
            }

            public EntityCollection<Cabinet> Cabinets { get; set; }

            public string Description { get; set; }

            public EntityCollection<Employee> Employees { get; set; }

            public int ID_Dept { get; set; }

            public string Name { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies EmployeeMetadata as the class
    // that carries additional metadata for the Employee class.
    [MetadataTypeAttribute(typeof(Employee.EmployeeMetadata))]
    public partial class Employee
    {

        // This class allows you to attach custom attributes to properties
        // of the Employee class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class EmployeeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private EmployeeMetadata()
            {
            }

            public string AtWork { get; set; }

            public Cabinet Cabinet { get; set; }

            public string Category { get; set; }

            public EntityCollection<Day> Days { get; set; }

            [Include]
            public Department Department { get; set; }

            public string Email { get; set; }

            public EntityCollection<Examination> Examinations { get; set; }

            public EntityCollection<ExaminationTemplate> ExaminationTemplates { get; set; }

            public string FirstName { get; set; }

            public int ID_Cabinet { get; set; }

            public int ID_Dept { get; set; }

            public int ID_Employee { get; set; }

            public int ID_User { get; set; }

            public EntityCollection<News> News { get; set; }

            public string Patronymic { get; set; }

            public string Position { get; set; }

            public int Rate { get; set; }

            public EntityCollection<Referral> Referrals { get; set; }

            public string Specialty { get; set; }

            public string Surname { get; set; }

            public string Username { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ExaminationMetadata as the class
    // that carries additional metadata for the Examination class.
    [MetadataTypeAttribute(typeof(Examination.ExaminationMetadata))]
    public partial class Examination
    {

        // This class allows you to attach custom attributes to properties
        // of the Examination class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ExaminationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ExaminationMetadata()
            {
            }

            public string Conclusion { get; set; }

            public string Consultation { get; set; }

            [Include]
            public Employee Employee { get; set; }

            [Include]
            public ExaminationType ExaminationType { get; set; }

            public int ExaminationType_ID_ExmType { get; set; }

            public int ID_Employee { get; set; }

            public int ID_Examination { get; set; }

            public int ID_ExmType { get; set; }

            public int ID_Patient { get; set; }

            public Patient Patient { get; set; }

            public string Protocol { get; set; }

            public string Recommendation { get; set; }

            public Referral Referral { get; set; }

            public Nullable<int> Referral_ID_Referral { get; set; }

            public DateTime StartTime { get; set; }

            public byte Status { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ExaminationTemplateMetadata as the class
    // that carries additional metadata for the ExaminationTemplate class.
    [MetadataTypeAttribute(typeof(ExaminationTemplate.ExaminationTemplateMetadata))]
    public partial class ExaminationTemplate
    {

        // This class allows you to attach custom attributes to properties
        // of the ExaminationTemplate class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ExaminationTemplateMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ExaminationTemplateMetadata()
            {
            }

            public string Body { get; set; }

            public Employee Employee { get; set; }

            public int EmployeeID_Employee { get; set; }

            public ExaminationType ExaminationType { get; set; }

            public int ExaminationTypeID_ExmType { get; set; }

            public int Id { get; set; }

            public bool IsPrivate { get; set; }

            public string Name { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ExaminationTypeMetadata as the class
    // that carries additional metadata for the ExaminationType class.
    [MetadataTypeAttribute(typeof(ExaminationType.ExaminationTypeMetadata))]
    public partial class ExaminationType
    {

        // This class allows you to attach custom attributes to properties
        // of the ExaminationType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ExaminationTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ExaminationTypeMetadata()
            {
            }

            public string Description { get; set; }

            public int Duration { get; set; }

            public EntityCollection<Examination> Examinations { get; set; }

            public EntityCollection<ExaminationTemplate> ExaminationTemplates { get; set; }

            public int ID_ExmType { get; set; }

            public string Name { get; set; }

            public double Price { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies NewsMetadata as the class
    // that carries additional metadata for the News class.
    [MetadataTypeAttribute(typeof(News.NewsMetadata))]
    public partial class News
    {

        // This class allows you to attach custom attributes to properties
        // of the News class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NewsMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NewsMetadata()
            {
            }

            public Employee Employee { get; set; }

            public int ID_Dept { get; set; }

            public int ID_Employee { get; set; }

            public int ID_News { get; set; }

            public string Text { get; set; }

            public string Topic { get; set; }

            public byte Type { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PatientMetadata as the class
    // that carries additional metadata for the Patient class.
    [MetadataTypeAttribute(typeof(Patient.PatientMetadata))]
    public partial class Patient
    {

        // This class allows you to attach custom attributes to properties
        // of the Patient class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PatientMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PatientMetadata()
            {
            }

            public string Address { get; set; }

            public DateTime BirthDate { get; set; }

            public string City { get; set; }

            public bool Civil_Servant { get; set; }

            public string Comment { get; set; }

            public string Email { get; set; }

            public EntityCollection<Examination> Examinations { get; set; }

            public string FirstName { get; set; }

            public int ID_Patient { get; set; }

            public string Password { get; set; }

            public string Patronymic { get; set; }

            public string Phone { get; set; }

            public EntityCollection<Referral> Referrals { get; set; }

            public string Sex { get; set; }

            public string Specialty { get; set; }

            public string Surname { get; set; }

            public bool Workplace { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ReferralMetadata as the class
    // that carries additional metadata for the Referral class.
    [MetadataTypeAttribute(typeof(Referral.ReferralMetadata))]
    public partial class Referral
    {

        // This class allows you to attach custom attributes to properties
        // of the Referral class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ReferralMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ReferralMetadata()
            {
            }

            public DateTime CreationDate { get; set; }

            public Employee Employee { get; set; }

            public EntityCollection<Examination> Examinations { get; set; }

            public int ID_Employee { get; set; }

            public Nullable<int> ID_Examination { get; set; }

            public int ID_Patient { get; set; }

            public int ID_Referral { get; set; }

            public Patient Patient { get; set; }

            public DateTime VisitDate { get; set; }
        }
    }
}
