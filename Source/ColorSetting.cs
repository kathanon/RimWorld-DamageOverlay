using HugsLib.Settings;
using RimWorld;
using System;
using Verse;
using Verse.Sound;
using UnityEngine;

namespace DamageOverlay
{
    internal class ConvertibleColor : SettingHandleConvertible
    {
        private const float satMin = 0.4f;
        private const float valMin = 0.4f;
        private const float scale  = 0.5f;

        private Color color;
        private float hue;
        private float light;
        private readonly float hueDefault;
        private readonly float lightDefault;

        public Color Color { get => color; set { color = value; FromRGB(); } }
        public float Hue { get => hue; set { hue = value; ToRGB(); } }
        public float Light { get => light; set { light = value; ToRGB(); } }

        public ConvertibleColor()
        {
            hueDefault   = -1f;
            lightDefault = -1f;
        }

        public ConvertibleColor(Color defaultColor)
        {
            color = defaultColor;
            FromRGB();
            hueDefault = hue;
            lightDefault = light;
        }

        public override void FromString(string settingValue)
        {
            var parts = settingValue.Split(',');
            color = new Color(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));
            FromRGB();
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", color.r, color.g, color.b);
        }

        public override bool ShouldBeSaved => hue != hueDefault || light != lightDefault;

        private void FromRGB()
        {
            float sat, val;
            Color.RGBToHSV(color, out hue, out sat, out val);
            sat = (sat <= satMin) ? 0f : ((sat - satMin) / (1 - satMin));
            val = (val <= valMin) ? 0f : ((val - valMin) / (1 - valMin));
            light = (val <= sat) ? scale * val : 1f - (1f - scale) * sat;
            // H + L is the limiting one, so convert back to make sure we are in the representable region
            ToRGB();
        }

        private void ToRGB()
        {
            float sat, val;
            if (light < scale)
            {
                val = light / scale;
                sat = 1f;
            }
            else
            {
                val = 1f;
                sat = 1f - (light - scale) / (1f - scale);
            }
            val = valMin + val * (1f - valMin);
            sat = satMin + sat * (1f - satMin);
            color = Color.HSVToRGB(hue, sat, val, false);
        }
    }

    internal class ColorSetting
    {
        private readonly SettingHandle<ConvertibleColor> setting;
        private readonly Action<ColorSetting> onChange;
        private Color defaultColor;

        public ConvertibleColor Value { get => Setting.Value; set => Setting.Value = value; }

        private SettingHandle<ConvertibleColor> Setting
        {
            get {
                if (setting.Value == null)
                {
                    setting.Value = new ConvertibleColor(defaultColor);
                }
                return setting; 
            }
        }


        public ColorSetting(ModSettingsPack pack, string name, string title, string description, Color defaultColor, Action<ColorSetting> onChange = null)
        {
            this.defaultColor = defaultColor;
            this.onChange = onChange;
            setting = pack.GetHandle<ConvertibleColor>(name, title, description);
            setting.CustomDrawer = Update;
            setting.ContextMenuEntries = new[] {
                new ContextMenuEntry(Strings.color_red,     () => Value.Color = Color.red),
                new ContextMenuEntry(Strings.color_green,   () => Value.Color = Color.green),
                new ContextMenuEntry(Strings.color_blue,    () => Value.Color = Color.blue),
                new ContextMenuEntry(Strings.color_yellow,  () => Value.Color = Color.yellow),
                new ContextMenuEntry(Strings.color_cyan,    () => Value.Color = Color.cyan),
                new ContextMenuEntry(Strings.color_magenta, () => Value.Color = Color.magenta),
            };
 
        }

        public static implicit operator Color(ColorSetting cs)
        {
            return cs.Value.Color;
        }

        private const float colorMargin = 3f;
        private const float gap = 8f;

        public bool Update(Rect rect)
        {
            rect.SplitVertically(rect.height + gap / 2f, out Rect colorPart, out Rect sliderPart, gap / 2f);
            colorPart = colorPart.ContractedBy(colorMargin);

            Widgets.DrawBoxSolid(colorPart, this);

            bool hueChanged   = Slider(sliderPart.TopHalf(),    Value, true);
            bool lightChanged = Slider(sliderPart.BottomHalf(), Value, false);

            if (hueChanged || lightChanged)
            {
                onChange?.Invoke(this);
                return true;
            }
            return false;
        }

        private bool Slider(Rect rect, ConvertibleColor color, bool hue)
        {
            float oldVal = hue ? color.Hue : color.Light;
            float newVal = Widgets.HorizontalSlider(rect, oldVal, 0f, 1f, roundTo: 0.001f);
            if (newVal != oldVal)
            {
                SoundDefOf.DragSlider.PlayOneShotOnCamera();
                if (hue)
                {
                    color.Hue = newVal;
                } 
                else
                {
                    color.Light = newVal;
                }
                return true;
            }
            return false;

        }
    }
}
