using Core;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    public class FindPropertyParentsStage : PipelineStage<HierarchyData>
    {

        public override Task Process(HierarchyData model)
        {
            if (model.LevelCode == "2")
            {
                model.ParentReference = model.OwnerReference;
                model.ParentAddress = model.OwnerAddress;
            }
            else if (model.LevelCode == "3")
            {
                FillForBlock(model);
            }
            else if (model.LevelCode == "4")
            {
                FillForSubblock(model);
            } else
            {
                FillForFacility(model);
            }

            return Task.CompletedTask;
        }

        private static void FillForFacility(HierarchyData model)
        {
            if (!string.IsNullOrWhiteSpace(model.SubblockReference))
            {
                model.ParentAddress = model.SubblockAddress;
                model.SubblockReference = model.SubblockReference;

                if (!string.IsNullOrWhiteSpace(model.BlockReference))
                {
                    model.GrandParentAddress = model.BlockAddress;
                    model.GrandParentReference = model.BlockReference;
                }
                else if (!string.IsNullOrWhiteSpace(model.EstateReference))
                {
                    model.GrandParentAddress = model.EstateAddress;
                    model.GrandParentReference = model.EstateReference;
                }
                else
                {
                    model.GrandParentReference = model.OwnerReference;
                    model.GrandParentAddress = model.OwnerAddress;
                }
            }
            else
            {
                FillForSubblock(model);
            }
        }

        private static void FillForSubblock(HierarchyData model)
        {
            if (!string.IsNullOrWhiteSpace(model.BlockReference))
            {
                model.ParentReference = model.BlockReference;
                model.ParentAddress = model.BlockAddress;

                if (!string.IsNullOrWhiteSpace(model.EstateReference))
                {
                    model.GrandParentReference = model.EstateReference;
                    model.GrandParentAddress = model.EstateAddress;
                }
                else
                {
                    model.GrandParentReference = model.OwnerReference;
                    model.GrandParentAddress = model.OwnerAddress;
                }
            }
            else
            {
                FillForBlock(model);
            }
        }

        private static void FillForBlock(HierarchyData model)
        {
            if (!string.IsNullOrWhiteSpace(model.EstateReference))
            {
                model.ParentReference = model.EstateReference;
                model.ParentAddress = model.EstateAddress;
                model.GrandParentReference = model.OwnerReference;
                model.GrandParentAddress = model.OwnerAddress;
            }
            else
            {
                model.ParentReference = model.OwnerReference;
                model.ParentAddress = model.OwnerAddress;
            }
        }
    }

    public class HierarchyData
    {
        [PropertyKey(Keys.Sub_Block_Ref)]
        public string SubblockReference { get; set; }
        
        [PropertyKey(Keys.Sub_Block_Name)]
        public string SubblockAddress { get; set; }
        
        [PropertyKey(Keys.Block_Ref)]
        public string BlockReference { get; set; }
        
        [PropertyKey(Keys.Block_Name)]
        public string BlockAddress { get; set; }
        
        [PropertyKey(Keys.Estate_Ref)]
        public string EstateReference { get; set; }
        
        [PropertyKey(Keys.Estate_Name)]
        public string EstateAddress { get; set; }
        
        [PropertyKey(Keys.Owner_ref)]
        public string OwnerReference { get; set; }
        
        [PropertyKey(Keys.Owner_Name)]
        public string OwnerAddress { get; set; }
        
        [PropertyKey(Keys.Level_Code)]
        public string LevelCode { get; set; }
        
        [PropertyKey(Keys.Parent_Reference)]
        public string ParentReference { get; set; }
        
        [PropertyKey(Keys.Parent_Address)]
        public string ParentAddress { get; set; }
        
        [PropertyKey(Keys.Grandparent_Reference)]
        public string GrandParentReference { get; set; }
        
        [PropertyKey(Keys.Grandparent_Address)]
        public string GrandParentAddress { get; set; }
    }
}
