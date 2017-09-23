namespace CSharpInformSystem.Shape
{
    /// <summary>
    /// Interface that is responsible for doing some shapes' calculation
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// Calculates square of the element
        /// </summary>
        /// <returns>Type of return: float </returns>
        float ComputeSquare();

        /// <summary>
        /// Calculates perimeter of the element
        /// </summary>
        /// <returns>Type of return: float</returns>
        float ComputePerimeter();
    }
}