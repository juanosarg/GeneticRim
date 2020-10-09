using UnityEngine;
using System.Reflection;
using Verse;

namespace GeneticRim
{
    class Hediff_HumanHybrids : Hediff
    {

        public int tickCounter = 250;
        public const int tickCounterMax = 250;
        private PawnRenderer pawn_renderer;
      
        public override void Tick()
        {

            tickCounter++;
            if(tickCounter > tickCounterMax) {
                this.pawn_renderer = this.pawn.Drawer.renderer;
               
                LongEventHandler.ExecuteWhenFinished(delegate
                {
                    Vector2 vector = new Vector2(1, 1);
                    Graphic_Multi headGraphicNew = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>("Things/Pawn/Heads/Bear/Male_Average_Normal", ShaderDatabase.Cutout, vector, this.pawn_renderer.graphics.nakedGraphic.Color);
                    this.pawn_renderer.graphics.headGraphic = headGraphicNew;

                });
                tickCounter = 0;
            }
            base.Tick();

        }

    }
}
