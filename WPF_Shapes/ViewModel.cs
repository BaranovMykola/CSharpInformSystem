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

        public ViewModel(WindowMediator colorPicker)
        {
            ClickCanvasCommand = new RelayCommand(ClickCanvas);
            DrawPolyCommand = new RelayCommand(DrawPoly);
            InvokeColorDialogoCommand = new RelayCommand(OpenColorDialog);
            ColorDialogInvoker = colorPicker;
            SelectPolygonCommand = new RelayCommand(SelectPolygon);
            SaveCommand = new RelayCommand(Save);
            OpenCommand = new RelayCommand(Open);
            NewCommand = new RelayCommand(New);
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

        public ICommand NewCommand { get; set; }

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
            if (polyognEdges.Count > 1)
            {
                if (!Polygons.Any(s => s.CanDrag))
                {
                    var p = new Polygon() {Points = new PointCollection(polyognEdges)};

                    InvokeColorDialogoCommand?.Execute(null);

                    if (ColorDialogViewModel.DialogResult)
                    {
                        int average = ComputeAverageColor();
                        Brush stroke = average < 127 ? Brushes.OrangeRed : Brushes.Black;
                        Polygons.Add(new PolygonWrapper
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
            }
            polyognEdges.Clear();
        }

        private int ComputeAverageColor()
        {
            var fill = ColorDialogViewModel.ColorPicker;
            var color = (Color)fill.GetValue(SolidColorBrush.ColorProperty);
            var average = (color.R + color.G + color.B) / 3;
            return average;
        }

        private void OpenColorDialog(object parametr)
        {
            ColorDialogViewModel.DialogResult = false;
            ColorDialogInvoker.ShowDialog(this.ColorDialogViewModel);
        }

        private void SelectPolygon(object parameter)
        {
            var polygonWrapper = parameter as PolygonWrapper;
            polygonWrapper?.SwapStrokeThicknes(3);
            //polygonWrapper.CanDrag = !polygonWrapper.CanDrag;
            OnPropertyChanged(nameof(Polygons));
        }

        private void Save(object parameter)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "XML (*.xml)|*.xml";
            var confirm = dialog.ShowDialog();
            if (confirm ?? false)
            {
                File.WriteAllText(dialog.FileName, string.Empty);

                using (var stream = File.Open(dialog.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Delete))
                {
                    var a = new PolygonWrapperCollection(Polygons.ToList());

                    foreach (var wrapper in a)
                    {
                        var points = wrapper.Pol.Points;
                        for (int i = 0; i < points.Count; i++)
                        {
                            points[i] = new Point(points[i].X + wrapper.RTR.Value.OffsetX, points[i].Y + wrapper.RTR.Value.OffsetY);
                        }
                    }

                    XamlWriter.Save(a, stream);

                    foreach (var wrapper in a)
                    {
                        var points = wrapper.Pol.Points;
                        for (int i = 0; i < points.Count; i++)
                        {
                            points[i] = new Point(points[i].X - wrapper.RTR.Value.OffsetX, points[i].Y - wrapper.RTR.Value.OffsetY);
                        }
                    }
                }
            }
        }

        private void Open(object parametr)
        {
            var dialog = new OpenFileDialog {Filter = "XML (*.xml)|*.xml"};
            var confirm = dialog.ShowDialog();
            if (confirm ?? false)
            {
                using (var stream = File.Open(dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var obj = XamlReader.Load(stream);
                    Polygons = new ObservableCollection<PolygonWrapper>(obj as PolygonWrapperCollection);
                    OnPropertyChanged(nameof(Polygons));
                }
            }
        }

        private void New(object parameter)
        {
            Polygons.Clear();
        }
    }
}
