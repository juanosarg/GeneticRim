using System;
using Verse;
using RimWorld;



namespace GeneticRim
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
                if (!pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_BearHybridGenes")) &&
                    !pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_BoomalopeHybridGenes")) &&
                    !pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_CatHybridGenes")) &&
                    !pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_ChickenHybridGenes")) &&
                    !pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_MuffaloHybridGenes")) &&
                    !pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_RodentHybridGenes")) &&
                    !pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_WolfHybridGenes")) &&
                    !pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_ReptileHybridGenes")) &&
                    !pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_InsectoidHybridGenes")) &&
                    !pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_ThrumboHybridGenes")) &&
                    !pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_MechanoidHybridGenes"))

                    )
                {
                    pawn.health.AddHediff(HediffDef.Named(Props.genes));
                    addHediffOnce = false;
                }
                else
                {
                    Messages.Message("GR_CantUseMoreGenetrainers".Translate(), pawn, MessageTypeDefOf.NeutralEvent);
                }




            }
        }

      
    }
}
