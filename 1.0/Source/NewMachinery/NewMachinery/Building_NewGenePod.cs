

using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;


namespace NewMachinery
{
    class Building_NewGenePod : Building, IThingHolder
    {
  

        public int tickCounter = 0;
        public int workTicks = 1000;
        public bool PodHasGenes1 = false;
        public bool PodHasGenes2 = false;
        public bool PodIsFull = false;
        public bool SignalInsertGenes1 = false;
        public bool SignalInsertGenes2 = false;

        public Thing typeOfGenesToInsert1 = null;
        public Thing typeOfGenesToInsert2 = null;




        public ThingOwner innerContainerGenes1 = null;
        public ThingOwner innerContainerGenes2 = null;

        protected bool contentsKnown1 = false;
        protected bool contentsKnown2 = false;

        public Map map;

        public Building_NewGenePod()
        {
            this.innerContainerGenes1 = new ThingOwner<Thing>(this, false, LookMode.Deep);
            this.innerContainerGenes2 = new ThingOwner<Thing>(this, false, LookMode.Deep);
       
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Deep.Look<ThingOwner>(ref this.innerContainerGenes1, "innerContainerGenes1", new object[]
            {
                this
            });
            Scribe_Deep.Look<ThingOwner>(ref this.innerContainerGenes2, "innerContainerGenes2", new object[]
            {
                this
            });
         /*   Scribe_Deep.Look<Thing>(ref this.typeOfGenesToInsert1, "typeOfGenesToInsert1", new object[]
           {
                this
           });
            Scribe_Deep.Look<Thing>(ref this.typeOfGenesToInsert2, "typeOfGenesToInsert2", new object[]
           {
                this
           });*/

            Scribe_Values.Look<bool>(ref this.contentsKnown1, "contentsKnown1", false, false);
            Scribe_Values.Look<bool>(ref this.contentsKnown2, "contentsKnown2", false, false);
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounter", 0, false);
            Scribe_Values.Look<bool>(ref this.PodHasGenes1, "PodHasGenes1", false, false);
            Scribe_Values.Look<bool>(ref this.PodHasGenes2, "PodHasGenes2", false, false);
            Scribe_Values.Look<bool>(ref this.PodIsFull, "PodIsFull", false, false);
            Scribe_Values.Look<bool>(ref this.SignalInsertGenes1, "SignalInsertGenes1", false, false);
            Scribe_Values.Look<bool>(ref this.SignalInsertGenes2, "SignalInsertGenes2", false, false);


        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            if (base.Faction != null && base.Faction.IsPlayer)
            {
                this.contentsKnown1 = true;
                this.contentsKnown2 = true;
            }
        }

        public override Graphic Graphic
        {
            get
            {
                if (this.PodHasGenes1 && this.PodHasGenes2)
                {

                    Graphic newgraphic = GraphicDatabase.Get(this.def.graphicData.graphicClass, "Things/Building/GR_NewGenePodFull", this.def.graphicData.shaderType.Shader, this.def.graphicData.drawSize, this.DrawColor, this.DrawColorTwo);

                    return newgraphic;
                }
                else if (this.PodHasGenes1 && !this.PodHasGenes2)

                {
                    Graphic newgraphic = GraphicDatabase.Get(this.def.graphicData.graphicClass, "Things/Building/GR_NewGenePodLeftFull", this.def.graphicData.shaderType.Shader, this.def.graphicData.drawSize, this.DrawColor, this.DrawColorTwo);


                    return newgraphic;
                }
                else if (!this.PodHasGenes1 && this.PodHasGenes2)
                {
                    Graphic newgraphic = GraphicDatabase.Get(this.def.graphicData.graphicClass, "Things/Building/GR_NewGenePodRightFull", this.def.graphicData.shaderType.Shader, this.def.graphicData.drawSize, this.DrawColor, this.DrawColorTwo);


                    return newgraphic;
                }
                else return base.Graphic;


            }
        }

        public override string GetInspectString()
        {
            string text = base.GetInspectString();
            string strHasBothGenes = "";
            
            if (innerContainerGenes1.NullOrEmpty()|| innerContainerGenes2.NullOrEmpty())

            { strHasBothGenes= "GR_AllGenesNotPresent".Translate(); }
            else
            {
                strHasBothGenes = "GR_MixingGenes".Translate() + " " + ((float)tickCounter / workTicks).ToStringPercent();

            }



            return text + strHasBothGenes;
        }

