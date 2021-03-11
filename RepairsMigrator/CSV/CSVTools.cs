using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSV
{
    public static class CSVLoader
    {
        public static IEnumerable<T> LoadCsv<T>(string path)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                IgnoreBlankLines = true,
                PrepareHeaderForMatch = HeaderFixer
            };

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            return csv.GetRecords<T>().ToList();
        }

        private static string HeaderFixer(PrepareHeaderForMatchArgs args)
        {
            string result = Regex.Replace(args.Header, @"\(.*\)", string.Empty);
            result = Regex.Replace(result, @"[\.\?]", string.Empty);
            result = Regex.Replace(result.Trim(), @"\s", "_");
            return result;
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
