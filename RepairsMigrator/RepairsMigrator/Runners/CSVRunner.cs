using Core;
using CSV;
using RepairsMigrator.SheetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Runners
{
    class CSVRunner
    {
        public static async Task ProcessCSVs(Pipeline<TargetOutputSheet> pipeline)
        {
            var output = new List<TargetOutputSheet>();

            output.AddRange(await LoadAndRun<AxisSheet>("Resources/Axis.csv", pipeline));
            output.AddRange(await LoadAndRun<AvonlineSheet>("Resources/Avonline.csv", pipeline));
            output.AddRange(await LoadAndRun<AlphatrackSheet>("Resources/Alphatrack.csv", pipeline));
            output.AddRange(await LoadAndRun<HertsSheet>("Resources/Herts.csv", pipeline));
            output.AddRange(await LoadAndRun<PurdySheet>("Resources/Purdy.csv", pipeline));
            output.AddRange(await LoadAndRun<StannahSheet>("Resources/Stannah.csv", pipeline));

            CSVSaver.SaveCsv("out_csv.csv", output);
        }

        private static async Task<IEnumerable<TargetOutputSheet>> LoadAndRun<TIn>(string path, Pipeline<TargetOutputSheet> pipeline)
            where TIn : class, new()
        {
            var dloIn = CSVLoader.LoadCsv<TIn>(path);

            return await pipeline.Run(dloIn);
        }
    }
}
