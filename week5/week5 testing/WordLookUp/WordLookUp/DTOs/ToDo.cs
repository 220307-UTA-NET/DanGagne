using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLookUp.DTOs
{
        public class ToDo
        {
            //[JsonProperty("word")]
            public string word { get; set; }

            // [JsonProperty("phonetic")]
            //public string phonetic { get; set; }

            // [JsonProperty("phonetics")]
            public phonetic[] phonetics { get; set; }

            //[JsonProperty("origin")]
            public string origin { get; set; }

            //[JsonProperty("meanings")]
            public meaning[] meanings { get; set; }
        }

        //public partial class Meaning
        //{
        //    // [JsonProperty("partOfSpeech")]
        //    public string PartOfSpeech { get; set; }

        //    //[JsonProperty("definitions")]
        //    public Definition[] Definitions { get; set; }
        //}

        //public partial class Definition
        //{
        //    //[JsonProperty("definition")]
        //    public string DefinitionDefinition { get; set; }

        //    //[JsonProperty("example")]
        //    public string Example { get; set; }

        //    // [JsonProperty("synonyms")]
        //    public object[] Synonyms { get; set; }

        //    //[JsonProperty("antonyms")]
        //    public object[] Antonyms { get; set; }
        //}

        //public partial class Phonetic
        //{
        //    //[JsonProperty("text")]
        //    public string Text { get; set; }

        //    //[JsonProperty("audio", NullValueHandling = NullValueHandling.Ignore)]
        //    public string Audio { get; set; }
        //}
    }



