
using RimWorld;
using Verse;

namespace ExplosionTypes
{
    public class DeathActionWorker_BiggerExplosion : DeathActionWorker
    {


      
            public override void PawnDied(Corpse corpse)
            {
                float radius;
                if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 0)
                {
                    radius = 2.5f;
                }
                else if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 1)
                {
                    radius = 4.0f;
                }
                else
                {
                    radius = 6.9f;
                }
                GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, DamageDefOf.Flame, corpse.InnerPawn, -1,-1,null, null, null, null,null, 0f, 1, false, null, 0f, 1);
            }
        
    
    }
}
