using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
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
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(sheetId, sheetName);
            ValueRange response = await request.ExecuteAsync();
            var values = response.Values;

            return ConvertResponse(values, typeof(TModel)).Cast<TModel>();
        }

        public async Task<IEnumerable<object>> LoadSheet(string sheetId, string sheetName, Type type)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(sheetId, sheetName);
            ValueRange response = await request.ExecuteAsync();
            var values = response.Values;

            return ConvertResponse(values, type);
        }

        private IEnumerable<object> ConvertResponse(IList<IList<object>> values, Type type)
        {
            Dictionary<int, PropertyInfo> propMap = MapHeaders(values, type);
            return LoadData(values, propMap, type);
        }

        private static List<object> LoadData(IList<IList<object>> values, Dictionary<int, PropertyInfo> propMap, Type type)
        {
            List<object> result = new List<object>();
            foreach (var row in values.Skip(1))
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

        private static Dictionary<int, PropertyInfo> MapHeaders(IList<IList<object>> values, Type type)
        {
            var properties = type.GetProperties();
            var propMap = new Dictionary<int, PropertyInfo>();

            var sheetHeaders = values.First().Select(h => FixHeader(h.ToString())).ToList();
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
            result = Regex.Replace(result, @"[\.\?\-:\,]", string.Empty);
            result = Regex.Replace(result.Trim(), @"\s", "_");
            return result;
        }
    }
}
