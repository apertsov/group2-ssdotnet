
namespace DCExternalSite.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using DCExternalSite.Web.Models;


    // The MetadataTypeAttribute identifies ExaminationsMetadata as the class
    // that carries additional metadata for the Examinations class.
    [MetadataTypeAttribute(typeof(Examinations.ExaminationsMetadata))]
    public partial class Examinations
    {

        // This class allows you to attach custom attributes to properties
        // of the Examinations class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ExaminationsMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ExaminationsMetadata()
            {
            }

            public string Conclusion { get; set; }

            public string Consultation { get; set; }

            public Employees Employees { get; set; }

            public EntityCollection<ExaminationTypes> ExaminationTypes { get; set; }

            public int ID_Employee { get; set; }

            public int ID_Examination { get; set; }

            public int ID_ExmType { get; set; }

            public int ID_Patient { get; set; }

            public Patients Patients { get; set; }

            public string Protocol { get; set; }

            public string Recommendation { get; set; }

            public EntityCollection<Referrals> Referrals { get; set; }

            public DateTime StartTime { get; set; }

            public byte Status { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ExaminationTypesMetadata as the class
    // that carries additional metadata for the ExaminationTypes class.
    [MetadataTypeAttribute(typeof(ExaminationTypes.ExaminationTypesMetadata))]
    public partial class ExaminationTypes
    {

        // This class allows you to attach custom attributes to properties
        // of the ExaminationTypes class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ExaminationTypesMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ExaminationTypesMetadata()
            {
            }

            public string Description { get; set; }

            public int Duration { get; set; }

            public int Examination_ID_Examination { get; set; }

            public Examinations Examinations { get; set; }

            public int ID_ExmType { get; set; }

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

            public Employees Employees { get; set; }

            public int ID_Dept { get; set; }

            public int ID_Employee { get; set; }

            public int ID_News { get; set; }

            public string Text { get; set; }

            public byte Type { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PatientsMetadata as the class
    // that carries additional metadata for the Patients class.
    [MetadataTypeAttribute(typeof(Patients.PatientsMetadata))]
    public partial class Patients
    {

        // This class allows you to attach custom attributes to properties
        // of the Patients class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PatientsMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PatientsMetadata()
            {
            }

            public string Address { get; set; }

            public DateTime BirthDate { get; set; }

            public string City { get; set; }

            public bool Civil_Servant { get; set; }

            public string Comment { get; set; }

            public string Email { get; set; }

            public EntityCollection<Examinations> Examinations { get; set; }

            public string FirstName { get; set; }

            public int ID_Patient { get; set; }

            public string Password { get; set; }

            public string Patronymic { get; set; }

            public string Phone { get; set; }

            public EntityCollection<Referrals> Referrals { get; set; }

            public string Sex { get; set; }

            public string Specialty { get; set; }

            public string Surname { get; set; }

            public bool Workplace { get; set; }
        }
    }
}
