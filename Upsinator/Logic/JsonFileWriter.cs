using System.IO;

namespace Upsinator.Logic
{
    public class JsonFileWriter
    {

        public static void WriteObjectToFile(object obj, string path)
        {
            // Get string from json
            var test = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);

            // Write to file
            using (var sw = new StreamWriter(path))
            {
                sw.WriteLine(test);
            }
        }
    }
}
