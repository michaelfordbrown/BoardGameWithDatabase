using System;
using System.Collections.Generic;

namespace BoardGame.Models
{
    public partial class QPlayers
    {
        public QPlayers()
        {
            Tblboardsquaresv2 = new HashSet<Tblboardsquaresv2>();
        }

        public int Id { get; set; }
        public string Playername { get; set; }
        public string Facingdirection { get; set; }

        public ICollection<Tblboardsquaresv2> Tblboardsquaresv2 { get; set; }
    }
}
