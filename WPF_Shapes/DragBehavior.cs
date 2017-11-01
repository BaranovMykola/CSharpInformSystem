using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_Shapes
{
    public class DragBehavior
    {
        public static readonly DependencyProperty IsDragProperty = DependencyProperty.RegisterAttached("Drag", typeof(bool), typeof(DragBehavior), new PropertyMetadata(false, OnDragChanged));

        private Point elementStartPosition2;

        private Point mouseStartPosition2;

        public static DragBehavior Instance { get; set; } = new DragBehavior();

        public static List<UIElement> UiElements { get; set; } = new List<UIElement>();

        public static List<bool> Bools { get; set; } = new List<bool>();

        public TranslateTransform Transform { get; set; } = new TranslateTransform();

        public static bool GetDrag(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDragProperty);
        }

        public static void SetDrag(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragProperty, value);
        }

        private static void OnDragChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var element = (UIElement)sender;
            var isDrag = (bool)e.NewValue;

            Instance = new DragBehavior();

            if (isDrag)
            {
                if (UiElements.Find(s => s == element) == null)
                {
                    ((UIElement)sender).RenderTransform = Instance.Transform;
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
            mouseStartPosition2 = mouseButtonEventArgs.GetPosition(parent);
            ((UIElement)sender).CaptureMouse();
        }

        private void ElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            ((UIElement)sender).ReleaseMouseCapture();
            elementStartPosition2.X = Transform.X;
            elementStartPosition2.Y = Transform.Y;
        }

        private void ElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var parent = Application.Current.MainWindow;
            var mousePos = mouseEventArgs.GetPosition(parent);
            var diff = mousePos - mouseStartPosition2;
            if (!((UIElement)sender).IsMouseCaptured || !Bools[UiElements.FindIndex(s => s == sender as UIElement)])
            {
                return;
            }

            Transform.X = elementStartPosition2.X + diff.X;
            Transform.Y = elementStartPosition2.Y + diff.Y;
            var bindingExpression = BindingOperations.GetBindingExpression(sender as DependencyObject, UIElement.RenderTransformProperty);
            bindingExpression?.UpdateSource();
        }
    }
}