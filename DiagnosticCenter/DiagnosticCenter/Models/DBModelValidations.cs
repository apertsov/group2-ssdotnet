using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DiagnosticCenter.Resources.Models.Cabinets;
using DiagnosticCenter.Resources.Models.Examinations;
using DiagnosticCenter.Classes;
using DiagnosticCenter.Resources.Models.Settings;

namespace DiagnosticCenter.Models
{
// Settings
    [MetadataType(typeof(SettingsMetadata))]
    public partial class Settings { }
    public class SettingsMetadata
    {
        [Display(ResourceType = typeof(SettingsFieldNames), Name = "CenterName")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SettingsValidationStrings))]
        public System.String CenterName { get; set; }

        [Display(ResourceType = typeof(SettingsFieldNames), Name = "CenterDetails")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SettingsValidationStrings))]
        public System.String CenterDetails { get; set; }

        [Display(ResourceType = typeof(SettingsFieldNames), Name = "CenterAddress")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SettingsValidationStrings))]
        public System.String CenterAddress { get; set; }

        [Display(ResourceType = typeof(SettingsFieldNames), Name = "CenterPhoneNo")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SettingsValidationStrings))]
        public System.String CenterPhoneNo { get; set; }

        [Display(ResourceType = typeof(SettingsFieldNames), Name = "DefaultLanguage")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SettingsValidationStrings))]
        public System.String LanguageId { get; set; }

        [Display(ResourceType = typeof(SettingsFieldNames), Name = "DefaultTheme")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SettingsValidationStrings))]
        public System.String ThemeId { get; set; }

        [Display(ResourceType = typeof(SettingsFieldNames), Name = "StartHour")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SettingsValidationStrings))]
        public System.Int32 WorkDayStartHour { get; set; }

        [Display(ResourceType = typeof(SettingsFieldNames), Name = "EndHour")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SettingsValidationStrings))]
        public System.Int32 WorkDayEndHour { get; set; }
    }

// ExaminationType
    [MetadataType(typeof(ExaminationTypeMetadata))]
    public partial class ExaminationType { }
    public class ExaminationTypeMetadata
    {
        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(ExaminationTypesStrings))]
        [Display(ResourceType = typeof(ExaminationTypesStrings), Name = "ExaminationTypeName")]
        public global::System.String Name;

        [Required(ErrorMessageResourceName = "DescriptionRequired", ErrorMessageResourceType = typeof(ExaminationTypesStrings))]
        [Display(ResourceType = typeof(ExaminationTypesStrings), Name = "ExaminationTypeDescription")]
        public global::System.String Description;

        [Required(ErrorMessageResourceName = "PriceRequired", ErrorMessageResourceType = typeof(ExaminationTypesStrings))]
        [Display(ResourceType = typeof(ExaminationTypesStrings), Name = "ExaminationTypePrice")]
        public global::System.Double Price;

        [Required(ErrorMessageResourceName = "DurationRequired", ErrorMessageResourceType = typeof(ExaminationTypesStrings))]
        [Display(ResourceType = typeof(ExaminationTypesStrings), Name = "ExaminationTypeDuration")]
        public global::System.Int32 Duration;
    }

// ExaminationTemplate
    [MetadataType(typeof(ExaminationTemplateMetadata))]
    public partial class ExaminationTemplate { }
    public class ExaminationTemplateMetadata
    {
        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(ExaminationTemplatesStrings))]
        [Display(ResourceType = typeof(ExaminationTemplatesStrings), Name = "TemplateName")]
        public global::System.String Name;

        [Required(ErrorMessageResourceName = "BodyRequired", ErrorMessageResourceType = typeof(ExaminationTemplatesStrings))]
        [Display(ResourceType = typeof(ExaminationTemplatesStrings), Name = "TemplateBody")]
        public global::System.String Body;

        [Required(ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof(ExaminationTemplatesStrings))]
        [Display(ResourceType = typeof(ExaminationTemplatesStrings), Name = "ExaminationType")]
        public global::System.Int32 ExaminationTypeID_ExmType;

        [Required(ErrorMessageResourceName = "AuthorRequired", ErrorMessageResourceType = typeof(ExaminationTemplatesStrings))]
        [Display(ResourceType = typeof(ExaminationTemplatesStrings), Name = "Author")]
        public global::System.Int32 EmployeeID_Employee;

        [Display(ResourceType = typeof(ExaminationTemplatesStrings), Name = "IsRequired")]
        public global::System.Boolean IsPrivate;
    }

//Cabinet    
    [MetadataType(typeof(CabinetMetadata))]
    public partial class Cabinet { }
    public class CabinetMetadata
    {     
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(CabinetsValidationStrings))]
        [RegularExpression("^[0-9]+$", ErrorMessageResourceName = "InvalidFormat", ErrorMessageResourceType = typeof(CabinetsValidationStrings))]
        //[LocalizedDisplayName("CabinetNo", NameResourceType = typeof(CabinetsFieldNames))]
        public System.Int32 Number {get; set;}

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(CabinetsValidationStrings))]
        [RegularExpression("^([0-9]{5,6})|([0-9]{1,2}-[0-9]{2}-[0-9]{2})$", ErrorMessageResourceName = "InvalidFormat", ErrorMessageResourceType = typeof(CabinetsValidationStrings))]
        //[LocalizedDisplayName("CabinetPhoneNo", NameResourceType = typeof(CabinetsFieldNames))]
        public System.String Phone {get; set;}

        //[LocalizedDisplayName("CabinetDescription", NameResourceType = typeof(CabinetsFieldNames))]
        public System.String Description {get; set;}
    }

