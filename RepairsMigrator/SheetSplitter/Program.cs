using CSV;
using Google;
using Google.Apis.Auth.OAuth2;
using RepairsMigrator;
using RepairsMigrator.Stages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SheetSplitter
{
    class Program
    {
        static void Main()
        {
            CreateDirectories();
            var manager = new SheetManager("RepairsMigration", GoogleCredential.FromFile("Resources/creds.json"));
            IEnumerable<FinanceData> finance = manager.LoadSheet<FinanceData>("1GSsvMcX3Rm0Lh1mMQiV-VvN6pTh8IghbA-3icBQP1cc", "PAYMENTS", 2).Result;

            SplitSheet<IntermediateSheet>("data.csv", d => d.Supplier_Name, k => $"tool/data_{CSVLoader.FixFileName(k)}.csv", 
                k => k.NoFinance == "True" && k.IsCommunal == "True");
            SplitSheet(finance, d => d.CedAr_Supplier, k => $"finance/data_{CSVLoader.FixFileName(k)}.csv",
                k =>
                {

                    bool hasDate = DateTime.TryParseExact(k.Invoice_date, "dd/MM/yyyy", null, DateTimeStyles.None, out var date);
                    return !(int.TryParse(k.UH_job_No, out var jobno) && jobno >= 1000000 && jobno <= 99999999)
                    && hasDate && date > new DateTime(2020, 9, 1);
                });
            //30/04/2020
        }

        private static void CreateDirectories()
        {
            const string toolFolderName = "tool";
            const string financeFolderName = "finance";
            ForceDirectory(toolFolderName);
            ForceDirectory(financeFolderName);
        }

        private static void ForceDirectory(string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
        }

        private static void SplitSheet<T>(string path, Func<T, string> keySelector, Func<string, string> outPathSelector, Func<T, bool> filter = null)
        {
            var data = CSVLoader.LoadCsv<T>(path);

            SplitSheet(data, keySelector, outPathSelector, filter);
        }

        private static void SplitSheet<T>(IEnumerable<T> data, Func<T, string> keySelector, Func<string, string> outPathSelector, Func<T, bool> filter = null)
        {
            var groups = data.Where(filter).GroupBy(keySelector);

            foreach (var group in groups)
            {
                CSVSaver.SaveCsv(outPathSelector(group.Key), group);
            }
        }
    }
}
