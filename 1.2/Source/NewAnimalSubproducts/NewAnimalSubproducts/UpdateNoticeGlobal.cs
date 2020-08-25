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
    public class UpdateNoticeGlobal : GameComponent
    {

        public bool sentUpdateLetterGlobal = false;

        public UpdateNoticeGlobal(Game game) 
        {

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<bool>(ref this.sentUpdateLetterGlobal, "sentUpdateLetterGlobal", false, true);


        }

      


    }


}

