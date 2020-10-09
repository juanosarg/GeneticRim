using Verse;

namespace GeneticRim
{
    public class HediffCompProperties_Milker : HediffCompProperties
    {

        public float hatcherDaystoHatch = 1f;
        public int hatcherYield = 1;


        public HediffCompProperties_Milker()
        {
            this.compClass = typeof(HediffComp_Milker);
        }
    }
}
