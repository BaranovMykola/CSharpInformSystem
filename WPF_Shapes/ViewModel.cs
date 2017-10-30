using System;

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

        public ViewModel(WindowMediator colorPicker)
        {
            ClickCanvasCommand = new RelayCommand(ClickCanvas);
            DrawPolyCommand = new RelayCommand(DrawPoly);
            InvokeColorDialogoCommand = new RelayCommand(s => ColorDialogInvoker.ShowDialog(this.ColorDialog));

            ColorDialogInvoker = colorPicker;
            ColorDialogInvoker.OnClose += ColorDialog_OnClose;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public WindowMediator ColorDialogInvoker { get; set; }

        public ColorDialogViewModel ColorDialog { get; set; } = new ColorDialogViewModel();

        public ObservableCollection<PolygonWrapper> Polygons { get; set; } = new ObservableCollection<PolygonWrapper>();

        public ICommand ClickCanvasCommand { get; set; }

        public ICommand DrawPolyCommand { get; set; }

        public ICommand InvokeColorDialogoCommand { get; set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ClickCanvas(object parameter)
        {
            polyognEdges.Add(Mouse.GetPosition(parameter as IInputElement));
        }

        private void DrawPoly(object parameter)
        {
            var p = new Polygon() { Points = new PointCollection(polyognEdges) };
            Point center = polyognEdges.Aggregate((a, b) => new Point(a.X + b.X, a.Y + b.Y));
            center.X /= polyognEdges.Count;
            center.Y /= polyognEdges.Count;
            ColorDialogInvoker.ShowDialog(this.ColorDialog);
            Polygons.Add(new PolygonWrapper() { Pol = p, Fill = ColorDialog.ColorPicker });
            OnPropertyChanged(nameof(Polygons));
            
            polyognEdges.Clear();
        }

        private void ColorDialog_OnClose(object sender, EventArgs e)
        {
            Console.WriteLine("dialog close");
        }
    }
}
