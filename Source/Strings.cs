using Verse;

namespace DamageOverlay
{
    internal class Strings
    {
        public static readonly string PREFIX = "kathanon.DamageOverlay.";

        public static readonly string opacity       = (PREFIX + "opacity.title" ).Translate();
        public static readonly string opacity_desc  = (PREFIX + "opacity.desc"  ).Translate();

        public static readonly string interval      = (PREFIX + "interval.title").Translate();
        public static readonly string interval_desc = (PREFIX + "interval.desc" ).Translate();

        public static readonly string numSteps      = (PREFIX + "numSteps.title").Translate();
        public static readonly string numSteps_desc = (PREFIX + "numSteps.desc" ).Translate();

        public static readonly string toggleToolTip = (PREFIX + "toggleToolTip" ).Translate();

        public static readonly string filter_prefix = (PREFIX + "filter.");
        public static readonly string filter        = (filter_prefix + "title"  ).Translate();
        public static readonly string filter_desc   = (filter_prefix + "desc"   ).Translate();

        public static readonly string minColor      = (PREFIX + "minColor.title").Translate();
        public static readonly string minColor_desc = (PREFIX + "minColor.desc" ).Translate();

        public static readonly string maxColor      = (PREFIX + "maxColor.title").Translate();
        public static readonly string maxColor_desc = (PREFIX + "maxColor.desc" ).Translate();

        public static readonly string color_red     = (PREFIX + "color.red"    ).Translate();
        public static readonly string color_green   = (PREFIX + "color.green"  ).Translate();
        public static readonly string color_blue    = (PREFIX + "color.blue"   ).Translate();
        public static readonly string color_yellow  = (PREFIX + "color.yellow" ).Translate();
        public static readonly string color_cyan    = (PREFIX + "color.cyan"   ).Translate();
        public static readonly string color_magenta = (PREFIX + "color.magenta").Translate();
    }
}
