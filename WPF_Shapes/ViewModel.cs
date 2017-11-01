using System;
using System.IO;
using System.Windows.Markup;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace WPF_Shapes
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using WPF_Shapes.Annotations;

    public class ViewModel : INotifyPropertyChanged
    {
        private readonly List<Point> polyognEdges = new List<Point>();
        private Transform _rtr;

        public ViewModel(WindowMediator colorPicker)
        {
            ClickCanvasCommand = new RelayCommand(ClickCanvas);
            DrawPolyCommand = new RelayCommand(DrawPoly);
            InvokeColorDialogoCommand = new RelayCommand(OpenColorDialog);
            ColorDialogInvoker = colorPicker;
            SelectPolygonCommand = new RelayCommand(SelectPolygon);
            SaveCommand = new RelayCommand(Save);
            OpenCommand = new RelayCommand(Open);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public WindowMediator ColorDialogInvoker { get; set; }

        public ColorDialogViewModel ColorDialogViewModel { get; set; } = new ColorDialogViewModel();

        public ObservableCollection<PolygonWrapper> Polygons { get; set; } = new ObservableCollection<PolygonWrapper>();

        public ICommand ClickCanvasCommand { get; set; }

        public ICommand DrawPolyCommand { get; set; }

        public ICommand InvokeColorDialogoCommand { get; set; }

        public ICommand SelectPolygonCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public ICommand OpenCommand { get; set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ClickCanvas(object parameter)
        {
            if (!Polygons.Any(s => s.CanDrag))
            {
                polyognEdges.Add(Mouse.GetPosition(parameter as IInputElement));
            }
        }

        private void DrawPoly(object parameter)
        {
            if (!Polygons.Any(s => s.CanDrag))
            {
                var p = new Polygon() {Points = new PointCollection(polyognEdges)};
                Point center = polyognEdges.Aggregate((a, b) => new Point(a.X + b.X, a.Y + b.Y));
                center.X /= polyognEdges.Count;
                center.Y /= polyognEdges.Count;

                InvokeColorDialogoCommand?.Execute(null);

                if (ColorDialogViewModel.DialogResult)
                {
                    var fill = ColorDialogViewModel.ColorPicker;
                    var color = (Color) fill.GetValue(SolidColorBrush.ColorProperty);
                    var average = (color.R + color.G + color.B)/3;

                    Brush stroke;
                    if (average < 127)
                    {
                        stroke = Brushes.White;
                    }
                    else
                    {
                        stroke = Brushes.Black;
                    }

                    Polygons.Add(new PolygonWrapper()
                    {
                        Pol = p,
                        Fill = ColorDialogViewModel.ColorPicker,
                        Id = $"Polygon {Polygons.Count + 1}",
                        Stroke = stroke
                    });
                    OnPropertyChanged(nameof(Polygons));
                }
            }
            else
            {
                MessageBox.Show("You cannot add new shapes while dragging");
            }

            polyognEdges.Clear();
        }

        private void OpenColorDialog(object parametr)
        {
            ColorDialogViewModel.DialogResult = false;
            ColorDialogInvoker.ShowDialog(this.ColorDialogViewModel);
        }

        private void SelectPolygon(object parameter)
        {
            var polygonWrapper = parameter as PolygonWrapper;
            polygonWrapper?.SwapStrokeThicknes(10);
            polygonWrapper.CanDrag = !polygonWrapper.CanDrag;
            OnPropertyChanged(nameof(Polygons));
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<PolygonWrapper> Lst
        {
            get { return Polygons.ToList(); }
        }

        private void Save(object parameter)
        {
            Console.WriteLine("ok?");
            var dialog = new SaveFileDialog();
            dialog.Filter = "XML (*.xml)|*.xml";
            var confirm = dialog.ShowDialog();
            if (confirm ?? false)
            {
                Console.WriteLine(dialog.FileName);
                var lst = Polygons.ToList();
                File.WriteAllText(dialog.FileName, string.Empty);
                using (
                    var stream = File.Open(dialog.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Delete))
                {
                    var a = new A();
                    foreach (var wrapper in Lst)
                    {
                        a.Add(wrapper);
                    }

                    for (int j = 0; j < a.Count; j++)
                    {
                        var points = a[j].Pol.Points;
                        for (int i = 0; i < points.Count; i++)
                        {
                            points[i] = new Point(points[i].X + a[j].RTR.Value.OffsetX, points[i].Y + a[j].RTR.Value.OffsetY);
                            //points[i] = new Point(0,0);
                        }
                    }

                    XamlWriter.Save(a, stream);

                    for (int j = 0; j < a.Count; j++)
                    {
                        var points = a[j].Pol.Points;
                        for (int i = 0; i < points.Count; i++)
                        {
                            points[i] = new Point(points[i].X - a[j].RTR.Value.OffsetX, points[i].Y - a[j].RTR.Value.OffsetY);
                            //points[i] = new Point(0,0);
                        }
                    }
                }
            }

        }

        private void Open(object parametr)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "XML (*.xml)|*.xml";
            var confirm = dialog.ShowDialog();
            if (confirm ?? false)
            {
                using (
    var stream = File.Open(dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var obj = XamlReader.Load(stream);
                    Polygons = new ObservableCollection<PolygonWrapper>((List<PolygonWrapper>)(obj as A));
                    OnPropertyChanged(nameof(Polygons));
                }
            }
        }
    }

    public class A : List<PolygonWrapper>
    {
        public A(List<PolygonWrapper> p): base(p)
        {
            
        }

        public A()
        {
            
        }
    }

    [Serializable]
    public class PolygonWrapper2
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
                //OnPropertyChanged(nameof(StrokeThinkness));
            }
        }

        public bool CanDrag
        {
            get { return _canDrag; }
            set
            {
                _canDrag = value;
                //OnPropertyChanged(nameof(CanDrag));
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

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

        //[NotifyPropertyChangedInvocator]
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

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
                //OnPropertyChanged(nameof(RTR));
            }
        }
    }
}
