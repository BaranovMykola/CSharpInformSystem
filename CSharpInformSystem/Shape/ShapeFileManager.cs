using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CSharpInformSystem.Shape
{
    class ShapeFileManager : IFileManager
    {
        public void SaveList<T>(List<T> figures, string fileName)
        {
            File.WriteAllText(fileName, String.Empty);
            using (var str = File.Open(fileName, FileMode.Open, FileAccess.Write))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<AbstractShape>));
                xml.Serialize(str, figures);
            }
        }
    }
}