using HarmonyLib;

namespace HarderHeck_Mod.Patches
{
    [HarmonyPatch(typeof(WeaponManager), nameof(WeaponManager.ThrowWeapon))]
    public class WeaponManagerPatch
    {
        private static float defaultThrowForce;
        public static void Prefix(WeaponManager __instance)
        {
            defaultThrowForce = __instance.throwForce;
            if (__instance.equippedWeapon.label == "Mine" || __instance.equippedWeapon.label == "Grenade")
            {
                float Multiplier = Plugin.ThrowForceMultiplier;
                int chance = Plugin.Random.Next(0, 101);
                if (chance <= 10)
                {
                    __instance.throwForce *= 0.40f * Multiplier;
                    return;
                }
                else if (chance > 10 && chance <= 40)
                {
                    __instance.throwForce *= 0.60f * Multiplier;
                    return;
                }
                else if (chance > 40 && chance <= 60)
                {
                    __instance.throwForce *= 0.65f * Multiplier;
                    return;
                }
                else
                {
                    __instance.throwForce *= 0.70f * Multiplier;
                    return;
                }
            }
            __instance.throwForce = defaultThrowForce;
        }
        public static void Postfix(WeaponManager __instance)
        {
            __instance.throwForce = defaultThrowForce;
        }
    }
}
