using System.Collections.Generic;
using CSharpInformSystem.Shape;

namespace CSharpInformSystem
{
    public interface IFileManager
    {
        void SaveList<T>(List<T> figures, string fileName);

        List<T> LoadList<T>(string fileName);
    }
}