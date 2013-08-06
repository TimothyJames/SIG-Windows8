using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DatumPrikker.UI.Common
{
    internal class Navigation
    {
        public static void GoTo(Type window)
        {
            var frame = new Frame();
            frame.Navigate(window);
            Window.Current.Content = frame;
        }
    }
}
