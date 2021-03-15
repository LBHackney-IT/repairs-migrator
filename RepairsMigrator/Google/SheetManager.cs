using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Google
{
    public class SheetManager
    {
        private readonly SheetsService service;

        public SheetManager(string appName, GoogleCredential creds)
        {
            this.service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = creds,
                ApplicationName = appName,
            });
        }

        public async Task<IEnumerable<TModel>> LoadSheet<TModel>(string sheetId, string sheetName)
            where TModel : class, new()
        {
            return (await LoadSheet(sheetId, sheetName, typeof(TModel))).Cast<TModel>();
        }

        public async Task<IEnumerable<object>> LoadSheet(string sheetId, string sheetName, Type type)
        {
            try
            {
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(sheetId, sheetName);
                ValueRange response = await request.ExecuteAsync();
                var values = response.Values;
                return ConvertResponse(values, type);
            } catch (Exception e)
            {
                var errorCode = Regex.Match(e.Message, @"\[\d+\]");
                Log.Error($"Failed Load Sheet {sheetId}, {sheetName} For {type.Name} with Error Code {errorCode}");
                return new List<object>();
            }
        }

        private IEnumerable<object> ConvertResponse(IList<IList<object>> values, Type type)
        {
            var skipRows = CalculateRowsToSkip(values);
            Dictionary<int, PropertyInfo> propMap = MapHeaders(values, type, skipRows);
            return LoadData(values, propMap, type, skipRows + 1);
        }

        private int CalculateRowsToSkip(IList<IList<object>> values)
        {
            return values.First().Count() <= 1 ? 1 : 0; // TODO: This is a hack and this behaviour should be configured per sheet
        }

        private static List<object> LoadData(IList<IList<object>> values, Dictionary<int, PropertyInfo> propMap, Type type, int startFrom = 1)
        {
            List<object> result = new List<object>();
            foreach (var row in values.Skip(startFrom))
            {
                var model = Activator.CreateInstance(type);

                for (int i = 0; i < row.Count; i++)
                {
                    var value = row[i].ToString();
                    if (propMap.TryGetValue(i, out var prop))
                    {
                        prop.SetValue(model, value);
                    }
                }

                result.Add(model);
            }

            return result;
        }

        private static Dictionary<int, PropertyInfo> MapHeaders(IList<IList<object>> values, Type type, int row = 0)
        {
            var properties = type.GetProperties();
            var propMap = new Dictionary<int, PropertyInfo>();

            var sheetHeaders = values.Skip(row).First().Select(h => FixHeader(h.ToString())).ToList();
            Log.Verbose("{model}, {@sheetHeaders}", type.Name, sheetHeaders);
            for (int i = 0; i < sheetHeaders.Count; i++)
            {
                var header = sheetHeaders[i];
                if (string.IsNullOrWhiteSpace(header)) continue;

                var matchingProp = properties.SingleOrDefault(p => p.Name == header);
                if (matchingProp != null) propMap.Add(i, matchingProp);
            }

            return propMap;
        }

        private static string FixHeader(string h)
        {
            string result = Regex.Replace(h, @"\(.*\)", string.Empty);
            result = Regex.Replace(result, @"[\.\?\-:\,\\\/']", string.Empty);
            result = Regex.Replace(result.Trim(), @"\s+", "_");
            return result;
        }
    }
}
