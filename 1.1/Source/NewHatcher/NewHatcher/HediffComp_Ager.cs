using System;
using Verse;
using RimWorld;


namespace NewHatcher
{
    public class HediffComp_Ager : HediffComp
    {


        public HediffCompProperties_Ager Props
        {
            get
            {
                return (HediffCompProperties_Ager)this.props;
            }
        }

        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            this.parent.pawn.ageTracker.DebugMake1YearOlder();
        }

        




    }
}

