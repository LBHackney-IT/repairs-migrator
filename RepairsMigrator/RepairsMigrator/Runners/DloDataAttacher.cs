using Core;
using CSV;
using Google;
using Google.Apis.Auth.OAuth2;
using RepairsMigrator.Filters;
using RepairsMigrator.SheetModels;
using RepairsMigrator.Stages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Runners
{
    class DloDataAttacher
    {
        public static async Task Run()
        {
            var manager = new SheetManager("RepairsMigration", GoogleCredential.FromFile("Resources/creds.json"));
            var pf = await manager.LoadSheet<ProFormaSheet>("1DU6OA7yMAB-jPTdhBhRMC9BfyOw8Ad83CdKdhkhI3s0", "Summary of all  jobs on sheet tabs");
            var dlo = await manager.LoadSheet<DLOSheet>("1i9q42Kkbugwi4f2S4zdyid2ZjoN1XLjuYvqYqfHyygs", "Form responses 1");

            var proFormaStage = new AttachDataStage<DLOSheet, ProFormaAttacher>(dlo.ToList(),
                pf => NormaliseDate(pf.DateCreated),
                pf => NormaliseDate(pf.Timestamp),
                (model, data) =>
                {
                    model.Marker = "True";
                    model.Address = data.Address_of_repair;
                    model.OAddress = data.Address_of_repair;
                    model.PropRef = data.UH_Property_Reference;

                    if (string.IsNullOrWhiteSpace(model.Description)) model.Description = data.Job_description;
                });

            var dateFormatter = new FormatterStage(Keys.Created_Date, date =>
            {
                if (string.IsNullOrEmpty(date)) return string.Empty;

                if (DateTime.TryParseExact(date, "ddMMyHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                {
                    return result.ToString("dd/MM/yyyy HH:mm:ss");
                }

                return date;
            });

            var pipeline = new PipelineBuilder()
                .With(new LogStage("Processing {count} Records"))
                .With(proFormaStage)
                //.With(new LoadAddressStoreStage())
                .With(new PadPropertyReferenceStage())
                .With(new ResolveAddressStage())
                .With(new ResolveHierarchyDetails())
                .With(new ResolveCommunalStage())
                .With(new LogStage("Filtering Communal Records"))
                .With(new LogStage("Translating property hierarchy to relative parents"))
                .With(new FindPropertyParentsStage())
                .With(dateFormatter)
                .With(new CommunalFilter())
                .With(new LogResultStage())
                .Build();

            pipeline.In(pf);

            await pipeline.Run();

            var data_out = pipeline.Out<LeaseHolderReportSheet>();

            CSVSaver.SaveCsv("out_leaseholder.csv", data_out);
        }

        private static string NormaliseDate(string dateCreated)
        {
            if (string.IsNullOrWhiteSpace(dateCreated)) return null;

            string cleanedDate = dateCreated.Replace("/202", "/2").Replace("/201", "/1")
                            .Replace("/", string.Empty).Replace(" ", string.Empty).Replace(":", string.Empty)
                            .Trim();

            if (long.TryParse(cleanedDate, out _))
                return cleanedDate;
            else
                return null;
        }

        public class ProFormaAttacher
        {
            [PropertyKey(Keys.Created_Date)]
            public string DateCreated { get; set; }

            [PropertyKey(Keys.Description)]
            public string Description { get; set; }

            [PropertyKey(Keys.Short_Address)]
            public string Address { get; set; }
            [PropertyKey(Keys.Original_Address)]
            public string OAddress { get; set; }

            [PropertyKey(Keys.Property_Reference)]
            public string PropRef { get; set; }

            [PropertyKey(Keys.ProFormaMarker)]
            public string Marker { get; set; }
        }
    }
}
