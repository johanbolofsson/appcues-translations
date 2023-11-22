using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppCuesTranslations
{
    public class LanguageSettings
    {
        public class Root
        {
            
            [JsonPropertyName("lokaliseToken")]
            public string LokaliseToken { get; set; }
            [JsonPropertyName("lokaliseProjectId")]
            public string LokaliseProjectId { get; set; }
            [JsonPropertyName("languages")]
            public List<Language> Languages { get; set; }
        }

        public class Language
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("iso")]
            public string IsoCode { get; set; }
        }
    }
}
