using Game.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;
using ColorAdjustmentsMod.XML;
using ColorAdjustments.UI;

namespace ColorAdjustments.Systems
{

    internal partial class PostProcessSystem : SystemBase
    {


        private UnityEngine.Rendering.HighDefinition.ColorAdjustments colorAdjustments;
        private PhotoModeRenderSystem PhotoModeRenderSystem;

        public static bool InitializedVolume = false;
        public static bool PanelInitialized = false;

        public static bool Panel = true;
        protected override void OnUpdate()
        {

            if (Panel)
            {
                GameObject newGameObject = new GameObject();
                newGameObject.AddComponent<SliderPanel>();
                PanelInitialized = true;
                Panel = false;
            }



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
                    colorAdjustments.hueShift.Override(GlobalVariables.Instance.hueShift);
                    colorAdjustments.saturation.Override(GlobalVariables.Instance.Saturation);

                    // Accessing the postExposure field
                    float exposureValue = colorAdjustments.postExposure.value;
                    float contrastvalue = colorAdjustments.contrast.value;



                    /// White Balance retrieval
                    Type photoModeRenderSystemType = typeof(PhotoModeRenderSystem);
                    FieldInfo whiteBalanceField = photoModeRenderSystemType.GetField("m_WhiteBalance", BindingFlags.NonPublic | BindingFlags.Instance);

                    if (whiteBalanceField != null) // Checking if the field is found
                    {
                        PhotoModeRenderSystem photoModeRenderSystemInstance = World.GetExistingSystemManaged<PhotoModeRenderSystem>();
                        if (photoModeRenderSystemInstance != null)
                        {
                            UnityEngine.Rendering.HighDefinition.WhiteBalance whiteBalanceValue = (UnityEngine.Rendering.HighDefinition.WhiteBalance)whiteBalanceField.GetValue(photoModeRenderSystemInstance);

#if DEBUG
                            // Using White Balance same as ColorAdjustments.
                            if (whiteBalanceValue != null)
                            {

                                whiteBalanceValue.temperature.Override(GlobalVariables.Instance.Temperature);
                                whiteBalanceValue.tint.Override(GlobalVariables.Instance.Tint);
                            }
#endif



                        }
                    }
                }
            }
        }
    }
}