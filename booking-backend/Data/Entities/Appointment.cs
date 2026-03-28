namespace Data.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ServiceId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Notes { get; set; } = string.Empty;   

        public string Title { get; set; } = string.Empty;

        public Customer? Customer { get; set; } 

        public Service? Service { get; set; }

        public AppointmentStatus Status { get; set; }
    }


    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Canceled
    }
}
