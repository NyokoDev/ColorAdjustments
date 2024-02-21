namespace ColorAdjustmentsMod.Mod
{

    using Colossal.Logging;
    using Game;
    using Game.Modding;
    using ReRenderingOptions.Systems;
    using Game.Tools;
    using UnityEngine.Rendering.HighDefinition;
    using UnityEngine.Rendering;
    using ReRenderingOptions;
    using System;
    using ColorAdjustmentsMod.Settings;
    using System.IO;
    using UnityEngine;
    using Game.UI.InGame;
    using System.Reflection;
    using Version = System.Version;

    public sealed class Mod : IMod
    {
        /// <summary>
        /// Mod properties.
        /// </summary>
        public const string ModName = "ColorAdjustments";                    
        public static Mod Instance { get; private set; }

        public static string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(); // Obtains the version
        internal ILog Log { get; private set; }

        public bool Compatible;
        public void OnLoad()
        {
            Instance = this;
            Log = LogManager.GetLogger(ModName);
            Log.Info("setting logging level to Debug");
            Log.effectivenessLevel = Level.Debug;
            Log.Info("loading");
        }

        /// <summary>
        /// Gets the mod's active settings configuration.
        /// </summary>
        internal ModSettings ActiveSettings { get; private set; }

        /// <summary>
        /// Called by the game when the game world is created. 
        /// </summary>
        /// <param name="updateSystem">Game update system.</param>
        public void OnCreateWorld(UpdateSystem updateSystem)
        {

            string gameVersion = Application.version.ToString();
     

            if (gameVersion.Equals("1.0.19f1"))
            {
                Compatible = true;
            }
            if (Compatible) {
                ActiveSettings = new(this);
                ActiveSettings.RegisterInOptionsUI();
                Localization.LoadTranslations(ActiveSettings, Log);
                updateSystem.UpdateAfter<System>(SystemUpdatePhase.GameSimulation);
                updateSystem.UpdateAfter<EnablerVolumeSystem>(SystemUpdatePhase.GameSimulation);
                updateSystem.UpdateAfter<System>(SystemUpdatePhase.GameSimulation);
                string localLowDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                localLowDirectory = Path.Combine(localLowDirectory, "..", "LocalLow");
                string assemblyDirectory = Path.Combine(localLowDirectory, "Colossal Order", "Cities Skylines II", "Mods", "ColorAdjustments");
                string settingsFilePath = Path.Combine(assemblyDirectory, "ColorAdjustments.xml");
                ///
                Console.WriteLine("ColorAdjustments " + Version, ConsoleColor.Blue, ConsoleColor.Blue);
                Console.WriteLine("Support: https://discord.gg/5gZgRNm29e", ConsoleColor.Blue, ConsoleColor.Blue);
                Console.WriteLine("Donate: https://shorturl.at/hmpCW", ConsoleColor.Blue, ConsoleColor.Blue);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" ────────────────────────────────── ");
                Console.ResetColor();
                ///
                UnityEngine.Debug.Log("ColorAdjustments exported settings located at:" + settingsFilePath); // Exports mod settings to an additional file on load.

                GlobalVariables.LoadFromFile(settingsFilePath);
            }
            else
            {
                Console.WriteLine("ColorAdjustments is not compatible with your game version. Contact the developer: https://discord.gg/5gZgRNm29e");
            }

        }
        /// <summary>
        /// Called by the game when the mod is disposed of.
        /// </summary>
        public void OnDispose()
        {
            Log.Info("disposing");
            Instance = null;
        }
    }
}