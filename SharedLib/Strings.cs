using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public static class Strings
    {
        public static string CompanyName = "Migo Company";
        public static string ImagesServerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MigoSystems", "POSFiles");
    }
}
