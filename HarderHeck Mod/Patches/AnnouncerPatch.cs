using HarmonyLib;

namespace HarderHeck_Mod.Patches
{

    [HarmonyPatch(typeof(Announcer), nameof(Announcer.AnnounceHealth))]
    public class AnnouncerPatch
    {
        public static void Prefix(ref string text)
        {
            if (text == "-1")
            {
                text = "<size=16>Skill Issue</size>";
            }
        }
    }
}
