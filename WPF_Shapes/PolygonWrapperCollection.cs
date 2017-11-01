namespace WPF_Shapes
{
    using System.Collections.Generic;

    public class PolygonWrapperCollection : List<PolygonWrapper>
    {
        public PolygonWrapperCollection()
        {
        }

        public PolygonWrapperCollection(List<PolygonWrapper> p) : base(p)
        {
        }
    }
}