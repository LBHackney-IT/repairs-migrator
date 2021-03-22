using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    class ResolveCommunalStage : PipelineStage<CommunalModel>
    {
        private const string TRUE = "True";
        private const string FALSE = "False";

        private readonly string[] communalKeywords =
        {
            "Estate",
            "Fence" ,
            "Fencing",
            "Roof Tiles",
            "Roofing",
            "Roofer",
            "Scaffold",
            "Scaffolding",
            "Scaffold Hire",
            "Roof Leak",
            "Balcony Leak",
            "Gutter",
            "Guttering",
            "Communal",
            "Barrier Gate",
            "Asphalt"
        };

        public override Task Process(CommunalModel model)
        {
            if (string.IsNullOrWhiteSpace(model.LevelCode))
            {
                return Task.CompletedTask;
            }

            if (int.Parse(model.LevelCode) == 6)
            {
                model.IsCommunal = TRUE;
                model.CommunalReason = "Communal Property";
            }

            if (communalKeywords.Any(kw => model.Description.Contains(kw)))
            {
                model.IsCommunal = TRUE;
                model.CommunalReason = "Found Communal Keywork\n in job description";
            }
            
            model.IsCommunal = FALSE;

            return Task.CompletedTask;
        }
    }

    internal class CommunalModel
    {
        [PropertyKey(Keys.Description)]
        public string Description { get; set; }

        [PropertyKey(Keys.Level_Code)]
        public string LevelCode { get; set; }

        [PropertyKey(Keys.IsCommunal)]
        public string IsCommunal { get; set; }

        [PropertyKey(Keys.CommunalReason)]
        public string CommunalReason { get; set; }
    }
}
