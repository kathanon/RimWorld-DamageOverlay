using HugsLib.Settings;

namespace DamageOverlay
{
    internal class MySettings
    {
        public MySettings(ModSettingsPack pack)
        {
            opacity = pack.GetHandle(
                "opacity",
                Strings.opacity,
                Strings.opacity_desc,
                0.33f,
                Validators.FloatRangeValidator(0f, 1f));
            opacity.ValueChanged += (o) => Main.Instance.Overlay.ResetDrawer();

            filter = pack.GetHandle(
                "filter",
                Strings.filter,
                Strings.filter_desc,
                Filters.Type.Repairable,
                null, 
                Strings.filter_prefix);
            filter.ValueChanged += (o) => Main.Instance.Overlay.ResetFilter();

            interval = pack.GetHandle(
                "interval",
                Strings.interval,
                Strings.interval_desc,
                60,
                Validators.IntRangeValidator(1, 10000));

            numSteps = pack.GetHandle(
                "numSteps",
                Strings.numSteps,
                Strings.numSteps_desc,
                25,
                Validators.IntRangeValidator(2, 256));
            numSteps.ValueChanged += (o) => Main.Instance.Overlay.ResetColorMap();
        }

        public SettingHandle<float>        opacity;
        public SettingHandle<int>          interval;
        public SettingHandle<int>          numSteps;
        public SettingHandle<Filters.Type> filter;
    }
}
