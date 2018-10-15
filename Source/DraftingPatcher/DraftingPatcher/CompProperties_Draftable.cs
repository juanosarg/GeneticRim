using Verse;

namespace DraftingPatcher
{
    public class CompProperties_Draftable : CompProperties
    {

        public bool explodable = false;
        public bool rage = false;
        public bool chickenRimPox = false;
        public bool carrymore = false;
        public bool adrenalineburst = false;
        public bool insectclouds = false;
        public bool stampede = false;
        public bool poisonouscloud = false;
        public bool burrowing = false;
        public bool dinostamina = false;
        public bool horror = false;
        public bool mechablast = false;





        public CompProperties_Draftable()
        {
            this.compClass = typeof(CompDraftable);
        }
    }
}
