using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGenerator.services.Utils
{
    public static class FileServices
    {
        public static void SaveFile(string path, StringBuilder fileText)
        {
            string FilePath = path + $"{DateTime.Now.ToString("dd-MM-yyyy")}.sql";
            if (!File.Exists(FilePath))
            {
                var x = File.CreateText(FilePath);

                x.Write(fileText.ToString());
                x.Close();
            };
        }
    }
}
