using DatumPrikker.UI.Data;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DatumPrikker.UI.Frames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dashboard : DatumPrikker.UI.Common.LayoutAwarePage
    {
        public Dashboard()
        {
            this.InitializeComponent();
            DeleteAddress.DeleteAddressEvent += DeleteAddress_DeleteAddressEvent;
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            BindAddressBook();
       }

        private void DeleteAddress_DeleteAddressEvent()
        {
            var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                    db.RunInTransaction(() =>
                    {
                        foreach (User user in AdressBookItems.SelectedItems)
                        {
                            db.Delete(user);
                        }
                    });
                    BindAddressBook();        
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void btnRequests_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Requests));
        }

        private void btnAddressBook_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddressBook));
        }

        private void BindAddressBook()
        {
            var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                db.CreateTable<AddressBookEntree>();

                var addressquery = (from x in db.Table<AddressBookEntree>()
                                   where  x.OwnerUserID == App.loggedInUser.Id select x).ToArray();

                var tempquery = addressquery.Select(x=>x.EntreeUserID).ToArray();

                var userquery = (from x in db.Table<User>()
                                 where tempquery.Contains(x.Id) select x).ToArray();


                AdressBookItems.ItemsSource = userquery;

            }
        }

        private void AdressBookItems_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (AdressBookItems.SelectedItems.Count > 0)
            {
                DeleteAddress.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                DeleteAddress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
    }
}
