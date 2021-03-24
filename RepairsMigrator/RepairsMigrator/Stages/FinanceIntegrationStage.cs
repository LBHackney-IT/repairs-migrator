using Core;
using FuzzySharp;
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
        private IEnumerable<FinanceData> data;
        private int successfulsRuns;
        private readonly bool onlyCommunals;
        private readonly int threshold;

        public FinanceIntegrationStage(bool onlyCommunals = false, int threshold = 0)
        {
            this.onlyCommunals = onlyCommunals;
            this.threshold = threshold;
        }

        public override async Task Startup()
        {
            Log.Information("Processing Finance Records");
            successfulsRuns = 0;
            var manager = new SheetManager("RepairsMigration", GoogleCredential.FromFile("Resources/creds.json"));
            IEnumerable<FinanceData> raw = await manager.LoadSheet<FinanceData>("1GSsvMcX3Rm0Lh1mMQiV-VvN6pTh8IghbA-3icBQP1cc", "PAYMENTS", 2);
            this.data = raw;
        }

        public override Task Process(WorkOrder model)
        {
            if (onlyCommunals && model.IsCommunal != "True")
            {
                return Task.CompletedTask;
            }

            FindBestFit(model);

            return Task.CompletedTask;
        }

        private void FindBestFit(WorkOrder model)
        {
            int maxHeuristic = 0;
            FinanceData finance_data = null;
            foreach (var item in data)
            {
                if (WorkOrdersMatch(model, item))
                {
                    AttachData(model, item);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(item.Job_Description)
                    && !string.IsNullOrWhiteSpace(item.Address)
                    && !string.IsNullOrWhiteSpace(model.Address)
                    && !string.IsNullOrWhiteSpace(model.JobDescription))
                {
                    var job_description_match_heuristic = Fuzz.Ratio(item.Job_Description, model.JobDescription);
                    var address_heuristic = Fuzz.Ratio(item.Address, model.Address);

                    if (job_description_match_heuristic + address_heuristic > maxHeuristic)
                    {
                        maxHeuristic = job_description_match_heuristic + address_heuristic;
                        finance_data = item;
                    }
                }
            }

            if (maxHeuristic > threshold)
            {
                AttachData(model, finance_data);
            }
        }

        private static bool WorkOrdersMatch(WorkOrder model, FinanceData item)
        {
            return item.UH_job_No?.ToLowerInvariant().Trim() == model.WorkOrderReference?.ToLowerInvariant().Trim();
        }

        private void AttachData(WorkOrder model, FinanceData item)
        {
            model.LocalSubJective = item.Local_subj;
            model.CorpSubjective = item.Corp_subj;
            model.InvoiceCost = item.Amount;
            model.InvoiceDate = item.Invoice_date;
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
        public string line_number { get; set; } = null;

        [PropertyKey(Keys.Work_Order_Reference)]
        public string WorkOrderReference { get; set; }

        [PropertyKey(Keys.IsCommunal)]
        public string IsCommunal { get; set; }

        [PropertyKey(Keys.Created_Date)]
        public string DateCreated { get; set; }

        [PropertyKey(Keys.Description)]
        public string JobDescription { get; set; }

        [PropertyKey(Keys.Original_Address)]
        public string Address { get; set; }

        [PropertyKey(Keys.Local_Subj_Code)]
        public string LocalSubJective { get; set; }
        
        [PropertyKey(Keys.Corp_Subj_Code)]
        public string CorpSubjective { get; set; }
        
        [PropertyKey(Keys.Date_Of_Invoice)]
        public string InvoiceDate { get; set; }
        
        [PropertyKey(Keys.Actual_cost_of_invoice)]
        public string InvoiceCost { get; set; }
    }

    public class FinanceData
    {
        public string Line_No { get; set; }
        public string Supplier_No { get; set; }
        public string CedAr_Supplier { get; set; }
        public string Cert_No { get; set; }
        public string Invoice_No { get; set; }
        public string Invoice_date { get; set; }
        public string UH_job_No { get; set; }
        public string Property_ref { get; set; }
        public string Address { get; set; }
        public string Area_Office { get; set; }
        public string Cost_Centre { get; set; }
        public string Corp_subj { get; set; }
        public string Local_subj { get; set; }
        public string Amount { get; set; }
        public string Month { get; set; }
        public string SCA { get; set; }
        public string Contract { get; set; }
        public string Name_of_CPA { get; set; }
        public string Job_Description { get; set; }
    }
}
