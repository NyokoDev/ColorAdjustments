using System;
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
                panelRect = GUI.Window(0, panelRect, PanelWindow, "ColorAdjustments");
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
            GUI.DragWindow(new Rect(0, 0, panelRect.width, 20));

            GUI.Label(new Rect(20, 20, 100, 20), "Post Exposure");
            slider1Value = GUI.HorizontalSlider(new Rect(20, 40, 300, 20), slider1Value, -500f, 1000f);
            GlobalVariables.Instance.PostExposure = slider1Value;

            GUI.Label(new Rect(20, 60, 100, 20), "Contrast");
            slider2Value = GUI.HorizontalSlider(new Rect(20, 80, 300, 20), slider2Value, -10000f, 10000f);
            slider2Value = Mathf.Round(slider2Value * 1000f) / 1000f; // Set step size
            GlobalVariables.Instance.Contrast = slider2Value;

            GUI.Label(new Rect(20, 100, 100, 20), "Hue Shift");
            slider3Value = GUI.HorizontalSlider(new Rect(20, 120, 300, 20), slider3Value, -10000f, 10000f);
            slider3Value = Mathf.Round(slider3Value * 1000f) / 1000f; // Set step size
            GlobalVariables.Instance.hueShift = slider3Value;

            GUI.Label(new Rect(20, 140, 100, 20), "Saturation");
            slider4Value = GUI.HorizontalSlider(new Rect(20, 160, 300, 20), slider4Value, -10000f, 10000f);
            slider4Value = Mathf.Round(slider4Value * 1000f) / 1000f; // Set step size
            GlobalVariables.Instance.Saturation = slider4Value;
            SaveToFileIn();


            //Position the below to the right of the above
            string newText1Value = GUI.TextField(new Rect(330, 40, 40, 20), slider1Value.ToString());
            slider1Value = float.Parse(newText1Value);
            string newText2Value = GUI.TextField(new Rect(330, 80, 40, 20), slider2Value.ToString());
            slider2Value = float.Parse(newText2Value);
            string newText3Value = GUI.TextField(new Rect(330, 120, 40, 20), slider3Value.ToString());
            slider3Value = float.Parse(newText3Value);
            string newText4Value = GUI.TextField(new Rect(330, 160, 40, 20), slider4Value.ToString());
            slider4Value = float.Parse(newText4Value);

     


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
