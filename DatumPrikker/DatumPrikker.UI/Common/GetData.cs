using DatumPrikker.UI.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatumPrikker.UI.Common
{
    public static class GetData
    {
        public static Array BindAddressBook()
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
                                 select x).ToArray();


                return userquery;
            }
        }

        public static Array BindRequests()
        {
            var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                db.CreateTable<Appointment>();

                var appoinmentquery = (from x in db.Table<Appointment>()
                                       where x.OwnerUserID == App.loggedInUser.Id
                                       orderby x.Date
                                       select x).ToArray();

                return appoinmentquery;
            }
        }
    }
}
