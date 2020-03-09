
using Verse;
using RimWorld;


namespace NewHatcher
{
    public class HediffComp_Exploder : HediffComp
    {


        public HediffCompProperties_Exploder Props
        {
            get
            {
                return (HediffCompProperties_Exploder)this.props;
            }
        }

      

        public override void Notify_PawnDied()
        {

          
            //Log.Warning("yep, dead");

            GenExplosion.DoExplosion(this.parent.pawn.Corpse.Position, this.parent.pawn.Corpse.Map, this.Props.explosionForce, DamageDefOf.Flame, this.parent.pawn.Corpse, -1,-1,null, null, null, null,null, 0f, 1, false, null, 0f, 1);

            
        }




    }
}
