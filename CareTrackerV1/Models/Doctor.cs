using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CareTrackerV1.Models
{
    public class Doctor
    {
        public int ID { get; set; }       //PK

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Required]                                  //******MAY CHANGE TO TOWN************
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "Address Line 3")]          //*********MAY CHANGE TO CITY*********
        public string AddressLine3 { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Mobile number")]
        public string Mobile { get; set; }

        [Required]                              //***********SHOULD THIS BE REQUIRED?????**********
        [Display(Name = "Email address")]
        [EmailAddress]
        public string Email { get; set; }

        //Navigation properties
        //One doctor could have many clients - use ICollection
        public virtual ICollection<Client> Clients { get; set; }
    }
}