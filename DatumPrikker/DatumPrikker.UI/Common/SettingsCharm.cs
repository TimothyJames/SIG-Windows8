using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;

namespace DatumPrikker.UI.Common
{
    public class SettingsCharm
    {
        // This is the container that will hold our custom content.
        private Popup settingsPopup;
        // This is the container that will hold our custom content.
        private Popup helpPopup;
        // Desired width for the settings UI. UI guidelines specify this should be 346 or 646 depending on your needs.
        private double settingsWidth = 646;
        // Used to determine the correct height to ensure our custom UI fills the screen.
        private Rect windowBounds;
        //for placing page specific settings
        private string pageName;

        public SettingsCharm(Rect windowbounds, string pagename)
        {
            this.windowBounds = windowbounds;
            this.pageName = pagename;
            SettingsPane.GetForCurrentView().CommandsRequested += onCommandsRequested;
        }


        #region Commands

        void onGeneralSettingsCommand(IUICommand command)
        {
            settingsPopup = setupSettings(settingsPopup);
            SettingsFlyout.StandardFlyout generalpane = new SettingsFlyout.StandardFlyout();
            generalpane.Width = settingsWidth;
            generalpane.Height = windowBounds.Height;
            // Place the SettingsFlyout inside our Popup window.
            settingsPopup.Child = generalpane;
            
            // Let's define the location of our Popup.
            settingsPopup.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (windowBounds.Width - settingsWidth) : 0);
            settingsPopup.SetValue(Canvas.TopProperty, 0);
            settingsPopup.IsOpen = true;
        }
        void onHelpSettingsCommand(IUICommand command)
        {
            helpPopup = setupSettings(helpPopup);
            SettingsFlyout.HelpFlyout helppane = new SettingsFlyout.HelpFlyout();
            helppane.Width = settingsWidth;
            helppane.Height = windowBounds.Height;
            // Place the SettingsFlyout inside our Popup window.
            helpPopup.Child = helppane;
            
            // Let's define the location of our Popup.
            helpPopup.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (windowBounds.Width - settingsWidth) : 0);
            helpPopup.SetValue(Canvas.TopProperty, 0);
            helpPopup.IsOpen = true;
        }

        Popup setupSettings(Popup popup)
        {
            // Create a Popup window which will contain our flyout.
            popup = new Popup();
            popup.Closed += OnPopupClosed;
            Window.Current.Activated += OnWindowActivated;
            popup.IsLightDismissEnabled = true;
            popup.Width = settingsWidth;
            popup.Height = windowBounds.Height;

            // Add the proper animation for the panel.
            popup.ChildTransitions = new TransitionCollection();
            popup.ChildTransitions.Add(new PaneThemeTransition()
            {
                Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right) ?
                       EdgeTransitionLocation.Right :
                       EdgeTransitionLocation.Left
            });

            return popup;
        }

        void onCommandsRequested(SettingsPane settingsPane, SettingsPaneCommandsRequestedEventArgs eventArgs)
        {
            UICommandInvokedHandler generalhandler = new UICommandInvokedHandler(onGeneralSettingsCommand);

            SettingsCommand generalCommand = new SettingsCommand("standardSettings", "Standards", generalhandler);
            if (eventArgs.Request.ApplicationCommands.Where(c => c.Id == generalCommand.Id).FirstOrDefault() != null)
            {
                SettingsPane.GetForCurrentView().CommandsRequested -= onCommandsRequested;
                eventArgs.Request.ApplicationCommands.Remove(eventArgs.Request.ApplicationCommands.Where(c => c.Id == generalCommand.Id).FirstOrDefault());
            }
            eventArgs.Request.ApplicationCommands.Add(generalCommand);

            UICommandInvokedHandler helphandler = new UICommandInvokedHandler(onHelpSettingsCommand);
            SettingsCommand helpCommand = new SettingsCommand("helpSettings", "Help", helphandler);
            if (eventArgs.Request.ApplicationCommands.Where(c => c.Id == helpCommand.Id).FirstOrDefault() != null)
            {
                SettingsPane.GetForCurrentView().CommandsRequested -= onCommandsRequested;
                eventArgs.Request.ApplicationCommands.Remove(eventArgs.Request.ApplicationCommands.Where(c => c.Id == helpCommand.Id).FirstOrDefault());
            }
            eventArgs.Request.ApplicationCommands.Add(helpCommand);
        }

        #endregion

        #region Popup

        /// <summary>
        /// We use the window's activated event to force closing the Popup since a user maybe interacted with
        /// something that didn't normally trigger an obvious dismiss.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="e">Event data describing the conditions that led to the event.</param>
        private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                if (settingsPopup != null)
                    settingsPopup.IsOpen = false;
                if (helpPopup != null)
                    helpPopup.IsOpen = false;
            }
        }

        /// <summary>
        /// When the Popup closes we no longer need to monitor activation changes.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="e">Event data describing the conditions that led to the event.</param>
        void OnPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
        }

        #endregion
    }
}
