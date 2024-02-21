using ColorAdjustmentsMod.Settings;
using Game.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection; // Add this using directive for reflection
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace ReRenderingOptions.Systems
{
    internal class EnablerVolumeSystem : SystemBase
    {
        /// <summary>
        /// Sets InitializedVolume to true on Game Simulation to obtain WhiteBalance.
        /// </summary>
        protected override void OnUpdate()
        {
            System.InitializedVolume = true; 
        }
    }
}