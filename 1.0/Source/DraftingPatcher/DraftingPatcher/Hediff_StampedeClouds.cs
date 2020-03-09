using RimWorld;
using System.Collections.Generic;
using Verse;

namespace DraftingPatcher
{
    public class Hediff_StampedeClouds : HediffWithComps
    {
        private int tickerInterval = 18;


        public override void Tick()
        {
            base.Tick();

            if ((this.Severity < 1) && (tickerInterval >= 18))
            {
                //GenExplosion.DoExplosion(this.pawn.Position, this.pawn.Map, 5f, DamageDefOf.Smoke, this.pawn, -1, null, null, null, ThingDefOf.Gas_Smoke, 1f, 1, false, null, 0f, 1, 0f, false);

                List<IntVec3> list = GenAdj.AdjacentCells8WayRandomized();
                for (int i = 0; i < 8; i++)
                {
                    IntVec3 c2 = this.pawn.Position + list[i];
                    if (c2.InBounds(pawn.Map))
                    {
                        Thing thing = ThingMaker.MakeThing(ThingDef.Named("GR_Gas_Dust"), null);

                        GenSpawn.Spawn(thing, c2, pawn.Map);
                    }
                }


               

                tickerInterval = 0;
            }
            tickerInterval++;


        }


    }
}