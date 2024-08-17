using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse;

namespace DisposalType
{
    [HarmonyPatch(typeof(ThingDef), "SpecialDisplayStats")]
    public static class ThingDef_SpecialDisplayStats_Patch
    {
        public static void Postfix(ThingDef __instance, ref IEnumerable<StatDrawEntry> __result)
        {
            if (__instance.IsCorpse || __instance.race != null || __instance.IsPlant || __instance.IsSmoothable || __instance.IsSmoothed || __instance.IsWithinCategory(ThingCategoryDefOf.Buildings) || __instance.IsWithinCategory(ThingCategoryDefOf.StoneChunks))
            {
                return;
            }
            string label = __instance.label.ToString();
            label = Utility.CapitalizeFirstLetter(label);
            bool burnable = __instance.smeltable;
            bool smeltable = __instance.smeltable;
            if (burnable && smeltable)
            {
                __result = __result.Concat(new StatDrawEntry(StatCategoryDefOf.BasicsNonPawn, "VR_DisposalMethod".Translate(), "VR_Disposal_Burnable_Smeltable".Translate(), "VR_Disposal_Burnable_Smeltable_Desc".Translate((string)label), 999));

            }
            else
            {
                if (__instance.burnableByRecipe)
                {
                    __result = __result.Concat(new StatDrawEntry(StatCategoryDefOf.BasicsNonPawn, "VR_DisposalMethod".Translate(), "VR_Disposal_Burnable".Translate(), "VR_Disposal_Burnable_Desc".Translate((string)label), 999));
                }
                if (__instance.smeltable)
                {
                    __result = __result.Concat(new StatDrawEntry(StatCategoryDefOf.BasicsNonPawn, "VR_DisposalMethod".Translate(), "VR_Disposal_Smeltable".Translate(), "VR_Disposal_Smeltable_Desc".Translate((string)label), 999));
                }
            }
        }
    }
}