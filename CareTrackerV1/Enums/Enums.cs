using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CareTrackerV1.Enums
{
    public enum Region
    {
        [Display(Name = "Dublin 1")]
        Dublin1,
        [Display(Name = "Dublin 2")]
        Dublin2,
        [Display(Name = "Dublin 3")]
        Dublin3,
        [Display(Name = "Dublin 4")]
        Dublin4,
        [Display(Name = "Dublin 5")]
        Dublin5,
        [Display(Name = "Dublin 6")]
        Dublin6
    }
}
