using System.Collections.Generic;
using CSharpInformSystem.Shape;

namespace CSharpInformSystem
{
    interface IFileManager
    {
        void SaveList<T>(List<T> figures, string fileName);
    }
}