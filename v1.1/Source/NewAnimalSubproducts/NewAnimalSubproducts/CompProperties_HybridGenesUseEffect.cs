using System;
using Verse;
using RimWorld;

namespace NewAnimalSubproducts
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