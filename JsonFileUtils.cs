using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppCuesTranslations
{
    public static class JsonFileUtils
    {
        private static readonly JsonSerializerOptions _options =
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        public static void PrettyWrite(object obj, string fileName)
        {
            var options = new JsonSerializerOptions(_options)
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
            };
            var jsonString = JsonSerializer.Serialize(obj, options);
            try
            {
                File.WriteAllText(fileName, jsonString);
            }
            catch (Exception e)
            {
                var a = e.ToString();
            }
        }
    }
}
