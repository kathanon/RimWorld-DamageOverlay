using System;
using RimWorld;
using Verse;

namespace DamageOverlay
{
    internal class Filters
    {
        public enum Type { All, NonMineable, Repairable, Owned, Walls }
 
        private static readonly Predicate<Thing>[] funcs = new Predicate<Thing>[] { DefaultFilter, NonMineable, Repairable, Owned, Walls };

        static internal Predicate<Thing> ForType(Type t) => funcs[(int)t];

        static internal bool DefaultFilter(Thing t) => 
            t is Building
            && t.MaxHitPoints > 0
            && t.HitPoints > 0
            && t.HitPoints < t.MaxHitPoints;

        static internal bool NonMineable(Thing t) =>
            DefaultFilter(t)
            && (!(t is Mineable) || t.def.IsSmoothed);

        static internal bool Repairable(Thing t) =>
            DefaultFilter(t)
            && NonMineable(t)
            && (t.def.building?.claimable ?? false)
            && (t.def.building?.repairable ?? false);

        static internal bool Owned(Thing t) =>
            DefaultFilter(t)
            && t.Faction == Faction.OfPlayer;

        static internal bool Walls(Thing t) =>
            DefaultFilter(t)
            && (t is Building_Door || t.def.label == "wall" || t.def.IsSmoothed);

    }
}