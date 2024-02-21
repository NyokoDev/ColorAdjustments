using BepInEx;
using Game.Vehicles;
using Game;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using ReRenderingOptions.Systems;
using Game.Common;
using Game.PSI;
using Game.Rendering;
using Game.Rendering.CinematicCamera;
using Game.SceneFlow;
using Game.UI.InGame;
using HarmonyLib;
using static UnityEngine.MonoBehaviour;
using Colossal.UI;
using UnityEngine.Rendering.HighDefinition;
using ReRenderingOptions;

#if BEPINEX_V6
    using BepInEx.Unity.Mono;
#endif

namespace ColorAdjustmentsMod.Mod
{
    [BepInPlugin(GUID, "ColorAdjustments", "1.3")]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "com.nyoko.coloradjustments";

        private Mod _mod;


        public void Awake()
        {

            _mod = new();
            _mod.OnLoad();

            // Apply Harmony patches.
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);
        }


        /// <summary>
        /// Harmony postfix to <see cref="SystemOrder.Initialize"/> to substitute for IMod.OnCreateWorld.
        /// </summary>
        /// <param name="updateSystem"><see cref="GameManager"/> <see cref="UpdateSystem"/> instance.</param>
        [HarmonyPatch(typeof(SystemOrder), nameof(SystemOrder.Initialize))]
        [HarmonyPostfix]
        private static void InjectSystems(UpdateSystem updateSystem) => Mod.Instance.OnCreateWorld(updateSystem);

    }


}