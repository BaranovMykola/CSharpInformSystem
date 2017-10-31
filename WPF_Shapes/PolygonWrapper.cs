using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_Shapes.Annotations;

namespace WPF_Shapes
{
    public class PolygonWrapper : INotifyPropertyChanged
    {
        private int _strokeThinkness;
        private bool _canDrag = false;
        public Polygon Pol { get; set; }

        public Brush Fill { get; set; }

        public string Id { get; set; }

        public Brush Stroke { get; set; }

        public int StrokeThinkness
        {
            get { return _strokeThinkness; }
            set
            {
                _strokeThinkness = value; 
                OnPropertyChanged(nameof(StrokeThinkness));
            }
        }

        public bool CanDrag
        {
            get { return _canDrag; }
            set
            {
                _canDrag = value; 
                OnPropertyChanged(nameof(CanDrag));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SwapStrokeThicknes(int val)
        {
            if (StrokeThinkness == val)
            {
                StrokeThinkness = 0;
            }
            else
            {
                StrokeThinkness = val;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Transform _rtr;

        public Transform RTR
        {
            get
            {
                return _rtr;

            }
            set
            {
                _rtr = value;
                OnPropertyChanged(nameof(RTR));
            }
        }
    }
}