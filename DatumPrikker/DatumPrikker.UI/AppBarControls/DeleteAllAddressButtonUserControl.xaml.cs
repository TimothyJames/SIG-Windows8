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
    public sealed partial class DeleteAllAddressButtonUserControl : UserControl
    {
        public DeleteAllAddressButtonUserControl()
        {
            this.InitializeComponent();
        }

        private void btnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                db.DeleteAll<User>();
                db.DeleteAll<AddressBookEntree>();
            }


            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Dashboard));
        }
    }
}
