using System.IO;
using Newtonsoft.Json;

namespace FileGenerator.Serializer
{
    public class FileJsonSerializer<T>
    {
        public void Serialize(T data, string filePath)
        {
            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(jsonWriter, data);
                }
            }
        }
    }
}
