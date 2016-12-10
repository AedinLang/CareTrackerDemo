using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CareTrackerV1.Models;
using CareTrackerV1.Enums;

namespace CareTrackerV1.DAL
{
    public class CareTrackerV1Initializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var doctors = new List<Doctor>
            {
                new Doctor {FirstName="Mary", Surname="Murphy", AddressLine1 = "22 Beverly Court", AddressLine2 = "Knocklyon", AddressLine3="Dublin 16", PhoneNumber="012222222", Mobile="0871111111", Email="mary123@hotmail.com" },
                new Doctor {FirstName="Jack", Surname="Black", AddressLine1 = "13 The Lane", AddressLine2 = "Ballinteer", AddressLine3="Dublin 14", PhoneNumber="015555555", Mobile="0852111111", Email="jack.black@gmail.com" },
                new Doctor {FirstName="Maeve", Surname="Smith", AddressLine1 = "Laurel House", AddressLine2 = "Templeogue", AddressLine3="Dublin 14", PhoneNumber="011234567", Mobile="0868989898", Email="smithM@gmail.com" }
            };
            doctors.ForEach(d => context.Doctors.Add(d));       
            context.SaveChanges();

            var nextOfKins = new List<NextOfKin>
            {
                new NextOfKin {FirstName="Jane", Surname="Blogs", AddressLine1="3 Belmont Place", AddressLine2="Cobh", AddressLine3="County Cork", PhoneNumber="0213333333", Mobile="0871234123", Email= "jane123@eircom.net" },
                new NextOfKin {FirstName="John", Surname="Doe", AddressLine1="Rock House", AddressLine2="Skerries", AddressLine3="County Dublin", PhoneNumber="012323232", Mobile="0871234123", Email= "jane123@eircom.net" },
                new NextOfKin {FirstName="Ann", Surname="O'Reilly", AddressLine1="15 Ballyroan Road", AddressLine2="Oranmore", AddressLine3="County Galway", PhoneNumber="0912222222", Mobile="085121212123", Email= "annor@hotmail.com" },
                new NextOfKin {FirstName="Brian", Surname="Dunne", AddressLine1="7 Smithfield Crescent", AddressLine2="Drumcondra", AddressLine3="Dublin 9", PhoneNumber="019876543", Mobile="0859876543", Email= "brian123@gmail.com" },
                new NextOfKin {FirstName="Catriona", Surname="Doyle", AddressLine1="Apartment 12", AddressLine2="Oaklands", AddressLine3="Dublin 13", PhoneNumber="014545455", Mobile="0859988776", Email= "catrionad@eircom.net" },
                new NextOfKin {FirstName="David", Surname="Clancy", AddressLine1="12 Anne Devlin Park", AddressLine2="Rathfarnham", AddressLine3="Dublin 16", PhoneNumber="012342341", Mobile="0873453456", Email= "dave123@hotmail.com" },
                new NextOfKin {FirstName="Emer", Surname="Gillen", AddressLine1="The Moorings", AddressLine2="Maree", AddressLine3="County Galway", PhoneNumber="0913333333", Mobile="0864646467", Email= "gillenem@eircom.net" }
            };
            nextOfKins.ForEach(n => context.NextOfKins.Add(n));     
            context.SaveChanges();

