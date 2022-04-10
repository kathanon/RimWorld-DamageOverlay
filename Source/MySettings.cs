using HugsLib.Settings;
using RimWorld;
using System;
using Verse;
using Verse.Sound;
using UnityEngine;

namespace DamageOverlay
{
    internal class MySettings
    {
        public MySettings(ModSettingsPack pack)
        {
            LabelAndSliderWidget.UseCommonMaxString("10000");
            opacity = pack.GetHandle(
                "opacity",
                Strings.opacity,
                Strings.opacity_desc,
                0.33f);
            opacity.ValueChanged += o => Main.Instance.Overlay.ResetDrawer();
            LabelAndSliderWidget.ForFloat(opacity, 0f, 1f, 0.01f, v => Math.Round(100f * v, 0) + "%");

            filter = pack.GetHandle(
                "filter",
                Strings.filter,
                Strings.filter_desc,
                Filters.Type.Repairable,
                null, 
                Strings.filter_prefix);
            filter.ValueChanged += o => Main.Instance.Overlay.ResetFilter();

            interval = pack.GetHandle(
                "interval",
                Strings.interval,
                Strings.interval_desc,
                60,
                Validators.IntRangeValidator(1, 10000));
            LabelAndSliderWidget.For<int>(interval, 1, 10000, 1f, v => v.ToString(), StepsFrom, StepsTo);

            numSteps = pack.GetHandle(
                "numSteps",
                Strings.numSteps,
                Strings.numSteps_desc,
                25,
                Validators.IntRangeValidator(2, 256));
            numSteps.ValueChanged += o => Main.Instance.Overlay.ResetColorMap();
            LabelAndSliderWidget.ForInt(numSteps, 2, 256);

            minColor = new ColorSetting(pack,
                "minColor",
                Strings.minColor,
                Strings.minColor_desc,
                Color.red,
                o => Main.Instance.Overlay.ResetColorMap());

            maxColor = new ColorSetting(pack,
                "maxColor",
                Strings.maxColor,
                Strings.maxColor_desc,
                Color.green,
                o => Main.Instance.Overlay.ResetColorMap());

        }

        private float StepsFrom(int v)
        {
            if (v > 1000)
            {
                return v / 100 + 100f;
            } 
            else if (v > 100)
            {
                return v / 10 + 90f;
            }
            else
            {
                return v;
            }
        }

        private int StepsTo(float v)
        {
            if (v > 110f)
            {
                return (int)(v - 100f) * 100;
            }
            else if (v > 100f)
            {
                return (int)(v - 90f) * 10;
            }
            else
            {
                return (int)v;
            }
        }

        public SettingHandle<float>        opacity;
        public SettingHandle<int>          interval;
        public SettingHandle<int>          numSteps;
        public SettingHandle<Filters.Type> filter;
        public ColorSetting                minColor;
        public ColorSetting                maxColor;
    }
}
