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
    }
}
