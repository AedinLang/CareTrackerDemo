using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareTrackerV1.Models
{
    public class VisitTask
    {
        public int ID { get; set; }

        public string Description { get; set; }

        //Navigation Property
        public virtual ICollection<Visit> Visits { get; set; }
    }
}