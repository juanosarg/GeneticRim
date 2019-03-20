using System;
using Verse;
using RimWorld;



namespace NewAnimalSubproducts
{
    public class CompUseEffect_ApplyHediff : CompUseEffect
    {
        private bool addHediffOnce = true;
       

        public CompProperties_HybridGenesUseEffect Props
        {
            get
            {
                return (CompProperties_HybridGenesUseEffect)this.props;
            }
        }

        public override void DoEffect(Pawn user)
        {
            if (addHediffOnce)
            {
                
                Pawn pawn = user;
                pawn.health.AddHediff(HediffDef.Named(Props.genes));
                addHediffOnce = false;

             
              
            }
        }

      
    }
}
