using System;
using System.Collections.Generic;

namespace BoardGame.Models
{
    public partial class Tblboardsquare
    {
        public int Id { get; set; }
        public int? Colposition { get; set; }
        public int? Rowposition { get; set; }
        public int? Boardspaceid { get; set; }
        public int? Northwall { get; set; }
        public int? Southwall { get; set; }
        public int? Westwall { get; set; }
        public int? Eastwall { get; set; }
    }
}
