using RimWorld;
using Verse;

using UnityEngine;


namespace NewMachinery
{
    public static class GeneListSetupUtility
    {
        public static Command_SetGeneList SetGeneListCommand(Building pod, Map map)
        {
            return new Command_SetGeneList()
            {
                defaultDesc = "GR_InsertGenesDesc".Translate(),
                defaultLabel = "GR_InsertGenes".Translate(),
                icon = ContentFinder<Texture2D>.Get("ui/commands/GR_InsertGeneticMaterial1", true),
                hotKey = KeyBindingDefOf.Misc1,
                map = map,
                buildingpod = pod
            };
        }

        public static Command_SetGene2List SetGene2ListCommand(Building pod, Map map)
        {
            return new Command_SetGene2List()
            {
                defaultDesc = "GR_InsertGenes2Desc".Translate(),
                defaultLabel = "GR_InsertGenes2".Translate(),
                icon = ContentFinder<Texture2D>.Get("ui/commands/GR_InsertGeneticMaterial2", true),
                hotKey = KeyBindingDefOf.Misc1,
                map = map,
                buildingpod = pod
            };
        }
    }
}
