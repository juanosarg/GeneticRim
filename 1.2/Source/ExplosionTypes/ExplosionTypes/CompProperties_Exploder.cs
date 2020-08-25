
using Verse;

namespace ExplosionTypes
{
    public class CompProperties_Exploder : CompProperties
    {


        public float wickTimeSeconds;
        public int wickTimeVariance;
        public float explosionForce;


        public CompProperties_Exploder()
        {
            //Messages.Message("Patataaa", MessageSound.Standard);
            this.compClass = typeof(CompExploder);
        }
    }
}