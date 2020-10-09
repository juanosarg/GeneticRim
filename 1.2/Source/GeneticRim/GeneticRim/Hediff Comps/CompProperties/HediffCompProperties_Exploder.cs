using Verse;

namespace GeneticRim
{
    public class HediffCompProperties_Exploder : HediffCompProperties
    {

        public float explosionForce = 5.9f;

        public HediffCompProperties_Exploder()
        {
            this.compClass = typeof(HediffComp_Exploder);
        }
    }
}
