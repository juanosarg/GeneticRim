
using UnityEngine;
using RimWorld;
using Verse;


namespace DraftingPatcher
{
    public class Hediff_Burrowing : HediffWithComps
    {
        private bool firstTick = true;
        private bool lastTick = true;
       

        Graphic storeGraphic;
       

        public override void Tick()
        {
            base.Tick();

            if (firstTick)
            {
                storeGraphic = this.pawn.Drawer.renderer.graphics.nakedGraphic;
                Graphic newGraphic = GraphicDatabase.Get<Graphic_Single>("Things/Pawn/Animal/Special/GR_Special_Burrowing", ShaderDatabase.Cutout, pawn.Drawer.renderer.graphics.nakedGraphic.drawSize, Color.white);
               
                pawn.Drawer.renderer.graphics.nakedGraphic = newGraphic;
               
                firstTick = false;
            }
           

            if ((this.Severity >= 1)&& lastTick)
            {
                pawn.Drawer.renderer.graphics.nakedGraphic = storeGraphic;
                lastTick = false;
            }


        }


    }
}