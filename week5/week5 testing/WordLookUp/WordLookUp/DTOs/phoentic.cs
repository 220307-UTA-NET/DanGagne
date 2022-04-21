using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLookUp.DTOs
{
    public class phonetic
    {
        //[JsonProperty("text")]
        public string? text { get; set; }

        //[JsonProperty("audio", NullValueHandling = NullValueHandling.Ignore)]
        public string? audio { get; set; }
        //public string? sourceUrl { get; set; } 
        //public license[] license { get; set; }
    }
}
