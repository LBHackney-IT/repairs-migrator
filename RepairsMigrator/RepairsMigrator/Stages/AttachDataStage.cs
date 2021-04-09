using Core;
using CSV;
using CsvHelper;
using Google;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    public class AttachDataStage<TData, TModel> : PipelineStage<TModel>
        where TModel : class, new()
    {
        private readonly Dictionary<string, TData> data;
        private readonly Func<TModel, string> modelKeyResolver;
        private readonly Func<TData, string> dataKeyResolver;
        private readonly Action<TModel, TData> dataAttacher;
        private readonly HashSet<string> usageTracker;
        private readonly string logText;
        private int successCounter;

        public AttachDataStage(IList<TData> data, 
            Func<TModel, string> modelKeyResolver, 
            Func<TData, string> dataKeyResolver, 
            Action<TModel, TData> dataAttacher)
        {
            this.modelKeyResolver = modelKeyResolver;
            this.dataKeyResolver = dataKeyResolver;
            this.dataAttacher = dataAttacher;
            usageTracker = new HashSet<string>();
            this.data = ToDictionary(data, d => this.dataKeyResolver(d));
            this.logText = $"Attaching {typeof(TData).Name} Records";
        }

        private Dictionary<string, TData> ToDictionary(IList<TData> data, Func<TData, string> keyResolver)
        {
            Dictionary<string, TData> result = new Dictionary<string, TData>();

            foreach (var item in data)
            {
                var key = keyResolver(item);

                if (key != null)
                {
                    result.TryAdd(key, item);
                    usageTracker.Add(key);
                }
            }

            return result;
        }

        public override Task Startup()
        {
            this.successCounter = 0;
            Log.Information(logText + " : Started");
            return base.Startup();
        }

        public override Task TearDown()
        {
            Log.Information(logText + " : Finished after matching {count} records", successCounter);
            return File.WriteAllTextAsync($"{typeof(TData).Name}-missed-records.csv", string.Join('\n', usageTracker));
        }

        public override Task Process(TModel model)
        {
            var key = this.modelKeyResolver(model);

            if (key != null && data.TryGetValue(key, out var value))
            {
                this.dataAttacher(model, value);
                usageTracker.Remove(key);
                successCounter++;
            }

            return Task.CompletedTask;
        }
    }
}
