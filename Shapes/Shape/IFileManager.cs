using System.Collections.Generic;

namespace CSharpInformSystem.Shape
{
    public interface IFileManager
    {
        void SaveList<T>(List<T> figures, string fileName);

        List<T> LoadList<T>(string fileName);
    }
}