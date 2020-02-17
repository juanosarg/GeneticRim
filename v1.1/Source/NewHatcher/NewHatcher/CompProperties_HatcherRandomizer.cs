
using Verse;

namespace NewHatcher
{
    public class CompProperties_HatcherRandomizer : CompProperties
    {
        

        public string hatcherItem;
        public string hatcherItemTwo;


        public CompProperties_HatcherRandomizer()
        {
            //Messages.Message("Patataaa", MessageSound.Standard);
            this.compClass = typeof(CompHatcherRandomizer);
        }
    }
}

