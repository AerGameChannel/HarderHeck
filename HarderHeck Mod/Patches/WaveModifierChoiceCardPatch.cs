using HarmonyLib;
using UnityEngine;

namespace HarderHeck_Mod.Patches
{
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
}
