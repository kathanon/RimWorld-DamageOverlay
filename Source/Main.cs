using HugsLib.Utils;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace DamageOverlay
{
    [DefOf]
    public static class MyKeyBindings
    {
        public static KeyBindingDef kathanon_ToggleDamageOverlay;
    }

    public class Main : HugsLib.ModBase
    {
        public Main()
        {
            Instance = this;
        }

        internal new ModLogger Logger => base.Logger;

        internal static Main Instance { get; private set; }

        public override string ModIdentifier => "kathanon.DamageOverlay";

        public bool ShowOverlay;

        private DamageOverlay _overlay;

        internal DamageOverlay Overlay { 
            get {
                if (_overlay == null)
                {
                    _overlay = new DamageOverlay();
                }
                return _overlay;
            } 
        }

        internal MySettings MySettings;

        internal void UpdateOverlay()
        {
             Overlay.Update(MySettings.interval);
        }

        public override void WorldLoaded()
        {
            Overlay.ResetDrawer();
        }

        public override void DefsLoaded()
        {
            MySettings = new MySettings(Settings);
        }

        public override void OnGUI()
        {
            if (Current.ProgramState != ProgramState.Playing ||
                Find.CurrentMap == null ||
                WorldRendererUtility.WorldRenderedNow ||
                _overlay == null)
            {
                return;
            }

            if (Event.current.type != EventType.KeyDown || Event.current.keyCode == KeyCode.None)
            {
                return;
            }

            if (MyKeyBindings.kathanon_ToggleDamageOverlay.JustPressed)
            {
                if (WorldRendererUtility.WorldRenderedNow)
                {
                    return;
                }
                ShowOverlay = !ShowOverlay;
            }
        }
    }
}
