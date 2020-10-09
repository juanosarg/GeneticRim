using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Verse;
using RimWorld;

namespace GeneticRim
{
    public class Building_ArchotechComputerCore : Building
    {
        private bool CanLaunchNow
        {
            get
            {
                return !ArchotechUtility.LaunchFailReasons(this).Any<string>();
            }
        }

        [DebuggerHidden]
        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo c in base.GetGizmos())
            {
                yield return c;
            }
            foreach (Gizmo c2 in ArchotechUtility.ShipStartupGizmos(this))
            {
                yield return c2;
            }
            Command_Action launch = new Command_Action();
            launch.action = new Action(this.TryLaunch);
            launch.defaultLabel = "GR_ArchotechProjectLaunch".Translate();
            launch.defaultDesc = "GR_ArchotechProjectLaunchDesc".Translate();
            if (!this.CanLaunchNow)
            {
                launch.Disable(ArchotechUtility.LaunchFailReasons(this).First<string>());
            }
            if (ShipCountdown.CountingDown)
            {
                launch.Disable(null);
            }
            launch.hotKey = KeyBindingDefOf.Misc1;
            launch.icon = ContentFinder<Texture2D>.Get("ui/commands/GR_AwakenArchotech", true);
            yield return launch;
        }

        private void TryLaunch()
        {
            if (this.CanLaunchNow)
            {
                ArchotechCountdown.InitiateCountdown(this);
            }
        }
    }
}