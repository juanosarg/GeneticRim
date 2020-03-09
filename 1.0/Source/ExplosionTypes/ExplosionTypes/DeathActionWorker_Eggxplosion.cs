using RimWorld;
using Verse;
using System;
using System.Threading;




namespace ExplosionTypes
{
    public class DeathActionWorker_Eggxplosion : DeathActionWorker
    {


        private Random rand = new Random(123);

        public Random Rand { get => rand; set => rand = value; }
        

        public override void PawnDied(Corpse corpse)
        {
            float radius;
            

            if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 0)
            {
                radius = 1.9f;
            }
            else if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 1)
            {
                radius = 2.9f;
            }
            else
            {
                radius = 4.9f;
            }
            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, DamageDefOf.Flame, corpse.InnerPawn, -1,-1,null, null, null, null, null, 0f, 1, false, null, 0f, 1);

            int randomNumber = Rand.Next(1,4);
                  
            if (randomNumber == 3)
            { 
                GenSpawn.Spawn(ThingDef.Named("GR_EggBomb"), (corpse.Position + IntVec3.FromString("0,0,1")), corpse.Map);
                Thread.Sleep(30);
                GenSpawn.Spawn(ThingDef.Named("GR_EggBomb"), (corpse.Position + IntVec3.FromString("1,0,0")), corpse.Map);
                Thread.Sleep(30);
                GenSpawn.Spawn(ThingDef.Named("GR_EggBomb"), (corpse.Position + IntVec3.FromString("2,0,2")), corpse.Map);
            }
            else if (randomNumber == 2)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_EggBomb"), (corpse.Position + IntVec3.FromString("0,0,1")), corpse.Map);
                Thread.Sleep(30);
                GenSpawn.Spawn(ThingDef.Named("GR_EggBomb"), (corpse.Position + IntVec3.FromString("1,0,0")), corpse.Map);
            }
            else
            {
                GenSpawn.Spawn(ThingDef.Named("GR_EggBomb"), (corpse.Position + IntVec3.FromString("0,0,1")), corpse.Map);
            }

        }

    }


}
