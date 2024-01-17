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





    /// <summary>
    /// The mod's settings.
    /// </summary>
    [FileLocation(Mod.ModName)]
    public class ModSettings : ModSetting
    {
        private bool ForceLowGlobalQualityValue;
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
        [SettingsUISlider(min = 0f, max = 100f, step = 1f, unit = "percentage", scaleDragVolume = true, scalarMultiplier = 100f)]
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
        [SettingsUISlider(min = 0f, max = 100f, step = 1f, unit = "percentage", scaleDragVolume = true, scalarMultiplier = 100f)]
        public float Contrast
        {
            get { return GlobalVariables.Instance.Contrast; }
            set
            {
                GlobalVariables.Instance.Contrast = value;
                SaveToFileIn();
            }
        }


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

        /// <summary>
        /// Restores mod settings to default.
        /// </summary>
        public override void SetDefaults()
        {
         


        }

        public void SaveToFileIn()
        {
            string assemblyDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ColorAdjustments.xml");
             GlobalVariables.SaveToFile(assemblyDirectory);
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


