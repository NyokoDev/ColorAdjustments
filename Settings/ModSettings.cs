// <copyright file="ModSettings.cs" company="algernon (K. Algernon A. Sheppard)">
// Copyright (c) algernon (K. Algernon A. Sheppard). All rights reserved.
// Licensed under the Apache Licence, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace ColorAdjustmentsMod.Mod
{
    using System.Xml.Serialization;
    using Colossal.IO.AssetDatabase;
    using ReRenderingOptions;

    using Game.Modding;
    using Game.Settings;
    using UnityEngine;
    using System;
    using Game.UI.Widgets;
    using System.Collections.Generic;

    using UnityEngine.Scripting;
    using Game.Rendering;
    using Unity.Entities;

    using Game.UI;
    using System.IO;
    using ColorAdjustmentsMod.Settings;
    using System.Diagnostics;





    /// <summary>
    /// The mod's settings.
    /// </summary>
    [FileLocation(Mod.ModName)]
    public class ModSettings : ModSetting
    {
        public float LowestValueSet = 0.001f;
        public float HighestValueSet = 5f;
        public static float Fraction = 1f;

        /// <summary>
        /// Boolean to call after settings load.
        /// </summary>
        public bool Loaded;




        /// <summary>
        /// Initializes a new instance of the <see cref="ModSettings"/> class.
        /// </summary>  
        /// <param name="mod"><see cref="IMod"/> instance.</param>
        public ModSettings(IMod mod)
            : base(mod)
        {
        }


        [SettingsUISection("Advanced")]
        [SettingsUISlider(min = -500f, max = 1000f, step = 1f, unit = "percentage", scaleDragVolume = true, scalarMultiplier = 100f)]
        public float PostExposure
        {
            get { return GlobalVariables.Instance.PostExposure; }
            set
            {
                GlobalVariables.Instance.PostExposure = value;
                SaveToFileIn();
            }
        }


        [SettingsUISection("Advanced")]
        [SettingsUISlider(min = -10000f, max = 10000f, step = 10.00f, unit = "integer", scaleDragVolume = true, scalarMultiplier = 100f)]
        public float Contrast
        {
            get { return GlobalVariables.Instance.Contrast; }
            set
            {
                GlobalVariables.Instance.Contrast = value;
                SaveToFileIn();
            }
        }

        [SettingsUISection("Advanced")]
        [SettingsUISlider(min = -10000f, max = 10000f, step = 10.00f, unit = "integer", scaleDragVolume = true, scalarMultiplier = 100f)]
        public float HueShift
        {
            get { return GlobalVariables.Instance.hueShift; }
            set
            {
                GlobalVariables.Instance.hueShift = value;
                SaveToFileIn();
            }
        }

        [SettingsUISection("Advanced")]
        [SettingsUISlider(min = -10000f, max = 10000f, step = 10.00f, unit = "integer", scaleDragVolume = true, scalarMultiplier = 100f)]
        public float Saturation
        {
            get { return GlobalVariables.Instance.Saturation; }
            set
            {
                GlobalVariables.Instance.Saturation = value;
                SaveToFileIn();
            }
        }

#if DEBUG
        [SettingsUISection("Advanced")]
        [SettingsUISlider(min = -100f, max = 100f, step = 10.00f, unit = "integer", scaleDragVolume = true, scalarMultiplier = 1f)]
        public float Temperature
        {
            get { return GlobalVariables.Instance.Temperature; }
            set
            {
                GlobalVariables.Instance.Temperature = value;
                SaveToFileIn();
            }
        }

        [SettingsUISection("Advanced")]
        [SettingsUISlider(min = -100f, max = 100f, step = 10.00f, unit = "integer", scaleDragVolume = true, scalarMultiplier = 1f)]
        public float Tint
        {
            get { return GlobalVariables.Instance.Tint; }
            set
            {
                GlobalVariables.Instance.Tint = value;
                SaveToFileIn();
            }
        }
#endif

        /// <summary>

        /// <summary>
        /// Sets a value indicating whether the mod's settings should be reset.
        /// </summary>
        [XmlIgnore]
        [SettingsUIButton]
        [SettingsUISection("ResetModSettings")]
        [SettingsUIConfirmation]
        public bool ResetModSettings
        {
            set
            {
                // Apply defaults.
                SetDefaults();

                // Save.
                ApplyAndSave();
            }
        }

        [SettingsUIButton]
        [SettingsUISection("Advanced")]
        public bool OpenLocationButton
        {
            set
            {
                OpenLocation();

            }
        }

        [SettingsUIButton]
        [SettingsUISection("Advanced")]
        public bool Support
        {
            set
            {
                OpenDiscordInvite();

            }
        }


        /// <summary>
        /// Restores mod settings to default.
        /// </summary>
        public override void SetDefaults()
        {
            GlobalVariables.Instance.hueShift = 1f;
            GlobalVariables.Instance.PostExposure = 1f;
            GlobalVariables.Instance.Saturation = 1f;
            GlobalVariables.Instance.Contrast = 1f;
            SaveToFileIn();


        }

        public void OpenLocation()
        {
            try
            {

                string localLowDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                localLowDirectory = Path.Combine(localLowDirectory, "..", "LocalLow");
                string assemblyDirectory = Path.Combine(localLowDirectory, "Colossal Order", "Cities Skylines II", "Mods", "ColorAdjustments");
                string settingsFilePath = Path.Combine(assemblyDirectory, "ColorAdjustments.xml");


                // Check if the file exists
                if (File.Exists(settingsFilePath))
                {

                    Process.Start(settingsFilePath);
                }
                else
                {
                    Console.WriteLine("File not found: " + settingsFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void OpenDiscordInvite()
        {
            try
            {
                // Replace the Discord invite link with your actual link
                string discordInviteLink = "https://discord.gg/5JcaKwDBHn";

                // Use Process.Start to open the URL in the default web browser
                System.Diagnostics.Process.Start(discordInviteLink);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }




        public void SaveToFileIn()
        {
            string localLowDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            localLowDirectory = Path.Combine(localLowDirectory, "..", "LocalLow");
            string assemblyDirectory = Path.Combine(localLowDirectory, "Colossal Order", "Cities Skylines II", "Mods", "ColorAdjustments");
            Directory.CreateDirectory(assemblyDirectory);
            string settingsFilePath = Path.Combine(assemblyDirectory, "ColorAdjustments.xml");
            GlobalVariables.SaveToFile(settingsFilePath);
        }

        /// <summary>
        /// Sets settings after succesfully loading them from XML file.
        /// </summary>
        public void SetSettings()
        {

        }

        /// <summary>
        /// Override apply method to apply settings on setting change (Changing a slider will trigger it).
        /// </summary>
        public override void Apply()
        {
            base.Apply();




        }


    }
}





