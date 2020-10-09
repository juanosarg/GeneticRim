using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Verse;
using RimWorld;

namespace GeneticRim
{
    public static class ArchotechUtility
    {
        private static Dictionary<ThingDef, int> requiredParts;

        private static List<Building> closedSet = new List<Building>();

        private static List<Building> openSet = new List<Building>();

        public static Dictionary<ThingDef, int> RequiredParts()
        {
            if (ArchotechUtility.requiredParts == null)
            {
                ArchotechUtility.requiredParts = new Dictionary<ThingDef, int>();
                ArchotechUtility.requiredParts[ThingDef.Named("GR_ArchotechPlatform")] = 1;
                ArchotechUtility.requiredParts[ThingDef.Named("GR_TurboChargedCryostabilizedEngine")] = 3;
                ArchotechUtility.requiredParts[ThingDef.Named("GR_RefrigeratedCoils")] = 1;
                ArchotechUtility.requiredParts[ThingDef.Named("GR_ArchotechDatabase")] = 1;
                ArchotechUtility.requiredParts[ThingDef.Named("GR_GeneticRepository")] = 5;
                ArchotechUtility.requiredParts[ThingDef.Named("GR_MechanoidTinkeringTable")] = 1;



            }
            return ArchotechUtility.requiredParts;
        }

        [DebuggerHidden]
        public static IEnumerable<string> LaunchFailReasons(Building rootBuilding)
        {
            List<Building> shipParts = ArchotechUtility.ShipBuildingsAttachedTo(rootBuilding).ToList<Building>();
            foreach (KeyValuePair<ThingDef, int> partDef in ArchotechUtility.RequiredParts())
            {
                int shipPartCount = shipParts.Count((Building pa) => pa.def == partDef.Key);
                if (shipPartCount < partDef.Value)
                {
                    yield return string.Format("{0}: {1}x {2} ({3} {4})", new object[]
                    {
                        "ShipReportMissingPart".Translate(),
                        partDef.Value - shipPartCount,
                        partDef.Key.label,
                        "ShipReportMissingPartRequires".Translate(),
                        partDef.Value
                    });
                }
            }
          
            foreach (Building part in shipParts)
            {
                CompHibernatable hibernatable = part.TryGetComp<CompHibernatable>();
                if (hibernatable != null && hibernatable.State == HibernatableStateDefOf.Hibernating)
                {
                    yield return string.Format("{0}: {1}", "ShipReportHibernating".Translate(), part.LabelCap);
                }
                if (hibernatable != null && !hibernatable.Running)
                {
                    yield return string.Format("{0}: {1}", "ShipReportNotReady".Translate(), part.LabelCap);
                }
            }
           
        }

        public static bool HasHibernatingParts(Building rootBuilding)
        {
            List<Building> list = ArchotechUtility.ShipBuildingsAttachedTo(rootBuilding).ToList<Building>();
            foreach (Building current in list)
            {
                CompHibernatable compHibernatable = current.TryGetComp<CompHibernatable>();
                if (compHibernatable != null && compHibernatable.State == HibernatableStateDefOf.Hibernating)
                {
                    return true;
                }
            }
            return false;
        }

        public static void StartupHibernatingParts(Building rootBuilding)
        {
            List<Building> list = ArchotechUtility.ShipBuildingsAttachedTo(rootBuilding).ToList<Building>();
            foreach (Building current in list)
            {
                CompHibernatable compHibernatable = current.TryGetComp<CompHibernatable>();
                if (compHibernatable != null && compHibernatable.State == HibernatableStateDefOf.Hibernating)
                {
                    compHibernatable.Startup();
                }
            }
        }

        public static List<Building> ShipBuildingsAttachedTo(Building root)
        {
            ArchotechUtility.closedSet.Clear();
            if (root == null || root.Destroyed)
            {
                return ArchotechUtility.closedSet;
            }
            ArchotechUtility.openSet.Clear();
            ArchotechUtility.openSet.Add(root);
            while (ArchotechUtility.openSet.Count > 0)
            {
                Building building = ArchotechUtility.openSet[ArchotechUtility.openSet.Count - 1];
                ArchotechUtility.openSet.Remove(building);
                ArchotechUtility.closedSet.Add(building);
                foreach (IntVec3 current in GenAdj.CellsAdjacentCardinal(building))
                {
                    Building edifice = current.GetEdifice(building.Map);
                    if (edifice != null && (edifice.TryGetComp<CompArchotechPart>()!=null) && !ArchotechUtility.closedSet.Contains(edifice) && !ArchotechUtility.openSet.Contains(edifice))
                    {
                        ArchotechUtility.openSet.Add(edifice);
                    }
                }
            }
            return ArchotechUtility.closedSet;
        }

        [DebuggerHidden]
        public static IEnumerable<Gizmo> ShipStartupGizmos(Building building)
        {
            if (ArchotechUtility.HasHibernatingParts(building))
            {
                yield return new Command_Action
                {
                    action = delegate
                    {
                       
                        DiaNode diaNode = new DiaNode("GR_ArchotechProjectText".Translate());
                        DiaOption diaOption = new DiaOption("Confirm".Translate());
                        diaOption.action = delegate
                        {
                            ArchotechUtility.StartupHibernatingParts(building);
                        };
                        diaOption.resolveTree = true;
                        diaNode.options.Add(diaOption);
                        DiaOption diaOption2 = new DiaOption("GoBack".Translate());
                        diaOption2.resolveTree = true;
                        diaNode.options.Add(diaOption2);
                        Find.WindowStack.Add(new Dialog_NodeTree(diaNode, true, false, null));
                    },
                    defaultLabel = "GR_CommandArchotechStartup".Translate(),
                    defaultDesc = "GR_CommandArchotechStartupDesc".Translate(),
                    hotKey = KeyBindingDefOf.Misc1,
                    icon = ContentFinder<Texture2D>.Get("ui/commands/GR_Microscope", true)
                };
            }
        }
    }
}

