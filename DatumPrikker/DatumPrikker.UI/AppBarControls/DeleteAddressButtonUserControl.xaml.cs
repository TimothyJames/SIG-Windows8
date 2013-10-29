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
    public sealed partial class DeleteAddressButtonUserControl : UserControl
    {
        public DeleteAddressButtonUserControl()
        {
            this.InitializeComponent();
        }

        public delegate void DeleteAddressEventHandler();

        public event DeleteAddressEventHandler DeleteAddressEvent;

        public void RaiseDeleteAddressEvent()
        {
            if(DeleteAddressEvent != null)
            {
                DeleteAddressEvent();
            }
        }

        private void DeleteAddress_Click_1(object sender, RoutedEventArgs e)
        {
            RaiseDeleteAddressEvent();
        }


    }
}
