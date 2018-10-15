using RimWorld;
using Verse;
using System;
using System.Collections.Generic;

namespace DraftingPatcher
{
    public class Gas_InsanityCloud : Gas
    {
        private int tickerInterval = 18;




        public override void Tick()
        {
            base.Tick();
            try
            {
                if (tickerInterval >= 18)
                {

                    HashSet<Thing> hashSet = new HashSet<Thing>(this.Position.GetThingList(this.Map));
                    if (hashSet != null)
                    {
                        foreach (Thing current in hashSet)
                        {
                            Pawn pawn = current as Pawn;
                            bool flag = (pawn != null);
                            if (flag)
                            {
                                if (pawn.TryGetComp<CompDraftable>() != null)
                                {
                                    if (!pawn.TryGetComp<CompDraftable>().GetHorror)
                                    {
                                        pawn.health.AddHediff(HediffDef.Named("ROM_SanityLoss"));
                                        HealthUtility.AdjustSeverity(pawn, HediffDef.Named("ROM_SanityLoss"), (float)0.10);
                                        this.Destroy();
                                    }
                                }
                                else {
                                    pawn.health.AddHediff(HediffDef.Named("ROM_SanityLoss"));
                                    HealthUtility.AdjustSeverity(pawn, HediffDef.Named("ROM_SanityLoss"), (float)0.10);

                                    this.Destroy();
                                }



                            }
                        }

                    }
                    tickerInterval = 0;



                }
                tickerInterval++;
            }
            catch (NullReferenceException e)
            {
                //A weird error is produced sometimes when GetThingList returns a NullReferenceException. I did a try-catch which is inellegant, but it works
            }

        }


    }
}
