using HarmonyLib;
using RimWorld;
using Verse;

namespace DamageOverlay
{
    [HarmonyPatch(typeof(PlaySettings), "DoPlaySettingsGlobalControls")]
    public static class AddToggle_Patch {
        [HarmonyPostfix]
        public static void PostFix(WidgetRow row, bool worldView) {
            if (worldView)
                return;

            if (row == null || Resources.Icon == null)
                return;

            row.ToggleableIcon(ref Main.Instance.ShowOverlay, Resources.Icon,
                Strings.toggleToolTip, SoundDefOf.Mouseover_ButtonToggle);
        }
    }
}
