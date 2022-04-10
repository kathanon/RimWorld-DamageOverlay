using HugsLib.Settings;
using RimWorld;
using System;
using Verse;
using Verse.Sound;
using UnityEngine;

namespace DamageOverlay
{
    internal class LabelAndSliderWidget
    {
        public static void ForFloat(SettingHandle<float> setting, float min, float max, float roundTo = -1, Func<float, string> label = null)
        {
            var instance = new Widget<float>(setting, label ?? floatLabel, floatFrom, floatTo, min, max, roundTo);
            setting.CustomDrawer = instance.Update;
        }

        public static void ForInt(SettingHandle<int> setting, int min, int max, int roundTo = 1, Func<int, string> label = null)
        {
            var instance = new Widget<int>(setting, label ?? intLabel, intFrom, intTo, min, max, roundTo);
            setting.CustomDrawer = instance.Update;
        }

        public static void For<T>(SettingHandle<T> setting, T min, T max, float roundTo, Func<T, string> label, Func<T, float> from, Func<float, T> to)
        {
            var instance = new Widget<T>(setting, label, from, to, min, max, roundTo);
            setting.CustomDrawer = instance.Update;
        }

        private static readonly Func<float, float>  floatFrom  = v => v;
        private static readonly Func<float, float>  floatTo    = v => v;
        private static readonly Func<float, string> floatLabel = v => v.ToString();
        private static readonly Func<int,   float>  intFrom    = v => v;
        private static readonly Func<float, int>    intTo      = v => (int) v;
        private static readonly Func<int,   string> intLabel   = v => v.ToString();

        private class Widget<T>
        {
            private static string longestMaxText = ""; 

            private readonly SettingHandle<T> setting;
            private readonly Func<T, string> label;
            private readonly Func<T, float> from;
            private readonly Func<float, T> to;
            private readonly float min;
            private readonly float max;
            private readonly float roundTo;
            private readonly string maxText;

            private const float border = 4f;
            private const float gap = 8f;
            private const float sliderTopBorder = 4f;

            public Widget(SettingHandle<T> setting, Func<T, string> label, Func<T, float> from, Func<float, T> to, T min, T max, float roundTo)
            {
                this.setting = setting;
                this.label = label;
                this.from = from;
                this.to = to;
                this.min = from(min);
                this.max = from(max);
                this.roundTo = roundTo;
                maxText = label(max);
                if (maxText.Length > longestMaxText.Length)
                {
                    longestMaxText = maxText;
                }
            }

            public bool Update(Rect rect)
            {
                T old = setting;
                float oldVal = from(old);

                float labelWidth = Text.CalcSize(longestMaxText).x + 1f;
                Rect bounds = rect.ContractedBy(border);
                Rect labelBounds = bounds.LeftPartPixels(labelWidth);
                Rect sliderBounds = bounds.RightPartPixels(bounds.width - labelWidth - gap);
                sliderBounds.y += sliderTopBorder;
                sliderBounds.height -= sliderTopBorder;
                string text = label(old);
                float textWidth = Text.CalcSize(text).x + 1f;
                Rect textBounds = (textWidth < labelWidth) ? labelBounds.RightPartPixels(textWidth) : labelBounds;

                Widgets.Label(textBounds, text);
                float newVal = Widgets.HorizontalSlider(sliderBounds, oldVal, min, max, roundTo: roundTo);
                if (newVal != oldVal)
                {
                    SoundDefOf.DragSlider.PlayOneShotOnCamera();
                    setting.Value = to(newVal);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
