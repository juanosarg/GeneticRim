using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;
using Verse.AI;

namespace DraftingPatcher
{
    public class JobDriver_AttackMeleeOnceAndChickenRimPox : JobDriver
    {
        private int numMeleeAttacksMade;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.numMeleeAttacksMade, "numMeleeAttacksMade", 0, false);
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            IAttackTarget attackTarget = this.job.targetA.Thing as IAttackTarget;
            if (attackTarget != null)
            {
                this.pawn.Map.attackTargetReservationManager.Reserve(this.pawn, this.job, attackTarget);
            }
            return true;
        }

        [DebuggerHidden]
        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_Misc.ThrowColonistAttackingMote(TargetIndex.A);
            yield return Toils_Combat.FollowAndMeleeAttack(TargetIndex.A, delegate
            {
            Thing thing = this.job.GetTarget(TargetIndex.A).Thing;
            if (this.pawn.meleeVerbs.TryMeleeAttack(thing, this.job.verbToUse, false))

                {
                if (this.pawn.CurJob == null || this.pawn.jobs.curDriver != this)
					{
                    return;
                }
                this.numMeleeAttacksMade++;
                    if (thing.GetType()==typeof(Pawn)) {
                        Pawn targetPawn = thing as Pawn;
                        targetPawn.health.AddHediff(HediffDef.Named("GR_ChickenRimPox"));
                        HealthUtility.AdjustSeverity(targetPawn, HediffDef.Named("GR_ChickenRimPox"), 0.3f);
                    }
                   
                    if (this.numMeleeAttacksMade >= 1)
					{
                    this.EndJobWith(JobCondition.Succeeded);
                        pawn.drafter.Drafted = false;

                        return;
                }
            }
        }).FailOnDespawnedOrNull(TargetIndex.A);
    }

    public override void Notify_PatherFailed()
    {
        if (this.job.attackDoorIfTargetLost)
        {
            Thing thing;
            using (PawnPath pawnPath = base.Map.pathFinder.FindPath(this.pawn.Position, base.TargetA.Cell, TraverseParms.For(this.pawn, Danger.Deadly, TraverseMode.PassDoors, false), PathEndMode.OnCell))
            {
                if (!pawnPath.Found)
                {
                    return;
                }
                IntVec3 intVec;
                thing = pawnPath.FirstBlockingBuilding(out intVec, this.pawn);
            }
            if (thing != null)
            {
                this.job.targetA = thing;
                this.job.maxNumMeleeAttacks = Rand.RangeInclusive(2, 5);
                this.job.expiryInterval = Rand.Range(2000, 4000);
                return;
            }
        }
        base.Notify_PatherFailed();
    }

    public override bool IsContinuation(Job j)
    {
        return this.job.GetTarget(TargetIndex.A) == j.GetTarget(TargetIndex.A);
    }
}
}