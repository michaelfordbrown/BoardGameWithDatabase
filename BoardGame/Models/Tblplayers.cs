using System;
using System.Collections.Generic;

namespace BoardGame.Models
{
    public partial class Tblplayers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Facingdirection { get; set; }
        public int? Colposition { get; set; }
        public int? Rowposition { get; set; }
    }
}
