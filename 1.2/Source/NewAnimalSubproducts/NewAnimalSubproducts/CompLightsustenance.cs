﻿using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace NewAnimalSubproducts
{
    public class CompLightsustenance : ThingComp
    {


        public float growOptimalGlow = 0.4f;
        private bool addHediffOnce = true;
         


        public CompProperties_Lightsustenance Props
        {
            get
            {
                return (CompProperties_Lightsustenance)this.props;
            }
        }
       

        public override void CompTickRare()
        {
            Pawn pawn = this.parent as Pawn;
            if (addHediffOnce)
            {
                pawn.health.AddHediff(HediffDef.Named("GR_LightSustenance"));
                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("GR_LightSustenance"), false);
                hediff.Severity = 0.0f;
                addHediffOnce = false;
            }
            float num = this.parent.Map.glowGrid.GameGlowAt(this.parent.Position, false);
           //Log.Warning("Light level "+num.ToString());

            if (num >= growOptimalGlow)
            {

                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("GR_LightSustenance"), false);

                if ((hediff != null) && hediff.Severity > 0f)
                {
                    hediff.Severity -= 0.005f;
                    //Log.Warning("Severity " + hediff.Severity.ToString());
                }
            } else
            {
                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("GR_LightSustenance"), false);
                                
                if ((hediff != null) && hediff.Severity<1f)
                {

                    hediff.Severity += 0.005f;
                   // Log.Warning("Severity " + hediff.Severity.ToString());

                }
            }
        }


    }
}

