using System;
using System.Collections.Generic;

namespace BoardGame.Models
{
    public partial class Tblboardsquaresv2
    {
        public int Id { get; set; }
        public int? Playerid { get; set; }
        public int Northwall { get; set; }
        public int Southwall { get; set; }
        public int Westwall { get; set; }
        public int Eastwall { get; set; }
        public int Colposition { get; set; }
        public int Rowposition { get; set; }

        public Tblplayersv2 Player { get; set; }
    }
}
