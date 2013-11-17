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
using WinRTXamlToolkit.Controls.Extensions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DatumPrikker.UI.Frames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : DatumPrikker.UI.Common.LayoutAwarePage
    {
        public Register()
        {
            this.InitializeComponent();
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
            if (FieldValidationExtensions.GetIsValid(RegisterUserName) && FieldValidationExtensions.GetIsValid(RegisterPassword) && FieldValidationExtensions.GetIsValid(RegisterEmail))
            {
                var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
                using (var db = new SQLite.SQLiteConnection(dbPath))
                {
                    db.CreateTable<User>();

                    db.RunInTransaction(() =>
                        {
                            db.Insert(new User() { UserName = RegisterUserName.Text, PassWord = RegisterPassword.Password, EmailAddress = RegisterEmail.Text });
                        });
                }

                this.Frame.Navigate(typeof(Dashboard));
            }
        }
    }
}
