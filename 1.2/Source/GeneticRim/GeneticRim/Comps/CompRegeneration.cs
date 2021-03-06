﻿
using UnityEngine;
using Verse;
using System.Linq;


namespace GeneticRim
{
    public class CompRegeneration : ThingComp
    {

        public int tickCounter = 0;



        public CompProperties_Regeneration Props
        {
            get
            {
                return (CompProperties_Regeneration)this.props;
            }
        }

        protected int rateInTicks
        {
            get
            {
                return this.Props.rateInTicks;
            }
        }


        public override void CompTick()
        {
            tickCounter++;

            if (tickCounter >= rateInTicks)
            {
                Pawn pawn = this.parent as Pawn;

                if (pawn.health != null)
                {
                    if (pawn.health.hediffSet.GetInjuriesTendable() != null && pawn.health.hediffSet.GetInjuriesTendable().Count<Hediff_Injury>() > 0)
                    {
                        foreach (Hediff_Injury injury in pawn.health.hediffSet.GetInjuriesTendable())
                        {
                            injury.Severity = Mathf.Clamp(injury.Severity - 0.1f, 0.0f, 1.0f);
                        }
                    }
                }
                tickCounter = 0;
            }
        }


    }
}


