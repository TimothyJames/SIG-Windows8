using DatumPrikker.UI.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace DatumPrikker.UI.Frames.New
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class AddRequest : DatumPrikker.UI.Common.LayoutAwarePage
    {
        private ObservableCollection<UserSelected> mUsersSelected = null;

        public AddRequest()
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
            BindCheckboxes();
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

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (FieldValidationExtensions.GetIsValid(RequestTitle) && FieldValidationExtensions.GetIsValid(RequestLocation) && FieldValidationExtensions.GetIsValid(RequestDescription))
            {

                var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
                using (var db = new SQLite.SQLiteConnection(dbPath))
                {
                        db.CreateTable<Appointment>();
                        db.CreateTable<AppointmentInvitee>();

                        Appointment appointment = new Appointment();
                        appointment.Title = RequestTitle.Text;
                        appointment.Location = RequestLocation.Text;
                        appointment.Description = RequestDescription.Text;
                        appointment.Date = RequestDate.SelectedDate;
                        appointment.OwnerUserID = App.loggedInUser.Id;

                        db.RunInTransaction(() =>
                        {
                            db.Insert(appointment);

                            foreach (UserSelected item in mUsersSelected)
                            {
                                if (item.IsSelected)
                                {
                                    db.Insert(new AppointmentInvitee() { OwnerUserID = App.loggedInUser.Id, InviteeAppointmentID = appointment.Id, InviteeUserID = item.Id });
                                }
                            }
                         });
   
                }

                this.Frame.Navigate(typeof(Dashboard));
            }

        }

        private void BindCheckboxes()
        {
            var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {

                db.CreateTable<AddressBookEntree>();

                var addressquery = (from x in db.Table<AddressBookEntree>()
                                    where x.OwnerUserID == App.loggedInUser.Id
                                    select x).ToArray();

                var tempquery = addressquery.Select(x => x.EntreeUserID).ToArray();

                var userquery = (from x in db.Table<User>()
                                 where tempquery.Contains(x.Id)
                                 select x).ToList();

                mUsersSelected = new ObservableCollection<UserSelected>();
                foreach (var item in userquery)
                {
                    mUsersSelected.Add(new UserSelected(item.Id,item.UserName));
                }

                Addresses.ItemsSource = mUsersSelected;

            }
        }
    }

    internal class UserSelected : INotifyPropertyChanged
    {
        public UserSelected(int id,string username)
        {
            mId = id;
            mUserName = username;
            IsSelected = false;
        }

        private int mId;
        private string mUserName;
        private bool mIsSelected;

        public int Id
        {
            get { return mId; }
        }

        public string UserName
        {
            get { return mUserName; }
            set
            {
                mUserName = value;
            }
        }

        
        public bool IsSelected
        {
            get { return mIsSelected; }
            set
            {
                mIsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
