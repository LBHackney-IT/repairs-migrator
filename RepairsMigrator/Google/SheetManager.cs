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

        public async Task<IEnumerable<TModel>> LoadSheet<TModel>(string sheetId, string sheetName, int? skip = null)
            where TModel : class, new()
        {
            return (await LoadSheet(sheetId, sheetName, typeof(TModel), skip)).Cast<TModel>();
        }

        public Task<IEnumerable<object>> LoadSheet(string sheetId, string sheetName, Type type, int? skip = null)
        {
            return new SheetLoader(service, sheetId, sheetName, type, skip).LoadSheet();
        }
    }
}
