using Verse;

namespace GeneticRim
{
    public class HediffCompProperties_GenericHatcher : HediffCompProperties
    {

        public float hatcherDaystoHatch = 1f;
        public string thingToHatch = "";
        public int amount = 1;

        public HediffCompProperties_GenericHatcher()
        {
            this.compClass = typeof(HediffComp_GenericHatcher);
        }
    }
}