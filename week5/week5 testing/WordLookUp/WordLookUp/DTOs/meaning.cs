using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLookUp.DTOs
{
    public class meaning
    {
        // [JsonProperty("partOfSpeech")]
        public string partOfSpeech { get; set; }

        //[JsonProperty("definitions")]
        public definitions[] definitions { get; set; }
    }

}
