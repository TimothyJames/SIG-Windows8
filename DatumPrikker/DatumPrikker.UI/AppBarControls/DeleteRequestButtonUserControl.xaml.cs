using DatumPrikker.UI.Data;
using DatumPrikker.UI.Frames;
using DatumPrikker.UI.Frames.New;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DatumPrikker.UI.AppBarControls
{
    public sealed partial class DeleteRequestButtonUserControl : UserControl
    {
        public DeleteRequestButtonUserControl()
        {
            this.InitializeComponent();
        }

        public delegate void DeleteRequestEventHandler();

        public event DeleteRequestEventHandler DeleteRequestEvent;

        public void RaiseDeleteRequestEvent()
        {
            if (DeleteRequestEvent != null)
            {
                DeleteRequestEvent();
            }
        }

        private void DeleteRequest_Click_1(object sender, RoutedEventArgs e)
        {
            RaiseDeleteRequestEvent();
        }


    }
}
