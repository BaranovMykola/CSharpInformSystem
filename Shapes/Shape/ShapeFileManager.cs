using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CSharpInformSystem.Shape
{
    /// <summary>
    /// Class provides interface for loading\saving list of entities
    /// </summary>
    public class ShapeFileManager : IFileManager
    {
        /// <summary>
        /// Implementation of IFileManager method, saves shapes to file
        /// </summary>
        /// <typeparam name="T">Template parameter</typeparam>
        /// <param name="figures">List of template elements</param>
        /// <param name="fileName">File, where figures will be saved</param>
        public void SaveList<T>(List<T> figures, string fileName)
        {
            File.WriteAllText(fileName, string.Empty);
            using (var str = File.Open(fileName, FileMode.Open, FileAccess.Write))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<AbstractShape>));
                xml.Serialize(str, figures);
            }
        }

        /// <summary>
        /// Implementation of IFileManager method, reads shapes from file
        /// </summary>
        /// <typeparam name="T">Template parameter</typeparam>
        /// <param name="fileName">File, from where figures will be read</param>
        /// <returns>List of T elements, read from file</returns>
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