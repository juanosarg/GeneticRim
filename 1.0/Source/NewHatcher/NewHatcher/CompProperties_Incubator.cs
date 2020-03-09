using Verse;

namespace NewHatcher
{
    public class CompProperties_Incubator : CompProperties
    {
        public float hatcherDaystoHatch = 1f;

        public PawnKindDef hatcherPawn;

        public CompProperties_Incubator()
        {
            this.compClass = typeof(CompIncubator);
        }
    }
}