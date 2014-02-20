using DatumPrikker.UI.Common;
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
            DeleteRequest.DeleteRequestEvent += DeleteRequest_DeleteRequestEvent;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            BindLists();
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
                    BindLists();
            }
        }

        void DeleteRequest_DeleteRequestEvent()
        {
            var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                db.RunInTransaction(() =>
                {
                    foreach (Appointment appointment in RequestItems.SelectedItems)
                    {
                        db.Delete(appointment);
                    }
                });

                BindLists();

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

        private void BindLists()
        {
            RequestItems.ItemsSource = GetData.BindRequests();
            AdressBookItems.ItemsSource = GetData.BindAddressBook();
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

        private void RequestItems_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (RequestItems.SelectedItems.Count > 0)
            {
                DeleteRequest.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                DeleteRequest.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

        }
    }
}
