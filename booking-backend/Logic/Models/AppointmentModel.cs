using Data.Entities;

namespace Logic.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ServiceId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Notes { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public CustomerModel? Customer { get; set; }

        public ServiceModel? Service { get; set; }

        public AppointmentModelStatus Status { get; set; }
    }

    public enum AppointmentModelStatus
    {
        Scheduled,
        Completed,
        Canceled
    }
}
