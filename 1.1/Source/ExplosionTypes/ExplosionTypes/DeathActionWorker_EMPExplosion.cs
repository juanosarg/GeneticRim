using RimWorld;
using Verse;

namespace ExplosionTypes
{
    public class DeathActionWorker_EMPExplosion : DeathActionWorker
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
                radius = 4.9f;
            }
            else
            {
                radius = 6.9f;
            }
            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, DamageDefOf.EMP, corpse.InnerPawn, -1,-1,null, null, null, null, ThingDef.Named("Gas_Smoke"), .7f, 1, false, null, 0f, 1);
        }


    }
}