            var clients = new List<Client>
            {
                new Client {FirstName="Joe", Surname = "Blogs", DOB = DateTime.Parse("01-12-1940"), AddressLine1="10 The Rise", AddressLine2="Rathfarnham", Region=Region.Dublin1, PhoneNumber="014444444", Medication="A cocktail of drugs", HealthSummary="Not too good", DoctorID=1, NextOfKinID=1, CareGivers=new List<CareGiver>()},   //Creating an empty list for CareGivers
                new Client {FirstName="Jane", Surname = "Doe", DOB = DateTime.Parse("23-01-1934"), AddressLine1="The Orchard", AddressLine2="Templeogue", Region=Region.Dublin5, PhoneNumber="013333333", Medication="A mixture of drugs", HealthSummary="Poor health", DoctorID=1, NextOfKinID=2, CareGivers=new List<CareGiver>()},
                new Client {FirstName="Mary", Surname = "O'Reilly", DOB = DateTime.Parse("14-01-1943"), AddressLine1="27 College Close", AddressLine2="Terenure", Region=Region.Dublin2, PhoneNumber="018666666", Medication="A lot of different tablets", HealthSummary="Poor health", DoctorID=1, NextOfKinID=3, CareGivers=new List<CareGiver>()},
                new Client {FirstName="Sheila", Surname = "Dunne", DOB = DateTime.Parse("06-07-1936"), AddressLine1="6 Butterfield Park", AddressLine2="Rathfarnham", Region=Region.Dublin6, PhoneNumber="017899999", Medication="A mixture of drugs", HealthSummary="Very poor", DoctorID=3, NextOfKinID=4, CareGivers=new List<CareGiver>()},
                new Client {FirstName="Kevin", Surname = "Doyle", DOB = DateTime.Parse("05-03-1930"), AddressLine1="7 Nutgrove Avenue", AddressLine2="Rathfarnham", Region=Region.Dublin3, PhoneNumber="012121211", Medication="A mixture of drugs", HealthSummary="Poor health", DoctorID=2, NextOfKinID=5, CareGivers=new List<CareGiver>()},
                new Client {FirstName="Louise", Surname = "Clancy", DOB = DateTime.Parse("16-10-1938"), AddressLine1="12 Ann Devlin Park", AddressLine2="Rathfarnham", Region=Region.Dublin5, PhoneNumber="012342341", Medication="A mixture of drugs", HealthSummary="Poor health", DoctorID=1, NextOfKinID=6, CareGivers=new List<CareGiver>()},
                new Client {FirstName="Frank", Surname = "Gillen", DOB = DateTime.Parse("03-07-1937"), AddressLine1="3 Cypress Court", AddressLine2="Templeogue", Region=Region.Dublin6, PhoneNumber="019797978", Medication="A mixture of drugs", HealthSummary="Poor health", DoctorID=1, NextOfKinID=2, CareGivers=new List<CareGiver>()}
            };
            
            clients.ForEach(c => context.Clients.Add(c));       
            context.SaveChanges();

            var caregivers = new List<CareGiver>
            {
                new CareGiver {FirstName="Frank", Surname="O'Reilly",AddressLine1="10 Holly Park", AddressLine2="Crumlin",Region=Region.Dublin1, Email="Frank@gmail.com",PhoneNumber="017897899", Mobile="0861234567",Qualifications="Test",CV="Test",References="Test" },
                new CareGiver {FirstName="Kay", Surname="Reidy",AddressLine1="17 Prospect Heath", AddressLine2="Rathfarnham",Region=Region.Dublin6, Email="Kay@gmail.com",PhoneNumber="014988888", Mobile="0872233567",Qualifications="Test",CV="Test",References="Test" },
                new CareGiver {FirstName="Graham", Surname="Donohoe",AddressLine1="4 Wainsfort Road", AddressLine2="Terenure",Region=Region.Dublin4, Email="Graham@gmail.com",PhoneNumber="015989895", Mobile="0859494943",Qualifications="Test",CV="Test",References="Test" },
                new CareGiver {FirstName="Helena", Surname="Kiernan",AddressLine1="12 Templeville Avenue", AddressLine2="Templeogue",Region=Region.Dublin4, Email="Helena@gmail.com",PhoneNumber="016988886", Mobile="0876869665",Qualifications="Test",CV="Test",References="Test" },
                new CareGiver {FirstName="Ian", Surname="Stack",AddressLine1="3 Willowbank Drive", AddressLine2="Templeogue",Region=Region.Dublin2, Email="Ian@gmail.com",PhoneNumber="014555555", Mobile="0873344556",Qualifications="Test",CV="Test",References="Test" },
                new CareGiver {FirstName="Jean", Surname="Murphy",AddressLine1="24 The Glen", AddressLine2="Ballyboden Way",Region=Region.Dublin6, Email="Jean@gmail.com",PhoneNumber="0149777777", Mobile="0854455667",Qualifications="Test",CV="Test",References="Test" },
                new CareGiver {FirstName="Keith", Surname="Lynch",AddressLine1="1 Orlagh Rise", AddressLine2="Knocklyon",Region=Region.Dublin5, Email="Keith@gmail.com",PhoneNumber="014941414", Mobile="0853563569",Qualifications="Test",CV="Test",References="Test" },
                new CareGiver {FirstName="Lucy", Surname="Whelan",AddressLine1="64 Woodfield", AddressLine2="Knocklyon",Region=Region.Dublin4, Email="Lucy@gmail.com",PhoneNumber="012892896", Mobile="0877788990",Qualifications="Test",CV="Test",References="Test" },
                new CareGiver {FirstName="Mathew", Surname="McMoran",AddressLine1="15 Ludford Park", AddressLine2="Ballinteer",Region=Region.Dublin2, Email="Mathew@gmail.com",PhoneNumber="012876599", Mobile="0869865211",Qualifications="Test",CV="Test",References="Test" },

            };

