using System;
using Verse;
using RimWorld;


namespace GeneticRim
{
    public class HediffComp_GenericHatcher : HediffComp
    {

        private int HatchingTicker = 0;

        public HediffCompProperties_GenericHatcher Props
        {
            get
            {
                return (HediffCompProperties_GenericHatcher)this.props;
            }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            Hatch();
        }

        public void Hatch()
        {
            if (HatchingTicker < (this.Props.hatcherDaystoHatch*60000)) {
                HatchingTicker += 1;
            } else
            {
                if ((this.parent.pawn.Map != null)&&((this.parent.pawn.Faction == Faction.OfPlayer)|| ((this.parent.pawn.IsPrisoner)&& (this.parent.pawn.Map.IsPlayerHome)))) {
                    for(int i = 0; i < this.Props.amount; i++) {
                        GenSpawn.Spawn(ThingDef.Named(this.Props.thingToHatch), this.parent.pawn.Position, this.parent.pawn.Map);
                    }
                    
                }
                HatchingTicker = 0;

            }


        }


    }
}
