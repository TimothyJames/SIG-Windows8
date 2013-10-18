using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatumPrikker.DataModel
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PassWord { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }

    public class Appointment
    {
        public int AppointmentID { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public int? UserID { get; set; }
        public User OwnerUser { get; set; }
        public ICollection<AppointmentDate> Dates { get; set; }
        public ICollection<AppointmentInvitee> Invitees { get; set; }
    }

    public class AppointmentInvitee
    {
        public int InviteeUserID { get; set; }
        public int InviteeAppointmentID { get; set; }

        public User User { get; set; }
        public Appointment Appointment { get; set; }
    }

    public class AppointmentDate
    {
        public int AppointmentDateID { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int AppointmentID { get; set; }
        public Appointment Appointment { get; set; }
    }

    public class AddressBookEntree
    {
        public int OwnerUserID { get; set; }
        public int EntreeUserID { get; set; }

        public virtual User OwnerUser { get; set; }
        public virtual User EntreeUser { get; set; }
    }

    public class AppointmentDateAttendee
    {
        public int AppointmentDateAttendeeID { get; set; }
        public int AppointmentDateID { get; set; }
        public int UserID { get; set; }
        public string Remarks { get; set; }
        public AttendStatus Status { get; set; }

        public User User { get; set; }
        public AppointmentDate AppointmentDate { get; set; }
    }

    public class DatumPrikkerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDate> AppointmentDates { get; set; }
        public DbSet<AppointmentInvitee> AppointmentsInvitees { get; set; }
        public DbSet<AddressBookEntree> AddressBookEntrees { get; set; }
        public DbSet<AppointmentDateAttendee> AppointmentDateAttendees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //configure model using the fluent API

            //set the key for the Addressbook
            modelBuilder.Entity<AddressBookEntree>()
                .HasKey(ab => new { ab.OwnerUserID, ab.EntreeUserID });
            //set the key for AppointmentInvitee
            modelBuilder.Entity<AppointmentInvitee>()
                .HasKey(ai => new { ai.InviteeAppointmentID, ai.InviteeUserID });
        }
    }

    public enum AttendStatus
    {
        PRESENT, NOT_PRESENT, NOT_PREFERRED
    }
}

////Using the model

// using (var db = new DatumPrikkerContext())
//{
//    Console.Write("Enter a name for a new User: ");
//    var name = Console.ReadLine();

//    var user = new User { UserName = name };
//    db.Users.Add(user);
//    db.SaveChanges();

//    Console.WriteLine("All blogs in the database:");

//    var query = from u in db.Users
//                orderby u.UserName
//                select u;

//    foreach (var item in query)
//    {
//        Console.WriteLine(item.UserName);
//    }

//    Console.WriteLine("Press any key to exit...");
//    Console.ReadKey();
//}