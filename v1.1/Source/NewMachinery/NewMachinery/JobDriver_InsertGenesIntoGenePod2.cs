using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;
using Verse.AI;

namespace NewMachinery
{
    public class JobDriver_InsertGenesIntoGenePod2 : JobDriver
    {
        private const TargetIndex BarrelInd = TargetIndex.A;


        private Random rand = new Random();

        private const int Duration = 200;



        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return this.pawn.Reserve(this.job.GetTarget(TargetIndex.A).Thing, this.job, 1, -1, null) && this.pawn.Reserve(this.job.targetB, this.job, 1, -1, null);
        }

      

        [DebuggerHidden]
        protected override IEnumerable<Toil> MakeNewToils()
        {

            Toil reserveGenes = Toils_Reserve.Reserve(TargetIndex.B, 1, -1, null);

            yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.ClosestTouch).FailOnDespawnedNullOrForbidden(TargetIndex.B).FailOnSomeonePhysicallyInteracting(TargetIndex.B);
            yield return Toils_Haul.StartCarryThing(TargetIndex.B, false, true, false).FailOnDestroyedNullOrForbidden(TargetIndex.B);
            yield return Toils_Haul.CheckForGetOpportunityDuplicate(reserveGenes, TargetIndex.B, TargetIndex.None, true, null);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            yield return Toils_General.Wait(240).FailOnDestroyedNullOrForbidden(TargetIndex.B).FailOnDestroyedNullOrForbidden(TargetIndex.A).FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch).WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
            yield return new Toil
            {
                initAction = delegate
                {
                    Building_NewGenePod building_genepod = (Building_NewGenePod)this.job.GetTarget(TargetIndex.A).Thing;
                    building_genepod.TryAcceptThing2(this.job.targetB.Thing, true);
                    building_genepod.PodHasGenes2 = true;
                    building_genepod.SignalInsertGenes2 = false;

                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };

          
         
        }
    }
}
