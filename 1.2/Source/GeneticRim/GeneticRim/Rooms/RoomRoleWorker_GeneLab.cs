using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace GeneticRim
{
    class RoomRoleWorker_GeneLab : RoomRoleWorker
    {
        public override float GetScore(Room room)
        {
            int num = 0;
            List<Thing> containedAndAdjacentThings = room.ContainedAndAdjacentThings;
            for (int i = 0; i < containedAndAdjacentThings.Count; i++)
            {
                Thing thing = containedAndAdjacentThings[i];
                if (thing is Building_WorkTable && thing.def.defName== "GR_GeneticExtractionTable")
                {
                    num++;
                }
            }
            return 30f * (float)num;
        }
    }
}
