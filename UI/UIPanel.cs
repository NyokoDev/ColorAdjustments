﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

namespace ColorAdjustments.UI
{
    using ColorAdjustmentsMod.XML;
    using System.IO;
    using System.Runtime.Remoting;

    public class SliderPanel : MonoBehaviour
    {
        private float slider1Value = GlobalVariables.Instance.PostExposure;
        private float slider2Value = GlobalVariables.Instance.Contrast;
        private float slider3Value = GlobalVariables.Instance.hueShift;
        private float slider4Value = GlobalVariables.Instance.Saturation;

        private float LongitudeValue = GlobalVariables.Instance.Longitude;
        private float LatitudeValue = GlobalVariables.Instance.Latitude;

        private bool panelVisible = false;

        private Rect panelRect = new Rect(10, 10, 400, 400);
        private Rect buttonRect = new Rect(10, 10, 150, 30);
        private Vector2 panelOffset;
        private Vector2 buttonOffset;
        private bool isDraggingPanel = false;
        private bool isDraggingButton = false;
        private Vector2 buttonDragStartPosition;

        public static bool buttonVisible { get; set; }


        /// <summary>
        /// Toggle panel with ALT + C
        /// </summary>
        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    panelVisible = !panelVisible;
                }
            }
        }


        private void OnGUI()
        {
            if (buttonVisible)
            {
                buttonRect = GUI.Window(1, buttonRect, ButtonWindow, "ColorAdjustments");
            }

            if (panelVisible)
            {
                GUIStyle blackWindowStyle = new GUIStyle(GUI.skin.window);
                blackWindowStyle.normal.textColor = Color.white;

                panelRect = GUI.Window(0, panelRect, PanelWindow, "ColorAdjustments", blackWindowStyle);

            }
            // else block can be omitted if you don't want to do anything when panelVisible is false
        }


        void ButtonWindow(int windowID)
        {

            // Draw the button
            if (GUI.Button(new Rect(0, 0, buttonRect.width, buttonRect.height), "ColorAdjustments"))
            {
                panelVisible = !panelVisible; // Toggle panel visibility
            }

            if (Event.current.type == EventType.MouseDown && Event.current.button == 1)
            {
                isDraggingButton = true;
                buttonDragStartPosition = Event.current.mousePosition - buttonRect.position;
            }

            if (isDraggingButton)
            {
                buttonRect.position = Event.current.mousePosition - buttonDragStartPosition;
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
        
        void PanelWindow(int windowID)
        {
            float YControl = 20;
            GUI.DragWindow(new Rect(0, 0, panelRect.width, 20));

            GUI.Label(new Rect(20, YControl, 100, 20), "Post Exposure");
            Rect Slider1Rect = new Rect(20, 40, 300, 20);
            slider1Value = GUI.HorizontalSlider(Slider1Rect, slider1Value, -20f, 30f);
            GlobalVariables.Instance.PostExposure = slider1Value;

            GUI.Label(new Rect(20, YControl += 30, 100, 20), "Contrast");
            Rect Slider2Rect = new Rect(20, YControl += 30, 300, 20);
            slider2Value = GUI.HorizontalSlider(Slider2Rect, slider2Value, -10000f, 10000f);
            slider2Value = Mathf.Round(slider2Value * 1000f) / 1000f; // Set step size
            GlobalVariables.Instance.Contrast = slider2Value;

            GUI.Label(new Rect(20, YControl += 30, 100, 20), "Hue Shift");
            Rect Slider3Rect = new Rect(20, YControl += 30, 300, 20);
            slider3Value = GUI.HorizontalSlider(Slider3Rect, slider3Value, -10000f, 10000f);
            slider3Value = Mathf.Round(slider3Value * 1000f) / 1000f; // Set step size
            GlobalVariables.Instance.hueShift = slider3Value;

            GUI.Label(new Rect(20, YControl += 30, 100, 20), "Saturation");
            Rect Slider4Rect = new Rect(20, YControl += 30, 300, 20);
            slider4Value = GUI.HorizontalSlider(Slider4Rect, slider4Value, -10000f, 10000f);
            slider4Value = Mathf.Round(slider4Value * 1000f) / 1000f; // Set step size
            GlobalVariables.Instance.Saturation = slider4Value;
            SaveToFileIn();

            // Planetary Settings

            GUI.skin.label.wordWrap = false;
            GUI.skin.label.fontSize = 14; // Or any other size you prefer
            GUI.Label(new Rect(20, YControl += 30, 100, 20), "Planetary Settings");

            GUI.Label(new Rect(20, YControl += 30, 100, 20), "Longitude");
            Rect LongitudeRect = new Rect(20, YControl += 20, 300, 20);
            LongitudeValue = GUI.HorizontalSlider(LongitudeRect, LongitudeValue, -10000f, 10000f);
            LongitudeValue = Mathf.Round(LongitudeValue * 1000f) / 1000f; // Set step size
            GlobalVariables.Instance.Longitude = LongitudeValue;


            GUI.Label(new Rect(20, YControl += 30, 100, 20), "Latitude");
            Rect LatitudeRect = new Rect(20, YControl += 20, 300, 20);
            LatitudeValue = GUI.HorizontalSlider(LatitudeRect, LatitudeValue, -10000f, 10000f);
            LatitudeValue = Mathf.Round(LatitudeValue * 1000f) / 1000f; // Set step size
            GlobalVariables.Instance.Latitude = LatitudeValue;

 



            string newText1Value = GUI.TextField(new Rect(330, Slider1Rect.y, 40, 20), slider1Value.ToString());
            if (float.TryParse(newText1Value, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float parsedValue1))
            {
                slider1Value = parsedValue1;
            }

            string newText2Value = GUI.TextField(new Rect(330, Slider2Rect.y, 40, 20), slider2Value.ToString());
            if (float.TryParse(newText2Value, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float parsedValue2))
            {
                slider2Value = parsedValue2;
            }

            string newText3Value = GUI.TextField(new Rect(330, Slider3Rect.y, 40, 20), slider3Value.ToString());
            if (float.TryParse(newText3Value, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float parsedValue3))
            {
                slider3Value = parsedValue3;
            }

            string newText4Value = GUI.TextField(new Rect(330, Slider4Rect.y, 40, 20), slider4Value.ToString());
            if (float.TryParse(newText4Value, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float parsedValue4))
            {
                slider4Value = parsedValue4;
            }

            string LongitudeValueParsed = GUI.TextField(new Rect(330, LongitudeRect.y, 40, 20), LongitudeValue.ToString());
            if (float.TryParse(LongitudeValueParsed, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float parsedValue5))
            {
                LongitudeValue = parsedValue5;
            }

            string LatitudeValueParsed = GUI.TextField(new Rect(330, LatitudeRect.y, 40, 20), LatitudeValue.ToString());
            if (float.TryParse(LatitudeValueParsed, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float parsedValue6))
            {
                LatitudeValue = parsedValue6;
            }



            if (Event.current.type == EventType.MouseDrag && Event.current.button == 0)
            {
                isDraggingPanel = true;
                panelOffset = panelRect.position - Event.current.mousePosition;
            }
            else if (Event.current.type == EventType.MouseUp)
            {
                isDraggingPanel = false;
            }
        }
    }
}
