using DatumPrikker.UI.Frames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DatumPrikker.UI.SettingsFlyout
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StandardFlyout : DatumPrikker.UI.Common.LayoutAwarePage
    {
        // The guidelines recommend using 100px offset for the content animation.
        const int ContentAnimationOffset = 100;

        public StandardFlyout()
        {
            this.InitializeComponent();

            SetValues();

            FlyoutContent.Transitions = new TransitionCollection();
            FlyoutContent.Transitions.Add(new EntranceThemeTransition()
            {
                FromHorizontalOffset = (SettingsPane.Edge == SettingsEdgeLocation.Right) ? ContentAnimationOffset : (ContentAnimationOffset * -1)
            });
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// This loads the saved values of the controls
        /// </summary>
        void SetValues()
        {
            if (ApplicationData.Current.LocalSettings.Values["ciberbackground"] != null)
                CiberBackground.IsOn = (bool)ApplicationData.Current.LocalSettings.Values["ciberbackground"];
        }


        /// <summary>
        /// This is the click handler for the back button on the Flyout.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MySettingsBackClicked(object sender, RoutedEventArgs e)
        {
            // First close our Flyout.
            Popup parent = this.Parent as Popup;
            if (parent != null)
            {
                parent.IsOpen = false;
            }

            // If the app is not snapped, then the back button shows the Settings pane again.
            if (Windows.UI.ViewManagement.ApplicationView.Value != Windows.UI.ViewManagement.ApplicationViewState.Snapped)
            {
                SettingsPane.Show();
            }
        }

        /// <summary>
        /// This is the click handler for the back button on the Flyout.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchBackground(object sender, RoutedEventArgs re)
        {
            ToggleSwitch backgroundSwitch = (ToggleSwitch)sender;
            ApplicationData.Current.LocalSettings.Values["ciberbackground"] = backgroundSwitch.IsOn;

            Frame rootFrame = Window.Current.Content as Frame;
            if (backgroundSwitch.IsOn)
            {
                rootFrame.Background = new SolidColorBrush(Windows.UI.Colors.Violet);
            }
            else
            {
                rootFrame.Background = null;
            }
        }
    }
}
