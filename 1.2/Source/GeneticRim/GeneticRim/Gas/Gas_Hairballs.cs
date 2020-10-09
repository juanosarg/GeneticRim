using RimWorld;
using Verse;
using System;
using System.Collections.Generic;

namespace GeneticRim
{
    public class Gas_Hairballs : Gas
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
                                
                                        //pawn.health.AddHediff(HediffDef.Named("GR_HairballDamage"));

                                        HealthUtility.AdjustSeverity(pawn, HediffDef.Named("GR_HairballDamage"), 0.25f);
                                        
                                        this.Destroy();
                                  
                               



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

