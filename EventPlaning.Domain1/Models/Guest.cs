namespace EventPlanning.Domain.Models
{
    public class Guest : IBaseModel
    {
        public Guid Id { get; set; }

        public Account Account { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
