using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HarderHeck_Mod.Patches
{
    [HarmonyPatch(typeof(Weapon), "Start")]
    public class WeaponPatch
    {
        public static void Postfix(Weapon __instance)
        {
            if (!Plugin.ImpossibleMode.Value) return;
            if (__instance.label == "Mine" || __instance.label == "Grenade") return;
            __instance.maxAmmo -= 1;
            __instance.ammo = __instance.maxAmmo;
        }
    }
    [HarmonyPatch(typeof(WaspBrain), "Start")]
    public class WaspBrainPatch
    {
        public static void Postfix(WaspBrain __instance)
        {
            if (!Plugin.ImpossibleMode.Value) return;
            __instance.attackCooldown /= 2;
        }
    }
    [HarmonyPatch(typeof(ExplodingRoller), "TriggerExplosive")]
    public class ExplodingRollerPatch
    {
        public static bool Prefix(ExplodingRoller __instance)
        {
            if (!Plugin.ImpossibleMode.Value) return true;
            var traverse = Traverse.Create(__instance);
            if (traverse.Field("_triggered").GetValue<bool>())
            {
                return false;
            }
            traverse.Field("_triggered").SetValue(true);
            traverse.Field("_timeToExplode").SetValue(Time.time + 1f);
            return false;
        }
    }
    [HarmonyPatch(typeof(WhispBrain), "Start")]
    public class WhispBrainPatch
    {
        public static void Prefix(WhispBrain __instance)
        {
            if (!Plugin.ImpossibleMode.Value) return;
            __instance.movementCooldown /= 2;
        }
    }
    [HarmonyPatch(typeof(KhepriBrain), "LaunchChargeAttack")]
    public class KhepriBrainPatch
    {
        public static void Postfix(KhepriBrain __instance)
        {
            if (!Plugin.ImpossibleMode.Value) return;
            Traverse.Create(__instance).Field("_shotCooldownTill").SetValue(Time.time + (__instance.shotCooldown / 2));
        }
    }
    [HarmonyPatch(typeof(ModifierManager))]
    public class ModifierManagerPatch
    {
        [HarmonyPatch("GetNonMaxedWavesMods")]
        public static bool Prefix(ModifierManager __instance, ref List<Modifier> __result)
        {
            if (!Plugin.ImpossibleMode.Value) return true;

            __result = Traverse.Create(__instance).Field("_modifiers").GetValue<List<Modifier>>().Where((Modifier m) => m.levelInWaves < m.data.maxLevel && m.data.waves && Plugin.ModifierTitlesList.Contains(m.data.title)).ToList();

            return false;
        }
        [HarmonyPatch("Awake")]
        public static void Postfix(ModifierManager __instance)
        {
            var modifiers = Traverse.Create(__instance).Field("_modifiers").GetValue<List<Modifier>>().Where((Modifier m) => m.levelInWaves < m.data.maxLevel && m.data.waves);
            if (Plugin.ModifierTitlesList.Count > 0) return;

            for(int i = 0; i < 3; i++)
            {
                Plugin.ModifierTitlesList.Add(modifiers.ElementAt(Plugin.Random.Next(0, modifiers.Count() + 1)).data.title);
            }

            Plugin.Log.LogInfo(Plugin.ModifierTitlesList.Join(delimiter: ", "));
        }
        [HarmonyPatch("ResetAllWaveModifiers")]
        public static void Prefix()
        {
            Plugin.ModifierTitlesList.Clear();
        }
    }
}
