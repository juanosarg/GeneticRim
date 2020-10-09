using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;

namespace GeneticRim
{
    public class CompArchotechPart : ThingComp
    {
        [DebuggerHidden]
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            yield return new Command_Action
            {
                action = new Action(this.ShowReport),
                defaultLabel = "GR_CommandArchotechReport".Translate(),
                defaultDesc = "GR_CommandArchotechReportDesc".Translate(),
                hotKey = KeyBindingDefOf.Misc4,
                icon = ContentFinder<Texture2D>.Get("ui/commands/GR_ProjectDossier", true)
            };
        }

        public void ShowReport()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!ArchotechUtility.LaunchFailReasons((Building)this.parent).Any<string>())
            {
                stringBuilder.AppendLine("GR_ArchotechReportCanLaunch".Translate());
            }
            else
            {
                stringBuilder.AppendLine("GR_ArchotechReportCannotLaunch".Translate());
                foreach (string current in ArchotechUtility.LaunchFailReasons((Building)this.parent))
                {
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine(current);
                }
            }
            Dialog_MessageBox window = new Dialog_MessageBox(stringBuilder.ToString(), null, null, null, null, null, false, null, null);
            Find.WindowStack.Add(window);
        }
    }
}
