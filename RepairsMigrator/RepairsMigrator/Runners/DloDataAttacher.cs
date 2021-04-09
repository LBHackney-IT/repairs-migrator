using Core;
using Google;
using Google.Apis.Auth.OAuth2;
using RepairsMigrator.Filters;
using RepairsMigrator.SheetModels;
using RepairsMigrator.Stages;
using System;
using System.Collections.Generic;
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
            var pf = await manager.LoadSheet<ProFormaSheet>("1k_C5UtLHGRDioNfORP8AzKR6ouLcP95c9huZVzNt86g", "Summary of all jobs");
            var dlo = await manager.LoadSheet<DLOSheet>("1i9q42Kkbugwi4f2S4zdyid2ZjoN1XLjuYvqYqfHyygs", "Form responses 1");

            var proFormaStage = new AttachDataStage<ProFormaSheet, ProFormaAttacher>(pf.ToList(),
                pf => NormaliseDate(pf.DateCreated), pf => NormaliseDate(pf.Reference_number_of_proforma),
                (model, data) =>
                {
                    model.Amount = data.Pounds;
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
                .With(new CommunalFilter())
                .With(new LogStage("Translating property hierarchy to relative parents"))
                .With(new FindPropertyParentsStage())
                .With(new LogResultStage())
                .Build();

            pipeline.In(dlo);

            await pipeline.Run();


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

            [PropertyKey(Keys.Actual_cost_of_invoice)]
            public string Amount { get; set; }
        }
    }
}