//Day
    [MetadataType(typeof(DayMetadata))]
    public partial class Day { }
    public class DayMetadata
    {
       
        public System.DateTime Date {get; set;}
        
        public System.DateTime StartTime {get; set;}
        
        public System.DateTime EndTime {get; set;}
        
    }
//Department
    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department { }
    public class DepartmentMetadata
    {
        [Required(ErrorMessage = "Обов'язкове поле")]
        [RegularExpression(@"^[А-ЯЇІЙЄа-яіїйє]+$", ErrorMessage = "Невірний формат")]
        public global::System.String Name {get; set;}
        
        public global::System.String Description {get; set;}

    }
//Employee
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee { }
    public class EmployeeMetadata
    {
        [Required(ErrorMessage = "Обов'язкове поле")]
        [RegularExpression("^([а-яійїєА-Яійїє0-9]+\\s?)+$",ErrorMessage = "Невірний формат")]
        public global::System.String Category {get; set;}
        
        [Required(ErrorMessage = "Обов'язкове поле")]
        public global::System.String Specialty {get; set;}
        
        [RegularExpression("[2-8]",ErrorMessage="Невірна кількість годин")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public global::System.Int32 Rate {get; set;}

        [RegularExpression("^[А-ЯЇІЙЄа-яіїйє][А-ЯЇІЙЄа-яіїйє]*'?[А-ЯЇІЙЄа-яіїйє]+$", ErrorMessage = "Невірний формат")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public global::System.String FirstName { get; set; }

        [RegularExpression("^[А-ЯЇІЙЄа-яіїйє][А-ЯЇІЙЄа-яіїйє]*-?[А-ЯЇІЙЄа-яіїйє]+$", ErrorMessage = "Невірний формат")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public global::System.String Surname { get; set; }

        [RegularExpression("^[А-ЯЇІЙЄа-яіїйє][А-ЯЇІЙЄа-яіїйє]*'?[А-ЯЇІЙЄа-яіїйє]+(вич|вна)$", ErrorMessage = "Невірний формат")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public global::System.String Patronymic { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        [Remote("UserExist", "Employees","Creating")]
        [RegularExpression("^[a-zA-z][a-zA-Z1-9_$()]+$",ErrorMessage = "Невірний формат")]
        public global::System.String Username { get; set; }

        [RegularExpression("^(([^<>()[\\]\\.,;:\\s@\"]+(\\.[^<>()[\\]\\.,;:\\s@\"]+)*)|" +
                          "(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]" +
                          "{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Невірний формат")]
        [Remote("UserExists", "Employees", "Creating")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public global::System.String Email { get; set; }
        
        
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


        [RegularExpression("^[А-ЯЇІЙЄа-яіїйє][А-ЯЇІЙЄа-яіїйє]*'?[А-ЯЇІЙЄа-яіїйє]+$", ErrorMessage = "Невірний формат")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public global::System.String FirstName {get; set;}

        [RegularExpression("^[А-ЯЇІЙЄа-яіїйє][А-ЯЇІЙЄа-яіїйє]*-?[А-ЯЇІЙЄа-яіїйє]+$", ErrorMessage = "Невірний формат")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public global::System.String Surname { get; set; }

        [RegularExpression("^[А-ЯЇІЙЄа-яіїйє][А-ЯЇІЙЄа-яіїйє]*'?[А-ЯЇІЙЄа-яіїйє]+(вич|вна)$", ErrorMessage = "Невірний формат")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public global::System.String Patronymic { get; set; }

        [RegularExpression("^(([^<>()[\\]\\.,;:\\s@\"]+(\\.[^<>()[\\]\\.,;:\\s@\"]+)*)|" +
                           "(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]" +
                           "{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Невірний формат")]
        public global::System.String Email { get; set; }
        
        [Required(ErrorMessage = "Обов'язкове поле")]
        [RegularExpression("^([А-ЯЇІЙа-яіїй]+\\s?)+", ErrorMessage = "Невірний формат")]
        public global::System.String Specialty {get; set;}
        
        [Required(ErrorMessage = "Обов'язкове поле")]
        [RegularExpression("^([А-ЯЇІЙа-яіїй]+(\\s|-)?)+", ErrorMessage = "Невірний формат")]
        public global::System.String City {get; set;}
        
              
        [Required(ErrorMessage = "Обов'язкове поле")]
        [RegularExpression("^((пр-т\\.)|(вул\\.)|(б-р\\.)|(пл\\.)|(пер\\.))\\s" +
                           "([А-ЯІЇЙЄ]\\.\\s)?([А-ЯІЇЙЄа-яійїє\\-]*\\s?)+[1-9][0-9]" + 
                           "*[А-ЯІЇЙЄа-яійїє]?(/[1-9][0-9]*)?$", ErrorMessage = "Невірний формат")]
        public global::System.String Address {get; set;}
        
        [Required(ErrorMessage = "Обов'язкове поле")]
        [RegularExpression("^(([0-9]{5,6})|([0-9]{1,2}-[0-9]{2}-[0-9]{2})|" + 
                           "([0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2})|([0-9]{10}))$", ErrorMessage = "Невірний формат")]
        public global::System.String Phone {get; set;}

        [Required(ErrorMessage = "Обов'язкове поле")]
        [DataType(DataType.Date,ErrorMessage = "Невірний формат")]
        public global::System.DateTime BirthDate { get; set; }
       
        
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