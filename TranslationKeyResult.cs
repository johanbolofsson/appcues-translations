using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppCuesTranslations
{
    public class TranslationKeyResult
    {
        [JsonPropertyName("translations")]
        public List<Translation> Translations { get; set; }

        public class Translation
        {
            [JsonPropertyName("key_id")]
            public int Key { get; set; }
            [JsonPropertyName("language_iso")]
            public string LanguageIso { get; set; }
            [JsonPropertyName("translation")]
            public string Value { get; set; }
        }

    }
}
