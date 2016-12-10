using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CareTrackerV1.Models
{
    public class File
    {
        public int FileId { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }
        public int CareGiverID { get; set; }
        //public int ClientID { get; set; }
        public byte[] Content { get; set; }

        //Navigation properties
        public FileType FileType { get; set; }
        [ForeignKey("CareGiverID")]
        public virtual CareGiver CareGiver { get; set; }

        //A Visit will have 1 Client only
        //[ForeignKey("ClientID")]
        //public virtual Client Client { get; set; }
    }
}
 
