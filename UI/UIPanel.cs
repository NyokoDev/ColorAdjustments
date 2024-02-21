using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

using UnityEngine;

using UnityEngine;

using UnityEngine;

namespace ColorAdjustments.UI
{
    using ColorAdjustmentsMod.Settings;
    using System.Runtime.Remoting;

    public class SliderPanel : MonoBehaviour
    {
        private float slider1Value = GlobalVariables.Instance.PostExposure;
        private float slider2Value = GlobalVariables.Instance.Contrast;
        private float slider3Value = GlobalVariables.Instance.hueShift;
        private float slider4Value = GlobalVariables.Instance.Saturation;

        private bool panelVisible = false;

        private Rect panelRect = new Rect(10, 10, 300, 200);
        private Rect buttonRect = new Rect(10, 10, 150, 30);
        private Vector2 panelOffset;
        private Vector2 buttonOffset;
        private bool isDraggingPanel = false;
        private bool isDraggingButton = false;
        private Vector2 buttonDragStartPosition;


        /// <summary>
        /// Toggle panel with ALT 
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
            buttonRect = GUI.Window(1, buttonRect, ButtonWindow, "ColorAdjustments");
            if (panelVisible)
            {
                panelRect = GUI.Window(0, panelRect, PanelWindow, "ColorAdjustments");
            }
            else
            {
               
            }
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
    
        void PanelWindow(int windowID)
        {
            GUI.DragWindow(new Rect(0, 0, panelRect.width, 20));

            GUI.Label(new Rect(20, 20, 100, 20), "Post Exposure");
            slider1Value = GUI.HorizontalSlider(new Rect(20, 40, 100, 20), slider1Value, 0f, 100f);
            GlobalVariables.Instance.PostExposure = slider1Value;

            GUI.Label(new Rect(20, 60, 100, 20), "Contrast");
            slider2Value = GUI.HorizontalSlider(new Rect(20, 80, 100, 20), slider2Value, 0f, 100f);
            slider2Value = Mathf.Round(slider2Value * 1000f) / 1000f; // Set step size
            GlobalVariables.Instance.Contrast = slider2Value;

            GUI.Label(new Rect(20, 100, 100, 20), "Hue Shift");
            slider3Value = GUI.HorizontalSlider(new Rect(20, 120, 100, 20), slider3Value, -180f, 180f);
            slider3Value = Mathf.Round(slider3Value * 1000f) / 1000f; // Set step size
            GlobalVariables.Instance.hueShift = slider3Value;

            GUI.Label(new Rect(20, 140, 100, 20), "Saturation");
            slider4Value = GUI.HorizontalSlider(new Rect(20, 160, 100, 20), slider4Value, 0f, 100f);
            slider4Value = Mathf.Round(slider4Value * 1000f) / 1000f; // Set step size
            GlobalVariables.Instance.Saturation = slider4Value;


            string newText1Value = GUI.TextField(new Rect(130, 40, 40, 20), slider1Value.ToString());
            slider1Value = float.Parse(newText1Value);
            string newText2Value = GUI.TextField(new Rect(130, 80, 40, 20), slider2Value.ToString());
            slider2Value = float.Parse(newText2Value);
            string newText3Value = GUI.TextField(new Rect(130, 120, 40, 20), slider3Value.ToString());
            slider3Value = float.Parse(newText3Value);
            string newText4Value = GUI.TextField(new Rect(130, 160, 40, 20), slider4Value.ToString());
            slider4Value = float.Parse(newText4Value);

            // Place textboxes aswell besides the sliders to adjust the same values


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
