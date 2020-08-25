using RimWorld;
using Verse;
using System;
using System.Collections.Generic;

namespace DraftingPatcher
{
    public class Gas_InsectClouds : Gas
    {
        private int tickerInterval = 30;




        public override void Tick()
        {
            base.Tick();
            try
            {
                    if (tickerInterval >= 30)
                    {
                
                        HashSet<Thing> hashSet = new HashSet<Thing>(this.Position.GetThingList(this.Map));
                        if (hashSet!=null) { 
                            foreach (Thing current in hashSet)
                            {
                                Pawn pawn = current as Pawn;
                                bool flag = (pawn != null);
                                if (flag)
                                {
                                    if (pawn.TryGetComp<CompDraftable>() != null)
                                    {
                                        if (!pawn.TryGetComp<CompDraftable>().GetCanDoInsectClouds)
                                        {
                                            pawn.TakeDamage(new DamageInfo(DamageDefOf.Cut, 1,0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown));
                                        } 
                                    }   else pawn.TakeDamage(new DamageInfo(DamageDefOf.Cut, 1,0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown));


                                }
                            }

                        }
                        tickerInterval = 0;

                
                   
                    } tickerInterval++;
            }
            catch (NullReferenceException e)
            {
                //A weird error is produced sometimes when GetThingList returns a NullReferenceException. I did a try-catch which is inellegant, but it works
            }

        }


    }
}