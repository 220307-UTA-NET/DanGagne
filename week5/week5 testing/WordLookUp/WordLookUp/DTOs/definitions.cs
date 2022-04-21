using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLookUp.DTOs
{
    public class definitions
    {
        //[JsonProperty("definition")]
        public string definition { get; set; }

        //[JsonProperty("example")]
        public string example { get; set; }

        // [JsonProperty("synonyms")]
        public object[] synonyms { get; set; }

        //[JsonProperty("antonyms")]
        public object[] antonyms { get; set; }
    }
}
