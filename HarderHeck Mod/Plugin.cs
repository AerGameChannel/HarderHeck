using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace HarderHeck_Mod
{
    [BepInPlugin("com.aer.harderheck", "HarderHeck Mod", "1.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static float ThrowForceMultiplier = 1f;
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
            StartCoroutine(CheckModifiers());
        }

        private IEnumerator<WaitForSeconds> CheckModifiers()
        {
            for (; ; )
            {
                ThrowForceMultiplier = ModifierManager.instance.GetModLevel("muscleArms") < 1f ? ThrowForceMultiplier = 1f : ModifierManager.instance.GetModLevel("muscleArms") switch
                {
                    1 => 1.40f,
                    2 => 1.70f,
                    _ => 1f
                };
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
