using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;
using RimWorld;

namespace GeneticRim
{
    public class Building_ArchotechPlatform : Building
    {
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
        }
    }
}