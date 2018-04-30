using System;
using System.Collections.Generic;

namespace BoardGame.Models
{
    public partial class Tbluserlogin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string SecurityLevel { get; set; }
    }
}
