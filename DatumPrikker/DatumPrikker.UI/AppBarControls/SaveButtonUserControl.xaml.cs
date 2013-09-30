using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class SaveButtonUserControl : UserControl
    {
        public SaveButtonUserControl()
        {
            this.InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            switch (rootFrame.CurrentSourcePageType.Name)
            {
                case "Register":
                    RegisterClick(rootFrame);
                    break;
            }
        }

        private async void RegisterClick(Frame frame)
        {
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog("Your account has not really been made. For this function still needs to be developed.");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand("Close", new UICommandInvokedHandler(this.CloseCommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        #region Commands
        /// <summary>
        /// Callback function for the invocation of the dialog commands.
        /// </summary>
        /// <param name="command">The command that was invoked.</param>
        private void CloseCommandInvokedHandler(IUICommand command)
        {
        }
        #endregion
    }
}
