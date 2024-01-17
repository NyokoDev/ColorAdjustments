using System;
using System.IO;
using System.Xml.Serialization;

namespace ColorAdjustmentsMod.Settings
{
    [Serializable]
    public class GlobalVariables
    {
        /// <summary>
        /// PostExposure
        /// </summary>
        [XmlElement]
        public float PostExposure { get; set; }

        [XmlElement]
        public float Contrast { get; set; }

        [XmlElement]
        public float hueShift { get; set; }

        [XmlElement]
        public float Saturation { get; set; }


        public static void SaveToFile(string filePath)
        {
            try
            {
                // Create an XmlSerializer for the GlobalVariables type.
                XmlSerializer serializer = new XmlSerializer(typeof(GlobalVariables));

                // Create or open the file for writing.
                using (TextWriter writer = new StreamWriter(filePath))
                {
                    // Serialize the current static object to the file.
                    serializer.Serialize(writer, Instance);
                }

               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving GlobalVariables to file: {ex.Message}");
            }
        }

        public static GlobalVariables LoadFromFile(string filePath)
        {
            try
            {
                // Create an XmlSerializer for the GlobalVariables type.
                XmlSerializer serializer = new XmlSerializer(typeof(GlobalVariables));

                // Open the file for reading.
                using (TextReader reader = new StreamReader(filePath))
                {
                    // Deserialize the object from the file.
                    GlobalVariables loadedVariables = (GlobalVariables)serializer.Deserialize(reader);

                    // Set the loaded values to the corresponding properties.
                    GlobalVariables.Instance.PostExposure = loadedVariables.PostExposure;
                    GlobalVariables.Instance.Contrast = loadedVariables.Contrast;
                    GlobalVariables.Instance.hueShift = loadedVariables.hueShift;
                    GlobalVariables.Instance.Saturation = loadedVariables.Saturation;

                    return loadedVariables;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading GlobalVariables from file: {ex.Message}");
                return null;
            }
        }


        // Singleton pattern to ensure only one instance of GlobalVariables exists.
        private static GlobalVariables instance;
        public static GlobalVariables Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalVariables();
                }
                return instance;
            }
        }
    }
}
