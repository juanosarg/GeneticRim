using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;
using RimWorld.Planet;



// So, let's comment this code, since it uses Harmony and has moderate complexity

namespace GeneticRim
{

    /*This Harmony Prefix makes the creature carry more weight
*/
    [HarmonyPatch(typeof(MassUtility))]
    [HarmonyPatch("Capacity")]
    public static class GeneticRim_MassUtility_Capacity_Patch
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
