using DatumPrikker.UI.Common;
using DatumPrikker.UI.Data;
using DatumPrikker.UI.Frames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DatumPrikker.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : DatumPrikker.UI.Common.LayoutAwarePage
    {
        // Used to determine the correct height to ensure our custom UI fills the screen.
        private Rect windowBounds;

        public MainPage()
        {
            this.InitializeComponent();
            windowBounds = Window.Current.Bounds;
            
            // Added to listen for events when the window size is updated.
            Window.Current.SizeChanged += OnWindowSizeChanged;

            SettingsCharm charmSetting = new SettingsCharm(windowBounds, "main");

        }

        /// <summary>
        /// Invoked when the window size is updated.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="e">Event data describing the conditions that led to the event.</param>
        void OnWindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            windowBounds = Window.Current.Bounds;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Register));
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            tbError.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            bool success = false;
            var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                db.CreateTable<User>();

                var query = db.Table<User>().Where(x => x.UserName == LoginUserName.Text && x.PassWord == LoginPassWord.Password).FirstOrDefault();

                success = !(query == null);


                if (success)
                {
                    App.loggedInUser = query;
                    this.Frame.Navigate(typeof(Dashboard));
                }
                else
                {
                    tbError.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
            }
        }
    }
}
