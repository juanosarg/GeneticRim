using System;
using Verse;
using RimWorld;

namespace GeneticRim
{
    public class CompProperties_HybridGenesUseEffect : CompProperties
    {
        public CompProperties_HybridGenesUseEffect()
        {
            this.compClass = typeof(CompUseEffect_ApplyHediff);
        }

        public bool doCameraShake;
        public string genes = "";
       
    }
}