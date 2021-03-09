using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CSV
{
    public static class CSVLoader
    {
        public static IEnumerable<T> LoadCsv<T>(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            return csv.GetRecords<T>().ToList();
        }
    }
    public static class CSVSaver
    {
        public static void SaveCsv<T>(string path, IEnumerable<T> data)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                csv.WriteRecords(data);
        }
    }

}
