using System;
using UnityEngine;
using Verse;

namespace DamageOverlay
{
    public class DamageOverlay : ICellBoolGiver
    {
        private Color[] colors;
        private int hp;
        private int nextUpdateTick;
        private CellBoolDrawer _drawer;
        private readonly MySettings settings = Main.Instance.MySettings;

        private int numSteps;
        private Predicate<Thing> filter;

        public DamageOverlay()
        {
            ResetColorMap();
            ResetFilter();
        }

        public void ResetColorMap()
        {
            Color minColor = settings.minColor;
            Color maxColor = settings.maxColor;
            numSteps = settings.numSteps;
            colors = new Color[numSteps];
            for (int i = 0; i < numSteps; i++)
            {
                var min = ((float) (colors.Length - i)) / numSteps;
                var max = ((float) i) / numSteps;
                var r = min * minColor.r + max * maxColor.r;
                var g = min * minColor.g + max * maxColor.g;
                var b = min * minColor.b + max * maxColor.b;
                colors[i] = new Color(r, g, b);
            }
            nextUpdateTick = 0;
        }

        internal void ResetFilter()
        {
            filter = Filters.ForType(settings.filter);
            nextUpdateTick = 0;
        }

        internal void ResetDrawer()
        {
            _drawer = null;
            nextUpdateTick = 0;
        }

        public CellBoolDrawer Drawer
        {
            get
            {
                if (_drawer == null)
                {
                    var map = Find.CurrentMap;
                    _drawer = new CellBoolDrawer(this, map.Size.x, map.Size.z, settings.opacity);
                }
                return _drawer;
            }
        }
        
        public void Update(int updateDelay)
        {
            if (Main.Instance.ShowOverlay)
            {
                Drawer.MarkForDraw();
                var tick = Find.TickManager.TicksGame;
                if (tick >= nextUpdateTick)
                {
                    Drawer.SetDirty();
                    nextUpdateTick = tick + updateDelay;
                }
                Drawer.CellBoolDrawerUpdate();
            }
        }
        public Color Color => Color.white;

        public bool GetCellBool(int index)
        {
            var map = Find.CurrentMap;
            if (map.fogGrid.IsFogged(index))
            {
                return false;
            }

            var cell = map.cellIndices.IndexToCell(index);
            var thing = map.thingGrid.ThingsListAt(cell).Find(filter);
            if (thing == null)
            {
                return false;
            }

            hp = numSteps * thing.HitPoints / thing.MaxHitPoints;
            return hp >= 0 && hp < numSteps;
        }

        public Color GetCellExtraColor(int index)
        {
            return colors[hp];
        }
    }
}