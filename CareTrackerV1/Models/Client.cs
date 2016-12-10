using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CareTrackerV1.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using PagedList;

namespace CareTrackerV1.Models
{
    public class Client
    {
        public int ID { get; set; }       //PK

        [Required(ErrorMessage = "Please enter a value")]
        [Display(Name = "First Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "First name minimum 3 characters, maximum 40 characters.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Disallowed characters used.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        [Display(Name = "Surname")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Surname minimum 3 characters, maximum 40 characters.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Disallowed characters used.")]
        public string Surname { get; set; }

        [DataType(DataType.Date)]   //gives drop down calender
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]      //DataFormatString has to appear like this for date to appear in edit mode in Chrome
        [Display(Name = "Date of birth")]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        [Display(Name = "Address Line 1")]
        [StringLength(40, ErrorMessage = "Maximum number of characters allowed is 40")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        [StringLength(40, ErrorMessage = "Maximum number of characters allowed is 40")]
        public string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "Region")]
        public Region Region { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        [StringLength(20, ErrorMessage = "Maximum number length is 20")]
        [RegularExpression(@"^[+0-9\s]{1,20}$", ErrorMessage = "Please enter a phone valid number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Medication")]               //************NEED TO DECIDE HOW TO READ THIS IN????LIST?
        public string Medication { get; set; }

        [Required]                                  //************LIKELY TO NEED TEXT BOX/ATTACH FILE
        [Display(Name = "Health Summary")]
        public string HealthSummary { get; set; }

        public int DoctorID { get; set; }

        public int NextOfKinID { get; set; }

        //Navigation properties
        //A Client has 1 Doctor
        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { get; set; }

        //A Client has 1 Next Of Kin
        [ForeignKey("NextOfKinID")]
        public virtual NextOfKin NextOfKin { get; set; }

        //One Client could have many CareGivers - use ICollection
        public virtual ICollection<CareGiver> CareGivers { get; set; }

        //One Client can have many visits
        public virtual ICollection<Visit> Visits { get; set; }



    }
}