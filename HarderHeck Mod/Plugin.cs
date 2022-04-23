using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace HarderHeck_Mod
{
    [BepInPlugin("com.aer.harderheck", "HarderHeck Mod", "1.3.0")]
    public class Plugin : BaseUnityPlugin
    {	
        internal static float ThrowForceMultiplier = 0f;
        private Harmony Harmony;
        internal static System.Random Random = new();
        internal static PluginInfo PluginInfo;
        internal static ManualLogSource Log;
        internal static bool CanTrollAgain = true;
        internal static GameLevel GameLevel;
		public static ConfigEntry<bool> ImpossibleMode { get; private set; }
#pragma warning disable IDE0051
        private void Awake()
#pragma warning restore IDE0051
        {
            Harmony = new Harmony($"com.aer.harderheck");
            ImpossibleMode = Config.Bind("Features", "ImpossibleMode", false, "The game will become EVEN. MORE. HARDER.");


            Log = Logger;
            PluginInfo = Info;
            Harmony.PatchAll();
            Logger.LogInfo("HarderHeck Mod successfully loaded! Made by Aer");

            if (!ImpossibleMode.Value) StartCoroutine(CheckModifiers());
            else StartCoroutine(SetGameLevel());
        }

        private IEnumerator<WaitForSeconds> CheckModifiers()
        {
            for (; ; )
            {
                ThrowForceMultiplier = ModifierManager.instance.GetModLevel("muscleArms") < 1 ? ThrowForceMultiplier = 1f : ModifierManager.instance.GetModLevel("muscleArms") switch
                {
                    1 => 1.40f,
                    2 => 1.70f,
                    _ => 1f
                };
                yield return new WaitForSeconds(1f);
            }
        }
        private IEnumerator<WaitForSeconds> SetGameLevel()
        {
            yield return new WaitForSeconds(5);
            var filter = new string[] { "launch", "humdrum", "ant domain", "bouncer" };
            var levels = GameSettings.Instance.GetAvailableSurvivalLevels().Where(l => filter.Contains(l.label.ToLower())).ToList();
            GameLevel = levels[Random.Next(0, levels.Count + 1)];
            yield break;
        }
    }
}
