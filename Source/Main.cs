using HugsLib.Utils;

namespace DamageOverlay
{
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
   }
}
