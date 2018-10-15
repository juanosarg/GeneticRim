using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse.AI;
using Verse;


namespace NewMachinery
{
    [StaticConstructorOnStartup]
    public class Command_SetGeneList : Command
    {

        public Map map;
        public Building buildingpod;
        public Thing gene;



        public Command_SetGeneList()
        {

        }

        public override void ProcessInput(Event ev)
        {
            base.ProcessInput(ev);
            List<FloatMenuOption> list = new List<FloatMenuOption>();

            if (map.listerThings.ThingsOfDef(DefDatabase<ThingDef>.GetNamed("GR_BearGenetic", true)).Count > 0)
            {
                list.Add(new FloatMenuOption("Bear Genes", delegate
                {
                   
                    gene = map.listerThings.ThingsOfDef(DefDatabase<ThingDef>.GetNamed("GR_BearGenetic", true)).RandomElement(); 
                    Building_NewGenePod pod = (Building_NewGenePod)buildingpod;
                    pod.typeOfGenesToInsert1 = gene;
                    Log.Message("Gene is: " + gene.ToString(), false);
                    if (pod.typeOfGenesToInsert2 != null) {Log.Message("pod.typeOfGenesToInsert2 is: " + pod.typeOfGenesToInsert2.ToString(), false); } else Log.Message("pod.typeOfGenesToInsert2 is null", false);


                    if (pod.typeOfGenesToInsert1 == pod.typeOfGenesToInsert2)
                    {
                        pod.typeOfGenesToInsert1 = null;
                        pod.SignalInsertGenes1 = false;

                    }
                    else { pod.SignalInsertGenes1 = true; }
                    
                }, MenuOptionPriority.Default, null, null, 29f, null, null));
            }

           

            if (list.Count > 0)
            {

            }
            else
            {
                list.Add(new FloatMenuOption("GR_NoGenes".Translate(), delegate
                {

                }, MenuOptionPriority.Default, null, null, 29f, null, null));
            }
            Find.WindowStack.Add(new FloatMenu(list));
        }

  

     


    }


}


