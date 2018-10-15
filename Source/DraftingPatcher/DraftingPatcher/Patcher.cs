using Harmony;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;


// So, let's comment this code, since it uses Harmony and has moderate complexity

namespace DraftingPatcher
{
    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = HarmonyInstance.Create("com.geneticrim");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

     
    }
    /*This first Harmony postfix deals with adding a Pawn_DraftController if it detects the creature
     * belongs to the player and to the custom class CompDraftable. It also adds a Pawn_EquipmentTracker
     * because some ugly errors are produced otherwise, though it is basically unused
     * 
     */
    [HarmonyPatch(typeof(PawnComponentsUtility))]
    [HarmonyPatch("AddAndRemoveDynamicComponents")]
    public static class PawnComponentsUtility_AddAndRemoveDynamicComponents_Patch
    {
        [HarmonyPostfix]
        static void AddDraftability(Pawn pawn)   
        {           
            //These two flags detect if the creature is part of the colony and if it has the custom class
            bool flagIsCreatureMine = pawn.Faction != null && pawn.Faction.IsPlayer;
            bool flagIsCreatureDraftable = (pawn.TryGetComp<CompDraftable>() != null);

                 
            if (flagIsCreatureMine && flagIsCreatureDraftable)
            {
                //Log.Message("Patching "+ pawn.kindDef.ToString() + " with a draft controller and equipment tracker");
                //If everything goes well, add drafter and equipment to the pawn 
                pawn.drafter = new Pawn_DraftController(pawn);
                pawn.equipment = new Pawn_EquipmentTracker(pawn);
            }
        }
    }

    /*This second Harmony postfix adds or removes gizmos from the pawn's gizmo list (which is actually IEnumerable)
     * 
     */
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("GetGizmos")]

    static class Pawn_GetGizmos_Patch
    {
        [HarmonyPostfix]

        public static void AddGizmo(Pawn __instance, ref IEnumerable<Gizmo> __result)
        {
            //I want access to the pawn object, and want to modify the original method's result
            var pawn = __instance;
            var gizmos = __result.ToList();
            // First two flags detect if the pawn is mine, and if it is 
           
            bool flagIsCreatureMine = pawn.Faction != null && pawn.Faction.IsPlayer;
            bool flagIsCreatureDraftable = (pawn.TryGetComp<CompDraftable>() != null);
            /* I check these flags only inside flagIsCreatureDraftable true to avoid errors due to pawn.TryGetComp<CompDraftable>() being null in most pawns. The code inside
             * the conditional only executes if it isn't*/
            bool flagIsCreatureRageable = false;
            bool flagIsCreatureExplodable = false;
            bool flagIsCreatureChickenRimPox = false;
            bool flagCanCreatureCarryMore = false;
            bool flagCanCreatureAdrenalineBurst = false;
            bool flagCanCanDoInsectClouds = false;
            bool flagCanStampede = false;
            bool flagCanDoPoisonousCloud = false;
            bool flagCanBurrow = false;
            bool flagCanStamina = false;
            bool flagCanHorrorize = false;
            bool flagCanMechaBlast = false;




            bool flagIsMindControlBuildingPresent = false;

            if (flagIsCreatureDraftable)
            {
                /*Inside here, I check if the Building is present in the map. I only want to do the check for 
                 *hybrids, or it will do this iterator for every creature in the map 
                 */
                foreach (Thing t in pawn.Map.listerThings.ThingsOfDef(ThingDef.Named("GR_AnimalControlHub")))
                {
                    Thing mindcontrolhub = t as Thing;
                    if (t != null)
                    {
                        flagIsCreatureRageable = pawn.TryGetComp<CompDraftable>().GetRage;
                        flagIsCreatureExplodable = pawn.TryGetComp<CompDraftable>().GetExplodable;
                        flagIsCreatureChickenRimPox = pawn.TryGetComp<CompDraftable>().GetChickenRimPox;
                        flagCanCreatureCarryMore = pawn.TryGetComp<CompDraftable>().GetCanCarryMore;
                        flagCanCreatureAdrenalineBurst = pawn.TryGetComp<CompDraftable>().GetAdrenalineBurst;
                        flagCanCanDoInsectClouds = pawn.TryGetComp<CompDraftable>().GetCanDoInsectClouds;
                        flagCanStampede = pawn.TryGetComp<CompDraftable>().GetCanStampede;
                        flagCanDoPoisonousCloud = pawn.TryGetComp<CompDraftable>().GetCanDoPoisonousCloud;
                        flagCanBurrow = pawn.TryGetComp<CompDraftable>().GetCanBurrow;
                        flagCanStamina = pawn.TryGetComp<CompDraftable>().HasDinoStamina;
                        flagCanHorrorize = pawn.TryGetComp<CompDraftable>().GetHorror;
                        flagCanMechaBlast = pawn.TryGetComp<CompDraftable>().GetMechablast;


                        flagIsMindControlBuildingPresent = true;
                    }
                }
            }

            
            /*Is the creature part of the colony, and draftable (the custom comp class)? Then display the drafting gizmo, called
             * Mind Control. It's action is just calling on toggle the Drafted method in the pawn's drafter, which
             * we initialized in the first Harmony Postfix
            */
            if ((pawn.drafter != null) && flagIsCreatureMine && flagIsCreatureDraftable && flagIsMindControlBuildingPresent)
            {
                Command_Action GR_Gizmo_MindControl = new Command_Action();
                GR_Gizmo_MindControl.action = delegate
                {
                    pawn.drafter.Drafted = !pawn.drafter.Drafted;
                };
                GR_Gizmo_MindControl.defaultLabel = "GR_CreatureMindControl".Translate();
                GR_Gizmo_MindControl.defaultDesc = "GR_CreatureMindControlDesc".Translate();
                GR_Gizmo_MindControl.icon = ContentFinder<Texture2D>.Get("ui/commands/GR_ControlAnimal", true);
                gizmos.Insert(0, GR_Gizmo_MindControl);
            }
            /*If the creature is draftable, drafted at the moment and the rage property (which is passed through XML and the custom comp class) is true,
             * we add a second gizmo, which copies the code from melee attacks, and thus allows targeting melee attacks
           */
            if ((pawn.drafter != null) && flagIsCreatureRageable && flagIsCreatureMine && pawn.drafter.Drafted && flagIsMindControlBuildingPresent)
            {
                Command_Target GR_Gizmo_AttackRage = new Command_Target();
                GR_Gizmo_AttackRage.defaultLabel = "GR_CreatureRageAttack".Translate();
                GR_Gizmo_AttackRage.defaultDesc = "GR_CreatureRageAttackDesc".Translate();
                GR_Gizmo_AttackRage.targetingParams = TargetingParameters.ForAttackAny();
                GR_Gizmo_AttackRage.icon = ContentFinder<Texture2D>.Get("Things/Item/AnimalPaws", true);

                GR_Gizmo_AttackRage.action = delegate (Thing target)
                {
                    IEnumerable<Pawn> enumerable = Find.Selector.SelectedObjects.Where(delegate (object x)
                    {
                        Pawn pawn2 = x as Pawn;
                        return pawn2 != null && pawn2.Drafted;
                    }).Cast<Pawn>();
                    foreach (Pawn current in enumerable)
                    {
                        Job job = new Job(JobDefOf.AttackMelee, target);
                        Pawn pawn2 = target as Pawn;
                        if (pawn2 != null)
                        {
                            job.killIncappedTarget = pawn2.Downed;
                        }
                        pawn.jobs.TryTakeOrderedJob(job, JobTag.Misc);
                    }
                };
                gizmos.Insert(1, GR_Gizmo_AttackRage);

            }
            /*This adds a gizmo that makes the creature attack once, and then cause a Hediff disease (GR_ChickenRimPox), then cancels the draft. I used a custom Jobdriver class for that
            */
            if ((pawn.drafter != null) && flagIsCreatureChickenRimPox && flagIsCreatureMine && pawn.drafter.Drafted && flagIsMindControlBuildingPresent)
            {
                Command_Target GR_Gizmo_AttackPox = new Command_Target();
                GR_Gizmo_AttackPox.defaultLabel = "GR_InflictChickenRimPox".Translate();
                GR_Gizmo_AttackPox.defaultDesc = "GR_InflictChickenRimPoxDesc".Translate();
                GR_Gizmo_AttackPox.targetingParams = TargetingParameters.ForAttackAny();
                GR_Gizmo_AttackPox.icon = ContentFinder<Texture2D>.Get("ui/commands/GR_ChickenRimPox", true);

                GR_Gizmo_AttackPox.action = delegate (Thing target)
                {
                    IEnumerable<Pawn> enumerable = Find.Selector.SelectedObjects.Where(delegate (object x)
                    {
                        Pawn pawn2 = x as Pawn;
                        return pawn2 != null && pawn2.Drafted;
                    }).Cast<Pawn>();
                    foreach (Pawn current in enumerable)
                    {
                        Job job = new Job(DefDatabase<JobDef>.GetNamed("GR_AttackMeleeOnceAndChickenRimPox", true), target);
                        Pawn pawn2 = target as Pawn;
                        if (pawn2 != null)
                        {
                            job.killIncappedTarget = pawn2.Downed;
                        }
                        pawn.jobs.TryTakeOrderedJob(job, JobTag.Misc);
                    }
                };
                gizmos.Insert(1, GR_Gizmo_AttackPox);

            }
            /*If the creature is explodable, we add this gizmo, which causes a Heddif called "sudden explosion" (GR_Kamikaze), and increases severity to
             * 1 to make the creature die. This only works if the creature also has DeathActionWorker.
           */
            if ((pawn.drafter != null) && flagIsCreatureExplodable && flagIsCreatureMine && flagIsMindControlBuildingPresent)
            {
                Command_Action GR_Gizmo_Detonate = new Command_Action();
                GR_Gizmo_Detonate.action = delegate
                {
                    pawn.health.AddHediff(HediffDef.Named("GR_Kamikaze"));
                    HealthUtility.AdjustSeverity(pawn, HediffDef.Named("GR_Kamikaze"), 1);
                };
                GR_Gizmo_Detonate.defaultLabel = "GR_DetonateChemfuel".Translate();
                GR_Gizmo_Detonate.defaultDesc = "GR_DetonateChemfuelDesc".Translate();
                GR_Gizmo_Detonate.icon = ContentFinder<Texture2D>.Get("UI/Commands/Detonate", true);
                gizmos.Insert(1, GR_Gizmo_Detonate);
            }
            /* This is a dummy gizmo. It only displays, but does nothing on click. The processing is done below in another Harmony patch to MassUtility.Capacity
             */
            if ((pawn.drafter != null) && flagCanCreatureCarryMore && flagIsCreatureMine && flagIsMindControlBuildingPresent)
            {
                Command_Action GR_Gizmo_Carry = new Command_Action();
                GR_Gizmo_Carry.action = delegate
                {
                   
                };
                GR_Gizmo_Carry.defaultLabel = "GR_CarryMore".Translate();
                GR_Gizmo_Carry.defaultDesc = "GR_CarryMoreDesc".Translate();
                GR_Gizmo_Carry.icon = ContentFinder<Texture2D>.Get("ui/commands/GR_IncreasedCarry", true);
                gizmos.Insert(1, GR_Gizmo_Carry);
            }
            /*This gizmo applies a Hediff that makes the pawn move faster for a while
            */
            if ((pawn.drafter != null) && flagCanCreatureAdrenalineBurst && flagIsCreatureMine  && flagIsMindControlBuildingPresent)
            {
                Command_Action GR_Gizmo_AdrenalineBurst = new Command_Action();
                GR_Gizmo_AdrenalineBurst.defaultLabel = "GR_StartAdrenalineBurst".Translate();
                GR_Gizmo_AdrenalineBurst.defaultDesc = "GR_StartAdrenalineBurstDesc".Translate();
                GR_Gizmo_AdrenalineBurst.icon = ContentFinder<Texture2D>.Get("ui/commands/GR_AdrenalineBurst", true);

                GR_Gizmo_AdrenalineBurst.action = delegate
                {
                    if (!pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_AdrenalineBurst"))) {
                        pawn.health.AddHediff(HediffDef.Named("GR_AdrenalineBurst"));
                    } else
                    {
                        Messages.Message("GR_AbilityRecharging".Translate(), pawn, MessageTypeDefOf.NeutralEvent);

                    }
                };
                gizmos.Insert(1, GR_Gizmo_AdrenalineBurst);

            }
            /*This gizmo applies a Hediff that makes the pawn release insect clouds for a while
            */
            if ((pawn.drafter != null) && flagCanCanDoInsectClouds && flagIsCreatureMine && flagIsMindControlBuildingPresent)
            {
                Command_Action GR_Gizmo_InsectClouds = new Command_Action();
                GR_Gizmo_InsectClouds.defaultLabel = "GR_ReleaseInsectClouds".Translate();
                GR_Gizmo_InsectClouds.defaultDesc = "GR_ReleaseInsectCloudsDesc".Translate();
                GR_Gizmo_InsectClouds.icon = ContentFinder<Texture2D>.Get("ui/commands/GR_Insectclouds", true);

                GR_Gizmo_InsectClouds.action = delegate
                {
                    if (!pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_InsectClouds")))

                    {
                        pawn.health.AddHediff(HediffDef.Named("GR_InsectClouds"));

                    } else
                    {
                        Messages.Message("GR_AbilityRecharging".Translate(), pawn, MessageTypeDefOf.NeutralEvent);
                    }

                };
                gizmos.Insert(1, GR_Gizmo_InsectClouds);

            }
            /*This gizmo applies a Hediff that makes the pawn generate stampede clouds for a while
            */
            if ((pawn.drafter != null) && flagCanStampede && flagIsCreatureMine && flagIsMindControlBuildingPresent)
            {
                Command_Action GR_Gizmo_Stampede = new Command_Action();
                GR_Gizmo_Stampede.defaultLabel = "GR_StartStampede".Translate();
                GR_Gizmo_Stampede.defaultDesc = "GR_StartStampedeDesc".Translate();
                GR_Gizmo_Stampede.icon = ContentFinder<Texture2D>.Get("ui/commands/GR_StampedeClouds", true);

                GR_Gizmo_Stampede.action = delegate
                {
                    if (!pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_Stampeding")))
                    {
                        pawn.health.AddHediff(HediffDef.Named("GR_Stampeding"));

                    } else
                    {
                        Messages.Message("GR_AbilityRecharging".Translate(), pawn, MessageTypeDefOf.NeutralEvent);
                    }
                };
                gizmos.Insert(1, GR_Gizmo_Stampede);

            }

            /*This gizmo adds an attack that spawns a poison cloud at a target's location
           */
            if ((pawn.drafter != null) && flagCanDoPoisonousCloud && flagIsCreatureMine && flagIsMindControlBuildingPresent)
            {
                Command_Target GR_Gizmo_PoisonCloud = new Command_Target();
                GR_Gizmo_PoisonCloud.defaultLabel = "GR_CreatePoisonousCloud".Translate();
                GR_Gizmo_PoisonCloud.defaultDesc = "GR_CreatePoisonousCloudDesc".Translate();
                GR_Gizmo_PoisonCloud.targetingParams = TargetingParameters.ForAttackAny();
                GR_Gizmo_PoisonCloud.icon = ContentFinder<Texture2D>.Get("ui/commands/GR_PoisonousCloud", true);

                GR_Gizmo_PoisonCloud.action = delegate (Thing target)
                {

                    if (!pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_CausedPoisonCloud")))
                    {
                        Pawn pawn2 = target as Pawn;
                        if (pawn2 != null)
                        {

                            if (pawn.Position.InHorDistOf(pawn2.Position, 10))
                            {
                                List<IntVec3> list = GenAdj.AdjacentCells8WayRandomized();
                                for (int i = 0; i < 8; i++)
                                {
                                    IntVec3 c2 = pawn2.Position + list[i];
                                    if (c2.InBounds(pawn2.Map))
                                    {
                                        Thing thing = ThingMaker.MakeThing(ThingDef.Named("GR_Poison_Cloud"), null);

                                        GenSpawn.Spawn(thing, c2, pawn2.Map);
                                    }
                                }
                                pawn.health.AddHediff(HediffDef.Named("GR_CausedPoisonCloud"));
                            }
                            else
                            {
                                Messages.Message("GR_PoisonCloudRange".Translate(), pawn, MessageTypeDefOf.NeutralEvent);

                            }

                        }
                    } else
                    {
                        Messages.Message("GR_AbilityRecharging".Translate(), pawn, MessageTypeDefOf.NeutralEvent);
                    }

                };
                gizmos.Insert(1, GR_Gizmo_PoisonCloud);

            }
            /*This gizmo puts the creature into burrowing mode
           */
            if ((pawn.drafter != null) && flagCanBurrow && flagIsCreatureMine && flagIsMindControlBuildingPresent)
            {
                Command_Action GR_Gizmo_Burrowing = new Command_Action();
                GR_Gizmo_Burrowing.action = delegate
                {
                    if (!pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_Burrowing")))
                    {
                        pawn.health.AddHediff(HediffDef.Named("GR_Burrowing"));
                    } else
                    {
                        Messages.Message("GR_AbilityRecharging".Translate(), pawn, MessageTypeDefOf.NeutralEvent);
                    }
                };
                GR_Gizmo_Burrowing.defaultLabel = "GR_StartBurrowing".Translate();
                GR_Gizmo_Burrowing.defaultDesc = "GR_StartBurrowingDesc".Translate();
                GR_Gizmo_Burrowing.icon = ContentFinder<Texture2D>.Get("Things/Pawn/Animal/Special/GR_Special_Burrowing", true);
                gizmos.Insert(1, GR_Gizmo_Burrowing);
            }

            /*This gizmo makes the animal more resistant for a while
           */
            if ((pawn.drafter != null) && flagCanStamina && flagIsCreatureMine && flagIsMindControlBuildingPresent)
            {
                Command_Action GR_Gizmo_Stamina = new Command_Action();
                GR_Gizmo_Stamina.action = delegate
                {
                    if (!pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_Stamina")))
                    {
                        pawn.health.AddHediff(HediffDef.Named("GR_Stamina"));
                    } else
                    {
                        Messages.Message("GR_AbilityRecharging".Translate(), pawn, MessageTypeDefOf.NeutralEvent);
                    }
                };
                GR_Gizmo_Stamina.defaultLabel = "GR_StartStamina".Translate();
                GR_Gizmo_Stamina.defaultDesc = "GR_StartStaminaDesc".Translate();
                GR_Gizmo_Stamina.icon = ContentFinder<Texture2D>.Get("ui/commands/GR_Stamina", true);
                gizmos.Insert(1, GR_Gizmo_Stamina);
            }

            /*This gizmo makes the animal cast a horror ability
          */
            if ((pawn.drafter != null) && flagCanHorrorize && flagIsCreatureMine && flagIsMindControlBuildingPresent)
            {
                Command_Target GR_Gizmo_Horror = new Command_Target();
                GR_Gizmo_Horror.defaultLabel = "GR_StartInvokingInsanity".Translate();
                GR_Gizmo_Horror.defaultDesc = "GR_StartInvokingInsanityDesc".Translate();
                GR_Gizmo_Horror.targetingParams = TargetingParameters.ForAttackAny();
                GR_Gizmo_Horror.icon = ContentFinder<Texture2D>.Get("Item/Weapon/MiGoCasterWeapon/MiGoCasterWeaponA", true);

                GR_Gizmo_Horror.action = delegate (Thing target)
                {
                    if (!pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_CausedHorror")))
                    {
                        Pawn pawn2 = target as Pawn;
                        if (pawn2 != null)
                        {

                            if (pawn.Position.InHorDistOf(pawn2.Position, 10))
                            {
                                List<IntVec3> list = GenAdj.AdjacentCells8WayRandomized();
                                for (int i = 0; i < 8; i++)
                                {
                                    IntVec3 c2 = pawn2.Position + list[i];
                                    if (c2.InBounds(pawn2.Map))
                                    {
                                        Thing thing = ThingMaker.MakeThing(ThingDef.Named("GR_Insanity_Cloud"), null);

                                        GenSpawn.Spawn(thing, c2, pawn2.Map);
                                    }
                                }
                                pawn.health.AddHediff(HediffDef.Named("GR_CausedHorror"));
                            }
                            else
                            {
                                Messages.Message("GR_InvokingInsanityRange".Translate(), pawn, MessageTypeDefOf.NeutralEvent);

                            }

                        }
                    }
                    else
                    {
                        Messages.Message("GR_AbilityRecharging".Translate(), pawn, MessageTypeDefOf.NeutralEvent);
                    }
                };
                gizmos.Insert(1, GR_Gizmo_Horror);
            }

            /*This gizmo makes the animal release a burning explosion
          */
            if ((pawn.drafter != null) && flagCanMechaBlast && flagIsCreatureMine && flagIsMindControlBuildingPresent)
            {
                Command_Action GR_Gizmo_MechaBlast = new Command_Action();
                GR_Gizmo_MechaBlast.action = delegate
                {
                    if (!pawn.health.hediffSet.HasHediff(HediffDef.Named("GR_VentedExhaust")))
                    {

                        foreach (IntVec3 c in GenAdj.CellsAdjacent8Way(pawn))
                        {
                            GenExplosion.DoExplosion(c, pawn.Map, (float)0.25, DamageDefOf.Flame, pawn, 25, 5, null, null, null, null, ThingDef.Named("Filth_Ash"), .7f, 1, false, null, 0f, 1);
                        }
                       

                        pawn.health.AddHediff(HediffDef.Named("GR_VentedExhaust"));
                    }
                    else
                    {
                        Messages.Message("GR_AbilityRecharging".Translate(), pawn, MessageTypeDefOf.NeutralEvent);
                    }
                };
                GR_Gizmo_MechaBlast.defaultLabel = "GR_StartMechaBlast".Translate();
                GR_Gizmo_MechaBlast.defaultDesc = "GR_StartMechaBlastDesc".Translate();
                GR_Gizmo_MechaBlast.icon = ContentFinder<Texture2D>.Get("ui/commands/GR_MechaBlast", true);
                gizmos.Insert(1, GR_Gizmo_MechaBlast);
            }



            __result = gizmos;
        }


        


         
    }

    /*This Harmony Postfix makes the creature respond to clicks on the map screen, so it can be controlled
     */
    [HarmonyPatch(typeof(FloatMenuMakerMap))]
    [HarmonyPatch("CanTakeOrder")]
    public static class FloatMenuMakerMap_CanTakeOrder_Patch
    {
        [HarmonyPostfix]
        public static void MakePawnControllable(Pawn pawn, ref bool __result)

        {
            bool flagIsCreatureMine = pawn.Faction != null && pawn.Faction.IsPlayer;
            bool flagIsCreatureDraftable = (pawn.TryGetComp<CompDraftable>() != null);

            if (flagIsCreatureDraftable && flagIsCreatureMine)
            {
                //Log.Message("You should be controllable now");
                __result = true;
            }
            
        }
    }

    /*This Harmony Prefix makes the creature carry more weight
    */
    [HarmonyPatch(typeof(MassUtility))]
    [HarmonyPatch("Capacity")]
    public static class MassUtility_Capacity_Patch
    {
        [HarmonyPostfix]
        public static void MakeThemCarryMore(Pawn p, ref float __result)

        {
            bool flagIsCreatureMine = p.Faction != null && p.Faction.IsPlayer;
            bool flagIsCreatureDraftable = (p.TryGetComp<CompDraftable>() != null);
            bool flagCanCreatureCarryMore = false;

            if (flagIsCreatureDraftable)
            {

                List<Map> maps = Find.Maps;
                
                for (int i = 0; i < maps.Count; i++)
                {
                    if (maps[i].IsPlayerHome)
                    {
                        foreach (Thing t in maps[i].listerThings.ThingsOfDef(ThingDef.Named("GR_AnimalControlHub")))
                        {
                            Thing mindcontrolhub = t as Thing;
                            if (t != null)
                            {
                                flagCanCreatureCarryMore = p.TryGetComp<CompDraftable>().GetCanCarryMore;
                                //if (flagCanCreatureCarryMore) { Log.Message("Creature " + p.kindDef.ToString() + " should carry more now"); }
                            }


                        }
                    }
                }
               

            }

            if (flagIsCreatureDraftable && flagIsCreatureMine && flagCanCreatureCarryMore)
            {
                __result = p.BodySize * 50f;
            }

        }
    }









}
