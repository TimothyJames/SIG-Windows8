using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatumPrikker.UI.Data
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PassWord { get; set; }

//        public List<Appointment> Appointments { get; set; }
    }

    public class Appointment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int OwnerUserID { get; set; }
        //public List<AppointmentDate> Dates { get; set; }
        //public List<AppointmentInvitee> Invitees { get; set; }
    }

    public class AppointmentInvitee
    {
        public int OwnerUserID { get; set; }
        public int InviteeUserID { get; set; }
        public int InviteeAppointmentID { get; set; }

        //public User User { get; set; }
        //public Appointment Appointment { get; set; }
    }

    public class AppointmentDate
    {
        public int AppointmentDateID { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int AppointmentID { get; set; }
        //public Appointment Appointment { get; set; }
    }

    public class AddressBookEntree
    {
        public int OwnerUserID { get; set; }
        public int EntreeUserID { get; set; }

        //public virtual User OwnerUser { get; set; }
        //public virtual User EntreeUser { get; set; }
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

    public enum AttendStatus
    {
        PRESENT, NOT_PRESENT, NOT_PREFERRED
    }
}
