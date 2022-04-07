using HarmonyLib;

namespace HarderHeck_Mod.Patches
{
    [HarmonyPatch(typeof(WeaponManager), "ThrowWeapon")]
    public class WeaponManagerPatch
    {
        private static float defaultThrowForce;
        public static void Prefix(WeaponManager __instance)
        {
            if (__instance.equippedWeapon.type.Contains(Weapon.WeaponType.Explosive))
            {
                defaultThrowForce = __instance.throwForce;
                float Multiplier = Plugin.ThrowForceMultiplier;
                int chance = Plugin.Random.Next(0, 101);
                if (chance <= 10)
                {
                    __instance.throwForce *= 0.40f * Multiplier;
                }
                else if (chance > 10 && chance <= 40)
                {
                    __instance.throwForce *= 0.60f * Multiplier;
                }
                else if (chance > 40 && chance <= 60)
                {
                    __instance.throwForce *= 0.65f * Multiplier;
                }
                else if (chance > 60)
                {
                    __instance.throwForce *= 0.70f * Multiplier;
                }
            }
        }
        public static void Postfix(WeaponManager __instance)
        {
            __instance.throwForce = defaultThrowForce;
        }
    }
}
