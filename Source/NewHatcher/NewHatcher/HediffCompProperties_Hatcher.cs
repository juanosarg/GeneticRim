using Verse;

namespace NewHatcher
{
    public class HediffCompProperties_Hatcher : HediffCompProperties
    {

        public float hatcherDaystoHatch = 1f;

        public HediffCompProperties_Hatcher()
        {
            this.compClass = typeof(HediffComp_Hatcher);
        }
    }
}
