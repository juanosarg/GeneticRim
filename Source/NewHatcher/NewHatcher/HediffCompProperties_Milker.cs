using Verse;

namespace NewHatcher
{
    public class HediffCompProperties_Milker : HediffCompProperties
    {

        public float hatcherDaystoHatch = 1f;

        public HediffCompProperties_Milker()
        {
            this.compClass = typeof(HediffComp_Milker);
        }
    }
}
