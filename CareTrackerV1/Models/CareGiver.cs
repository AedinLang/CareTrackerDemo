using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CareTrackerV1.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CareTrackerV1.Models
{
    public class CareGiver
    {
        public int ID { get; set; }    //PK

        [Required(ErrorMessage = "Please enter a value")]
        [Display(Name = "First Name *")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "First name minimum 3 characters, maximum 40 characters.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Disallowed characters used.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        [Display(Name = "Surname *")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Surname minimum 3 characters, maximum 40 characters.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Disallowed characters used.")]                        //Allows "'"
        public string Surname { get; set; }

        //[Required(ErrorMessage = "Please enter a value")]
        [Display(Name = "Address Line 1 *")]
        [StringLength(40, ErrorMessage = "Maximum number of characters allowed is 40")]
        public string AddressLine1 { get; set; }

        //[Display(Name = "Address Line 2")]
        [StringLength(40, ErrorMessage = "Maximum number of characters allowed is 40")]
        public string AddressLine2 { get; set; }

        //[Required]
        [Display(Name = "Region *")]
        public Region Region { get; set; }

        [Required]
        [Display(Name = "Email address")]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        [Display(Name = "Phone number *")]
        [StringLength(20, ErrorMessage = "Maximum number length is 20")]
        [RegularExpression(@"^[+0-9\s]{1,20}$", ErrorMessage = "Please enter a valid phone number")]         //info found at https://msdn.microsoft.com/en-us/library/ff650303.aspx
        public string PhoneNumber { get; set; }

        //[Required]
        [Display(Name = "Mobile number *")]
        [StringLength(20, ErrorMessage = "Maximum number length is 20")]
        [RegularExpression(@"^[+0-9\s]{1,20}$", ErrorMessage = "Please enter a valid mobile number")]
        public string Mobile { get; set; }

        //[Required]
        [Display(Name = "Qualifications *")]     //*************HOW TO READ THIS IN???? SELECT FROM A LIST AND THEN SAVE A LIST OF QUALIFICATIONS PER CARE GIVER???
        public string Qualifications { get; set; }

        //[Required]
        [Display(Name = "CV *")]     //**************WANT THIS TO BE A TEXT FILE - HOW TO DO THIS?
        public string CV { get; set; }

        //[Required]
        [Display(Name = "References *")]     //********WANT THIS TO BE A TEXT FILE - HOW TO DO THIS?
        public string References { get; set; }

        
        //[Required]
        public string UserID { get; set; }      //FK for ApplicationUser

        //Navigation properties
        //One CareGiver could have many Clients - use ICollection
        public virtual ICollection<Client> Clients { get; set; }          //ASSOCIATIVE TABLE UNDER THE HOOD DEFINED IN IDENTITYMODEL

        //One CareGiver could have many Visits
        public virtual ICollection<Visit> Visits { get; set; }            //1-M FK REQUIRED IN VISIT TABLE

        
        //One CarGiver has one User ID
        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

        public IEnumerable<SelectListItem> AllClients { get; set; }

        private List<int> _selectedClients;
        public List<int> SelectedClients
        {
            get
            {
                if (_selectedClients == null)
                {
                    _selectedClients = Clients.Select(c => c.ID).ToList();
                }
                return _selectedClients;
            }
            set
            {
                _selectedClients = value;
            }
        }
        public virtual ICollection<File> Files { get; set; }

    }
}


