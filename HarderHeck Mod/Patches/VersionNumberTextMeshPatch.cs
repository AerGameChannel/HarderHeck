using HarmonyLib;

namespace HarderHeck_Mod.Patches
{
    [HarmonyPatch(typeof(VersionNumberTextMesh), "Start")]
    public class VersionNumberTextMeshPatch
    {
        public static void Postfix(VersionNumberTextMesh __instance)
        {
            string isImpossibleMode = "";
            if (Plugin.ImpossibleMode.Value) isImpossibleMode = "IMPOSSIBLE MODE";
            __instance.textMesh.text += $"\n<color=red>HarderHeck Mod v{Plugin.PluginInfo.Metadata.Version} by Aer\n{isImpossibleMode}</color>";
        }
    }
}