            caregivers.ForEach(g => context.CareGivers.Add(g));       
            context.SaveChanges();

            var visits = new List<Visit>
            {
                new Visit {StartTime=DateTime.Parse("10:30am"), Date=DateTime.Parse("10-6-2016"), CareGiverID=1, ClientID=1, Details="Frank visits Joe", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {StartTime=DateTime.Parse("04:00pm"), Date=DateTime.Parse("10-6-2016"), CareGiverID=1, ClientID=1, Details="Frak visits Joe", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {StartTime=DateTime.Parse("05:00pm"), Date=DateTime.Parse("10-6-2016"), CareGiverID=2, ClientID=2, Details="Kay visits Jane", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {StartTime=DateTime.Parse("05:00pm"), Date=DateTime.Parse("10-6-2016"), CareGiverID=2, ClientID=1, Details="Kay visits Joe", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {StartTime=DateTime.Parse("05:30pm"), Date=DateTime.Parse("10-6-2016"), CareGiverID=3, ClientID=2, Details="Graham visits Jane", AlertType="Test", AlertDetails="Test alert details"}
                /*new Visit {Time=DateTime.Parse("08:00pm"), Date=DateTime.Parse("10-6-2016"), CareGiverID=6, ClientID=3, Details="Test details 6", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {Time=DateTime.Parse("08:30pm"), Date=DateTime.Parse("10-6-2016"), CareGiverID=3, ClientID=4, Details="Test details 7", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {Time=DateTime.Parse("07:30am"), Date=DateTime.Parse("11-6-2016"), CareGiverID=2, ClientID=5, Details="Test details 8", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {Time=DateTime.Parse("09:00am"), Date=DateTime.Parse("11-6-2016"), CareGiverID=1, ClientID=1, Details="Test details 9", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {Time=DateTime.Parse("10:30am"), Date=DateTime.Parse("11-6-2016"), CareGiverID=2, ClientID=3, Details="Test details 10", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {Time=DateTime.Parse("10:30am"), Date=DateTime.Parse("11-6-2016"), CareGiverID=3, ClientID=4, Details="Test details 11", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {Time=DateTime.Parse("12:30pm"), Date=DateTime.Parse("11-6-2016"), CareGiverID=4, ClientID=5, Details="Test details 12", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {Time=DateTime.Parse("01:00pm"), Date=DateTime.Parse("11-6-2016"), CareGiverID=1, ClientID=2, Details="Test details 13", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {Time=DateTime.Parse("02:30pm"), Date=DateTime.Parse("11-6-2016"), CareGiverID=7, ClientID=9, Details="Test details 14", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {Time=DateTime.Parse("05:30pm"), Date=DateTime.Parse("11-6-2016"), CareGiverID=5, ClientID=8, Details="Test details 15", AlertType="Test", AlertDetails="Test alert details"},
                new Visit {Time=DateTime.Parse("08:30pm"), Date=DateTime.Parse("11-6-2016"), CareGiverID=1, ClientID=2, Details="Test details 16", AlertType="Test", AlertDetails="Test alert details"}*/

            };

            visits.ForEach(v => context.Visits.Add(v));
            context.SaveChanges();

            
        }
    }
}