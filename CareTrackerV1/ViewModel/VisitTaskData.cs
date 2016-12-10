using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareTrackerV1.ViewModel
{
    public class VisitTaskData
    {
        public int VisitID { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }
    }
}