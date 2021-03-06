using Core;
using Google;
using Google.Apis.Auth.OAuth2;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    public class FinanceIntegrationStage : PipelineStage<WorkOrder>
    {
        private Dictionary<string, IList<FinanceData>> data;
        private int successfulsRuns;

        public override async Task Startup()
        {
            Log.Information("Processing Finance Records");
            successfulsRuns = 0;
            var manager = new SheetManager("RepairsMigration", GoogleCredential.FromFile("Resources/creds.json"));
            IEnumerable<FinanceData> raw = await manager.LoadSheet<FinanceData>("1GSsvMcX3Rm0Lh1mMQiV-VvN6pTh8IghbA-3icBQP1cc", "PAYMENTS", 2);
            this.data = GroupFinanceData(raw);
        }

        private static Dictionary<string, IList<FinanceData>> GroupFinanceData(IEnumerable<FinanceData> raw)
        {
            var result = new Dictionary<string, IList<FinanceData>>();

            foreach (var item in raw)
            {
                AddItem(result, item);
            }

            return result;
        }

        private static void AddItem(Dictionary<string, IList<FinanceData>> result, FinanceData item)
        {
            var key = item.UH_job_No?.ToLowerInvariant().Trim();

            if (key is null) return;

            if (!result.ContainsKey(key))
            {
                result[key] = new List<FinanceData>();
            }

            result[key].Add(item);
        }

        public override Task Process(WorkOrder model)
        {

            TryProcessFinanceData(model);

            return Task.CompletedTask;
        }

        private void TryProcessFinanceData(WorkOrder model)
        {
            if (!string.IsNullOrWhiteSpace(model.WorkOrderReference) 
                && data.TryGetValue(model.WorkOrderReference.ToLowerInvariant().Trim(), out var financeData))
            {
                ProcessFinanceData(model, financeData);
            }
            else
            {
                LogError(ErrorKeys.NoFinance);
            }
        }

        private void ProcessFinanceData(WorkOrder model, IList<FinanceData> financeData)
        {
            if (financeData.Count == 1)
            {
                AttachData(model, financeData);
            }
            else
            {
                LogError(ErrorKeys.MultipleFinance);
            }
        }

        private void AttachData(WorkOrder model, IList<FinanceData> financeData)
        {
            var record = financeData.Single();

            model.LocalSubJective = record.Local_subj;
            model.CorpSubjective = record.Corp_subj;
            model.InvoiceDate = record.Invoice_date;
            model.InvoiceCost = record.Amount;
            successfulsRuns++;
        }

        public override Task TearDown()
        {
            Log.Information("Attached Fincance Data for {count} records", successfulsRuns);
            return base.TearDown();
        }
    }

    public class WorkOrder
    {
        [PropertyKey(Keys.Work_Order_Reference)]
        public string WorkOrderReference { get; set; }

        [PropertyKey(Keys.Local_Subj_Code)]
        public string LocalSubJective { get; set; }
        
        [PropertyKey(Keys.Corp_Subj_Code)]
        public string CorpSubjective { get; set; }
        
        [PropertyKey(Keys.Date_Of_Invoice)]
        public string InvoiceDate { get; set; }
        
        [PropertyKey(Keys.Actual_cost_of_invoice)]
        public string InvoiceCost { get; set; }
    }
}
