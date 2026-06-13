using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace DarkIsTheNight
{
    [StaticConstructorOnStartup]
    public static class DecalBootstrap
    {
        static DecalBootstrap()
        {
            try
            {
                new Harmony("DarkIsTheNight.Decals").PatchAll();
                Log.Message("[DarkIsTheNight] loaded successfully.");
            }
            catch (Exception e)
            {
                Log.Error("[DarkIsTheNight] Decal System failed to load:\n" + e);
            }
        }
    }

    [StaticConstructorOnStartup]
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.GetGizmos))]
    public static class PatchPawnGetGizmosDecals
    {
        private static readonly Texture2D GizmoIcon =
            ContentFinder<Texture2D>.Get("UI/CustomizeDecal");

        public static void Postfix(Pawn __instance, ref IEnumerable<Gizmo> __result)
        {
            if (__instance.Faction != Faction.OfPlayerSilentFail ||
                !DecalUtil.PawnHasAnyDecalApparel(__instance)) return;

            __result = AppendGizmo(__result, CreateDecalGizmo(__instance));
        }

        private static IEnumerable<Gizmo> AppendGizmo(IEnumerable<Gizmo> source, Gizmo gizmo)
        {
            foreach (var g in source) yield return g;
            yield return gizmo;
        }

        private static Gizmo CreateDecalGizmo(Pawn pawn)
        {
            return new Command_Action
            {
                defaultLabel = "DarkIsTheNight_StyleDecalsGizmo".Translate(pawn.LabelCap),
                defaultDesc = "DarkIsTheNight_StyleDecalsDesc".Translate(),
                icon = GizmoIcon,
                action = () => Find.WindowStack.Add(new DialogEditDecals(pawn))
            };
        }
    }
}