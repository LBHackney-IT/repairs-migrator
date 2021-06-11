using Core;
using CSV;
using Google;
using Google.Apis.Auth.OAuth2;
using RepairsMigrator.JobTrackerModels;
using RepairsMigrator.Stages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairsMigrator.Runners
{
    class DloDataAttacher
    {
        public static async Task Run()
        {
            var manager = new SheetManager("RepairsMigration", GoogleCredential.FromFile("Resources/creds.json"));
            var dlo = await manager.LoadSheet<DLO>("1L09sWeWShUu2_fZGSLrAkytAvTIWQwihjTfPbWVEBN0", "1. DLO work completion", 1);
            var coomunal = await manager.LoadSheet<Communal>("1Jpgsx2JJDlcWpt9vlrEVZbiDpIg2etDuxSn3gQl0dpI", "1. Capital & communal works not matched items", 2);
            var pest = await manager.LoadSheet<PestControl>("1Jpgsx2JJDlcWpt9vlrEVZbiDpIg2etDuxSn3gQl0dpI", "2. Relevant Pest control", 1);
            var cctv = await manager.LoadSheet<CCTV>("1Jpgsx2JJDlcWpt9vlrEVZbiDpIg2etDuxSn3gQl0dpI", "3. CCTV Relevant not matched", 1);
            var scaffolding = await manager.LoadSheet<Scaffolding>("1Jpgsx2JJDlcWpt9vlrEVZbiDpIg2etDuxSn3gQl0dpI", "4. Pride Scaffolding Relevant", 1);

            var pipeline = new PipelineBuilder()
                .With(new LogStage("Processing {count} Records"))
                .With(new LoadAddressStoreStage())
                .With(new PadPropertyReferenceStage())
                .With(new ResolveAddressStage())
                .With(new ResolveHierarchyDetails())
                .With(new FindPropertyParentsStage())
                .Build();


            pipeline.In(dlo);
            pipeline.In(coomunal);
            pipeline.In(pest);
            pipeline.In(cctv);
            pipeline.In(scaffolding);

            await pipeline.Run();

            var outDlo = pipeline.Out<DLO>();
            var outCom = pipeline.Out<Communal>();
            var outPest = pipeline.Out<PestControl>();
            var outCCTV = pipeline.Out<CCTV>();
            var outScaf = pipeline.Out<Scaffolding>();

            CSVSaver.SaveCsv("DLO work completion.csv", outDlo.Where(r => r.Marker == "DLO"));
            CSVSaver.SaveCsv("Capital & communal works not matched items.csv", outCom.Where(r => r.Marker == "COMMUNAL"));
            CSVSaver.SaveCsv("Relevant Pest control.csv", outPest.Where(r => r.Marker == "PEST"));
            CSVSaver.SaveCsv("CCTV Relevant not matched.csv", outCCTV.Where(r => r.Marker == "CCTV"));
            CSVSaver.SaveCsv("Pride Scaffolding Relevant.csv", outScaf.Where(r => r.Marker == "SCAF"));
        }

        public static async Task RunPipeline<T>(Pipeline pipeline, IEnumerable<T> data, string filename)
            where T : class, new()
        {
            pipeline.In(data);

            await pipeline.Run();

            var outData = pipeline.Out<T>();

            CSVSaver.SaveCsv(filename, outData);
        }
    }
}
