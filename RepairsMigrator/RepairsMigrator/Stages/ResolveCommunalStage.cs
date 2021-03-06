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
            "Asphalt",
            "Door entry",
            "Lift",
            "outside number",
            "Bulkhead",
            "Bollard",
            "Play Area",
            "Tarmac",
            "Paving",
            " MUGA "
        };

        private readonly int[] communalPropRefs =
        {
            2, 3, 4, 6
        };

        public override Task Process(CommunalModel model)
        {
            if (string.IsNullOrWhiteSpace(model.LevelCode))
            {
                return Task.CompletedTask;
            }

            if (communalPropRefs.Contains(int.Parse(model.LevelCode)))
            {
                model.IsCommunal = TRUE;
                model.CommunalReason = "Communal Property";
                return Task.CompletedTask;
            }

            if (communalKeywords.Any(kw => !string.IsNullOrWhiteSpace(model.Description) 
                                            && model.Description.ToLower().Contains(kw.ToLower())))
            {
                model.IsCommunal = TRUE;
                model.CommunalReason = "Found Communal Keywork\n in job description";
                return Task.CompletedTask;
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
