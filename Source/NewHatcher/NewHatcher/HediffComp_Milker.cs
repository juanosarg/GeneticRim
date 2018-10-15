using System;
using Verse;
using RimWorld;


namespace NewHatcher
{
    public class HediffComp_Milker : HediffComp
    {

        private int HatchingTicker = 0;

        public HediffCompProperties_Milker Props
        {
            get
            {
                return (HediffCompProperties_Milker)this.props;
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
                if ((this.parent.pawn.Map != null) && ((this.parent.pawn.Faction == Faction.OfPlayer) || ((this.parent.pawn.IsPrisoner) && (this.parent.pawn.Map.IsPlayerHome))))
                {
                    GenSpawn.Spawn(ThingDef.Named("Milk"), this.parent.pawn.Position, this.parent.pawn.Map);
                }
                HatchingTicker = 0;

            }


            //this.parent.Destroy(DestroyMode.Vanish);
        }


    }
}
