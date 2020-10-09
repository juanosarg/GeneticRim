
using RimWorld;
using Verse;

namespace GeneticRim
{
    class DeathActionWorker_HairballExplosion : DeathActionWorker
    {



        public override void PawnDied(Corpse corpse)
        {
            float radius;
            if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 0)
            {
                radius = 1.9f;
            }
            else if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 1)
            {
                radius = 3.9f;
            }
            else
            {
                radius = 5.9f;
            }



            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, Named("GR_HairballExplosion"), corpse.InnerPawn, 1, -1, SoundDef.Named("Explosion_Bomb"), null, null, null, ThingDef.Named("GR_Hairballs"), 1f, 1, false, null, 0f, 1);
        }

        public static DamageDef Named(string defName)
        {
            return DefDatabase<DamageDef>.GetNamed(defName, true);
        }


    }
}