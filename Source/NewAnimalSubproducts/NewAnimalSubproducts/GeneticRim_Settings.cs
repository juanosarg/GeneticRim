using RimWorld;
using UnityEngine;
using Verse;


namespace NewAnimalSubproducts
{
    class GeneticRim_Settings : ModSettings

    {


        public bool useLeglessGraphics = true;
     


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.useLeglessGraphics, "useLeglessGraphics", true);
         


        }


    }
    class GeneticRim_Mod : Mod
    {
        public static GeneticRim_Settings settings;
        public GeneticRim_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<GeneticRim_Settings>();
        }
        public override string SettingsCategory() => "Genetic Rim";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();
            ls.Begin(inRect);
            ls.Gap(12f);
            ls.CheckboxLabeled("GR_useLeglessGraphics".Translate(), ref settings.useLeglessGraphics, null);
            ls.Gap(12f);
            settings.Write();
            ls.End();


        }
    }
}
