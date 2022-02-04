using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using HarmonyLib;

namespace TrolleyHeck_Mod
{
    [BepInPlugin("com.aer.trolleyheck", "HarderHeck Mod", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {

        private Harmony Harmony;
        internal static System.Random Random = new();
        internal static PluginInfo PluginInfo;
        internal static ManualLogSource Log;
        internal static bool CanTrollAgain = true;
        private void Awake()
        {
            Log = Logger;
            PluginInfo = Info;
            Harmony = new Harmony($"com.aer.harderheck");
            Harmony.PatchAll();
            Logger.LogInfo("HarderHeck Mod successfully loaded! Made by Aer");
        }
    }
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
    [HarmonyPatch(typeof(VersionNumberTextMesh), "Start")]
    public class VersionNumberTextMeshPatch
    {
        public static void Postfix(VersionNumberTextMesh __instance)
        {
            __instance.textMesh.text += $"\n<color=red>HarderHeck Mod v{Plugin.PluginInfo.Metadata.Version} by Aer</color>";
        }
    }
    [HarmonyPatch(typeof(WaveModifierChoiceCard), nameof(WaveModifierChoiceCard.SetupCard))]
    public class WaveModifierChoiceCardPatch
    {
        public static void Postfix(WaveModifierChoiceCard __instance)
        {
            __instance.mapNameText.text = "??? Unknown ???";
            __instance.perkNameText.text = "??? Unknown ???";
            __instance.perkDescriptionText.text = "ERROR: DESCRIPTION NOT FOUND";
            __instance.modifierIconImage.sprite = default;
            __instance.modifierIconImage.color = new Color(0, 0, 0, 0);
            __instance.mapImage.sprite = default;
            __instance.mapImage.color = new Color(0, 0, 0, 0);
        }
    }
    [HarmonyPatch(typeof(WeaponManager), "ThrowWeapon")]
    public class WeaponManagerPatch
    {
        public static float defaultThrowForce;
        public static bool changedForce = false;
        public static void Prefix(WeaponManager __instance)
        {
            if (__instance.equippedWeapon.type.Contains(Weapon.WeaponType.Explosive))
            {
                defaultThrowForce = __instance.throwForce;
                var chance = Plugin.Random.Next(0, 101);
                if (chance <= 10)
                {
                    __instance.throwForce *= 0.40f;
                }
                else if(chance > 10 && chance <= 40)
                {
                    __instance.throwForce *= 0.60f;
                }
                else if(chance > 40 && chance <= 60)
                {
                    __instance.throwForce *= 0.65f;
                }
                else if(chance > 60)
                {
                    __instance.throwForce *= 0.80f;
                }
                changedForce = true;
            }
        }
        public static void Postfix(WeaponManager __instance)
        {
            if(changedForce)
            {
                __instance.throwForce = defaultThrowForce;
            }
        }
    }
}
