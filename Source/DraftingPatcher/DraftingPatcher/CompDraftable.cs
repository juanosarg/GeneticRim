
using Verse;

namespace DraftingPatcher
{
    class CompDraftable : ThingComp
    {
        

        public CompProperties_Draftable Props
        {
            get
            {
                return (CompProperties_Draftable)this.props;
            }
        }

        public bool GetExplodable
        {
            get
            {
                return this.Props.explodable;
            }
        }

        public bool GetRage
        {
            get
            {
                return this.Props.rage;
            }
        }

        public bool GetChickenRimPox
        {
            get
            {
                return this.Props.chickenRimPox;
            }
        }

        public bool GetCanCarryMore
        {
            get
            {
                return this.Props.carrymore;
            }
        }

        public bool GetAdrenalineBurst
        {
            get
            {
                return this.Props.adrenalineburst;
            }
        }

        public bool GetCanDoInsectClouds
        {
            get
            {
                return this.Props.insectclouds;
            }
        }
        public bool GetCanStampede
        {
            get
            {
                return this.Props.stampede;
            }
        }

        public bool GetCanDoPoisonousCloud
        {
            get
            {
                return this.Props.poisonouscloud;
            }
        }
        public bool GetCanBurrow
        {
            get
            {
                return this.Props.burrowing;
            }
        }
        public bool HasDinoStamina
        {
            get
            {
                return this.Props.dinostamina;
            }
        }
        public bool GetHorror
        {
            get
            {
                return this.Props.horror;
            }
        }

        public bool GetMechablast
        {
            get
            {
                return this.Props.mechablast;
            }
        }
    }
}
