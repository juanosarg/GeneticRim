using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

namespace GeneticRim
{
    public static class ArchotechCountdown
    {
        private static float timeLeft = -1000f;

        private static Building shipRoot;

        private const float InitialTime = 7.2f;

        public static bool CountingDown
        {
            get
            {
                return ArchotechCountdown.timeLeft >= 0f;
            }
        }

        public static void InitiateCountdown(Building launchingShipRoot)
        {
            SoundDefOf.ShipTakeoff.PlayOneShotOnCamera(null);
            ArchotechCountdown.shipRoot = launchingShipRoot;
            ArchotechCountdown.timeLeft = 7.2f;
            ScreenFader.StartFade(Color.white, 7.2f);
        }

        public static void ShipCountdownUpdate()
        {
            if (ArchotechCountdown.timeLeft > 0f)
            {
                ArchotechCountdown.timeLeft -= Time.deltaTime;
                if (ArchotechCountdown.timeLeft <= 0f)
                {
                    ArchotechCountdown.CountdownEnded();
                }
            }
        }

        public static void CancelCountdown()
        {
            ArchotechCountdown.timeLeft = -1000f;
        }

        private static void CountdownEnded()
        {
            List<Building> list = ArchotechUtility.ShipBuildingsAttachedTo(ArchotechCountdown.shipRoot).ToList<Building>();
            StringBuilder stringBuilder = new StringBuilder();
            Pawn theFutureTamer = null;
            foreach (Pawn current in PawnsFinder.AllMaps_FreeColonistsSpawned)
            {
                stringBuilder.AppendLine("   " + current.LabelCap);
                theFutureTamer = current;
            }
            
            foreach (Building current in list)
            {
                if (current.def.defName== "GR_ArchotechPlatform") {
                    Map mapToPlaceAt = current.Map;
                    IntVec3 positionToPlaceAt = current.Position;
                    current.Destroy(DestroyMode.Vanish);
                    Building new_Platform = (Building)ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("GR_SpentArchotechPlatform", true));
                    new_Platform.SetFaction(Faction.OfPlayer);
                    GenSpawn.Spawn(new_Platform, positionToPlaceAt, mapToPlaceAt);

                    PawnGenerationRequest request = new PawnGenerationRequest(PawnKindDef.Named("GR_ArchotechCentipede"), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, false, true, false, false, true, false, 1f, false, true, true, false, false);
                    Pawn pawn = PawnGenerator.GeneratePawn(request);
                    pawn.training.Train(TrainableDefOf.Obedience, theFutureTamer, true);
                    pawn.training.Train(TrainableDefOf.Release, theFutureTamer, true);
                   

                    PawnUtility.TrySpawnHatchedOrBornPawn(pawn, new_Platform);

                }
                
            }
            string victoryText = "GR_GameOverArchotech".Translate(stringBuilder.ToString());
            GameVictoryUtility.ShowCredits(victoryText);
        }
    }
}

