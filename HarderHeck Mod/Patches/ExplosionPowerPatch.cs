using HarmonyLib;
using UnityEngine;

namespace HarderHeck_Mod.Patches
{
    [HarmonyPatch(typeof(Explosion), "Start")]
    public class ExplosionPowerPatch
    {
        public static void Prefix(Explosion __instance)
        {
            var chance = Plugin.Random.Next(0, 1001);
            if (chance >= 1000)
            {

                if (!Plugin.CanTrollAgain)
                {
                    Plugin.CanTrollAgain = true;
                    return;
                }
                __instance.deathRadius *= 2008;
                __instance.knockBackRadius *= 2008;
                __instance.ignorePerks = true;
                __instance.transform.localScale *= 2008f;
                Traverse.Create(__instance).Field("_explosionSizeMod").SetValue(2008f);
                Traverse.Create(__instance).Field("_playerDeathRadius").SetValue(2008f);
                Announcer.instance.Announce("<color=#11ff00>YOU'VE BEEN TROLLED</color>", new Color(255, 188, 43), true);
                Plugin.CanTrollAgain = false;
            }
            return;
        }
    }
}
