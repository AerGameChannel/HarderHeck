using HarmonyLib;


namespace HarderHeck_Mod.Patches
{
    [HarmonyPatch(typeof(WaveModeHud), "Update")]
    public class LifeCounterPatch
    {
        public static void Postfix(WaveModeHud __instance)
        {
            __instance.livesCounter.text = $"<size=22><color=#f7b55e>forget about this</color></size>";
        }
    }
}
