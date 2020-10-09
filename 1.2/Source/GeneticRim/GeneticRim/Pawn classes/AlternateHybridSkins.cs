using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using Verse;


namespace GeneticRim
{
    class AlternateHybridSkins : Pawn
    {
        private PawnRenderer pawn_renderer;

        public string main_graphic;

      

        // AnimalVariations.AnimalMultiSkins
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            if (!LoadedModManager.GetMod<GeneticRim_Mod>().GetSettings<GeneticRim_Settings>().useLeglessGraphics) {

                this.ChangeTheGraphics();
                
            }

        }

        public override void TickRare()
        {
            if (!LoadedModManager.GetMod<GeneticRim_Mod>().GetSettings<GeneticRim_Settings>().useLeglessGraphics)
            {

                this.ChangeTheGraphics();

            }else if (LoadedModManager.GetMod<GeneticRim_Mod>().GetSettings<GeneticRim_Settings>().useLeglessGraphics)
            {

                this.RestoreTheGraphics();

            }
            base.TickRare();
        }

        // AnimalVariations.AnimalMultiSkins
        public override void Tick()
        {
            if (Find.TickManager.TicksGame % 250 == 0)
            {
                this.TickRare();
            }
           
            base.Tick();
        }

        public void ChangeTheGraphics()
        {
            this.main_graphic = this.ageTracker.CurKindLifeStage.bodyGraphicData.texPath;
            this.pawn_renderer = ((Pawn_DrawTracker)typeof(Pawn).GetField("drawer", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this)).renderer;


            LongEventHandler.ExecuteWhenFinished(delegate
            {
                Vector2 vector = this.ageTracker.CurKindLifeStage.bodyGraphicData.drawSize;
                //Log.Message(this.ageTracker.CurKindLifeStage.bodyGraphicData.texPath + "Alternate");
                Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(this.ageTracker.CurKindLifeStage.bodyGraphicData.texPath + "Alternate", ShaderDatabase.Cutout, vector, Color.white);
                this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;
                (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = this.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;
            });

        }

        public void RestoreTheGraphics()
        {
            this.main_graphic = this.ageTracker.CurKindLifeStage.bodyGraphicData.texPath;
            this.pawn_renderer = ((Pawn_DrawTracker)typeof(Pawn).GetField("drawer", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this)).renderer;


            LongEventHandler.ExecuteWhenFinished(delegate
            {
                Vector2 vector = this.ageTracker.CurKindLifeStage.bodyGraphicData.drawSize;
                //Log.Message(this.ageTracker.CurKindLifeStage.bodyGraphicData.texPath + "Alternate");
                Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(this.ageTracker.CurKindLifeStage.bodyGraphicData.texPath, ShaderDatabase.Cutout, vector, Color.white);
                this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;
                (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = this.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;
            });

        }




    }
}
