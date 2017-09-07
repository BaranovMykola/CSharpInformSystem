using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace CSharpInformSystem.Shape
{
    /// <summary>
    /// Class provides interface for loading\saving list of entities
    /// </summary>
    public class ShapeFileManager : IFileManager
    {
        public void SaveList<T>(List<T> figures, string fileName)
        {
            File.WriteAllText(fileName, string.Empty);
            using (var str = File.Open(fileName, FileMode.Open, FileAccess.Write))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<AbstractShape>));
                xml.Serialize(str, figures);
            }
        }

        public List<T> LoadList<T>(string fileName)
        {
            List<T> shapes;
            using (var str = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<AbstractShape>));
                shapes = xml.Deserialize(str) as List<T>;
            }

            return shapes;
        }
    }
}