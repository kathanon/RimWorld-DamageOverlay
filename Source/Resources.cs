using UnityEngine;
using Verse;

namespace DamageOverlay
{
    [StaticConstructorOnStartup]
    public class Resources
    {
        public static Texture2D Icon = ContentFinder<Texture2D>.Get("DamageOverlay");
    }
}