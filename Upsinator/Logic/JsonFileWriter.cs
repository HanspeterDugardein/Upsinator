using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
