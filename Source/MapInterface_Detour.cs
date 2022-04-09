using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace DamageOverlay
{
    [HarmonyPatch(typeof(MapInterface), "MapInterfaceUpdate")]
    public static class MapInterface_MapInterfaceUpdate_Detour
    {
        [HarmonyPostfix]
        static void Postfix()
        {
            if (Find.CurrentMap == null || WorldRendererUtility.WorldRenderedNow)
            {
                return;
            }

            Main.Instance.UpdateOverlay();
        }
    }

    [HarmonyPatch(typeof(MapInterface), "Notify_SwitchedMap")]
    internal static class MapInterface_Notify_SwitchedMap_Detour
    {
        [HarmonyPostfix]
        static void Postfix()
        {
            Main.Instance.Overlay.ResetDrawer();
        }
    }
}
