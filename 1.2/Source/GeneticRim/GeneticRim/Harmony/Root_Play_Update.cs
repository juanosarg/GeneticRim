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

    /*This Harmony postfix checks the Archotech Project countdown counter (why didn't Tynan do this with a tick counter??)
   * 
   */
    [HarmonyPatch(typeof(Root_Play))]
    [HarmonyPatch("Update")]

    static class Root_Play_Update_Patch
    {
        [HarmonyPostfix]

        public static void TickCountDown()
        {

            ArchotechCountdown.ShipCountdownUpdate();


        }
    }

}