        [DebuggerHidden]
        public override IEnumerable<Gizmo> GetGizmos()
        {
            map = this.Map;
            foreach (Gizmo g in base.GetGizmos())
            {
                yield return g;
            }

            if (innerContainerGenes1.NullOrEmpty())
            {
                yield return GeneListSetupUtility.SetGeneListCommand(this, map);
            }
            else
            {
                Command_Action RB_Gizmo_Empty_Gene1 = new Command_Action();
                RB_Gizmo_Empty_Gene1.action = delegate
                {
                    this.EjectContents();
                    PodHasGenes1 = false;
                    typeOfGenesToInsert1 = null;

                };
                RB_Gizmo_Empty_Gene1.defaultLabel = "GR_ExtractGenes".Translate();
                RB_Gizmo_Empty_Gene1.defaultDesc = "GR_ExtractGenesDesc".Translate();
                RB_Gizmo_Empty_Gene1.icon = ContentFinder<Texture2D>.Get("ui/commands/GR_ExtractGeneticMaterial1", true);
                yield return RB_Gizmo_Empty_Gene1;
            }

            if (innerContainerGenes2.NullOrEmpty())
            {
                yield return GeneListSetupUtility.SetGene2ListCommand(this, map);
            }
            else
            {
                Command_Action RB_Gizmo_Empty_Gene2 = new Command_Action();
                RB_Gizmo_Empty_Gene2.action = delegate
                {
                    this.EjectContents2();
                    PodHasGenes2 = false;
                    typeOfGenesToInsert2 = null;

                };
                RB_Gizmo_Empty_Gene2.defaultLabel = "GR_ExtractGenes2".Translate();
                RB_Gizmo_Empty_Gene2.defaultDesc = "GR_ExtractGenes2Desc".Translate();
                RB_Gizmo_Empty_Gene2.icon = ContentFinder<Texture2D>.Get("ui/commands/GR_ExtractGeneticMaterial2", true);
                yield return RB_Gizmo_Empty_Gene2;
            }
         
        }

      

        public bool TryAcceptThing(Thing thing, bool allowSpecialEffects = true)
        {
            bool result;

            bool flag;
            if (thing.holdingOwner != null)
            {
                thing.holdingOwner.TryTransferToContainer(thing, this.innerContainerGenes1, thing.stackCount, true);
                flag = true;
            }
            else
            {
                flag = this.innerContainerGenes1.TryAdd(thing, true);
            }
            if (flag)
            {
                if (thing.Faction != null && thing.Faction.IsPlayer)
                {
                    this.contentsKnown1 = true;
                }
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public bool TryAcceptThing2(Thing thing, bool allowSpecialEffects = true)
        {
            bool result;

            bool flag;
            if (thing.holdingOwner != null)
            {
                thing.holdingOwner.TryTransferToContainer(thing, this.innerContainerGenes2, thing.stackCount, true);
                flag = true;
            }
            else
            {
                flag = this.innerContainerGenes2.TryAdd(thing, true);
            }
            if (flag)
            {
                if (thing.Faction != null && thing.Faction.IsPlayer)
                {
                    this.contentsKnown2 = true;
                }
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public ThingOwner GetDirectlyHeldThings()
        {
            return this.innerContainerGenes1;
        }

        public void GetChildHolders(List<IThingHolder> outChildren)
        {
            ThingOwnerUtility.AppendThingHoldersFromThings(outChildren, this.GetDirectlyHeldThings());
        }

        public virtual void EjectContents()
        {
            this.innerContainerGenes1.TryDropAll(this.InteractionCell, base.Map, ThingPlaceMode.Near, null, null);
            this.typeOfGenesToInsert1 = null;
            this.contentsKnown1 = true;
        }

        public virtual void EjectContents2()
        {
            this.innerContainerGenes2.TryDropAll(this.InteractionCell, base.Map, ThingPlaceMode.Near, null, null);
            this.typeOfGenesToInsert2 = null;
            this.contentsKnown2 = true;
        }


        public override void Tick()
        {
            base.Tick();
            tickCounter++;
            if(tickCounter> workTicks + 1)
            {
                tickCounter = 0;
            }


        }

       

      

        public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
        {

            EjectContents();
            EjectContents2();
            base.Destroy(mode);
        }

    }
}
