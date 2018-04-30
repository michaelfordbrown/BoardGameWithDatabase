using System;
using System.Collections.Generic;

namespace BoardGame.Models
{
    public partial class Tblpersonnel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? PayRate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
