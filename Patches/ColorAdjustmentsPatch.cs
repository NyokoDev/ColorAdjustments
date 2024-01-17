using HarmonyLib;

namespace UnityEngine.Rendering.HighDefinition
{
    [HarmonyPatch(typeof(ColorAdjustments))]
    [HarmonyPatch("IsActive")]
    class ColorAdjustmentsPatch
    {
        static void Postfix(ref bool __result)
        {
            // Always set the result to true
            UnityEngine.Debug.Log("Patched ColorAdjustments");
            __result = true;
        }
    }
}
