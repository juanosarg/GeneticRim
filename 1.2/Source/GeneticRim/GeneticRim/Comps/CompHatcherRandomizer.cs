
using Verse;
using System;

namespace GeneticRim
{
    class CompHatcherRandomizer : ThingComp
    {

        Random rand = new Random();

        public CompProperties_HatcherRandomizer Props
        {
            get
            {
                return (CompProperties_HatcherRandomizer)this.props;
            }
        }

        public override void CompTick()
        {
            if (!(this.parent.ParentHolder is Pawn_CarryTracker) && this.parent.Map!=null) {
                this.Hatch();                   
            }
        }


        public void Hatch()
        {
            if(rand.NextDouble() > 0.5)
            {
                GenSpawn.Spawn(ThingDef.Named(this.Props.hatcherItem),this.parent.Position, this.parent.Map);
            } else
            {
                GenSpawn.Spawn(ThingDef.Named(this.Props.hatcherItemTwo), this.parent.Position, this.parent.Map);

            }

            this.parent.Destroy(DestroyMode.Vanish);
        }


       
    }
}

