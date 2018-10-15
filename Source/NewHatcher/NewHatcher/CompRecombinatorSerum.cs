
using Verse;
using System;




namespace NewHatcher
{
    class CompRecombinatorSerum : ThingComp
    {

        private Random rand = new Random();


        public CompProperties_RecombinatorSerum Props
        {
            get
            {
                return (CompProperties_RecombinatorSerum)this.props;
            }
        }



        public override void CompTick()
        {

            // Log.Warning(this.parent.ParentHolder.ToString());
            if (!(this.parent.ParentHolder is Pawn_CarryTracker) && this.parent.Map.IsPlayerHome)
            {
                this.Hatch();

            }

        }


        public void Hatch()
        {

            int randomNumber = rand.Next(1, 16);
            //Log.Warning(randomNumber.ToString());


            if (randomNumber == 15)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_BearSerum"), this.parent.Position, this.parent.Map);

            }
            else if (randomNumber == 14)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_BoarSerum"), this.parent.Position, this.parent.Map);

            }
            else if (randomNumber == 13)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_BoomalopeSerum"), this.parent.Position, this.parent.Map);

            }
            else if (randomNumber == 12)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_ChickenSerum"), this.parent.Position, this.parent.Map);

            }
            else if (randomNumber == 11)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_EmuSerum"), this.parent.Position, this.parent.Map);

            }
            else if (randomNumber == 10)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_MuffaloSerum"), this.parent.Position, this.parent.Map);

            }
            else if (randomNumber == 9)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_OstrichSerum"), this.parent.Position, this.parent.Map);

            }
            else if (randomNumber == 8)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_RhinoSerum"), this.parent.Position, this.parent.Map);

            }
            else if (randomNumber == 7)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_WolfSerum"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 6)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_HareSerum"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 5)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_IguanaSerum"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 4)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_PigSerum"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 3)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_RatSerum"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 2)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_TortoiseSerum"), this.parent.Position, this.parent.Map);
            }
            else
            {
                GenSpawn.Spawn(ThingDef.Named("GR_RuinedGenetic"), this.parent.Position, this.parent.Map);
            }

            this.parent.Destroy(DestroyMode.Vanish);
        }

       

        



    }
}

