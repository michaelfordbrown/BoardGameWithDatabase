using System;
using System.Collections.Generic;

namespace BoardGame.Models
{
    public partial class Xrefboardsquareplayer
    {
        public int Id { get; set; }
        public int? Boardsquareid { get; set; }
        public int? Playerid { get; set; }
    }
}
