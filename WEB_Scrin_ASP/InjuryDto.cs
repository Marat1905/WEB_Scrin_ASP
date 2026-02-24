using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_Scrin_ASP
{
    public class InjuryDto
    {
        public string Id { get; set; }
        public string Date { get; set; } // ISO
        public string Type { get; set; }
        public string Description { get; set; }
    }
}