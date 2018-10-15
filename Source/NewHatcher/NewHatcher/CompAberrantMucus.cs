
using RimWorld;
using Verse;


namespace NewHatcher
{
    class CompAberrantMucus : ThingComp
    {

        private int mucusProgress = 0;

        public CompProperties_AberrantMucus Props
        {
            get
            {
                return (CompProperties_AberrantMucus)this.props;
            }
        }
        public override void CompTick()
        {
            this.mucusProgress += 1;
            if (this.mucusProgress > 60)
            {
                FilthMaker.MakeFilth(this.parent.PositionHeld, this.parent.MapHeld, ThingDef.Named("GR_FilthMucus"), 1);
                this.mucusProgress = 0;
            }
        }
    }
}
