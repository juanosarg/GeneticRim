using Verse;

namespace GeneticRim
{
    public class HediffCompProperties_Ager : HediffCompProperties
    {

        public int daysToAdvance = 1;

        public HediffCompProperties_Ager()
        {
            this.compClass = typeof(HediffComp_Ager);
        }
    }
}
