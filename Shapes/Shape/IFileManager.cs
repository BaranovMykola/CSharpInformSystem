using System.Collections.Generic;

namespace CSharpInformSystem.Shape
{
    /// <summary>
    /// Interface that is responsible for exchanging information between data file and program 
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Saves list of elements of type T to file
        /// </summary>
        /// <typeparam name="T">Template parameter</typeparam>
        /// <param name="figures">List of template parameters</param>
        /// <param name="fileName">Name of file, where elements are saved</param>
        void SaveList<T>(List<T> figures, string fileName);

        /// <summary>
        /// Loads list of elements of type T from file
        /// </summary>
        /// <typeparam name="T">Template parameter</typeparam>
        /// <param name="fileName">Name of file, from where elements will are loaded</param>
        /// <returns>List of T elements, read from file</returns>
        List<T> LoadList<T>(string fileName);
    }
}