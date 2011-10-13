using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticCenter.Models
{
//Cabinet    
    [MetadataType(typeof(CabinetMetadata))]
    public partial class Cabinet { }
    public class CabinetMetadata
    {     
        [Required(ErrorMessage = "Обов'язкове поле")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Невірний формат вводу")]
        public global::System.Int32 Number {get; set;}
        
        [Required(ErrorMessage = "Обов'язкове поле")]
        [RegularExpression("^([0-9]{5,6})|([0-9]{1,2}-[0-9]{2}-[0-9]{2})$", ErrorMessage = "Невірний формат вводу")]
        public global::System.String Phone {get; set;}
        
        public global::System.String Description {get; set;}       
    }

//Day
    [MetadataType(typeof(DayMetadata))]
    public partial class Day { }
    public class DayMetadata
    {
       
        public global::System.DateTime Date {get; set;}
        
        public global::System.DateTime StartTime {get; set;}
        
        public global::System.DateTime EndTime {get; set;}
        
        public global::System.DateTime StartBreak {get; set;}
        
        public global::System.DateTime EndBreak {get; set;}
        
    }
//Department
    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department { }
    public class DepartmentMetadata
    {
        public global::System.String Name {get; set;}
        
        public global::System.String Description {get; set;}

    }
//Employee
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee { }
    public class EmployeeMetadata
    {
        public global::System.String Category {get; set;}
        
        public global::System.String Specialty {get; set;}
        
        public global::System.String Position {get; set;}
        
        public global::System.Int32 Rate {get; set;}
        
        public global::System.Int32 ID_Dept {get; set;}
        
        public global::System.Int32 ID_Cabinet {get; set;}
        
        public global::System.String AtWork {get; set;}
        
        public global::System.Guid ID_User {get; set;}
        
    }
//Examination
    [MetadataType(typeof(ExaminationMetadata))]
    public partial class Examination { }
    public class ExaminationMetadata
    {
        public global::System.Byte Status {get; set;}
        
        public global::System.String Protocol {get; set;}
        
        public global::System.String Conclusion {get; set;}
        
        public global::System.String Recommendation {get; set;}
        
        public global::System.String Consultation {get; set;}
        
        public global::System.DateTime StartTime {get; set;}
        
        public global::System.Int32 ID_Employee {get; set;}
        
        public global::System.Int32 ID_Patient {get; set;}
        
        public global::System.Int32 ID_ExmType {get; set;}
        
    }
//ExaminationType
    [MetadataType(typeof(ExaminationTypeMetadata))]
    public partial class ExaminationType { }
    public class ExaminationTypeMetadata
    {
        public global::System.Int32 ID_ExmType {get; set;}
        
        public global::System.String Description {get; set;}
        
        public global::System.Double Price {get; set;}
        
        public global::System.Int32 Duration {get; set;}
        
    }
//News
    [MetadataType(typeof(NewsMetadata))]
    public partial class News { }
    public class NewsMetadata
    {
        public global::System.Int32 ID_News {get; set;}
        
        public global::System.Int32 ID_Dept {get; set;}
        
        public global::System.String Text {get; set;}
        
        public global::System.Byte Type {get; set;}
        
        public global::System.Int32 ID_Employee {get; set;}
        
    }
//Patient
    [MetadataType(typeof(PatientMetadata))]
    public partial class Patient { }
    public class PatientMetadata 
    {
        public global::System.Int32 ID_Patient {get; set;}
        
        [RegularExpression("^([А-ЯЇІЙа-яіїй]+\\s){2}[А-ЯЇІЙа-яіїй]+$", ErrorMessage = "Невірний формат вводу")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public global::System.String Name {get; set;}
        
        [Required(ErrorMessage = "Обов'язкове поле")]
        [RegularExpression("^([А-ЯЇІЙа-яіїй]+\\s?)+", ErrorMessage = "Невірний формат вводу")]
        public global::System.String Specialty {get; set;}
        
        [Required(ErrorMessage = "Обов'язкове поле")]
        [RegularExpression("^((пр-т.)|([А-ЯІЇЙа-яійї]+\\.?))(\\s?[А-ЯІЇЙа-яійї])*\\s[1-9][0-9]*[А-ЯІЇЙа-яійї]?$", ErrorMessage = "Невірний формат вводу")]
        public global::System.String Address {get; set;}
        
        [Required(ErrorMessage = "Обов'язкове поле")]
        [RegularExpression("^([0-9]{10})|([0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2})$", ErrorMessage = "Невірний формат вводу")]
        public global::System.String Phone {get; set;}
        
        public global::System.String Comment {get; set;}
        
        public global::System.Boolean Workplace {get; set;}
        
        public global::System.Boolean Civil_Servant {get; set;}
        
    }
//Referral
    [MetadataType(typeof(ReferralMetadata))]
    public partial class Referral { }
    public class ReferralMetadata
    {
        public global::System.Int32 ID_Referral {get; set;}
        
        public global::System.DateTime CreationDate {get; set;}
        
        public global::System.DateTime VisitDate {get; set;}
        
        public global::System.Int32 ID_Patient {get; set;}
        
        public global::System.Int32 ID_Employee {get; set;}
        
        public global::System.Int32 ID_Examination {get; set;}
        
    }
}