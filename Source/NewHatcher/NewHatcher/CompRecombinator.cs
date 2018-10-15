
using Verse;
using System;




namespace NewHatcher
{
    class CompRecombinator : ThingComp
    {

        private Random rand = new Random();


        public CompProperties_Recombinator Props
        {
            get
            {
                return (CompProperties_Recombinator)this.props;
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

            int randomNumber = rand.Next(1, 12);
            //Log.Warning(randomNumber.ToString());


            if (randomNumber == 11)
            {
                if (ResearchProjectDef.Named("GR_AdvancedGeneticEngineering").IsFinished)
                {
                    GenSpawn.Spawn(ThingDef.Named("GR_ThrumboGenetic"), this.parent.Position, this.parent.Map);
                }
                else
                {
                    RecombinateAgain();
                }
            }
            else if (randomNumber == 10)
            {
                if (ResearchProjectDef.Named("GR_HumanoidGeneticEngineering").IsFinished)
                {
                    GenSpawn.Spawn(ThingDef.Named("GR_HumanoidGenetic"), this.parent.Position, this.parent.Map);
                }
                else
                {
                    RecombinateAgain();
                }
            }
            else if (randomNumber == 9)
            {
                if (ResearchProjectDef.Named("GR_ReptilianGenome").IsFinished)
                {
                    GenSpawn.Spawn(ThingDef.Named("GR_ReptilianGenetic"), this.parent.Position, this.parent.Map);
                }
                else
                {
                    RecombinateAgain();
                }
            }
            else if (randomNumber == 8)
            {
                if (ResearchProjectDef.Named("GR_InsectoidGenome").IsFinished)
                {
                    GenSpawn.Spawn(ThingDef.Named("GR_InsectoidGenetic"), this.parent.Position, this.parent.Map);
                }
                else
                {
                    RecombinateAgain();
                }
            }
            else if (randomNumber == 7)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_RodentGenetic"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 6)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_BearGenetic"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 5)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_ChickenGenetic"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 4)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_BoomalopeGenetic"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 3)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_MuffaloGenetic"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 2)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_WolfGenetic"), this.parent.Position, this.parent.Map);
            }
            else
            {
                GenSpawn.Spawn(ThingDef.Named("GR_RuinedGenetic"), this.parent.Position, this.parent.Map);
            }

            this.parent.Destroy(DestroyMode.Vanish);
        }

        private void RecombinateAgain()
        {
            int randomNumber = rand.Next(1, 8);

            if (randomNumber == 7)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_RodentGenetic"), this.parent.Position, this.parent.Map);
            }
            else if(randomNumber == 6)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_BearGenetic"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 5)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_ChickenGenetic"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 4)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_BoomalopeGenetic"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 3)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_MuffaloGenetic"), this.parent.Position, this.parent.Map);
            }
            else if (randomNumber == 2)
            {
                GenSpawn.Spawn(ThingDef.Named("GR_WolfGenetic"), this.parent.Position, this.parent.Map);
            }
            else
            {
                GenSpawn.Spawn(ThingDef.Named("GR_RuinedGenetic"), this.parent.Position, this.parent.Map);
            }

        }



    }
}

