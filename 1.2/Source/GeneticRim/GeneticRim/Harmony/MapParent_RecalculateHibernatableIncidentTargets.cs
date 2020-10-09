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

    /*This Harmony postfix tries to add an endgame event. Specifically, it adds GR_ArchotechPlatform to a list of hibernatableIncidentTargets to increase raid commonality
    * 
    */
    [HarmonyPatch(typeof(MapParent))]
    [HarmonyPatch("RecalculateHibernatableIncidentTargets")]

    static class GeneticRim_MapParent_RecalculateHibernatableIncidentTargets_Patch
    {
        [HarmonyPostfix]

        public static void AddEndParts(ref MapParent __instance)
        {
            HashSet<IncidentTargetTagDef> theListToChange;
            theListToChange = (HashSet<IncidentTargetTagDef>)typeof(MapParent).GetField("hibernatableIncidentTargets", AccessTools.all).GetValue(__instance);
            //typeof(MapParent).GetField("hibernatableIncidentTargets", AccessTools.all).SetValue(__instance,null);
            //__instance.hibernatableIncidentTargets = null;
            foreach (ThingWithComps current in __instance.Map.listerThings.ThingsOfDef(ThingDef.Named("GR_ArchotechPlatform")).OfType<ThingWithComps>())
            {
                CompHibernatable compHibernatable = current.TryGetComp<CompHibernatable>();
                if (compHibernatable != null && compHibernatable.State == HibernatableStateDefOf.Starting && compHibernatable.Props.incidentTargetWhileStarting != null)
                {
                    if (theListToChange == null)
                    {
                        typeof(MapParent).GetField("hibernatableIncidentTargets", AccessTools.all).SetValue(__instance, new HashSet<IncidentTargetTagDef>());

                        // this.hibernatableIncidentTargets = new HashSet<IncidentTargetTagDef>();
                    }
                    theListToChange = (HashSet<IncidentTargetTagDef>)typeof(MapParent).GetField("hibernatableIncidentTargets", AccessTools.all).GetValue(__instance);
                    theListToChange.Add(compHibernatable.Props.incidentTargetWhileStarting);
                    typeof(MapParent).GetField("hibernatableIncidentTargets", AccessTools.all).SetValue(__instance, theListToChange);
                    Log.Message("Genetic Rim is starting the Archotech Project. Prepare for raids!");

                    // this.hibernatableIncidentTargets.Add(compHibernatable.Props.incidentTargetWhileStarting);
                }
            }
        }
    }


}
