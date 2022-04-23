using HarmonyLib;
using UnityEngine;

namespace HarderHeck_Mod.Patches
{
    [HarmonyPatch(typeof(Weapon))]
    public class WeaponPatch
    {
        [HarmonyPatch(nameof(Weapon.Start))]
        public static void Postfix(Weapon __instance)
        {
            if (!Plugin.ImpossibleMode.Value) return;
            if (__instance.label == "Mine" || __instance.label == "Grenade") return;
            __instance.maxAmmo -= 1;
            __instance.ammo = __instance.maxAmmo;
        }
    }
    [HarmonyPatch(typeof(WaspBrain), nameof(WaspBrain.Start))]
    public class WaspBrainPatch
    {
        public static void Postfix(WaspBrain __instance)
        {
            if (Plugin.ImpossibleMode.Value) __instance.attackCooldown /= 2;
        }
    }
    [HarmonyPatch(typeof(ExplodingRoller), nameof(ExplodingRoller.TriggerExplosive))]
    public class ExplodingRollerPatch
    {
        public static bool Prefix(ExplodingRoller __instance)
        {
            if (!Plugin.ImpossibleMode.Value) return true;
            if (__instance._triggered)
            {
                return false;
            }
            __instance._triggered = true;
            __instance._timeToExplode = Time.time + 1f;
            return false;
        }
    }
    [HarmonyPatch(typeof(WhispBrain), nameof(WhispBrain.Start))]
    public class WhispBrainPatch
    {
        public static void Prefix(WhispBrain __instance)
        {
            if (!Plugin.ImpossibleMode.Value) return;
            __instance.movementCooldown /= 2;
        }
    }
    [HarmonyPatch(typeof(KhepriBrain), nameof(KhepriBrain.LaunchChargeAttack))]
    public class KhepriBrainPatch
    {
        public static void Postfix(KhepriBrain __instance)
        {
            if (!Plugin.ImpossibleMode.Value) return;
            __instance._shotCooldownTill = Time.time + (__instance.shotCooldown / 2);
        }
    }
    [HarmonyPatch(typeof(ModifierManager), nameof(ModifierManager.GetNonMaxedWavesMods))]
    public class ModifierManagerPatch
    {
        private static readonly List<Modifier> emptyList = new();
        public static bool Prefix(ref List<Modifier> __result)
        {
            if (!Plugin.ImpossibleMode.Value) return true;

            __result = emptyList;
            return false;
        }
    }
    [HarmonyPatch(typeof(ParticleBlade))]
    public class ParticleBladePatch
    {
        [HarmonyPatch(nameof(ParticleBlade.Damage))]
        [HarmonyPrefix]
        public static void DamagePrefix(ParticleBlade __instance)
        {
            if (!Plugin.ImpossibleMode.Value) return;
            if (__instance.ammo > 0f) __instance.ammo -= 1f;
        }
        [HarmonyPatch(nameof(ParticleBlade.Impact))]
        [HarmonyPrefix]
        public static void ImpactPrefix(ParticleBlade __instance)
        {
            if (!Plugin.ImpossibleMode.Value) return;
            if (__instance.ammo > 0f) __instance.ammo -= 1f;
        }
    }
    [HarmonyPatch(typeof(WeaponSpawner), nameof(WeaponSpawner.Start))]
    public class WeaponSpawnerPatch
    {
        public static void Postfix(WeaponSpawner __instance)
        {
            if (!Plugin.ImpossibleMode.Value) return;
            __instance._spawnRateMod = 0.5f;
            __instance.spawnDelay = 6f;
        }
    }
    [HarmonyPatch(typeof(WaveMode), nameof(WaveMode.GetRandomLevels))]
    public class WaveModePatch
    {
        public static bool Prefix(ref List<GameLevel> __result)
        {
            if (!Plugin.ImpossibleMode.Value) return true;
            __result = new List<GameLevel>() { Plugin.GameLevel };
            return false;
        }
    }
    [HarmonyPatch(typeof(GameSettings), nameof(GameSettings.RandomSurvivalStartLevel))]
    public class GameSettingsPatch
    {
        public static bool Prefix(ref GameLevel __result)
        {
            if (!Plugin.ImpossibleMode.Value) return true;
            __result = Plugin.GameLevel;
            return false;
        }
    }
}
