using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_Shapes
{
    public class DragBehavior
    {
        public TranslateTransform Transform = new TranslateTransform();

        private Point _elementStartPosition2;

        private Point _mouseStartPosition2;

        public static DragBehavior Instance { get; set; } = new DragBehavior();

        public static bool GetDrag(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDragProperty);
        }

        public static void SetDrag(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragProperty, value);
        }

        public static readonly DependencyProperty IsDragProperty =
            DependencyProperty.RegisterAttached("Drag",
                typeof(bool), typeof(DragBehavior),
                new PropertyMetadata(false, OnDragChanged));

        public static List<UIElement> UiElements { get; set; } = new List<UIElement>();

        public static List<bool> Bools { get; set; } = new List<bool>();

        private static void OnDragChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // ignoring error checking
            var element = (UIElement)sender;
            var isDrag = (bool)(e.NewValue);

            Instance = new DragBehavior();

            if (isDrag)
            {
                //((UIElement)sender).RenderTransform = Instance.Transform;
                if (UiElements.Find(s => s == element) == null)
                {
                    ((UIElement) sender).RenderTransform = Instance.Transform;
                    UiElements.Add(element);
                    Bools.Add(true);
                }
                else
                {
                    Bools[UiElements.FindIndex(s => s == element)] = true;
                }

                element.MouseLeftButtonDown += Instance.ElementOnMouseLeftButtonDown;
                element.MouseLeftButtonUp += Instance.ElementOnMouseLeftButtonUp;
                element.MouseMove += Instance.ElementOnMouseMove;
            }
            else
            {
                Bools[UiElements.FindIndex(s => s == element)] = false;
                element.MouseLeftButtonDown -= Instance.ElementOnMouseLeftButtonDown;
                element.MouseLeftButtonUp -= Instance.ElementOnMouseLeftButtonUp;
                element.MouseMove -= Instance.ElementOnMouseMove;
            }
        }

        private void ElementOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var parent = Application.Current.MainWindow;
            _mouseStartPosition2 = mouseButtonEventArgs.GetPosition(parent);
            ((UIElement)sender).CaptureMouse();
        }

        private void ElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            ((UIElement)sender).ReleaseMouseCapture();
            _elementStartPosition2.X = Transform.X;
            _elementStartPosition2.Y = Transform.Y;
        }

        private void ElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var parent = Application.Current.MainWindow;
            var mousePos = mouseEventArgs.GetPosition(parent);
            var diff = (mousePos - _mouseStartPosition2);
            if (!((UIElement)sender).IsMouseCaptured || !(Bools[UiElements.FindIndex(s => s == (sender as UIElement))])) return;
            Transform.X = _elementStartPosition2.X + diff.X;
            Transform.Y = _elementStartPosition2.Y + diff.Y;
        }
    }
}