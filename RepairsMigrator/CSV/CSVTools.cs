using CsvHelper;
using CsvHelper.Configuration;
using System;
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

        public static IEnumerable<object> LoadCsv(string path, Type type)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                return csv.GetRecords(type).ToList();
        }

        public static string FixFileName(string path)
        {
            path = Regex.Replace(path, @"\/", "");
            return path;
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

        public static void SaveCsv(string path, IEnumerable<object> data, Type modelType)
        {
            var list = (IWrappedList)Activator.CreateInstance(typeof(WrappedList<>).MakeGenericType(modelType));
            list.SaveCsv(path, data);
        }
    }

    internal class WrappedList<T> : List<T>, IWrappedList
    {
        public void SaveCsv(string path, IEnumerable<object> data)
        {
            this.AddRange(data.Cast<T>());
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                csv.WriteRecords(this);
        }
    }

    internal interface IWrappedList
    {
        void SaveCsv(string path, IEnumerable<object> data);
    }
}
