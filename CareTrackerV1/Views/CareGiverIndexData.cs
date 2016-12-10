using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CareTrackerV1.Models;

namespace CareTrackerV1.ViewModel
{
    public class CareGiverIndexData
    {
        public IEnumerable<CareGiver> CareGivers { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Visit> Visits { get; set; }
    }
}