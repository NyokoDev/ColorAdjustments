using HarmonyLib;

namespace UnityEngine.Rendering.HighDefinition
{
    [HarmonyPatch(typeof(WhiteBalance))]
    [HarmonyPatch("IsActive")]
    class WhiteBalancePatch
    {
        static bool Prefix()
        {
            UnityEngine.Debug.Log("WhiteBalance.IsActive() has been prevented");
            return false; // Return false to indicate that the original method should not be executed
        }
    }
}
