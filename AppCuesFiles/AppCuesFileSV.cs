using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppCuesTranslations.AppCuesFiles
{
    public class AppCuesFileSV
    {
        public class Root
        {
            [JsonPropertyName("content")]
            public List<Content> Content { get; set; }
            [JsonPropertyName("flow")]
            public Flow Flow { get; set; }
            [JsonPropertyName("target_locale")]
            public TargetLocale Target_locale { get; set; }
        }

        public class Content
        {
            public string Swedish { get; set; }
            [JsonPropertyName("default")]
            public string Default { get; set; }
            [JsonPropertyName("id")]
            public string Id { get; set; }
        }

        public class Flow
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        public class TargetLocale
        {
            [JsonPropertyName("conditions")]
            public Conditions Conditions { get; set; }
            [JsonPropertyName("id")]
            public string Id { get; set; }
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        public class Conditions
        {
            [JsonPropertyName("and")]
            public List<And> And { get; set; }
        }

        public class And
        {
            [JsonPropertyName("properties")]
            public Properties Properties { get; set; }
        }

        public class Properties
        {
            [JsonPropertyName("operator")]
            public string Operator { get; set; }
            [JsonPropertyName("property")]
            public string Property { get; set; }
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }
    }
}
