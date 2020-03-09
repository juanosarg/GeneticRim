
using RimWorld;
using Verse;


namespace ExplosionTypes
{
    public class DeathActionWorker_FrostExplosion : DeathActionWorker
    {


      
            public override void PawnDied(Corpse corpse)
            {
                float radius;
                if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 0)
                {
                    radius = 3.9f;
                }
                else if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 1)
                {
                    radius = 5.9f;
                }
                else
                {
                    radius = 7.9f;
                }

                
               
            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, DamageDefOf.Frostbite, corpse.InnerPawn, 10,-1,SoundDef.Named("Explosion_Bomb"), null, null, null, ThingDef.Named("GR_Gas_Ice"), 1f, 1, false, null, 0f, 1);
            }
        
    
    }
}
