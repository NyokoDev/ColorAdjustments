using ColorAdjustmentsMod.Settings;
using Game.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection; // Add this using directive for reflection
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine.Rendering.HighDefinition;

namespace ReRenderingOptions.Systems
{
    internal class System : SystemBase
    {
        private UnityEngine.Rendering.HighDefinition.ColorAdjustments colorAdjustments;

        protected override void OnUpdate()
        {
            // Use reflection to get the private ColorAdjustments field from LightingSystem
            Type lightingSystemType = typeof(LightingSystem);
            FieldInfo colorAdjustmentsField = lightingSystemType.GetField("m_ColorAdjustments", BindingFlags.NonPublic | BindingFlags.Instance);

            if (colorAdjustmentsField != null)
            {
                LightingSystem lightingSystem = World.GetExistingSystemManaged<LightingSystem>();
                colorAdjustments = (UnityEngine.Rendering.HighDefinition.ColorAdjustments)colorAdjustmentsField.GetValue(lightingSystem);

                if (colorAdjustments != null)
                {
                    // Set the exposure value to 0 using Override method
                    colorAdjustments.postExposure.Override(GlobalVariables.Instance.PostExposure);
                    colorAdjustments.contrast.Override(GlobalVariables.Instance.Contrast);

                    // Accessing the postExposure field
                    float exposureValue = colorAdjustments.postExposure.value;
                    float contrastvalue = colorAdjustments.contrast.value;

                    UnityEngine.Debug.Log("Contrast Value: " + contrastvalue);
                    // Do something with the exposure value, for example, log it
                    UnityEngine.Debug.Log("Post Exposure Value: " + exposureValue);
                }
            }
        }
    }
}
