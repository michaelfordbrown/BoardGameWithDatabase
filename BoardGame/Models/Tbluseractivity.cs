using System;
using System.Collections.Generic;

namespace BoardGame.Models
{
    public partial class Tbluseractivity
    {
        public int Id { get; set; }
        public string UserIp { get; set; }
        public DateTime? DateOfActivity { get; set; }
        public string FormAccessed { get; set; }
    }
}
