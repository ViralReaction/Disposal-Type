using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse;

namespace RecycleType
{
    [HarmonyPatch(typeof(ThingDef), "SpecialDisplayStats")]
    public static class Building_GeneAssembler_PowerOn_Patch
    {
        public static void Postfix(ThingDef __instance, ref IEnumerable<StatDrawEntry> __result)
        {
            if (__instance.IsCorpse || __instance.race != null || __instance.IsPlant || __instance.IsSmoothable || __instance.IsSmoothed || __instance.IsWithinCategory(ThingCategoryDefOf.Buildings) || __instance.IsWithinCategory(ThingCategoryDefOf.StoneChunks))
            {
                return;
            }
            string label = __instance.label.ToString();
            label = Utility.CapitalizeFirstLetter(label);
            if (__instance.smeltable)
            {
                __result = __result.Concat(new StatDrawEntry(StatCategoryDefOf.BasicsNonPawn, "VR_DisposalMethod".Translate(), "VR_Disposal_Smeltable".Translate(), "VR_Disposal_Smeltable_Desc".Translate((string)label), 999));
            }
            else if (__instance.burnableByRecipe)
            {
                __result = __result.Concat(new StatDrawEntry(StatCategoryDefOf.BasicsNonPawn, "VR_DisposalMethod".Translate(), "VR_Disposal_Burnable".Translate(), "VR_Disposal_Burnable_Desc".Translate((string)label), 999));
            }
            else if (!__instance.smeltable && !__instance.burnableByRecipe)
            {
                __result = __result.Concat(new StatDrawEntry(StatCategoryDefOf.BasicsNonPawn, "VR_DisposalMethod".Translate(), "VR_Disposal_Destroy".Translate(), "VR_Disposal_Destroy_Desc".Translate((string)label), 999));
            }
            else
                Log.Error("Error adding disposal type to" + " " + __instance.ToString());
        }
    }
}