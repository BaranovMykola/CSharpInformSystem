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
            InvokeColorDialogoCommand = new RelayCommand(OpenColorDialog);
            ColorDialogInvoker = colorPicker;
            SelectPolygonCommand = new RelayCommand(SelectPolygon);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public WindowMediator ColorDialogInvoker { get; set; }

        public ColorDialogViewModel ColorDialogViewModel { get; set; } = new ColorDialogViewModel();

        public ObservableCollection<PolygonWrapper> Polygons { get; set; } = new ObservableCollection<PolygonWrapper>();

        public ICommand ClickCanvasCommand { get; set; }

        public ICommand DrawPolyCommand { get; set; }

        public ICommand InvokeColorDialogoCommand { get; set; }

        public ICommand SelectPolygonCommand { get; set; }

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
            
            InvokeColorDialogoCommand?.Execute(null);

            if (ColorDialogViewModel.DialogResult)
            {
                var fill = ColorDialogViewModel.ColorPicker;
                var color = (Color)fill.GetValue(SolidColorBrush.ColorProperty);
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
        }
    }
}
