using System;
using System.IO;
using System.Xml.Serialization;

namespace ColorAdjustmentsMod.XML
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

        [XmlElement]
        public float Longitude { get; set; }

        [XmlElement]
        public float Latitude { get; set; }

#if DEBUG
        [XmlElement]
        public float Temperature { get; set; }

        [XmlElement]
        public float Tint { get; set; }

#endif

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

#if DEBUG
                    GlobalVariables.Instance.Temperature = loadedVariables.Temperature;
                    GlobalVariables.Instance.Tint = loadedVariables.Tint;
#endif



                    return loadedVariables;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load ColorAdjustments settings. Ensure that at least one setting is set.");
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