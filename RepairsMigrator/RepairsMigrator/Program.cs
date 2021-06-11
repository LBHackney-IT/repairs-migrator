using Core;
using CSV;
using DB;
using Google;
using Google.Apis.Auth.OAuth2;
using Mapster;
using RepairsMigrator.Runners;
using RepairsMigrator.Stages;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RepairsMigrator
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine(
@"
===========================================
  _______ _            _______          _ 
 |__   __| |          |__   __|        | |
    | |  | |__   ___     | | ___   ___ | |
    | |  | '_ \ / _ \    | |/ _ \ / _ \| |
    | |  | | | |  __/    | | (_) | (_) | |
    |_|  |_| |_|\___|    |_|\___/ \___/|_|
===========================================
");
#if DEBUG 
            Console.WriteLine("Running In Debug");
            var logFile = "log.txt";
            File.Delete(logFile);
#else
            Console.WriteLine("Running in not debug");
#endif
            Console.WriteLine("===========================================");


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
#if DEBUG
                .WriteTo.File(logFile)
#else
                //TODO Log to AWS sink for prod
#endif
                .CreateLogger();

            await DloDataAttacher.Run();

            Log.CloseAndFlush();
        }

        //private static async Task<SheetManager> FinanceData(Pipeline pipeline)
        //{
            
        //    IEnumerable<FinanceData> raw = await manager.LoadSheet<FinanceData>("17PrFYPWJ0_C_il8XwCHXuovTEZdP1iS6aU9SEWomoh8", "PAYMENTS", 2);

        //    pipeline.In(raw.Where(r => !string.IsNullOrWhiteSpace(r.Invoice_No)).Where(
        //        k =>
        //        {

        //            bool hasDate = DateTime.TryParseExact(k.Invoice_date, "dd/MM/yyyy", null, DateTimeStyles.None, out var date);
        //            return !(int.TryParse(k.UH_job_No, out var jobno) && jobno >= 1000000 && jobno <= 99999999)
        //            && hasDate && date > new DateTime(2020, 9, 1);
        //        }
        //        ));
        //    return manager;
        //}
    }
}
