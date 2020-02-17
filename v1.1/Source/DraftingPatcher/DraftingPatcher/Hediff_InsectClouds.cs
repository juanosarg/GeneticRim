using RimWorld;
using System;
using Verse;

namespace DraftingPatcher
{
    public class Hediff_InsectClouds : HediffWithComps
    {
        private int tickerInterval = 18;


        public override void Tick()
        {
            base.Tick();
            
            if ((this.Severity<1)&&(tickerInterval >= 18))
            {
                //GenExplosion.DoExplosion(this.pawn.Position, this.pawn.Map, 5f, DamageDefOf.Smoke, this.pawn, -1, null, null, null, ThingDefOf.Gas_Smoke, 1f, 1, false, null, 0f, 1, 0f, false);

                Thing thing = ThingMaker.MakeThing(ThingDef.Named("GR_InsectCloudMotes"), null);
                
                GenSpawn.Spawn(thing, this.pawn.Position, pawn.Map);
                
                tickerInterval = 0;
            }
            tickerInterval++;


        }

        
    }
}