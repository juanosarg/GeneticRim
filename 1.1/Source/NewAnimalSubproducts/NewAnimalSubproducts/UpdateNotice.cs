using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using RimWorld.Planet;

namespace NewAnimalSubproducts
{
    public class UpdateNotice : MapComponent
    {

        public bool sentUpdateLetter = false;
       
        public UpdateNotice(Map map) : base(map)
        {

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<bool>(ref this.sentUpdateLetter, "sentUpdateLetter", false, true);


        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();
            if (!sentUpdateLetter)
            {
                if (!ModLister.HasActiveModWithName("Vanilla Textures Expanded - Genetic Rim"))
                {
                    Find.LetterStack.ReceiveLetter("GR_UpdateLetterLabel".Translate(), "GR_UpdateLetterText".Translate(), DefDatabase<LetterDef>.GetNamed("GR_UpdateLetter"));
                }
                sentUpdateLetter = true;
            }

           
        }


    }


}

