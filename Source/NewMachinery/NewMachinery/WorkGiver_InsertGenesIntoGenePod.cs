using System;
using Verse;
using Verse.AI;
using RimWorld;

namespace NewMachinery
{
    public class WorkGiver_InsertGenesIntoGenePod : WorkGiver_Scanner
    {

        Building_NewGenePod building_genepod;

        public override ThingRequest PotentialWorkThingRequest
        {
            get
            {
                return ThingRequest.ForDef(DefDatabase<ThingDef>.GetNamed("GR_GenePod", true));
            }
        }

        public override PathEndMode PathEndMode
        {
            get
            {
                return PathEndMode.Touch;
            }
        }

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            building_genepod = t as Building_NewGenePod;
            bool result;
            if (building_genepod == null || !building_genepod.SignalInsertGenes1)
            {
                result = false;
            }
            else if (t.IsBurning())
            {
                result = false;
            }
            else
            {
                if (!t.IsForbidden(pawn))
                {
                    LocalTargetInfo target = t;
                    LocalTargetInfo target2 = building_genepod.typeOfGenesToInsert1;

                    if (pawn.CanReserve(target, 1, -1, null, forced))
                    {
                        if (pawn.CanReserve(target2, 1, -1, null, forced))
                        {
                            result = true;
                            return result;
                        }
                    }
                }
                result = false;
            }
            return result;
        }

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            LocalTargetInfo target2 = building_genepod.typeOfGenesToInsert1;
            if (pawn.CanReserve(target2, 1, -1, null, forced))
            {
                Job job = new Job(DefDatabase<JobDef>.GetNamed("GR_InsertGenesIntoGenePodJob", true), t, building_genepod.typeOfGenesToInsert1);
            job.count = 1;
            return job;
            }
            else return null;
        }
    }
}
